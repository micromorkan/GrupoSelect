using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;

namespace GrupoSelect.Services.Interface
{
    public interface IAccessService
    {
        Task<Result<User>> Authenticate(User user);
    }
}
