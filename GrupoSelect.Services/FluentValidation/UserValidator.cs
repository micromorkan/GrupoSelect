using FluentValidation;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;

namespace GrupoSelect.Services.FluentValidation
{
    public class UserValidator : AbstractValidator<Domain.Entity.User>
    {
        private IUnitOfWork _unitOfWork;

        public UserValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet(Constants.FLUENT_INSERT, () =>
            {
                RuleFor(usuario => usuario.Name).NotEmpty().WithMessage("O nome é obrigatório.");
                RuleFor(usuario => usuario.Cnpj).NotEmpty().WithMessage("Informe um email válido.");
                RuleFor(usuario => usuario.Representation).NotEmpty().WithMessage("O representante é obrigatório.");
                RuleFor(usuario => usuario.Login).NotEmpty().WithMessage("O login é obrigatório.");
                RuleFor(usuario => usuario.Password).NotEmpty().WithMessage("A senha é obrigatória.");
                RuleFor(usuario => usuario.Email).NotEmpty().EmailAddress().WithMessage("Informe um email válido.");
                RuleFor(usuario => usuario.Profile).NotEmpty().WithMessage("O perfil é obrigatório.");
                RuleFor(usuario => usuario).Custom(InsertUniqueCnpjLoginNameEmail);
            });

            RuleSet(Constants.FLUENT_UPDATE, () =>
            {
                RuleFor(usuario => usuario.Id).GreaterThan(0).WithMessage("O id do usuário é inválido.");
                RuleFor(usuario => usuario.Name).NotEmpty().WithMessage("O nome é obrigatório.");
                RuleFor(usuario => usuario.Cnpj).NotEmpty().WithMessage("Informe um email válido.");
                RuleFor(usuario => usuario.Representation).NotEmpty().WithMessage("Informe um representante válido.");
                RuleFor(usuario => usuario.Login).NotEmpty().WithMessage("Informe um login válido.");
                RuleFor(usuario => usuario.Password).NotEmpty().WithMessage("Informe uma senha válido.");
                RuleFor(usuario => usuario.Email).NotEmpty().EmailAddress().WithMessage("Informe um email válido.");
                RuleFor(usuario => usuario.Profile).NotEmpty().WithMessage("Informe um perfil válido.");
                RuleFor(usuario => usuario).Custom(EditUniqueCnpjLoginNameEmail);
            });

            RuleSet(Constants.FLUENT_DELETE, () =>
            {
                RuleFor(usuario => usuario.Id).GreaterThan(0).WithMessage("O id do usuário é inválido.");
            });

            RuleSet(Constants.FLUENT_AUTHENTICATE, () =>
            {
                RuleFor(usuario => usuario.Login).NotEmpty().WithMessage("O Login é obrigatório.");
                RuleFor(usuario => usuario.Password).NotEmpty().WithMessage("A Senha é obrigatória.");
            });
        }

        private void InsertUniqueCnpjLoginNameEmail(Domain.Entity.User user, ValidationContext<Domain.Entity.User> context)
        {
            var result = _unitOfWork.Users.GetAll(filter => filter.Cnpj == user.Cnpj || 
                                                            filter.Name == user.Name || 
                                                            filter.Login == user.Login ||
                                                            filter.Email == user.Email);

            if (result.Any())
            {
                var existUser = result.First();

                if (existUser.Cnpj == user.Cnpj)
                {
                    context.AddFailure("Já existe um Cnpj cadastrado");
                }
                
                if (existUser.Name == user.Name)
                {
                    context.AddFailure("Já existe um Nome cadastrado");
                }
                
                if (existUser.Login == user.Login)
                {
                    context.AddFailure("Já existe um Login cadastrado");
                }

                if (existUser.Email == user.Email)
                {
                    context.AddFailure("Já existe um Email cadastrado");
                }
            }
        }

        private void EditUniqueCnpjLoginNameEmail(Domain.Entity.User user, ValidationContext<Domain.Entity.User> context)
        {
            var result = _unitOfWork.Users.GetAll(filter => (filter.Cnpj == user.Cnpj ||
                                                            filter.Name == user.Name ||
                                                            filter.Login == user.Login ||
                                                            filter.Email == user.Email) &&
                                                            filter.Id != user.Id);

            if (result.Any())
            {
                var existUser = result.First();

                if (existUser.Cnpj == user.Cnpj)
                {
                    context.AddFailure("Já existe um Cnpj cadastrado");
                }

                if (existUser.Name == user.Name)
                {
                    context.AddFailure("Já existe um Nome cadastrado");
                }

                if (existUser.Login == user.Login)
                {
                    context.AddFailure("Já existe um Login cadastrado");
                }

                if (existUser.Email == user.Email)
                {
                    context.AddFailure("Já existe um Email cadastrado");
                }
            }
        }
    }
}
