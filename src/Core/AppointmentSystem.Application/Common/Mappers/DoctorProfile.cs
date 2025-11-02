using AppointmentSystem.Domain.Models;

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
                d.Email,
                d.Specialty,
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
                dto.AppUserId
            ));
    }
    }
