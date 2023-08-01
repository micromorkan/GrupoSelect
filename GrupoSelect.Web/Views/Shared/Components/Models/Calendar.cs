namespace GrupoSelect.Web.Views.Shared.Components.Models
{
    public class Calendar
    {
        public Calendar()
        {
            Eventos = new List<CalendarEvent>();
            Acoes = new CalendarActions();
        }

        /// <summary>
        /// Id do usuário caso ele faça exclusão ou conclusão de eventos.
        /// </summary>
        public long IdUsuario { get; set; }

        /// <summary>
        /// Nome do usuario que será mostrado na tela.
        /// </summary>
        public string NomeUsuario { get; set; }

        /// <summary>
        /// Lista de eventos que serão carregados no calendário.
        /// </summary>
        public List<CalendarEvent> Eventos { get; set; }

        /// <summary>
        /// Lista dos endereços das ações do calendário
        /// </summary>
        public CalendarActions Acoes { get; set; }
    }
}
