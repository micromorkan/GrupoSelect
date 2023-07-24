using System.ComponentModel.DataAnnotations;

namespace GrupoSelect.Web.ViewModel
{
    public class LoginVM
    {
        [Display(Name = "User")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string User { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Password { get; set; }
    }
}
