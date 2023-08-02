using GrupoSelect.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace GrupoSelect.Web.ViewModel
{
    public class ClientVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Name { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string CPF { get; set; }

        [Display(Name = "RG")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string RG { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Sex { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida.")]
        public DateTime DateBirth { get; set; }

        [Display(Name = "Naturalidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string NaturalFrom { get; set; }

        [Display(Name = "Nacionalidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Nationality { get; set; }

        [Display(Name = "Estado Civil")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Data de Expedição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida.")]
        public DateTime DateExp { get; set; }

        [Display(Name = "Orgão Expeditor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string OrganExp { get; set; }

        [Display(Name = "Contato")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Contact { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Email { get; set; }

        [Display(Name = "Profissão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Profession { get; set; }

        [Display(Name = "Renda")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Income { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Address { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Neighborhood { get; set; }

        [Display(Name = "Complemento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Complement { get; set; }

        [Display(Name = "Cep")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Cep { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string City { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string State { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? Active { get; set; }
        public virtual User User { get; set; }
    }
}
