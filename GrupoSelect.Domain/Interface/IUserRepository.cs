using GrupoSelect.Domain.Entity;

namespace GrupoSelect.Domain.Interface
{
    public interface IUserRepository
    {
        Task<User> Authenticate(User filter);
    }
}
