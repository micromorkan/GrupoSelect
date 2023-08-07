using GrupoSelect.Domain.Entity;

namespace GrupoSelect.Web.Views.Shared.Reports.Models
{
    public class ContractForm
    {
        public ContractForm(Client client, Proposal proposal, User user, Contract contract = null)
        {
            Client = client;
            Proposal = proposal;
            User = user;
            Contract = contract;
        }

        public Client Client { get; set; }
        public Proposal Proposal { get; set; }
        public User User { get; set; }
        public Contract Contract { get; set;}
    }
}
