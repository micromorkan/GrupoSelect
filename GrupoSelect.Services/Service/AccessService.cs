using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;

namespace GrupoSelect.Services.Service
{
    public class AccessService : IDisposable, IAccessService
    {
        private readonly IValidator<User> _validator;
        private readonly ILogService _logExceptions;
        private readonly IUnitOfWork _unitOfWork;

        public AccessService(IValidator<User> validator, ILogService logExceptions,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _logExceptions = logExceptions;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<User>> Authenticate(User user)
        {
            try
            {
                var resultValidation = _validator.Validate(user, options => options.IncludeRuleSets(Constants.FLUENT_AUTHENTICATE));

                if (!resultValidation.IsValid)
                {
                    return new Result<User>
                    {
                        Success = false,
                        Object = user,
                        Errors = resultValidation.Errors
                    };
                }

                var result = _unitOfWork.Users.GetAll(f => f.Login == user.Login && f.Password == user.Password, null, i=>i.GroupUsers);

                if (result.Count() > 0)
                {
                    User login = result.FirstOrDefault();

                    if (login.Active)
                    {
                        return new Result<User>
                        {
                            Success = true,
                            Object = login
                        };
                    }
                    else
                    {
                        return new Result<User>
                        {
                            Success = false,
                            Message = "Seu acesso foi bloqueado!"
                        };
                    }
                }
                else
                {
                    return new Result<User>
                    {
                        Success = false,
                        Message = "Usuário e/ou Senha estão inválidos!"
                    };
                }
            }
            catch (Exception ex)
            {
                _logExceptions.LogException(ex);

                return new Result<User>
                {
                    Success = false,
                    Message = Constants.SYSTEM_ERROR_MSG
                };
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}