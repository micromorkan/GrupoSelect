using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;

namespace GrupoSelect.Services.Service
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ProductType> _validator;

        public ProductTypeService(IUnitOfWork unitOfWork, IValidator<ProductType> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<ProductType>>> GetAll(ProductType filter)
        {
            return new Result<IEnumerable<ProductType>>
            {
                Success = true,
                Object = _unitOfWork.ProductTypes.GetAll(f => (string.IsNullOrEmpty(filter.ProductName) || f.ProductName.Contains(filter.ProductName))),
            };
        }

        public async Task<PaginateResult<IEnumerable<ProductType>>> GetAllPaginate(ProductType filter, int page, int qtPage)
        {
            return _unitOfWork.ProductTypes.GetAllPaginate(f => (string.IsNullOrEmpty(filter.ProductName) || f.ProductName.Contains(filter.ProductName)), null, page, qtPage);
        }

        public async Task<Result<ProductType>> GetById(int id)
        {
            ProductType model = _unitOfWork.ProductTypes.GetAll(f => f.Id == id).FirstOrDefault();

            if (model != null)
            {
                return new Result<ProductType>
                {
                    Success = true,
                    Object = model
                };
            }
            else
            {
                return new Result<ProductType>
                {
                    Success = false,
                    Message = Constants.SYSTEM_ERROR_ID
                };
            }
        }

        public Result<ProductType> Insert(ProductType model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_INSERT));

            if (!resultValidation.IsValid)
            {
                return new Result<ProductType>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            _unitOfWork.ProductTypes.Insert(model);
            _unitOfWork.ProductTypes.Save();

            return new Result<ProductType>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<ProductType> Update(ProductType model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_UPDATE));

            if (!resultValidation.IsValid)
            {
                return new Result<ProductType>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            _unitOfWork.ProductTypes.Update(model);
            _unitOfWork.ProductTypes.Save();

            return new Result<ProductType>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<ProductType> Delete(int id)
        {
            ProductType model = new ProductType { Id = id };
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_DELETE));

            if (!resultValidation.IsValid)
            {
                return new Result<ProductType>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            _unitOfWork.ProductTypes.Delete(model);
            _unitOfWork.ProductTypes.Save();

            return new Result<ProductType>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }
    }
}