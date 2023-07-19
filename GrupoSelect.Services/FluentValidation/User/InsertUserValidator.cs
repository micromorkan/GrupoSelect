using FluentValidation;

namespace GrupoSelect.Services.FluentValidation.User
{
    public class InsertUserValidator : AbstractValidator<GrupoSelect.Domain.Entity.User>
    {
        public InsertUserValidator()
        {
            RuleFor(usuario => usuario.Name).NotEmpty().WithMessage("O nome é obrigatório.");
            RuleFor(usuario => usuario.Email).NotEmpty().EmailAddress().WithMessage("Informe um email válido.");
        }
    }
}
