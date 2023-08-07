using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Domain.Entity
{
    public class Client
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Sex { get; set; }
        public string DateBirth { get; set; }
        public string NaturalFrom { get; set; }
        public string Nationality { get; set; }
        public string MaritalStatus { get; set; }
        public string DateExp { get; set; }
        public string OrganExp { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Profession { get; set; }
        public string Income { get; set; }
        public string Address { get; set; }
        public string Neighborhood { get; set; }
        public string? Complement { get; set; }
        public string Cep { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public virtual User User { get; set; }
    }
}
