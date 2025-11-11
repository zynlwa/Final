namespace AppointmentSystem.Application.Common.Mappers;

public class DoctorProfile:Profile
{
    public DoctorProfile() 
    {
        CreateMap<Doctor, DoctorDto>()
               .ConstructUsing(d => new DoctorDto(
                   d.Id,
                   d.FirstName,
                   d.LastName,
                   d.Specialty,
                   d.Email,
                   d.PhoneNumber,
                   d.IsDeleted,
                   d.DeletedAt,
                   d.DeletedBy
               ));

        
        CreateMap<CreateDoctorDto, Doctor>()
            .ConstructUsing(dto => new Doctor(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.Specialty,
                dto.PhoneNumber,
                null! 
            ));

    
    }
}
