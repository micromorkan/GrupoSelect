using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Domain.Entity
{
    public class GroupManager
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int ManagerId { get; set; }
        public Group Group  { get; set; }
        public User Manager { get; set; }
    }
}
