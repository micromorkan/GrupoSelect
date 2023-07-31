using GrupoSelect.Domain.Entity;
using GrupoSelect.Web.ViewModel;

namespace GrupoSelect.Web.Mapper
{
    public class ProposalMapper : AutoMapper.Profile
    {
        public ProposalMapper()
        {
            CreateMap<ProposalVM, Proposal>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}")).ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => $"{src.ClientId}")).ReverseMap().ForMember(dest => dest.ClientId, opt => opt.Ignore())
                .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => $"{src.ProductTypeName}")).ReverseMap().ForMember(dest => dest.ProductTypeName, opt => opt.Ignore())
                .ForMember(dest => dest.TableTypeTax, opt => opt.MapFrom(src => $"{src.TableTypeTax}")).ReverseMap().ForMember(dest => dest.TableTypeTax, opt => opt.Ignore())
                .ForMember(dest => dest.TableTypeFee, opt => opt.MapFrom(src => $"{src.TableTypeFee}")).ReverseMap().ForMember(dest => dest.TableTypeFee, opt => opt.Ignore())
                .ForMember(dest => dest.TableTypeRate, opt => opt.MapFrom(src => $"{src.TableTypeRate}")).ReverseMap().ForMember(dest => dest.TableTypeRate, opt => opt.Ignore())
                .ForMember(dest => dest.FinancialAdminName, opt => opt.MapFrom(src => $"{src.FinancialAdminName}")).ReverseMap().ForMember(dest => dest.FinancialAdminName, opt => opt.Ignore())
                .ForMember(dest => dest.CreditValue, opt => opt.MapFrom(src => $"{src.CreditValue}")).ReverseMap().ForMember(dest => dest.CreditValue, opt => opt.Ignore())
                .ForMember(dest => dest.CreditPortionValue, opt => opt.MapFrom(src => $"{src.CreditPortionValue}")).ReverseMap().ForMember(dest => dest.CreditPortionValue, opt => opt.Ignore())
                .ForMember(dest => dest.CreditMembershipValue, opt => opt.MapFrom(src => $"{src.CreditMembershipValue}")).ReverseMap().ForMember(dest => dest.CreditMembershipValue, opt => opt.Ignore())
                .ForMember(dest => dest.CreditTotalValue, opt => opt.MapFrom(src => $"{src.CreditTotalValue}")).ReverseMap().ForMember(dest => dest.CreditTotalValue, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreate, opt => opt.MapFrom(src => $"{src.DateCreate}")).ReverseMap().ForMember(dest => dest.DateCreate, opt => opt.Ignore())
                .ForMember(dest => dest.DateChecked, opt => opt.MapFrom(src => $"{src.DateChecked}")).ReverseMap().ForMember(dest => dest.DateChecked, opt => opt.Ignore())
                .ForMember(dest => dest.UserChecked, opt => opt.MapFrom(src => $"{src.UserChecked}")).ReverseMap().ForMember(dest => dest.UserChecked, opt => opt.Ignore())
                .ForMember(dest => dest.Aproved, opt => opt.MapFrom(src => $"{src.Aproved}")).ReverseMap().ForMember(dest => dest.Aproved, opt => opt.Ignore());
        }
    }
}
