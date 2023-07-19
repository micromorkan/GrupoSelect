using FluentValidation;

namespace GrupoSelect.Services.FluentValidation.User
{
    public class DeleteUserValidator : AbstractValidator<GrupoSelect.Domain.Entity.User>
    {
        public DeleteUserValidator()
        {
            RuleFor(usuario => usuario.Id).GreaterThan(0).WithMessage("O id do usuário é inválido.");
        }
    }
}
