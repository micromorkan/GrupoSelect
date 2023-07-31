﻿using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;

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

        public async Task<PaginateResult<IEnumerable<Proposal>>> GetAllPaginate(Proposal filter, int page, int qtPage)
        {
            return _unitOfWork.Proposals.GetAllPaginate(f => (filter.UserId == 0 || f.UserId == filter.UserId) &&
                                                         (filter.ClientId == 0 || f.ClientId == filter.ClientId) &&
                                                         (string.IsNullOrEmpty(filter.ProductTypeName) || f.ProductTypeName == filter.ProductTypeName) &&
                                                         (string.IsNullOrEmpty(filter.TableTypeTax) || f.TableTypeTax == filter.TableTypeTax) &&
                                                         (string.IsNullOrEmpty(filter.FinancialAdminName) || f.FinancialAdminName == filter.FinancialAdminName), null, page, qtPage);
        }

        public async Task<Result<Proposal>> GetById(int id)
        {
            Proposal model = _unitOfWork.Proposals.GetAll(f => f.Id == id).FirstOrDefault();

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
    }
}