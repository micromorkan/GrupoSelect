using FluentValidation.Results;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Services.FluentValidation;
using GrupoSelect.Services.FluentValidation.User;
using GrupoSelect.Services.Interface;
using GrupoSelect.Services.Model;

namespace GrupoSelect.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                return new Result<IEnumerable<User>>
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
                var validator = new InsertUserValidator();
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

                _unitOfWork.Users.Insert(user);
                _unitOfWork.Users.Save();

                return new Result<User>
                {
                    Success = true,
                    Object = user,
                    Message = "Operação realizada com sucesso!"
                };
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

        public Result<User> Update(User user)
        {
            try
            {
                var validator = new UpdateUserValidator();
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

                _unitOfWork.Users.Update(user);
                _unitOfWork.Users.Save();

                return new Result<User>
                {
                    Success = true,
                    Object = user,
                    Message = "Operação realizada com sucesso!"
                };
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

        public Result<User> Delete(int id)
        {
            try
            {
                User user = new User { Id = id };
                var validator = new DeleteUserValidator();
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

                _unitOfWork.Users.Delete(user);
                _unitOfWork.Users.Save();

                return new Result<User>
                {
                    Success = true,
                    Object = user,
                    Message = "Operação realizada com sucesso!"
                };
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