using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using System.Linq.Expressions;

namespace GrupoSelect.Domain.Interface
{
    public interface IContractRepository
    {
        Task<IEnumerable<Contract>> GetAll(Expression<Func<Contract, bool>>? filter);
        Task<PaginateResult<IEnumerable<Contract>>> GetAllPaginate(Expression<Func<Contract, bool>>? filter, int page, int qtPage);
        Task<IEnumerable<Contract>> GetAllBordero(int userId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Contract>> GetAllBorderoManager(int userId, DateTime startDate, DateTime endDate);
    }
}
