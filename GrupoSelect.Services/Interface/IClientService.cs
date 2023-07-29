using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Services.Interface
{
    public interface IClientService
    {
        Result<Client> Insert(Client model);
        Result<Client> Update(Client model);
        Result<Client> Delete(int id);
        Task<Result<Client>> GetById(int id);
        Task<Result<IEnumerable<Client>>> GetAll(Client filter);
        Task<PaginateResult<IEnumerable<Client>>> GetAllPaginate(Client filter, int page, int qtPage);
    }
}
