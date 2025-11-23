namespace AppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewController(IReviewService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto)
    {
        var result = await _service.CreateReviewAsync(dto);
        return Ok(Response<ReviewDto>.Success(result, 200));
    }

    [HttpGet("doctor/{doctorId}")]
    public async Task<IActionResult> GetDoctorReviews(string doctorId)
    {
        var result = await _service.GetReviewsByDoctorIdAsync(doctorId);
        return Ok(Response<IEnumerable<ReviewDto>>.Success(result, 200));
    }

    [HttpGet("doctor/{doctorId}/rating")]
    public async Task<IActionResult> GetDoctorRating(string doctorId)
    {
        var avg = await _service.GetDoctorAverageRatingAsync(doctorId);
        var count = await _service.GetDoctorReviewCountAsync(doctorId);

        var data = new
        {
            rating = avg,
            reviewCount = count
        };

        return Ok(Response<object>.Success(data, 200));
    }
}
