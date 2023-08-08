using GrupoSelect.Domain.Entity;
using GrupoSelect.Web.ViewModel;

namespace GrupoSelect.Web.Mapper
{
    public class TableTypeMapper : AutoMapper.Profile
    {
        public TableTypeMapper()
        {
            CreateMap<TableTypeVM, TableType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}")).ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TableTax, opt => opt.MapFrom(src => $"{src.TableTax}")).ReverseMap().ForMember(dest => dest.TableTax, opt => opt.Ignore())
                .ForMember(dest => dest.MembershipFee, opt => opt.MapFrom(src => $"{src.MembershipFee}")).ReverseMap().ForMember(dest => dest.MembershipFee, opt => opt.Ignore())
                .ForMember(dest => dest.RemainingRate, opt => opt.MapFrom(src => $"{src.RemainingRate}")).ReverseMap().ForMember(dest => dest.RemainingRate, opt => opt.Ignore())
                .ForMember(dest => dest.CommissionFee, opt => opt.MapFrom(src => $"{src.CommissionFee}")).ReverseMap().ForMember(dest => dest.CommissionFee, opt => opt.Ignore())
                .ForMember(dest => dest.ManagerFee, opt => opt.MapFrom(src => $"{src.ManagerFee}")).ReverseMap().ForMember(dest => dest.ManagerFee, opt => opt.Ignore());
        }
    }
}
