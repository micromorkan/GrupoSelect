using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;

namespace GrupoSelect.Services.Service
{
    public class ContractService : IContractService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Contract> _validator;

        public ContractService(IUnitOfWork unitOfWork, IValidator<Contract> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<Contract>>> GetAll(Contract filter)
        {
            return new Result<IEnumerable<Contract>>
            {
                Success = true,
                Object = _unitOfWork.Contracts.GetAll(f => (filter.Proposal.UserId == 0 || f.Proposal.UserId == filter.Proposal.UserId) &&
                                                         (filter.Proposal.ClientId == 0 || f.Proposal.ClientId == filter.Proposal.ClientId) &&
                                                         (string.IsNullOrEmpty(filter.Status) || f.Status == filter.Status)),
            };
        }

        public async Task<PaginateResult<IEnumerable<Contract>>> GetAllPaginate(Contract filter, int page, int qtPage, DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.Contracts.GetAllPaginate(f => (filter.Proposal.UserId == 0 || f.Proposal.UserId == filter.Proposal.UserId) &&
                                                         (filter.Proposal.ClientId == 0 || f.Proposal.ClientId == filter.Proposal.ClientId) &&
                                                         (string.IsNullOrEmpty(filter.Status) || f.Status == filter.Status) &&
                                                         (string.IsNullOrEmpty(filter.ContractNum) || f.ContractNum == filter.ContractNum) &&
                                                         (f.DateCreate.Date >= startDate.Date && f.DateCreate.Date <= endDate.Date), o => o.OrderByDescending(x => x.DateCreate), page, qtPage, i => i.Proposal.Client, i => i.Proposal.User);
        }

        public async Task<Result<Contract>> GetById(int id)
        {
            Contract model = _unitOfWork.Contracts.GetAll(f => f.Id == id, null, i => i.Proposal.User, i => i.Proposal.Client).FirstOrDefault();

            if (model != null)
            {
                return new Result<Contract>
                {
                    Success = true,
                    Object = model
                };
            }
            else
            {
                return new Result<Contract>
                {
                    Success = false,
                    Message = Constants.SYSTEM_ERROR_ID
                };
            }
        }

        public Result<Contract> Insert(Contract model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_INSERT));

            if (!resultValidation.IsValid)
            {
                return new Result<Contract>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            model.DateCreate = DateTime.Now;
            model.Status = Constants.CONTRACT_STATUS_AD;

            _unitOfWork.Contracts.Insert(model);
            _unitOfWork.Contracts.Save();

            return new Result<Contract>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<Contract> Update(Contract model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_UPDATE));

            if (!resultValidation.IsValid)
            {
                return new Result<Contract>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            _unitOfWork.Contracts.Update(model);
            _unitOfWork.Contracts.Save();

            return new Result<Contract>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }
    }
}