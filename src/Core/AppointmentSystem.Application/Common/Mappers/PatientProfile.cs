namespace AppointmentSystem.Application.Common.Mappers;

public class PatientProfile : Profile
{
    public PatientProfile()
    {

        CreateMap<Patient, PatientDto>()
             .ConstructUsing(p => new PatientDto(
                 p.Id,
                 p.FirstName,
                 p.LastName,
                 p.Email,
                 p.DateOfBirth,
                 p.PhoneNumber,
                 p.AppUserId,
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
                null!
            ));

        CreateMap<UpdatePatientDto, Patient>();
    }
}
