using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;

namespace GrupoSelect.Services.Service
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Client> _validator;

        public ClientService(IUnitOfWork unitOfWork, IValidator<Client> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<Client>>> GetAll(Client filter)
        {
            return new Result<IEnumerable<Client>>
            {
                Success = true,
                Object = _unitOfWork.Clients.GetAll(f => (string.IsNullOrEmpty(filter.Name) || f.Name.Contains(filter.Name)) &&
                                                         (string.IsNullOrEmpty(filter.CPF) || f.CPF == filter.CPF)),
            };
        }

        public async Task<PaginateResult<IEnumerable<Client>>> GetAllPaginate(Client filter, int page, int qtPage)
        {
            return _unitOfWork.Clients.GetAllPaginate(f => (string.IsNullOrEmpty(filter.Name) || f.Name.Contains(filter.Name)) &&
                                                         (string.IsNullOrEmpty(filter.CPF) || f.CPF == filter.CPF), null, page, qtPage);
        }

        public async Task<Result<Client>> GetById(int id)
        {
            Client model = _unitOfWork.Clients.GetAll(f => f.Id == id).FirstOrDefault();

            if (model != null)
            {
                return new Result<Client>
                {
                    Success = true,
                    Object = model
                };
            }
            else
            {
                return new Result<Client>
                {
                    Success = false,
                    Message = Constants.SYSTEM_ERROR_ID
                };
            }
        }

        public Result<Client> Insert(Client model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_INSERT));

            if (!resultValidation.IsValid)
            {
                return new Result<Client>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            model.DateCreate = DateTime.Now;
            model.Name = model.Name.ToUpper();
            model.NaturalFrom = model.NaturalFrom.ToUpper();
            model.Nationality = model.Nationality.ToUpper();
            model.OrganExp = model.OrganExp.ToUpper();
            model.Email = model.Email.ToUpper();
            model.Profession = model.Profession.ToUpper();
            model.Address = model.Address.ToUpper();
            model.Neighborhood = model.Neighborhood.ToUpper();
            model.Complement = model.Complement?.ToUpper();
            model.City = model.City.ToUpper();
            model.State = model.State.ToUpper();

            _unitOfWork.Clients.Insert(model);
            _unitOfWork.Clients.Save();

            return new Result<Client>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<Client> Update(Client model)
        {
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_UPDATE));

            if (!resultValidation.IsValid)
            {
                return new Result<Client>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            model.DateUpdate = DateTime.Now;
            model.Name = model.Name.ToUpper();
            model.NaturalFrom = model.NaturalFrom.ToUpper();
            model.Nationality = model.Nationality.ToUpper();
            model.OrganExp = model.OrganExp.ToUpper();
            model.Email = model.Email.ToUpper();
            model.Profession = model.Profession.ToUpper();
            model.Address = model.Address.ToUpper();
            model.Neighborhood = model.Neighborhood.ToUpper();
            model.Complement = model.Complement?.ToUpper();
            model.City = model.City.ToUpper();
            model.State = model.State.ToUpper();

            _unitOfWork.Clients.Update(model);
            _unitOfWork.Clients.Save();

            return new Result<Client>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<Client> Delete(int id)
        {
            Client model = new Client { Id = id };
            var resultValidation = _validator.Validate(model, options => options.IncludeRuleSets(Constants.FLUENT_DELETE));

            if (!resultValidation.IsValid)
            {
                return new Result<Client>
                {
                    Success = false,
                    Object = model,
                    Errors = resultValidation.Errors
                };
            }

            _unitOfWork.Clients.Delete(model);
            _unitOfWork.Clients.Save();

            return new Result<Client>
            {
                Success = true,
                Object = model,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }
    }
}