using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;

namespace GrupoSelect.Services.Service
{
    public class FinancialAdminService : IFinancialAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<FinancialAdmin> _validator;

        public FinancialAdminService(IUnitOfWork unitOfWork, IValidator<FinancialAdmin> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<FinancialAdmin>>> GetAll(FinancialAdmin filter)
        {
            return new Result<IEnumerable<FinancialAdmin>>
            {
                Success = true,
                Object = _unitOfWork.FinancialAdmins.GetAll(f => (string.IsNullOrEmpty(filter.Name) || f.Name.Contains(filter.Name))),
            };
        }

        public async Task<PaginateResult<IEnumerable<FinancialAdmin>>> GetAllPaginate(FinancialAdmin filter, int page, int qtPage)
        {
            return _unitOfWork.FinancialAdmins.GetAllPaginate(f => (string.IsNullOrEmpty(filter.Name) || f.Name.Contains(filter.Name)) && (filter.Active == null || f.Active == filter.Active), null, page, qtPage);
        }

        public async Task<Result<FinancialAdmin>> GetById(int id)
        {
            FinancialAdmin model = _unitOfWork.FinancialAdmins.GetAll(f => f.Id == id).FirstOrDefault();

            if (model != null)
            {
                return new Result<FinancialAdmin>
                {
                    Success = true,
                    Object = model
                };
            }
            else
            {
                return new Result<FinancialAdmin>
                {
                    Success = false,
                    Message = Constants.SYSTEM_ERROR_ID
                };
            }
        }

        public Result<FinancialAdmin> Insert(FinancialAdmin model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_INSERT));

            if (!resultValidation.IsValid)
            {
                return new Result<FinancialAdmin>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            model.Name = model.Name.ToUpper();

            _unitOfWork.FinancialAdmins.Insert(model);
            _unitOfWork.FinancialAdmins.Save();

            return new Result<FinancialAdmin>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<FinancialAdmin> Update(FinancialAdmin model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_UPDATE));

            if (!resultValidation.IsValid)
            {
                return new Result<FinancialAdmin>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            model.Name = model.Name.ToUpper();

            _unitOfWork.FinancialAdmins.Update(model);
            _unitOfWork.FinancialAdmins.Save();

            return new Result<FinancialAdmin>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<FinancialAdmin> Delete(int id)
        {
            FinancialAdmin model = new FinancialAdmin { Id = id };
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_DELETE));

            if (!resultValidation.IsValid)
            {
                return new Result<FinancialAdmin>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            _unitOfWork.FinancialAdmins.Delete(model);
            _unitOfWork.FinancialAdmins.Save();

            return new Result<FinancialAdmin>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }
    }
}