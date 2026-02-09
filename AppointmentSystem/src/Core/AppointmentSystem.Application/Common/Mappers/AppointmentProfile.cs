public class AppointmentProfile : Profile
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
         src.Id,
         src.DoctorId,
         src.Doctor != null ? $"{src.Doctor.FirstName} {src.Doctor.LastName}" : "Unknown",
         src.PatientId,
         src.Patient != null ? $"{src.Patient.FirstName} {src.Patient.LastName}" : "Unknown",
         src.AvailabilityId,
         src.Availability != null ? src.Availability.StartTime : DateTime.MinValue,
         src.Availability != null ? src.Availability.EndTime : DateTime.MinValue,
         src.MedicalServiceId,
         src.MedicalService != null ? src.MedicalService.Name : "Unknown",
         src.Status.ToString(),
         src.Notes
     ));

    }
}
