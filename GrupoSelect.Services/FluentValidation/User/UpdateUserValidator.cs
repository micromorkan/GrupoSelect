using FluentValidation;

namespace GrupoSelect.Services.FluentValidation.User
{
    public class UpdateUserValidator : AbstractValidator<GrupoSelect.Domain.Entity.User>
    {
        public UpdateUserValidator()
        {
            RuleFor(usuario => usuario.Id).GreaterThan(0).WithMessage("O id do usuário é inválido.");
            RuleFor(usuario => usuario.Name).NotEmpty().WithMessage("O nome é obrigatório.");
            RuleFor(usuario => usuario.Email).NotEmpty().EmailAddress().WithMessage("Informe um email válido.");
        }
    }
}
