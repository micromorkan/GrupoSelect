using GrupoSelect.Domain.Entity;

namespace GrupoSelect.Web.Views.Shared.Reports.Models
{
    public class RegistrationForm
    {
        public RegistrationForm(Client client, Proposal proposal, User user)
        {
            Client = client;
            Proposal = proposal;
            User = user;
        }

        public Client Client { get; set; }
        public Proposal Proposal { get; set; }
        public User User { get; set; }
    }

    public class Client
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Sexo { get; set; }
        public string DtNascimento { get; set; }
        public string Natural { get; set; }
        public string Nacionalidade { get; set; }
        public string EstadoCivil { get; set; }
        public string DtExp { get; set; }
        public string OrgaoExp { get; set; }
        public string Contato { get; set; }
        public string Email { get; set; }
        public string Profissao { get; set; }
        public string Renda { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool Ativo { get; set; }

    }
}
