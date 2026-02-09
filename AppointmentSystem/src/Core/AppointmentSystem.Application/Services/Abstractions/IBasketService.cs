namespace AppointmentSystem.Application.Services.Abstractions;

public interface IBasketService
{
    Task<BasketDto> GetBasketByPatientIdAsync(string patientId);
    Task<BasketDto> AddItemToBasketAsync(string patientId, string doctorId, string availabilityId, string medicalServiceId);
    Task<BasketDto> RemoveItemFromBasketAsync(string patientId, string itemId);
    Task<decimal> CheckoutAsync(string patientId, string? promoCode = null);
}