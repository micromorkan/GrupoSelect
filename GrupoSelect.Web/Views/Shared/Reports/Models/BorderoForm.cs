using GrupoSelect.Domain.Entity;

namespace GrupoSelect.Web.Views.Shared.Reports.Models
{
    public class BorderoForm
    {
        public BorderoForm(IEnumerable<Contract> contracts, User user, DateTime startDate, DateTime endDate)
        {
            Contracts = contracts;
            User = user;
            EndDate = endDate;
            StartDate = startDate;
        }

        public IEnumerable<Contract> Contracts { get; set; }
        public User User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
