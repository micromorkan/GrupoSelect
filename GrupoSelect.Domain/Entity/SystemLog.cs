﻿namespace GrupoSelect.Domain.Entity
{
    public class SystemLog
    {
        public SystemLog() 
        {
            Date = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Method { get; set; }
        public string Message { get; set; }
        public string Object { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
    }
}
