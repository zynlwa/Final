using AppointmentSystem.Application.Common.Models.Slots;

namespace AppointmentSystem.Application.Services.Abstractions
{
    public interface ISlotService
    {
        Task<IEnumerable<SlotDto>> GenerateSlotsAsync(
            string doctorId,
            DateTime date,
            string medicalServiceId,
            int intervalMinutes = 15);
    }
}
