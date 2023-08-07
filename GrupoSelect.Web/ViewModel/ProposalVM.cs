using System.ComponentModel.DataAnnotations;

namespace GrupoSelect.Web.ViewModel
{
    public class ProposalVM
    {
        public int Id { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int ClientId { get; set; }

        [Display(Name = "Crédito")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int CreditId { get; set; }

        [Display(Name = "Tipo Produto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int ProductTypeId { get; set; }

        [Display(Name = "Tipo Tabela")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int TableTypeId { get; set; }

        [Display(Name = "Administradora")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int FinancialAdminId { get; set; }

        public string ProductTypeName { get; set; }

        public string TableTypeTax { get; set; }
        public string TableTypeFee { get; set; }
        public string TableTypeCommission { get; set; }
        public string TableTypeRate { get; set; }

        public string FinancialAdminName { get; set; }

        [Display(Name = "Valor do Crédito")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CreditValue { get; set; }

        [Display(Name = "Valor da Parcela")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CreditPortionValue { get; set; }

        [Display(Name = "Valor da Adesão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CreditMembershipValue { get; set; }

        [Display(Name = "Valor Total")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CreditTotalValue { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        public DateTime DateCreate { get; set; }
        public DateTime? DateChecked { get; set; }
        public DateTime? UserChecked { get; set; }
        public bool Aproved { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public UserVM User { get; set; }
        public ClientVM Client { get; set; }

    }
}
