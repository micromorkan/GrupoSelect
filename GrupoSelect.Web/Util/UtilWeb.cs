using GrupoSelect.Domain.Entity;
using GrupoSelect.Services.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GrupoSelect.Web.Util
{
    public class UtilWeb
    {
        public static IEnumerable<SelectListItem> GetBooleanList(bool filter = true, bool addDefault = false)
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            if (addDefault)
            {
                items.Add(new SelectListItem() { Value = "", Text = "-- Selecione --", Selected = true });
                items.Add(new SelectListItem() { Value = "true", Text = "Sim", Selected = false });
                items.Add(new SelectListItem() { Value = "false", Text = "Não", Selected = false });
            }
            else
            {
                items.Add(new SelectListItem() { Value = "true", Text = "Sim", Selected = filter });
                items.Add(new SelectListItem() { Value = "false", Text = "Não", Selected = !filter });
            }

            return items;
        }
    }
}
