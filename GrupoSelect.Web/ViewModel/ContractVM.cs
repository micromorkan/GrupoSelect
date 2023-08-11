using GrupoSelect.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace GrupoSelect.Web.ViewModel
{
    public class ContractVM
    {
        public int Id { get; set; }        
        public int ProposalId { get; set; }

        [Display(Name = "Cliente")]
        public int ClientId { get; set; }

        [Display(Name = "Número do Contrato")]
        public string ContractNum { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Status { get; set; }

        [Display(Name = "Motivo Reprovação")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string? ReprovedReason { get; set; }

        [Display(Name = "Complemento Motivo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string? ReprovedExplain { get; set; }

        [Display(Name = "Contrato de Consultoria Assinado")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public IFormFile? ContractConsultancyFormFile { get; set; }

        [Display(Name = "Documentos e Comprovantes")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public IFormFile? ContractFinancialAdminFormFile { get; set; }

        [Display(Name = "Vídeo Aceite")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public IFormFile? VideoAgreeFormFile { get; set; }

        [Display(Name = "Data Cadastro Registro")]
        public DateTime DateCreate { get; set; }
        public DateTime? DateStatus { get; set; }

        [Display(Name = "Data da Aprovação")]
        public DateTime? DateAproved { get; set; }
        public int? UserIdAproved { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ProposalVM Proposal { get; set; }
    }
}
