using GrupoSelect.Web.Views.Shared.Components.Models;

namespace GrupoSelect.Web.ViewModel
{
    public class ComponentsVM
    {
        public ComponentsVM()
        {
            LstChart = new List<Chart>();
            LstChartMoney = new List<ChartMoney>();
            LstTile = new List<Tile>();
            LstFill = new List<FillBar>();
        }

        public List<Chart> LstChart { get; set; }
        public List<ChartMoney> LstChartMoney { get; set; }
        public List<Tile> LstTile { get; set; }
        public List<FillBar> LstFill { get; set; }
        public Calendar Calendar { get; set; }
    }
}
