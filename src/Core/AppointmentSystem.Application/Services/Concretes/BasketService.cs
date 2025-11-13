namespace AppointmentSystem.Application.Services.Concretes;

public class BasketService(IAppDbContext context, IMapper mapper) : IBasketService
{
    public async Task<BasketDto> GetBasketByPatientIdAsync(string patientId)
    {
        var basket = await context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.PatientId == patientId);

        if (basket == null) return null!;

        return mapper.Map<BasketDto>(basket);
    }

    public async Task<BasketDto> AddItemToBasketAsync(string patientId, string doctorId, string availabilityId, string medicalServiceId)
    {
        var basket = await context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.PatientId == patientId);

        if (basket == null)
        {
            basket = new Basket(patientId);
            context.Baskets.Add(basket);
        }

        var availability = await context.Availabilities
            .FirstOrDefaultAsync(a => a.Id == availabilityId);

        if (availability == null)
            throw new InvalidOperationException("Availability not found.");

        if (availability.DoctorId != doctorId)
            throw new InvalidOperationException("This availability does not belong to the specified doctor.");

        if (availability.IsBooked)
            throw new InvalidOperationException("This availability is already booked.");

        if (basket.Items.Any(i => i.AvailabilityId == availabilityId))
            throw new InvalidOperationException("This availability is already in the basket.");

        var medicalService = await context.MedicalServices
            .FirstOrDefaultAsync(ms => ms.Id == medicalServiceId);

        if (medicalService == null)
            throw new InvalidOperationException("Medical service not found.");

        if (medicalService.DoctorId != doctorId)
            throw new InvalidOperationException("This medical service does not belong to the specified doctor.");

            basket.AddItem(doctorId, availabilityId, medicalServiceId, medicalService.Price);

        await context.SaveChangesAsync();

        return mapper.Map<BasketDto>(basket);
    }

    public async Task<BasketDto> RemoveItemFromBasketAsync(string patientId, string itemId)
    {
        var basket = await context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.PatientId == patientId);

        if (basket == null)
            throw new InvalidOperationException("Basket not found.");

        var item = basket.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            throw new InvalidOperationException("Item not found in basket.");

        basket.RemoveItem(itemId);

        await context.SaveChangesAsync();

        return mapper.Map<BasketDto>(basket);
    }


    public async Task<decimal> CheckoutAsync(string patientId, string? promoCode = null)
    {
        var basket = await context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.PatientId == patientId);

        if (basket == null || !basket.Items.Any())
            throw new InvalidOperationException("Basket is empty.");

        decimal totalPrice = basket.Items.Sum(i => i.Price);

        if (!string.IsNullOrEmpty(promoCode))
        {
            var promo = await context.PromoCodes
                .FirstOrDefaultAsync(p => p.Code == promoCode && p.IsValid());

            if (promo != null)
            {
                if (promo.DiscountPercent > 0)
                    totalPrice -= totalPrice * (promo.DiscountPercent / 100);

                if (promo.DiscountAmount > 0)
                    totalPrice -= promo.DiscountAmount;

                if (totalPrice < 0) totalPrice = 0;
            }
        }

        foreach (var item in basket.Items)
        {
            var availability = await context.Availabilities
                .FirstOrDefaultAsync(a => a.Id == item.AvailabilityId);

            if (availability == null)
                throw new InvalidOperationException($"Availability {item.AvailabilityId} not found.");

            if (availability.DoctorId != item.DoctorId)
                throw new InvalidOperationException($"Availability {item.AvailabilityId} does not belong to doctor {item.DoctorId}.");

            if (availability.IsBooked)
                throw new InvalidOperationException($"Availability {item.AvailabilityId} is already booked.");

            
            var appointment = new Appointment(item.DoctorId, patientId, item.AvailabilityId, item.MedicalServiceId);
            context.Appointments.Add(appointment);

            availability.Book();
        }

        basket.Items.Clear();

        await context.SaveChangesAsync();

        return totalPrice;
    }
}
