using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Services.Interface
{
    public interface IProductTypeService
    {
        Result<ProductType> Insert(ProductType model);
        Result<ProductType> Update(ProductType model);
        Result<ProductType> Delete(int id);
        Task<Result<ProductType>> GetById(int id);
        Task<Result<IEnumerable<ProductType>>> GetAll(ProductType filter);
        Task<PaginateResult<IEnumerable<ProductType>>> GetAllPaginate(ProductType filter, int page, int qtPage);
    }
}
