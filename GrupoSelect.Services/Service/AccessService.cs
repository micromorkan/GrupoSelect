using FluentValidation.Results;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Services.FluentValidation;
using GrupoSelect.Services.FluentValidation.User;
using GrupoSelect.Services.Interface;
using GrupoSelect.Domain.Model;

namespace GrupoSelect.Services.Service
{
    public class AccessService : IAccessService
    {
        private readonly IUserRepository _userRepository;

        public AccessService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> Authenticate(User user)
        {
            try
            {
                var validator = new AuthenticateUserValidator();
                var resultadoValidacao = validator.Validate(user);

                if (!resultadoValidacao.IsValid)
                {
                    return new Result<User>
                    {
                        Success = false,
                        Object = user,
                        Errors = resultadoValidacao.Errors
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
                return new Result<User>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}