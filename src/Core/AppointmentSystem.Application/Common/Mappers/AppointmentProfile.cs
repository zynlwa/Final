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
            .ConstructUsing(src => new AppointmentDto(
                src.Id.ToString(),
                src.DoctorId.ToString(),
                $"{src.Doctor.FirstName} {src.Doctor.LastName}",
                src.PatientId.ToString(),
                $"{src.Patient.FirstName} {src.Patient.LastName}",
                src.AvailabilityId.ToString(),
                src.Availability.StartTime,
                src.Availability.EndTime,
                src.MedicalServiceId.ToString(),
                src.MedicalService.Name,
                src.Status.ToString(),
                src.Notes
            ));


    }
}
