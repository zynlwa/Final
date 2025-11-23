namespace AppointmentSystem.Application.Common.Models.Review;

public record ReviewDto
{
    public string Id { get; init; }
    public string PatientId { get; init; }
    public string DoctorId { get; init; }
    public int Rating { get; init; }
    public string Comment { get; init; }
    public string PatientName { get; init; }
    public string DoctorName { get; init; }
    public DateTime CreatedDate { get; init; }
}

public record CreateReviewDto(
    string PatientId,
    string DoctorId,
    int Rating,
    string Comment
);

