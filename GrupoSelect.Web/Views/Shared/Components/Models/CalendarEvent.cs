using GrupoSelect.Web.Util;

namespace Web.Views.Shared.Componentes.ComponentModels
{
    public class CalendarEvent
    {
        /// <summary>
        /// Id do evento. Único.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Título do Evento
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Descrição do evento.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Data do início do evento.
        /// </summary>
        public string DataIniEvento { get; set; }

        /// <summary>
        /// Data do fim do evento.
        /// </summary>
        public string DataFimEvento { get; set; }

        /// <summary>
        /// Boolean que identifica se o evento dua o dia inteiro.
        /// </summary>
        public bool DiaInteiro { get; set; }

        /// <summary>
        /// Cor do evento. Obter a cor correspondente ao evento no enum Util.WebUtil.CorEvento
        /// </summary>
        public string BackgroundColor {
            get
            {
                DateTime dataInicio = DateTime.Parse(DataIniEvento);

                switch (Status)  
                {
                    case 1:
                        if (dataInicio < DateTime.Now.AddDays(-7))
                            return UtilWeb.GetEnumDescription(UtilWebEnums.CorEvento.EventoVencido);
                        else if (dataInicio < DateTime.Now.Date)
                            return UtilWeb.GetEnumDescription(UtilWebEnums.CorEvento.EventoAtrasado);
                        else if (dataInicio.Date == DateTime.Now.Date)
                            return UtilWeb.GetEnumDescription(UtilWebEnums.CorEvento.EventoDeHoje);
                        else if (dataInicio.Date >= DateTime.Now.AddDays(1).Date)
                            return UtilWeb.GetEnumDescription(UtilWebEnums.CorEvento.EventoFuturo);
                        else
                            return "#FFF";
                    case 2:
                        return UtilWeb.GetEnumDescription(UtilWebEnums.CorEvento.EventoFinalizado);
                    case 3:
                        return UtilWeb.GetEnumDescription(UtilWebEnums.CorEvento.EventoCancelado);
                    default:
                        return "#FFF";
                }                           
            }
            private set { BackgroundColor = value; } }

        /// <summary>
        /// Status do evento. Enum Util.WebUtil.StatusEvento. 1 = Ativo | 2 = Finalizado | 3 = Cancelado
        /// </summary>
        public int Status { get; set; }
    }
}
