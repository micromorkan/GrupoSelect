using GrupoSelect.Domain.Entity;
using GrupoSelect.Services.Model;

namespace GrupoSelect.Services.Interface
{
    public interface IProfileService
    {
        Task<Result<IEnumerable<Profile>>> GetAll(Profile filter);
        Task<Result<Profile>> GetById(int id);
    }
}
