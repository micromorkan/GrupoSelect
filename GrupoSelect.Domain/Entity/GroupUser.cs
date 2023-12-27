using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GrupoSelect.Domain.Entity
{
    public class GroupUser
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public virtual Group Group { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual User User { get; set; }
    }
}
