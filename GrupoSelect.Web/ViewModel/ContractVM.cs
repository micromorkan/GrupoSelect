using GrupoSelect.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace GrupoSelect.Web.ViewModel
{
    public class ContractVM
    {
        public int Id { get; set; }        
        public int ProposalId { get; set; }

        [Display(Name = "Número do Contrato")]
        public string ContractNum { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Motivo Reprovação")]
        public string? ReprovedReason { get; set; }

        [Display(Name = "Complemento Motivo")]
        public string? ReprovedExplain { get; set; }

        [Display(Name = "Ficha Cadastral Assinada")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public byte[]? ContractConsultancy { get; set; }

        [Display(Name = "Documentos e Comprovantes")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public byte[]? ContractFinancialAdmin { get; set; }

        [Display(Name = "Vídeo Aceite")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public byte[]? VideoAgree { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateStatus { get; set; }
        public DateTime? DateAproved { get; set; }
        public int? UserIdAproved { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ProposalVM Proposal { get; set; }
    }
}
