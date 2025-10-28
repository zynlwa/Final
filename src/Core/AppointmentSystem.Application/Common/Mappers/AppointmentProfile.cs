using AppointmentSystem.Application.Common.Models.Appointment;

namespace AppointmentSystem.Application.Common.Mappers;

public class AppointmentProfile:Profile
{
    public AppointmentProfile()
    {
        CreateMap<CreateAppointmentDto, Appointment>()
     .ConstructUsing(dto => new Appointment(
         dto.DoctorId,
         dto.PatientId,
         dto.AvailabilityId,
         dto.MedicalServiceId,
         dto.Notes
     ));

        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.Doctor.FirstName} {src.Doctor.LastName}"))
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => $"{src.Patient.FirstName} {src.Patient.LastName}"))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Availability.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Availability.EndTime))
            .ForMember(dest => dest.MedicalServiceName, opt => opt.MapFrom(src => src.MedicalService.Name));

    }
}
