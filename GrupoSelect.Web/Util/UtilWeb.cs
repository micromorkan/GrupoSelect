using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GrupoSelect.Web.Util
{
    public class UtilWeb
    {
        public static IEnumerable<SelectListItem> GetBooleanList(bool filter = true, bool addDefault = false, string defaultText = "-- Selecione --")
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            if (addDefault)
            {
                items.Add(new SelectListItem() { Value = "", Text = defaultText, Selected = true });
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

        public static IEnumerable<SelectListItem> GetProposalStatusList(string filter, string defaultText = "-- Selecione --")
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Value = "", Text = defaultText, Selected = (string.IsNullOrEmpty(filter) ? true : false) });
            items.Add(new SelectListItem() { Value = Constants.PROPOSAL_STATUS_AC, Text = Constants.PROPOSAL_STATUS_AC, Selected = (filter == Constants.PROPOSAL_STATUS_AC ? true : false) });
            items.Add(new SelectListItem() { Value = Constants.PROPOSAL_STATUS_PC, Text = Constants.PROPOSAL_STATUS_PC, Selected = (filter == Constants.PROPOSAL_STATUS_PC ? true : false) });
            items.Add(new SelectListItem() { Value = Constants.PROPOSAL_STATUS_PF, Text = Constants.PROPOSAL_STATUS_PF, Selected = (filter == Constants.PROPOSAL_STATUS_PF ? true : false) });
            items.Add(new SelectListItem() { Value = Constants.PROPOSAL_STATUS_CA, Text = Constants.PROPOSAL_STATUS_CA, Selected = (filter == Constants.PROPOSAL_STATUS_CA ? true : false) });


            return items;
        }

        public static IEnumerable<SelectListItem> GetContractStatusList(string filter, bool checkContract = false, string defaultText = "-- Selecione --")
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Value = "", Text = defaultText, Selected = (string.IsNullOrEmpty(filter) ? true : false) });
            
            if (!checkContract)
            {
                items.Add(new SelectListItem() { Value = Constants.CONTRACT_STATUS_AD, Text = Constants.CONTRACT_STATUS_AD, Selected = (filter == Constants.CONTRACT_STATUS_AD ? true : false) });
                items.Add(new SelectListItem() { Value = Constants.CONTRACT_STATUS_PA, Text = Constants.CONTRACT_STATUS_PA, Selected = (filter == Constants.CONTRACT_STATUS_PA ? true : false) });
                items.Add(new SelectListItem() { Value = Constants.CONTRACT_STATUS_CA, Text = Constants.CONTRACT_STATUS_CA, Selected = (filter == Constants.CONTRACT_STATUS_CA ? true : false) });
                items.Add(new SelectListItem() { Value = Constants.CONTRACT_STATUS_CR, Text = Constants.CONTRACT_STATUS_CR, Selected = (filter == Constants.CONTRACT_STATUS_CR ? true : false) });
                items.Add(new SelectListItem() { Value = Constants.CONTRACT_STATUS_CC, Text = Constants.CONTRACT_STATUS_CC, Selected = (filter == Constants.CONTRACT_STATUS_CC ? true : false) });
            }
            else
            {
                items.Add(new SelectListItem() { Value = Constants.CONTRACT_STATUS_CA, Text = Constants.CONTRACT_STATUS_CA, Selected = (filter == Constants.CONTRACT_STATUS_CA ? true : false) });
                items.Add(new SelectListItem() { Value = Constants.CONTRACT_STATUS_CR, Text = Constants.CONTRACT_STATUS_CR, Selected = (filter == Constants.CONTRACT_STATUS_CR ? true : false) });
            }

            return items;
        }

        public static IEnumerable<SelectListItem> GetContractReprovalList(string filter = null, bool showVideoOpt = true)
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Value = "", Text = "-- Selecione --", Selected = (string.IsNullOrEmpty(filter) ? true : false) });
            items.Add(new SelectListItem() { Value = Constants.CONTRACT_REPROVAL_R1, Text = Constants.CONTRACT_REPROVAL_R1, Selected = (filter == Constants.CONTRACT_REPROVAL_R1 ? true : false) });
            items.Add(new SelectListItem() { Value = Constants.CONTRACT_REPROVAL_R2, Text = Constants.CONTRACT_REPROVAL_R2, Selected = (filter == Constants.CONTRACT_REPROVAL_R2 ? true : false) });
            items.Add(new SelectListItem() { Value = Constants.CONTRACT_REPROVAL_R3, Text = Constants.CONTRACT_REPROVAL_R3, Selected = (filter == Constants.CONTRACT_REPROVAL_R3 ? true : false) });
            items.Add(new SelectListItem() { Value = Constants.CONTRACT_REPROVAL_R4, Text = Constants.CONTRACT_REPROVAL_R4, Selected = (filter == Constants.CONTRACT_REPROVAL_R4 ? true : false) });

            if (showVideoOpt)
            {
                items.Add(new SelectListItem() { Value = Constants.CONTRACT_REPROVAL_R5, Text = Constants.CONTRACT_REPROVAL_R5, Selected = (filter == Constants.CONTRACT_REPROVAL_R5 ? true : false) });
            }

            return items;
        }

        public static IEnumerable<SelectListItem> GetStateList(string filter = null)
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Value = "", Text = "-- Selecione --", Selected = (string.IsNullOrEmpty(filter) ? true : false) });
            items.Add(new SelectListItem() { Value = "AC", Text = "AC", Selected = (filter == "AC" ? true : false) });
            items.Add(new SelectListItem() { Value = "AL", Text = "AL", Selected = (filter == "AL" ? true : false) });
            items.Add(new SelectListItem() { Value = "AP", Text = "AP", Selected = (filter == "AP" ? true : false) });
            items.Add(new SelectListItem() { Value = "AM", Text = "AM", Selected = (filter == "AM" ? true : false) });
            items.Add(new SelectListItem() { Value = "BA", Text = "BA", Selected = (filter == "BA" ? true : false) });
            items.Add(new SelectListItem() { Value = "CE", Text = "CE", Selected = (filter == "CE" ? true : false) });
            items.Add(new SelectListItem() { Value = "DF", Text = "DF", Selected = (filter == "DF" ? true : false) });
            items.Add(new SelectListItem() { Value = "ES", Text = "ES", Selected = (filter == "ES" ? true : false) });
            items.Add(new SelectListItem() { Value = "GO", Text = "GO", Selected = (filter == "GO" ? true : false) });
            items.Add(new SelectListItem() { Value = "MA", Text = "MA", Selected = (filter == "MA" ? true : false) });
            items.Add(new SelectListItem() { Value = "MT", Text = "MT", Selected = (filter == "MT" ? true : false) });
            items.Add(new SelectListItem() { Value = "MS", Text = "MS", Selected = (filter == "MS" ? true : false) });
            items.Add(new SelectListItem() { Value = "MG", Text = "MG", Selected = (filter == "MG" ? true : false) });
            items.Add(new SelectListItem() { Value = "PA", Text = "PA", Selected = (filter == "PA" ? true : false) });
            items.Add(new SelectListItem() { Value = "PB", Text = "PB", Selected = (filter == "PB" ? true : false) });
            items.Add(new SelectListItem() { Value = "PR", Text = "PR", Selected = (filter == "PR" ? true : false) });
            items.Add(new SelectListItem() { Value = "PE", Text = "PE", Selected = (filter == "PE" ? true : false) });
            items.Add(new SelectListItem() { Value = "PI", Text = "PI", Selected = (filter == "PI" ? true : false) });
            items.Add(new SelectListItem() { Value = "RJ", Text = "RJ", Selected = (filter == "RJ" ? true : false) });
            items.Add(new SelectListItem() { Value = "RN", Text = "RN", Selected = (filter == "RN" ? true : false) });
            items.Add(new SelectListItem() { Value = "RS", Text = "RS", Selected = (filter == "RS" ? true : false) });
            items.Add(new SelectListItem() { Value = "RO", Text = "RO", Selected = (filter == "RO" ? true : false) });
            items.Add(new SelectListItem() { Value = "RR", Text = "RR", Selected = (filter == "RR" ? true : false) });
            items.Add(new SelectListItem() { Value = "SC", Text = "SC", Selected = (filter == "SC" ? true : false) });
            items.Add(new SelectListItem() { Value = "SP", Text = "SP", Selected = (filter == "SP" ? true : false) });
            items.Add(new SelectListItem() { Value = "SE", Text = "SE", Selected = (filter == "SE" ? true : false) });
            items.Add(new SelectListItem() { Value = "TO", Text = "TO", Selected = (filter == "TO" ? true : false) });

            return items;
        }

        public static IEnumerable<SelectListItem> GetMaritalStatusList(string filter = null)
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Value = "", Text = "-- Selecione --", Selected = (string.IsNullOrEmpty(filter) ? true : false) });
            items.Add(new SelectListItem() { Value = "CASADO", Text = "CASADO", Selected = (filter == "CASADO" ? true : false) });
            items.Add(new SelectListItem() { Value = "DIVORCIADO", Text = "DIVORCIADO", Selected = (filter == "DIVORCIADO" ? true : false) });
            items.Add(new SelectListItem() { Value = "SEPARADO", Text = "SEPARADO", Selected = (filter == "SEPARADO" ? true : false) });
            items.Add(new SelectListItem() { Value = "SOLTEIRO", Text = "SOLTEIRO", Selected = (filter == "SOLTEIRO" ? true : false) });
            items.Add(new SelectListItem() { Value = "VIÚVO", Text = "VIÚVO", Selected = (filter == "VIÚVO" ? true : false) });

            return items;
        }

        public static IEnumerable<SelectListItem> GetSexList(string filter = null)
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem() { Value = "", Text = "-- Selecione --", Selected = (string.IsNullOrEmpty(filter) ? true : false) });
            items.Add(new SelectListItem() { Value = "MASCULINO", Text = "MASCULINO", Selected = (filter == "MASCULINO" ? true : false) });
            items.Add(new SelectListItem() { Value = "FEMININO", Text = "FEMININO", Selected = (filter == "FEMININO" ? true : false) });
            items.Add(new SelectListItem() { Value = "PREFIRO NÃO INFORMAR", Text = "PREFIRO NÃO INFORMAR", Selected = (filter == "PREFIRO NÃO INFORMAR" ? true : false) });

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
            [Description("line")]
            Linha = 4,
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
