using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Services.Interface
{
    public interface ITableTypeService
    {
        Result<TableType> Insert(TableType model);
        Result<TableType> Update(TableType model);
        Result<TableType> Delete(int id);
        Task<Result<TableType>> GetById(int id);
        Task<Result<IEnumerable<TableType>>> GetAll(TableType filter);
        Task<PaginateResult<IEnumerable<TableType>>> GetAllPaginate(TableType filter, int page, int qtPage);
    }
}
