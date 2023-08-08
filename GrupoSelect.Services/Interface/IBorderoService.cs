using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;

namespace GrupoSelect.Services.Interface
{
    public interface IBorderoService
    {
        Task<Result<IEnumerable<Contract>>> GetAll(int userId, DateTime startDate, DateTime endDate);
    }
}
