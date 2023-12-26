using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Services.Interface
{
    public interface IProposalService
    {
        Result<Proposal> Insert(Proposal model);
        Result<Proposal> Update(Proposal model);
        Result<Proposal> Delete(int id);
        Task<Result<Proposal>> GetById(int id);
        Task<Result<Proposal>> Check(int id, int userId);
        Task<Result<IEnumerable<Proposal>>> GetAll(Proposal filter);
        Task<PaginateResult<IEnumerable<Proposal>>> GetAllPaginate(Proposal filter, int page, int qtPage, DateTime startDate, DateTime endDate, int groupId= 0);
    }
}
