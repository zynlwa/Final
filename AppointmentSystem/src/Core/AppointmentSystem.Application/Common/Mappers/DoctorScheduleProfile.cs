using AppointmentSystem.Application.Common.Models.DoctorSchedule;

namespace AppointmentSystem.Application.Common.Mappers;

public class DoctorScheduleProfile : Profile
{
    public DoctorScheduleProfile()
    {
        // WorkSchedule
        CreateMap<DoctorWorkSchedule, WorkScheduleDto>();

        // Break
        CreateMap<DoctorBreak, BreakDto>();

        // Unavailability
        CreateMap<DoctorUnavailability, UnavailabilityDto>();
    }
}