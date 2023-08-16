using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;

namespace GrupoSelect.Services.Service
{
    public class TableTypeService : IDisposable, ITableTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<TableType> _validator;

        public TableTypeService(IUnitOfWork unitOfWork, IValidator<TableType> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<TableType>>> GetAll(TableType filter)
        {
            return new Result<IEnumerable<TableType>>
            {
                Success = true,
                Object = _unitOfWork.TableTypes.GetAll(f => (string.IsNullOrEmpty(filter.TableTax) || f.TableTax.Contains(filter.TableTax))),
            };
        }

        public async Task<PaginateResult<IEnumerable<TableType>>> GetAllPaginate(TableType filter, int page, int qtPage)
        {
            return _unitOfWork.TableTypes.GetAllPaginate(f => (string.IsNullOrEmpty(filter.TableTax) || f.TableTax.Contains(filter.TableTax)), null, page, qtPage);
        }

        public async Task<Result<TableType>> GetById(int id)
        {
            TableType model = _unitOfWork.TableTypes.GetAll(f => f.Id == id).FirstOrDefault();

            if (model != null)
            {
                return new Result<TableType>
                {
                    Success = true,
                    Object = model
                };
            }
            else
            {
                return new Result<TableType>
                {
                    Success = false,
                    Message = Constants.SYSTEM_ERROR_ID
                };
            }
        }

        public Result<TableType> Insert(TableType model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_INSERT));

            if (!resultValidation.IsValid)
            {
                return new Result<TableType>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            model.TableTax = model.TableTax.ToUpper();

            _unitOfWork.TableTypes.Insert(model);
            _unitOfWork.TableTypes.Save();
            _unitOfWork.Dispose();

            return new Result<TableType>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<TableType> Update(TableType model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_UPDATE));

            if (!resultValidation.IsValid)
            {
                return new Result<TableType>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            model.TableTax = model.TableTax.ToUpper();

            _unitOfWork.TableTypes.Update(model);
            _unitOfWork.TableTypes.Save();
            _unitOfWork.Dispose();

            return new Result<TableType>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<TableType> Delete(int id)
        {
            TableType model = new TableType { Id = id };
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_DELETE));

            if (!resultValidation.IsValid)
            {
                return new Result<TableType>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            _unitOfWork.TableTypes.Delete(model);
            _unitOfWork.TableTypes.Save();
            _unitOfWork.Dispose();

            return new Result<TableType>
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