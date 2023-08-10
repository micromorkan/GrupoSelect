using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using System.Reflection;

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
                                                         (string.IsNullOrEmpty(filter.ContractNum) || f.ContractNum.Contains(filter.ContractNum)) &&
                                                         (f.DateCreate.Date >= startDate.Date && f.DateCreate.Date <= endDate.Date), o => o.OrderByDescending(x => x.DateStatus), page, qtPage, i => i.Proposal.Client, i => i.Proposal.User);
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

            model.Status = Constants.CONTRACT_STATUS_PA;
            model.DateStatus = DateTime.Now;

            ContractHistoric contractHistoric = new ContractHistoric();

            contractHistoric.ContractNum = model.ContractNum;
            contractHistoric.ProposalId = model.ProposalId;
            contractHistoric.Status = model.Status;
            contractHistoric.DateRegister = (DateTime)model.DateStatus;
            contractHistoric.UserIdRegister = model.Proposal.UserId;
            contractHistoric.ReprovedReason = null;
            contractHistoric.ReprovedExplain = null;

            _unitOfWork.Contracts.Update(model);
            _unitOfWork.ContractHistorics.Insert(contractHistoric);
            _unitOfWork.SaveAllChanges();

            return new Result<Contract>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<Contract> Check(Contract model, int userId)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_CHECK));

            if (!resultValidation.IsValid)
            {
                return new Result<Contract>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            Contract contract = _unitOfWork.Contracts.GetAll(f => f.Id == model.Id, null, i => i.Proposal.User).First();

            contract.Status = model.Status;

            ContractHistoric contractHistoric = new ContractHistoric();

            if (model.Status == Constants.CONTRACT_STATUS_CA)
            {
                contract.UserIdAproved = userId;
                contract.DateAproved = DateTime.Now;
                contract.DateStatus = DateTime.Now;
                contract.ReprovedReason = null;
                contract.ReprovedExplain = null;
                contract.Proposal.Status = Constants.PROPOSAL_STATUS_PF;
                contract.Proposal.Aproved = true;

                if (!contract.Proposal.User.BranchWithoutAdm)
                {
                    contract.VideoAgree = model.VideoAgree;
                    contract.VideoAgreeFileType = model.VideoAgreeFileType;
                }
            }
            else if (model.Status == Constants.CONTRACT_STATUS_CR)
            {
                contract.DateStatus = DateTime.Now;
                contract.ReprovedReason = model.ReprovedReason;
                contract.ReprovedExplain = model.ReprovedExplain?.ToUpper();
                contractHistoric.ReprovedReason = model.ReprovedReason;
                contractHistoric.ReprovedExplain = model.ReprovedExplain?.ToUpper();
            }

            contractHistoric.ContractNum = contract.ContractNum;
            contractHistoric.ProposalId = contract.ProposalId;
            contractHistoric.Status = model.Status;
            contractHistoric.DateRegister = (DateTime)contract.DateStatus;
            contractHistoric.UserIdRegister = userId;

            _unitOfWork.Contracts.Update(contract);
            if (model.Status == Constants.CONTRACT_STATUS_CA)
            {
                _unitOfWork.Proposals.Update(contract.Proposal);
            }
            _unitOfWork.ContractHistorics.Insert(contractHistoric);
            _unitOfWork.SaveAllChanges();

            return new Result<Contract>
            {
                Success = true,
                Object = contract,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<Contract> Cancel(int id, int userId)
        {
            var resultValidation = _validator.Validate(new Contract { Id = id }, options => options.IncludeRuleSets(Constants.FLUENT_CANCEL));

            if (!resultValidation.IsValid)
            {
                return new Result<Contract>
                {
                    Success = false,
                    Object = null,
                    Errors = resultValidation.Errors
                };
            }

            Contract contract = _unitOfWork.Contracts.GetAll(f => f.Id == id, null, i => i.Proposal).First();

            contract.Status = Constants.CONTRACT_STATUS_CC;
            contract.DateStatus = DateTime.Now;
            contract.Proposal.Status = Constants.PROPOSAL_STATUS_CA;

            ContractHistoric contractHistoric = new ContractHistoric();

            contractHistoric.ContractNum = contract.ContractNum;
            contractHistoric.ProposalId = contract.ProposalId;
            contractHistoric.Status = Constants.CONTRACT_STATUS_CC;
            contractHistoric.DateRegister = (DateTime)contract.DateStatus;
            contractHistoric.UserIdRegister = userId;

            _unitOfWork.Contracts.Update(contract);
            _unitOfWork.Proposals.Update(contract.Proposal);
            _unitOfWork.ContractHistorics.Insert(contractHistoric);
            _unitOfWork.SaveAllChanges();

            return new Result<Contract>
            {
                Success = true,
                Object = contract,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }
    }
}