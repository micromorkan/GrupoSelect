using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace GrupoSelect.Services.Service
{
    public class ProposalService : IProposalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Proposal> _validator;

        public ProposalService(IUnitOfWork unitOfWork, IValidator<Proposal> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<Proposal>>> GetAll(Proposal filter)
        {
            return new Result<IEnumerable<Proposal>>
            {
                Success = true,
                Object = _unitOfWork.Proposals.GetAll(f => (filter.UserId == 0 || f.UserId == filter.UserId) &&
                                                         (filter.ClientId == 0 || f.ClientId == filter.ClientId) &&
                                                         (string.IsNullOrEmpty(filter.ProductTypeName) || f.ProductTypeName == filter.ProductTypeName) &&
                                                         (string.IsNullOrEmpty(filter.TableTypeTax) || f.TableTypeTax == filter.TableTypeTax) &&
                                                         (string.IsNullOrEmpty(filter.FinancialAdminName) || f.FinancialAdminName == filter.FinancialAdminName)),
            };
        }

        public async Task<PaginateResult<IEnumerable<Proposal>>> GetAllPaginate(Proposal filter, int page, int qtPage, DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.Proposals.GetAllPaginate(f => (filter.UserId == 0 || f.UserId == filter.UserId) &&
                                                         (filter.ClientId == 0 || f.ClientId == filter.ClientId) &&
                                                         (string.IsNullOrEmpty(filter.ProductTypeName) || f.ProductTypeName == filter.ProductTypeName) &&
                                                         (string.IsNullOrEmpty(filter.TableTypeTax) || f.TableTypeTax == filter.TableTypeTax) &&
                                                         (string.IsNullOrEmpty(filter.FinancialAdminName) || f.FinancialAdminName == filter.FinancialAdminName) &&
                                                         (filter.UserChecked == null || f.UserChecked == filter.UserChecked) &&
                                                         (filter.Status == null || f.Status == filter.Status) &&
                                                         (f.DateCreate.Date >= startDate.Date && f.DateCreate.Date <= endDate.Date), o => o.OrderByDescending(x => x.DateCreate), page, qtPage, i => i.User, i => i.Client);
        }

        public async Task<Result<Proposal>> GetById(int id)
        {
            Proposal model = _unitOfWork.Proposals.GetAll(f => f.Id == id, null, i => i.User).FirstOrDefault();

            if (model != null)
            {
                return new Result<Proposal>
                {
                    Success = true,
                    Object = model
                };
            }
            else
            {
                return new Result<Proposal>
                {
                    Success = false,
                    Message = Constants.SYSTEM_ERROR_ID
                };
            }
        }

        public Result<Proposal> Insert(Proposal model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_INSERT));

            if (!resultValidation.IsValid)
            {
                return new Result<Proposal>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            model.DateCreate = DateTime.Now;
            model.Aproved = false;

            _unitOfWork.Proposals.Insert(model);
            _unitOfWork.Proposals.Save();

            return new Result<Proposal>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<Proposal> Update(Proposal model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_UPDATE));

            if (!resultValidation.IsValid)
            {
                return new Result<Proposal>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            _unitOfWork.Proposals.Update(model);
            _unitOfWork.Proposals.Save();

            return new Result<Proposal>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<Proposal> Delete(int id)
        {
            Proposal model = new Proposal { Id = id };
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_DELETE));

            if (!resultValidation.IsValid)
            {
                return new Result<Proposal>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            _unitOfWork.Proposals.Delete(model);

            _unitOfWork.Proposals.Save();

            return new Result<Proposal>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public async Task<Result<Proposal>> CheckProposal(int id, int userId)
        {
            Proposal proposal = (await GetById(id)).Object;

            proposal.DateChecked = DateTime.Now;
            proposal.UserChecked = userId;
            proposal.Status = Constants.PROPOSAL_STATUS_PC;

            ContractConfig contractConfig = _unitOfWork.ContractConfigs.GetAll(f => f.Id == 1, null).First();
            contractConfig = Functions.GetNextContractNum(contractConfig);

            Contract contract = new Contract();

            contract.ContractNum = contractConfig.ContractNum;
            contract.ProposalId = proposal.Id;
            contract.Status = Constants.CONTRACT_STATUS_AD;
            contract.DateCreate = DateTime.Now;
            contract.DateStatus = DateTime.Now;

            ContractHistoric contractHistoric = new ContractHistoric();

            contractHistoric.ContractNum = contractConfig.ContractNum;
            contractHistoric.ProposalId = proposal.Id;
            contractHistoric.Status = Constants.CONTRACT_STATUS_AD;
            contractHistoric.DateRegister = contract.DateCreate;
            contractHistoric.UserIdRegister = userId;

            _unitOfWork.Contracts.Insert(contract);
            _unitOfWork.ContractHistorics.Insert(contractHistoric);
            _unitOfWork.ContractConfigs.Update(contractConfig);
            _unitOfWork.Proposals.Update(proposal);
            _unitOfWork.SaveAllChanges();

            return new Result<Proposal>
            {
                Success = true,
                Object = null,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }
    }
}