using GrupoSelect.Domain.Entity;
using GrupoSelect.Web.ViewModel;

namespace GrupoSelect.Web.Mapper
{
    public class FinancialAdminMapper : AutoMapper.Profile
    {
        public FinancialAdminMapper()
        {
            CreateMap<FinancialAdminVM, FinancialAdmin>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}")).ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name.ToUpperInvariant()}")).ReverseMap().ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => $"{src.Active}")).ReverseMap().ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}
