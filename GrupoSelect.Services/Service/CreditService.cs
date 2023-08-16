using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;

namespace GrupoSelect.Services.Service
{
    public class CreditService : IDisposable, ICreditService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Credit> _validator;

        public CreditService(IUnitOfWork unitOfWork, IValidator<Credit> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<Credit>>> GetAll(Credit filter)
        {
            return new Result<IEnumerable<Credit>>
            {
                Success = true,
                Object = _unitOfWork.Credits.GetAll(f => ((filter.ProductTypeId == 0) || f.ProductTypeId == filter.ProductTypeId) &&
                                                         ((filter.TableTypeId == 0) || f.TableTypeId == filter.TableTypeId) &&
                                                         ((filter.FinancialAdminId == 0) || f.FinancialAdminId == filter.FinancialAdminId), null, i => i.ProductType, i => i.FinancialAdmin, i => i.TableType),
            };
        }

        public async Task<PaginateResult<IEnumerable<Credit>>> GetAllPaginate(Credit filter, int page, int qtPage)
        {
            return _unitOfWork.Credits.GetAllPaginate(f => ((filter.ProductTypeId == 0) || f.ProductTypeId == filter.ProductTypeId) &&
                                                           ((filter.TableTypeId == 0) || f.TableTypeId == filter.TableTypeId) &&
                                                           ((filter.FinancialAdminId == 0) || f.FinancialAdminId == filter.FinancialAdminId), null, page, qtPage, i => i.ProductType, i => i.FinancialAdmin, i => i.TableType);
        }

        public async Task<Result<Credit>> GetById(int id)
        {
            Credit model = _unitOfWork.Credits.GetAll(f => f.Id == id, null, i => i.TableType, i => i.FinancialAdmin, i => i.ProductType).FirstOrDefault();

            if (model != null)
            {
                return new Result<Credit>
                {
                    Success = true,
                    Object = model
                };
            }
            else
            {
                return new Result<Credit>
                {
                    Success = false,
                    Message = Constants.SYSTEM_ERROR_ID
                };
            }
        }

        public Result<Credit> Insert(Credit model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_INSERT));

            if (!resultValidation.IsValid)
            {
                return new Result<Credit>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            model.DateCreate = DateTime.Now;

            _unitOfWork.Credits.Insert(model);
            _unitOfWork.Credits.Save();
            _unitOfWork.Dispose();

            return new Result<Credit>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<Credit> Update(Credit model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_UPDATE));

            if (!resultValidation.IsValid)
            {
                return new Result<Credit>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            model.DateUpdate = DateTime.Now;

            _unitOfWork.Credits.Update(model);
            _unitOfWork.Credits.Save();
            _unitOfWork.Dispose();

            return new Result<Credit>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<Credit> Delete(int id)
        {
            Credit model = new Credit { Id = id };
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_DELETE));

            if (!resultValidation.IsValid)
            {
                return new Result<Credit>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            _unitOfWork.Credits.Delete(model);
            _unitOfWork.Credits.Save();
            _unitOfWork.Dispose();

            return new Result<Credit>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}