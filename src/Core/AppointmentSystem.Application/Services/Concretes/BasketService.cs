

namespace AppointmentSystem.Application.Services.Concretes;

public class BasketService : IBasketService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public BasketService(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BasketDto> GetBasketByPatientIdAsync(string patientId)
    {
        var basket = await _context.Baskets
     .Include(b => b.Items)
         .ThenInclude(i => i.Doctor)
     .Include(b => b.Items)
         .ThenInclude(i => i.MedicalService)
     .Include(b => b.Items)
         .ThenInclude(i => i.Availability) // <- mütləq olmalıdır
     .FirstOrDefaultAsync(b => b.PatientId == patientId);



        if (basket == null)
            return null!;

        return _mapper.Map<BasketDto>(basket);
    }

    public async Task<BasketDto> AddItemToBasketAsync(string patientId, string doctorId, string availabilityId, string medicalServiceId)
    {
        var basket = await _context.Baskets
            .Include(b => b.Items)
                .ThenInclude(i => i.MedicalService)
            .Include(b => b.Items)
                .ThenInclude(i => i.Availability)
            .FirstOrDefaultAsync(b => b.PatientId == patientId);

        if (basket == null)
        {
            basket = new Basket(patientId);
            _context.Baskets.Add(basket);
        }

        var availability = await _context.Availabilities.FirstOrDefaultAsync(a => a.Id == availabilityId);
        if (availability == null)
            throw new InvalidOperationException("Availability not found.");
        if (availability.DoctorId != doctorId)
            throw new InvalidOperationException("Availability does not belong to the specified doctor.");
        if (availability.IsBooked)
            throw new InvalidOperationException("Availability is already booked.");
        if (basket.Items.Any(i => i.AvailabilityId == availabilityId))
            throw new InvalidOperationException("This availability is already in the basket.");

        var medicalService = await _context.MedicalServices.FirstOrDefaultAsync(ms => ms.Id == medicalServiceId);
        if (medicalService == null)
            throw new InvalidOperationException("Medical service not found.");
        if (medicalService.DoctorId != doctorId)
            throw new InvalidOperationException("Medical service does not belong to the specified doctor.");

        basket.AddItem(doctorId, availabilityId, medicalServiceId, medicalService.Price);

        await _context.SaveChangesAsync();

        // Burada mapper yerinə projection istifadə edirik
        var basketDto = await _context.Baskets
            .Where(b => b.PatientId == patientId)
            .Select(b => new BasketDto(
                b.Id,
                b.PatientId,
                b.Items.Select(i => new BasketItemDto(
                    i.Id,
                    i.DoctorId,
                    _context.Doctors.Where(d => d.Id == i.DoctorId)
                                    .Select(d => d.FirstName + " " + d.LastName)
                                    .FirstOrDefault() ?? string.Empty,
                    i.AvailabilityId,
                    i.MedicalServiceId,
                    i.MedicalService != null ? i.MedicalService.Name : "",
                    i.Availability != null ? i.Availability.StartTime.Date : DateTime.MinValue,
                    i.Availability != null ? i.Availability.StartTime.ToString("HH:mm") : "",
                    i.Price
                )).ToList()
            ))
            .FirstOrDefaultAsync();

        return basketDto!;
    }


    public async Task<BasketDto> RemoveItemFromBasketAsync(string patientId, string itemId)
    {
        var basket = await _context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.PatientId == patientId);

        if (basket == null)
            throw new InvalidOperationException("Basket not found.");

        var item = basket.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            throw new InvalidOperationException("Item not found in basket.");

        basket.RemoveItem(itemId);

        await _context.SaveChangesAsync();

        return _mapper.Map<BasketDto>(basket);
    }

    public async Task<decimal> CheckoutAsync(string patientId, string? promoCode = null)
    {
        var dbContext = _context as DbContext;
        if (dbContext == null)
            throw new InvalidOperationException("DbContext transaction cannot be started.");

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            var basket = await _context.Baskets
    .Include(b => b.Items)
        .ThenInclude(i => i.Doctor)
    .Include(b => b.Items)
        .ThenInclude(i => i.MedicalService)
    .Include(b => b.Items)
        .ThenInclude(i => i.Availability)
    .FirstOrDefaultAsync(b => b.PatientId == patientId);

            if (basket == null || !basket.Items.Any())
                throw new InvalidOperationException("Basket is empty.");

            decimal totalPrice = 0;
            var items = basket.Items.ToList();
            var availabilitiesToBook = new List<Availability>();

            foreach (var item in items)
            {
                var availability = await _context.Availabilities
                    .FirstOrDefaultAsync(a => a.Id == item.AvailabilityId);

                if (availability == null)
                    throw new InvalidOperationException($"Availability {item.AvailabilityId} not found.");

                if (availability.DoctorId != item.DoctorId)
                    throw new InvalidOperationException("Availability does not belong to this doctor.");

                if (availability.IsBooked)
                    throw new InvalidOperationException($"Availability {item.AvailabilityId} is already booked.");

                totalPrice += item.Price;
                availabilitiesToBook.Add(availability);
            }

            // Apply promo code if exists
            if (!string.IsNullOrEmpty(promoCode))
            {
                var promo = await _context.PromoCodes
                    .FirstOrDefaultAsync(p => p.Code == promoCode && p.IsValid());

                if (promo != null)
                {
                    if (promo.DiscountPercent > 0)
                        totalPrice -= totalPrice * (promo.DiscountPercent / 100m);

                    if (promo.DiscountAmount > 0)
                        totalPrice -= promo.DiscountAmount;

                    if (totalPrice < 0)
                        totalPrice = 0;
                }
            }

            // Book availabilities and create appointments
            foreach (var item in items)
            {
                var availability = availabilitiesToBook
                    .First(a => a.Id == item.AvailabilityId);

                availability.Book(); // ✔ Booked = true

                var appointment = new Appointment(
                    item.DoctorId,
                    patientId,
                    item.AvailabilityId,
                    item.MedicalServiceId
                );

                _context.Appointments.Add(appointment);
            }

            // Remove basket items
            _context.BasketItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return totalPrice;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
