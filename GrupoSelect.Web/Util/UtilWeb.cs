using GrupoSelect.Domain.Entity;
using GrupoSelect.Services.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
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

        public static string GetEnumDescription(Enum value)
        {
            if (value != null)
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
                else
                {
                    return value.ToString();
                }
            }

            return null;
        }
    }

    public class UtilWebEnums
    {
        public enum TipoChart
        {
            [Description("pie")]
            Torta = 1,
            [Description("bar")]
            BarraVertical = 2,
            [Description("horizontalBar")]
            BarraHorizontal = 3,
        }

        public enum CorEvento
        {
            [Description("#ff8080")]
            EventoVencido = 1,
            [Description("#ffff66")]
            EventoAtrasado = 2,
            [Description("#99ff99")]
            EventoDeHoje = 3,
            [Description("#85e0e0")]
            EventoFuturo = 4,
            [Description("#ff9933")]
            EventoFinalizado = 5,
            [Description("#cc99ff")]
            EventoCancelado = 6,
        }

        public enum StatusEvento
        {
            Ativo = 1,
            Finalizado = 2,
            Cancelado = 3,
        }
    }
}
