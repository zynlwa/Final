namespace AppointmentSystem.Application.Services.Abstractions;

public interface IReviewService
{
    Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto);
    Task<IEnumerable<ReviewDto>> GetReviewsByDoctorIdAsync(string doctorId);
    Task<double> GetDoctorAverageRatingAsync(string doctorId);
    Task<int> GetDoctorReviewCountAsync(string doctorId);
}

