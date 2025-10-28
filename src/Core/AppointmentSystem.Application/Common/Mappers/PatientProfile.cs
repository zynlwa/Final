namespace AppointmentSystem.Application.Common.Mappers;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        // Patient -> PatientDto
        CreateMap<Patient, PatientDto>()
            .ConstructUsing(p => new PatientDto(
                p.Id,
                p.FirstName,
                p.LastName,
                p.Email,
                p.DateOfBirth,
                p.PhoneNumber,
                p.IsDeleted,
                p.DeletedAt,
                p.DeletedBy
            ));

        CreateMap<CreatePatientDto, Patient>()
     .ConstructUsing(dto => new Patient(
         dto.FirstName,
         dto.LastName,
         dto.Email,
         dto.PhoneNumber,
         dto.DateOfBirth,
         dto.AppUserId
     ));
        CreateMap<UpdatePatientDto, Patient>();
    }
}
