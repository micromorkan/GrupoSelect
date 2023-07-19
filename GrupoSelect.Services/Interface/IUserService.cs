using GrupoSelect.Domain.Entity;
using GrupoSelect.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Services.Interface
{
    public interface IUserService
    {
        Result<User> Insert(User user);
        Result<User> Update(User user);
        Result<User> Delete(int id);
        Task<Result<User>> GetById(int id);
        Task<Result<IEnumerable<User>>> GetAll(User filter);
    }
}
