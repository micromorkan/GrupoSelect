using System.ComponentModel.DataAnnotations;

namespace GrupoSelect.Web.ViewModel
{
    public class CreditVM
    {
        public int Id { get; set; }

        [Display(Name = "Tipo Produto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int ProductTypeId { get; set; }

        [Display(Name = "Tipo Tabela")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int TableTypeId { get; set; }

        [Display(Name = "Administradora")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int FinancialAdminId { get; set; }

        [Display(Name = "Meses")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int Months { get; set; }

        [Display(Name = "Valor Crédito")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CreditValue { get; set; }

        [Display(Name = "Valor Parcela")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string PortionValue { get; set; }

        [Display(Name = "Valor Adesão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string MembershipValue { get; set; }

        [Display(Name = "Valor Total")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string TotalValue { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
