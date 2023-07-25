using GrupoSelect.Domain.Entity;
using GrupoSelect.Web.ViewModel;

namespace GrupoSelect.Web.Mapper
{
    public class ProductTypeMapper : AutoMapper.Profile
    {
        public ProductTypeMapper()
        {
            CreateMap<ProductTypeVM, ProductType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}")).ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => $"{src.ProductName.ToUpperInvariant()}")).ReverseMap().ForMember(dest => dest.ProductName, opt => opt.Ignore());
        }
    }
}
