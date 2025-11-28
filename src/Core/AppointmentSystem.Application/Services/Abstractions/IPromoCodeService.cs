namespace AppointmentSystem.Application.Services.Interfaces;

public interface IPromoCodeService
{
    Task<List<PromoCodeDto>> GetAllAsync();
    Task<PromoCodeDto?> GetByIdAsync(string id);
    Task<PromoCodeDto> CreateAsync(CreatePromoCodeDto dto);
    Task<PromoCodeDto?> UpdateAsync(string id, UpdatePromoCodeDto dto);
    Task<bool> DeleteAsync(string id);
    Task<PromoCodeDto?> GetByCodeAsync(string code);

}
