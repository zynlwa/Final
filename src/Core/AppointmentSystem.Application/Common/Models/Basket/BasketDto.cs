namespace AppointmentSystem.Application.Common.Models.Basket;
public record BasketItemDto(
    string Id,
    string DoctorId,
    string AvailabilityId,
    string MedicalServiceId,
    decimal Price 
);


public record BasketDto(
    string Id,
    string PatientId,
    List<BasketItemDto> Items
);

public record AddBasketItemDto(
    string DoctorId,
    string AvailabilityId,
    string MedicalServiceId

);
