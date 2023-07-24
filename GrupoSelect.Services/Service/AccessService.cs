using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.FluentValidation;
using GrupoSelect.Services.Interface;

namespace GrupoSelect.Services.Service
{
    public class AccessService : IAccessService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _validator;
        private readonly ILogService _logExceptions;

        public AccessService(IUserRepository userRepository, IValidator<User> validator, ILogService logExceptions)
        {
            _userRepository = userRepository;
            _validator = validator;
            _logExceptions = logExceptions;
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

                User login = await _userRepository.Authenticate(user);

                if (login != null)
                {
                    return new Result<User>
                    {
                        Success = true,
                        Object = await _userRepository.Authenticate(user)
                    };
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
    }
}