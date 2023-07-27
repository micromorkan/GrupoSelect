﻿using Web.Views.Shared.Componentes.ComponentModels;

namespace GrupoSelect.Web.ViewModel
{
    public class ComponentsVM
    {
        public ComponentsVM()
        {
            LstChart = new List<Chart>();
            LstTile = new List<Tile>();
            LstFill = new List<FillBar>();
        }

        public List<Chart> LstChart { get; set; }
        public List<Tile> LstTile { get; set; }
        public List<FillBar> LstFill { get; set; }
        public Calendar Calendar { get; set; }
    }
}
