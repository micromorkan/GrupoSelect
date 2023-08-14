namespace GrupoSelect.Domain.Entity
{
    public class LogSystem
    {
        public LogSystem()
        {
            this.Date = DateTime.Now;
        }

        public Guid Id { get; set; }

        public string Action { get; set; }

        public string Object { get; set; }

        public string Username { get; set; }

        public string? OriginalValues { get; set; }

        public string? NewValues { get; set; }

        public DateTime Date { get; set; }
    }
}
