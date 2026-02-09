namespace AppointmentSystem.Application.Common.Mappers;

public class MedicalServiceProfile:Profile
{
    public MedicalServiceProfile()
    {
        CreateMap<CreateMedicalServiceDto, MedicalService>()
                .ConstructUsing(dto => new MedicalService(
                    dto.Name,
                    dto.Description,
                    dto.Price,
                    dto.DurationMinutes

                ));
        CreateMap<UpdateMedicalServiceDto, MedicalService>();

        CreateMap<MedicalService, MedicalServiceDto>();

    }
}
