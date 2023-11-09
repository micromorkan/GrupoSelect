using GrupoSelect.Data.Context;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GrupoSelect.Data.Repository
{
    public class ContractRepository : IDisposable, IContractRepository
    {
        private readonly GSDbContext _dbContext;

        public ContractRepository(GSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Contract>> GetAll(Expression<Func<Contract, bool>>? filter)
        {

            return await _dbContext.Contracts.Where(filter)
                                             .OrderByDescending(x => x.DateStatus)
                                             .Select(x => new Contract
                                             {
                                                 Id = x.Id,
                                                 ProposalId = x.ProposalId,
                                                 ContractNum = x.ContractNum,
                                                 Status = x.Status,
                                                 ReprovedReason = x.ReprovedReason,
                                                 ReprovedExplain = x.ReprovedExplain,
                                                 DateCreate = x.DateCreate,
                                                 DateStatus = x.DateStatus,
                                                 DateAproved = x.DateAproved,
                                                 UserIdAproved = x.UserIdAproved,
                                                 Proposal = x.Proposal
                                             }).AsNoTracking().ToListAsync();
        }

        public async Task<PaginateResult<IEnumerable<Contract>>> GetAllPaginate(Expression<Func<Contract, bool>>? filter, int page, int qtPage)
        {
            PaginateResult<IEnumerable<Contract>> result = new PaginateResult<IEnumerable<Contract>>();

            result.Page = page;
            result.Success = true;

            result.Total = (await _dbContext.Contracts.Where(filter)
                                             .OrderByDescending(x => x.DateStatus)
                                             .Select(x => new Contract
                                             {
                                                 Id = x.Id,
                                             }).AsNoTracking().ToListAsync()).Count();

            result.Object = await _dbContext.Contracts.Include(x => x.Proposal.Client)
                                             .Include(x => x.Proposal.User)
                                             .Where(filter)
                                             .OrderByDescending(x => x.DateStatus)
                                             .Select(x => new Contract
                                             {
                                                 Id = x.Id,
                                                 ProposalId = x.ProposalId,
                                                 ContractNum = x.ContractNum,
                                                 Status = x.Status,
                                                 ReprovedReason = x.ReprovedReason,
                                                 ReprovedExplain = x.ReprovedExplain,
                                                 DateCreate = x.DateCreate,
                                                 DateStatus = x.DateStatus,
                                                 DateAproved = x.DateAproved,
                                                 UserIdAproved = x.UserIdAproved,
                                                 Proposal = x.Proposal
                                             }).Skip(qtPage * (page - 1)).Take(qtPage).AsNoTracking().ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Contract>> GetAllBordero(int userId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Contracts.Include(x => x.Proposal.Client)
                                             .Include(x => x.Proposal.User)
                                             .Where(f => f.Proposal.UserId == userId &&
                                                           f.Status == Constants.CONTRACT_STATUS_CA &&
                                                           f.DateAproved >= startDate &&
                                                           f.DateAproved <= endDate)
                                             .OrderBy(x => x.ContractNum)
                                             .Select(x => new Contract
                                             {
                                                 Id = x.Id,
                                                 ProposalId = x.ProposalId,
                                                 ContractNum = x.ContractNum,
                                                 Status = x.Status,
                                                 ReprovedReason = x.ReprovedReason,
                                                 ReprovedExplain = x.ReprovedExplain,
                                                 DateCreate = x.DateCreate,
                                                 DateStatus = x.DateStatus,
                                                 DateAproved = x.DateAproved,
                                                 UserIdAproved = x.UserIdAproved,
                                                 Proposal = x.Proposal
                                             }).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetAllBorderoManager(int userId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Contracts.Include(x => x.Proposal.Client)
                                             .Include(x => x.Proposal.User)
                                             .Where(f => (f.Proposal.User.Profile == Constants.PROFILE_GERENTE || f.Proposal.User.Profile == Constants.PROFILE_REPRESENTANTE) &&
                                                           f.Status == Constants.CONTRACT_STATUS_CA &&
                                                           f.DateAproved >= startDate &&
                                                           f.DateAproved <= endDate)
                                             .OrderBy(x => x.ContractNum)
                                             .Select(x => new Contract
                                             {
                                                 Id = x.Id,
                                                 ProposalId = x.ProposalId,
                                                 ContractNum = x.ContractNum,
                                                 Status = x.Status,
                                                 ReprovedReason = x.ReprovedReason,
                                                 ReprovedExplain = x.ReprovedExplain,
                                                 DateCreate = x.DateCreate,
                                                 DateStatus = x.DateStatus,
                                                 DateAproved = x.DateAproved,
                                                 UserIdAproved = x.UserIdAproved,
                                                 Proposal = x.Proposal
                                             }).AsNoTracking().ToListAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

