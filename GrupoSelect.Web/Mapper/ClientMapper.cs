using GrupoSelect.Domain.Entity;
using GrupoSelect.Web.ViewModel;

namespace GrupoSelect.Web.Mapper
{
    public class ClientMapper : AutoMapper.Profile
    {
        public ClientMapper()
        {
            CreateMap<ClientVM, Client>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}")).ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name}")).ReverseMap().ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => $"{src.CPF}")).ReverseMap().ForMember(dest => dest.CPF, opt => opt.Ignore())
                .ForMember(dest => dest.RG, opt => opt.MapFrom(src => $"{src.RG}")).ReverseMap().ForMember(dest => dest.RG, opt => opt.Ignore())
                .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => $"{src.Sex}")).ReverseMap().ForMember(dest => dest.Sex, opt => opt.Ignore())
                .ForMember(dest => dest.DateBirth, opt => opt.MapFrom(src => $"{src.DateBirth}")).ReverseMap().ForMember(dest => dest.DateBirth, opt => opt.Ignore())
                .ForMember(dest => dest.NaturalFrom, opt => opt.MapFrom(src => $"{src.NaturalFrom}")).ReverseMap().ForMember(dest => dest.NaturalFrom, opt => opt.Ignore())
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => $"{src.Nationality}")).ReverseMap().ForMember(dest => dest.Nationality, opt => opt.Ignore())
                .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => $"{src.MaritalStatus}")).ReverseMap().ForMember(dest => dest.MaritalStatus, opt => opt.Ignore())
                .ForMember(dest => dest.DateExp, opt => opt.MapFrom(src => $"{src.DateExp}")).ReverseMap().ForMember(dest => dest.DateExp, opt => opt.Ignore())
                .ForMember(dest => dest.OrganExp, opt => opt.MapFrom(src => $"{src.OrganExp}")).ReverseMap().ForMember(dest => dest.OrganExp, opt => opt.Ignore())
                .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => $"{src.Contact}")).ReverseMap().ForMember(dest => dest.Contact, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => $"{src.Email}")).ReverseMap().ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Profession, opt => opt.MapFrom(src => $"{src.Profession}")).ReverseMap().ForMember(dest => dest.Profession, opt => opt.Ignore())
                .ForMember(dest => dest.Income, opt => opt.MapFrom(src => $"{src.Income}")).ReverseMap().ForMember(dest => dest.Income, opt => opt.Ignore())
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address}")).ReverseMap().ForMember(dest => dest.Address, opt => opt.Ignore())
                .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => $"{src.Neighborhood}")).ReverseMap().ForMember(dest => dest.Neighborhood, opt => opt.Ignore())
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => $"{src.Complement}")).ReverseMap().ForMember(dest => dest.Complement, opt => opt.Ignore())
                .ForMember(dest => dest.Cep, opt => opt.MapFrom(src => $"{src.Cep}")).ReverseMap().ForMember(dest => dest.Cep, opt => opt.Ignore())
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => $"{src.City}")).ReverseMap().ForMember(dest => dest.City, opt => opt.Ignore())
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => $"{src.State}")).ReverseMap().ForMember(dest => dest.State, opt => opt.Ignore());
        }
    }
}
