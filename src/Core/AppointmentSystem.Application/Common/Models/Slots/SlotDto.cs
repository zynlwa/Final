namespace AppointmentSystem.Application.Common.Models.Slots;

public record SlotDto(
    DateTime StartTime,
    DateTime EndTime,
    bool IsBooked
);
