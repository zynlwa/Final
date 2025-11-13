namespace AppointmentSystem.Application.Common.Mappers;

public class BasketProfile : Profile
{
    public BasketProfile()
    {

        CreateMap<Basket, BasketDto>()
       .ConstructUsing(b => new BasketDto(
           b.Id,
           b.PatientId,
           b.Items.Select(i => new BasketItemDto(
               i.Id,
               i.DoctorId,
               i.AvailabilityId,
               i.MedicalServiceId,
               i.Price
           )).ToList()
       ));

        CreateMap<BasketItem, BasketItemDto>()
            .ConstructUsing(i => new BasketItemDto(
                i.Id,
                i.DoctorId,
                i.AvailabilityId,
                i.MedicalServiceId,
                i.Price
            ));

    }
}
