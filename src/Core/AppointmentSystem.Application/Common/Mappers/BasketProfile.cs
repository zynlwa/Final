using AutoMapper;
using AppointmentSystem.Domain.Models;
using AppointmentSystem.Application.Common.Models.Basket;
using System.Linq;

namespace AppointmentSystem.Application.Common.Mappers
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            // Basket → BasketDto
            CreateMap<Basket, BasketDto>()
    .ConstructUsing(b => new BasketDto(
        b.Id,
        b.PatientId,
        b.Items.Select(i => new BasketItemDto(
            i.Id,
            i.DoctorId,
            i.Doctor != null ? i.Doctor.FirstName + " " + i.Doctor.LastName : string.Empty,
            i.AvailabilityId,
            i.MedicalServiceId,
            i.MedicalService != null ? i.MedicalService.Name : string.Empty,
            i.Availability != null ? i.Availability.StartTime.Date : DateTime.MinValue,
            i.Availability != null ? i.Availability.StartTime.ToString("HH:mm") : string.Empty,
            i.Price
        )).ToList()
    ));

            // BasketItem → BasketItemDto
            CreateMap<BasketItem, BasketItemDto>()
    .ConstructUsing(i => new BasketItemDto(
        i.Id,
        i.DoctorId,
        i.Doctor != null ? i.Doctor.FirstName + " " + i.Doctor.LastName : string.Empty,
        i.AvailabilityId,
        i.MedicalServiceId,
        i.MedicalService != null ? i.MedicalService.Name : string.Empty,
        i.Availability != null ? i.Availability.StartTime.Date : DateTime.MinValue,
        i.Availability != null ? i.Availability.StartTime.ToString("HH:mm") : string.Empty,
        i.Price
    ));

        }
    }
}
