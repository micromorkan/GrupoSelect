using GrupoSelect.Domain.Entity;
using GrupoSelect.Services.Model;

namespace GrupoSelect.Services.Interface
{
    public interface IAccessService
    {
        Task<Result<User>> Authenticate(User user);
    }
}
