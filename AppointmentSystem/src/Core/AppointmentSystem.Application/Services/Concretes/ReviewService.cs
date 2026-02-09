namespace AppointmentSystem.Application.Services.Concretes;

public class ReviewService : IReviewService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public ReviewService(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReviewDto?> CreateReviewAsync(CreateReviewDto dto)
    {
        // Əgər patient artıq review veribsə, null qaytar frontda yoxlamaq üçün
        var exists = await _context.Reviews
            .AnyAsync(r => r.PatientId == dto.PatientId && r.DoctorId == dto.DoctorId);

        if (exists)
        {
            return null;
        }

        var review = new Review(
            dto.PatientId,
            dto.DoctorId,
            dto.Rating,
            dto.Comment
        );

        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();

        var full = await _context.Reviews
            .Include(r => r.Patient)
            .Include(r => r.Doctor)
            .FirstAsync(r => r.Id == review.Id);

        return _mapper.Map<ReviewDto>(full);
    }


    public async Task<IEnumerable<ReviewDto>> GetReviewsByDoctorIdAsync(string doctorId)
    {
        var reviews = await _context.Reviews
            .Include(r => r.Patient)
            .Include(r => r.Doctor)
            .Where(r => r.DoctorId == doctorId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<double> GetDoctorAverageRatingAsync(string doctorId)
    {
        return await _context.Reviews
            .Where(r => r.DoctorId == doctorId)
            .AverageAsync(r => (double?)r.Rating) ?? 0;
    }

    public async Task<int> GetDoctorReviewCountAsync(string doctorId)
    {
        return await _context.Reviews.CountAsync(r => r.DoctorId == doctorId);
    }
}
