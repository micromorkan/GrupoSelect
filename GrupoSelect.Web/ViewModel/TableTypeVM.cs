using System.ComponentModel.DataAnnotations;

namespace GrupoSelect.Web.ViewModel
{
    public class TableTypeVM
    {
        public int Id { get; set; }

        [Display(Name = "Tabela")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string TableTax { get; set; }

        [Display(Name = "Taxa Adesão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string MembershipFee { get; set; }

        [Display(Name = "Taxa Restante")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string RemainingRate { get; set; }

        [Display(Name = "Taxa Comissão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CommissionFee { get; set; }
    }
}
