using FluentValidation;

namespace GrupoSelect.Services.FluentValidation.User
{
    public class AuthenticateUserValidator : AbstractValidator<GrupoSelect.Domain.Entity.User>
    {
        public AuthenticateUserValidator()
        {
            RuleFor(usuario => usuario.Login).NotEmpty().WithMessage("O Login é obrigatório.");
            RuleFor(usuario => usuario.Password).NotEmpty().WithMessage("A Senha é obrigatória.");
        }
    }
}
