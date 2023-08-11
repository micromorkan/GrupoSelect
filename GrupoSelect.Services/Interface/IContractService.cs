using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Services.Interface
{
    public interface IContractService
    {
        Result<Contract> Update(Contract model);
        Result<Contract> Check(Contract model, int userId);
        Result<Contract> Cancel(int id, int userId, string reason);
        Task<Result<Contract>> GetById(int id);
        Task<Result<IEnumerable<Contract>>> GetAll(Contract filter);
        Task<PaginateResult<IEnumerable<Contract>>> GetAllPaginate(Contract filter, int page, int qtPage, DateTime startDate, DateTime endDate);
    }
}
