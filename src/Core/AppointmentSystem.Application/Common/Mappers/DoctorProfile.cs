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
         d.ImageUrl,
         d.PhoneNumber,
         d.ExperienceYears,
         d.Reviews.Any() ? d.Reviews.Average(r => r.Rating) : 0,
         d.Appointments.Count,
         d.IsDeleted,
         d.DeletedAt,
         d.DeletedBy
     ));



        CreateMap<CreateDoctorDto, Doctor>()
            .ConstructUsing(dto => new Doctor(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.ImageUrl,
                dto.Specialty,
                dto.PhoneNumber,
                dto.ExperienceYears,
                null! 
            ));

    
    }
}
