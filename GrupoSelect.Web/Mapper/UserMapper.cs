using GrupoSelect.Domain.Entity;
using GrupoSelect.Web.ViewModel;

namespace GrupoSelect.Web.Mapper
{
    public class UserMapper : AutoMapper.Profile
    {
        public UserMapper()
        {
            CreateMap<UserVM, User>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                ).ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.Name}")
                ).ForMember(
                    dest => dest.Cnpj,
                    opt => opt.MapFrom(src => $"{src.Cnpj}")
                ).ForMember(
                    dest => dest.Representation,
                    opt => opt.MapFrom(src => $"{src.Representation}")
                ).ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => $"{src.Email}")
                ).ForMember(
                    dest => dest.Login,
                    opt => opt.MapFrom(src => $"{src.Login}")
                ).ForMember(
                    dest => dest.Password,
                    opt => opt.MapFrom(src => $"{src.Password}")
                ).ForMember(
                    dest => dest.Profile,
                    opt => opt.MapFrom(src => $"{src.Profile}")
                ).ForMember(
                    dest => dest.Active,
                    opt => opt.MapFrom(src => $"{src.Active}")
                );
        }
    }
}
