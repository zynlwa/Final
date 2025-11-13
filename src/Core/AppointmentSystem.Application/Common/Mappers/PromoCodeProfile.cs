namespace AppointmentSystem.Application.Common.Mappers;

public class PromoCodeProfile : Profile
{
    public PromoCodeProfile()
    {
        CreateMap<PromoCode, PromoCodeDto>()
            .ForMember(dest => dest.IsValid, opt => opt.MapFrom(src => src.IsValid()));

        CreateMap<CreatePromoCodeDto, PromoCode>()
            .ConstructUsing(dto => new PromoCode(
                dto.Code,
                dto.DiscountAmount,
                dto.DiscountPercent,
                dto.ExpirationDate
            ));


        CreateMap<UpdatePromoCodeDto, PromoCode>()
            .ConstructUsing((dto, context) =>
            {
                var promo = context.Mapper.Map<PromoCode>(dto); 
                return promo;
            });
    }
}
