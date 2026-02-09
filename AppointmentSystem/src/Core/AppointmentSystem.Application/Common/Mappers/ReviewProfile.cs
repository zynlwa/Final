using AppointmentSystem.Application.Common.Models.Review;

namespace AppointmentSystem.Application.Common.Mappers;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.PatientName,
                opt => opt.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
            .ForMember(dest => dest.DoctorName,
                opt => opt.MapFrom(src => src.Doctor.FirstName + " " + src.Doctor.LastName));

        CreateMap<CreateReviewDto, Review>();
    }
}

