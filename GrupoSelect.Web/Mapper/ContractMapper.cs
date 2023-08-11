using GrupoSelect.Domain.Entity;
using GrupoSelect.Web.ViewModel;

namespace GrupoSelect.Web.Mapper
{
    public class ContractMapper : AutoMapper.Profile
    {
        public ContractMapper()
        {
            CreateMap<ContractVM, Contract>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}")).ReverseMap().ForMember(dest => dest.Id, opt => opt.AllowNull())
                .ForMember(dest => dest.ProposalId, opt => opt.MapFrom(src => $"{src.ProposalId}")).ReverseMap().ForMember(dest => dest.ProposalId, opt => opt.Ignore())
                .ForMember(dest => dest.ContractNum, opt => opt.MapFrom(src => $"{src.ContractNum}")).ReverseMap().ForMember(dest => dest.ContractNum, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => $"{src.Status}")).ReverseMap().ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.ReprovedReason, opt => opt.MapFrom(src => $"{src.ReprovedReason}")).ReverseMap().ForMember(dest => dest.ReprovedReason, opt => opt.Ignore())
                .ForMember(dest => dest.ReprovedExplain, opt => opt.MapFrom(src => $"{src.ReprovedExplain}")).ReverseMap().ForMember(dest => dest.ReprovedExplain, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreate, opt => opt.MapFrom(src => $"{src.DateCreate}")).ReverseMap().ForMember(dest => dest.DateCreate, opt => opt.Ignore())
                .ForMember(dest => dest.DateStatus, opt => opt.MapFrom(src => $"{src.DateStatus}")).ReverseMap().ForMember(dest => dest.DateStatus, opt => opt.Ignore())
                .ForMember(dest => dest.DateAproved, opt => opt.MapFrom(src => $"{src.DateAproved}")).ReverseMap().ForMember(dest => dest.DateAproved, opt => opt.Ignore())
                .ForMember(dest => dest.UserIdAproved, opt => opt.MapFrom(src => $"{src.UserIdAproved}")).ReverseMap().ForMember(dest => dest.UserIdAproved, opt => opt.Ignore());
        }
    }
}
