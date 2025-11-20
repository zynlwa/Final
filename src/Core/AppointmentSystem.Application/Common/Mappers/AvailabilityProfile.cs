namespace AppointmentSystem.Application.Common.Mappers;

public class AvailabilityProfile:Profile
{
    public AvailabilityProfile()
    {
        CreateMap<CreateAvailabilityDto, Availability>()
            .ConstructUsing(dto => new Availability(dto.DoctorId,dto.MedicalServiceId, dto.StartTime, dto.EndTime));

        CreateMap<Availability, AvailabilityDto>();
    }
}
