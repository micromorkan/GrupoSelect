using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Services.Interface
{
    public interface ICreditService
    {
        Result<Credit> Insert(Credit model);
        Result<Credit> Update(Credit model);
        Result<Credit> Delete(int id);
        Task<Result<Credit>> GetById(int id);
        Task<Result<IEnumerable<Credit>>> GetAll(Credit filter);
        Task<PaginateResult<IEnumerable<Credit>>> GetAllPaginate(Credit filter, int page, int qtPage);
    }
}
