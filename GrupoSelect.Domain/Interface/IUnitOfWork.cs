using GrupoSelect.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> Users { get; }
        void SaveAllChanges();
    }
}
