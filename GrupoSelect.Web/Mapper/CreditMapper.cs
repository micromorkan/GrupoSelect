using GrupoSelect.Domain.Entity;
using GrupoSelect.Web.ViewModel;

namespace GrupoSelect.Web.Mapper
{
    public class CreditMapper : AutoMapper.Profile
    {
        public CreditMapper()
        {
            CreateMap<CreditVM, Credit>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}")).ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductTypeId, opt => opt.MapFrom(src => $"{src.ProductTypeId}")).ReverseMap().ForMember(dest => dest.ProductTypeId, opt => opt.Ignore())
                .ForMember(dest => dest.TableTypeId, opt => opt.MapFrom(src => $"{src.TableTypeId}")).ReverseMap().ForMember(dest => dest.TableTypeId, opt => opt.Ignore())
                .ForMember(dest => dest.FinancialAdminId, opt => opt.MapFrom(src => $"{src.FinancialAdminId}")).ReverseMap().ForMember(dest => dest.FinancialAdminId, opt => opt.Ignore())
                .ForMember(dest => dest.Months, opt => opt.MapFrom(src => $"{src.Months}")).ReverseMap().ForMember(dest => dest.Months, opt => opt.Ignore())
                .ForMember(dest => dest.CreditValue, opt => opt.MapFrom(src => $"{src.CreditValue}")).ReverseMap().ForMember(dest => dest.CreditValue, opt => opt.Ignore())
                .ForMember(dest => dest.PortionValue, opt => opt.MapFrom(src => $"{src.PortionValue}")).ReverseMap().ForMember(dest => dest.PortionValue, opt => opt.Ignore())
                .ForMember(dest => dest.MembershipValue, opt => opt.MapFrom(src => $"{src.MembershipValue}")).ReverseMap().ForMember(dest => dest.MembershipValue, opt => opt.Ignore())
                .ForMember(dest => dest.TotalValue, opt => opt.MapFrom(src => $"{src.TotalValue}")).ReverseMap().ForMember(dest => dest.TotalValue, opt => opt.Ignore());
        }
    }
}
