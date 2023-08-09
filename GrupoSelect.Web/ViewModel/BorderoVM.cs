using System.ComponentModel.DataAnnotations;

namespace GrupoSelect.Web.ViewModel
{
    public class BorderoVM
    {
        [Display(Name = "Representante")]
        public int UserId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public UserVM User { get; set; }
    }
}
