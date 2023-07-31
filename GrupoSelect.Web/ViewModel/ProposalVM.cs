using System.ComponentModel.DataAnnotations;

namespace GrupoSelect.Web.ViewModel
{
    public class ProposalVM
    {
        public int Id { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int ClientId { get; set; }

        [Display(Name = "Tipo Produto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string ProductTypeName { get; set; }

        [Display(Name = "Tipo Tabela")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string TableTypeTax { get; set; }

        [Display(Name = "Taxa Adesão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string TableTypeFee { get; set; }

        [Display(Name = "Taxa Restante")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string TableTypeRate { get; set; }

        [Display(Name = "Administradora")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string FinancialAdminName { get; set; }

        [Display(Name = "Crédito")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CreditValue { get; set; }

        [Display(Name = "Valor Parcela")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CreditPortionValue { get; set; }

        [Display(Name = "Valor Adesão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CreditMembershipValue { get; set; }

        [Display(Name = "Valor Total")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CreditTotalValue { get; set; }

        public DateTime DateCreate { get; set; }
        public DateTime? DateChecked { get; set; }
        public DateTime? UserChecked { get; set; }
        public bool Aproved { get; set; }

    }
}
