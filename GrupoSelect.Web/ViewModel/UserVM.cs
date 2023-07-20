using System.ComponentModel.DataAnnotations;

namespace GrupoSelect.Web.ViewModel
{
    public class UserVM
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nome não pode ter menos de 1 ou mais de 50 caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Cnpj")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "O Cnpj deve conter 14 números")]
        public string Cnpj { get; set; }

        [Display(Name = "Representação")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nome não pode ter menos de 1 ou mais de 50 caracteres.")]
        public string Representation { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nome não pode ter menos de 1 ou mais de 50 caracteres.")]
        [EmailAddress(ErrorMessage = "O {0} informado é inválido")]
        public string Email { get; set; }

        [Display(Name = "Login")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nome não pode ter menos de 1 ou mais de 50 caracteres.")]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Nome não pode ter menos de 1 ou mais de 50 caracteres.")]
        public string Password { get; set; }

        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Profile { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }
    }
}
