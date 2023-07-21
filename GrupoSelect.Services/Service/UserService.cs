using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Model;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.FluentValidation.User;
using GrupoSelect.Services.Interface;
using GrupoSelect.Services.Interface.Helpers;

namespace GrupoSelect.Services.Service
{    
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<User> _validator;
        private readonly ILogExceptions _logExceptions;

        public UserService(IUnitOfWork unitOfWork, IValidator<User> validator, ILogExceptions logExceptions)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logExceptions = logExceptions;
        }

        public async Task<Result<IEnumerable<User>>> GetAll(User filter)
        {
            try
            {
                return new Result<IEnumerable<User>>
                {
                    Success = true,
                    Object = _unitOfWork.Users.GetAll(f => (string.IsNullOrEmpty(filter.Name) || f.Name.Contains(filter.Name)) && (string.IsNullOrEmpty(filter.Email) || f.Email.Contains(filter.Email))),
                };
            }
            catch (Exception ex)
            {
                _logExceptions.Log(ex);

                return new Result<IEnumerable<User>>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<PaginateResult<IEnumerable<User>>> GetAllPaginate(User filter, int page, int qtPage)
        {
            try
            {
                //throw new Exception("teste");
                return _unitOfWork.Users.GetAllPaginate(f => (string.IsNullOrEmpty(filter.Name) || f.Name.Contains(filter.Name)) && 
                                                             (string.IsNullOrEmpty(filter.Representation) || f.Representation.Contains(filter.Representation)) &&
                                                             (string.IsNullOrEmpty(filter.Login) || f.Login.Contains(filter.Login)) &&
                                                             (string.IsNullOrEmpty(filter.Cnpj) || f.Cnpj == filter.Cnpj), null, page, qtPage);
            }
            catch (Exception ex)
            {
                _logExceptions.Log(ex);

                return new PaginateResult<IEnumerable<User>>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Result<User>> GetById(int id)
        {
            try
            {
                User user = _unitOfWork.Users.GetAll(f => f.Id == id).FirstOrDefault();

                if (user != null)
                {
                    return new Result<User>
                    {
                        Success = true,
                        Object = user
                    };
                }
                else
                {
                    return new Result<User>
                    {
                        Success = false,
                        Message = "O id informado não foi encontrado."
                    };
                }
            }
            catch (Exception ex)
            {
                _logExceptions.Log(ex);

                return new Result<User>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public Result<User> Insert(User user)
        {
            try
            {
                var resultValidation = _validator.Validate(user, options => options.IncludeRuleSets(Constants.FLUENT_INSERT));

                if (!resultValidation.IsValid)
                {
                    return new Result<User>
                    {
                        Success = false,
                        Object = user,
                        Errors = resultValidation.Errors
                    };
                }

                _unitOfWork.Users.Insert(user);
                _unitOfWork.Users.Save();

                return new Result<User>
                {
                    Success = true,
                    Object = user,
                    Message = Constants.SYSTEM_SUCCESS
                };
            }
            catch (Exception ex)
            {
                _logExceptions.Log(ex);

                return new Result<User>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public Result<User> Update(User user)
        {
            try
            {
                var resultValidation = _validator.Validate(user, options => options.IncludeRuleSets(Constants.FLUENT_UPDATE));

                if (!resultValidation.IsValid)
                {
                    return new Result<User>
                    {
                        Success = false,
                        Object = user,
                        Errors = resultValidation.Errors
                    };
                }

                _unitOfWork.Users.Update(user);
                _unitOfWork.Users.Save();

                return new Result<User>
                {
                    Success = true,
                    Object = user,
                    Message = Constants.SYSTEM_SUCCESS
                };
            }
            catch (Exception ex)
            {
                _logExceptions.Log(ex);

                return new Result<User>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public Result<User> Delete(int id)
        {
            try
            {
                User user = new User { Id = id };
                var resultValidation = _validator.Validate(user, options => options.IncludeRuleSets(Constants.FLUENT_DELETE));
             
                if (!resultValidation.IsValid)
                {
                    return new Result<User>
                    {
                        Success = false,
                        Object = user,
                        Errors = resultValidation.Errors
                    };
                }

                _unitOfWork.Users.Delete(user);
                _unitOfWork.Users.Save();

                return new Result<User>
                {
                    Success = true,
                    Object = user,
                    Message = Constants.SYSTEM_SUCCESS
                };
            }
            catch (Exception ex)
            {
                _logExceptions.Log(ex);

                return new Result<User>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }        
    }
}