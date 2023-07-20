using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Model;

namespace GrupoSelect.Services.Interface
{
    public interface IAccessService
    {
        Task<Result<User>> Authenticate(User user);
    }
}
