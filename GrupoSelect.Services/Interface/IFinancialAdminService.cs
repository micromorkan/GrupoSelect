using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Services.Interface
{
    public interface IFinancialAdminService
    {
        Result<FinancialAdmin> Insert(FinancialAdmin model);
        Result<FinancialAdmin> Update(FinancialAdmin model);
        Result<FinancialAdmin> Delete(int id);
        Task<Result<FinancialAdmin>> GetById(int id);
        Task<Result<IEnumerable<FinancialAdmin>>> GetAll(FinancialAdmin filter);
        Task<PaginateResult<IEnumerable<FinancialAdmin>>> GetAllPaginate(FinancialAdmin filter, int page, int qtPage);
    }
}
