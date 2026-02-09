using AppointmentSystem.Application.Services.Interfaces;
namespace AppointmentSystem.Application.Services.Concretes;

public class PromoCodeService : IPromoCodeService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public PromoCodeService(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PromoCodeDto>> GetAllAsync()
    {
        return await _context.PromoCodes
            .Select(p => _mapper.Map<PromoCodeDto>(p))
            .ToListAsync();
    }

    public async Task<PromoCodeDto?> GetByIdAsync(string id)
    {
        var promo = await _context.PromoCodes.FindAsync(id);
        return promo == null ? null : _mapper.Map<PromoCodeDto>(promo);
    }

    public async Task<PromoCodeDto> CreateAsync(CreatePromoCodeDto dto)
    {
        var promo = _mapper.Map<PromoCode>(dto);
        _context.PromoCodes.Add(promo);
        await _context.SaveChangesAsync();
        return _mapper.Map<PromoCodeDto>(promo);
    }

    public async Task<PromoCodeDto?> UpdateAsync(string id, UpdatePromoCodeDto dto)
    {
        var promo = await _context.PromoCodes.FindAsync(id);
        if (promo == null)
            return null;

        promo.Update(dto.Code, dto.DiscountAmount, dto.DiscountPercent, dto.ExpirationDate);
        await _context.SaveChangesAsync();
        return _mapper.Map<PromoCodeDto>(promo);
    }
    public async Task<PromoCodeDto?> GetByCodeAsync(string code)
    {
        var promo = await _context.PromoCodes
            .FirstOrDefaultAsync(p => p.Code.ToUpper() == code.ToUpper());
        return promo == null ? null : _mapper.Map<PromoCodeDto>(promo);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var promo = await _context.PromoCodes.FindAsync(id);
        if (promo == null)
            return false;

        _context.PromoCodes.Remove(promo);
        await _context.SaveChangesAsync();
        return true;
    }
}
