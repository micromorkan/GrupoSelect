using FluentValidation;

namespace GrupoSelect.Services.FluentValidation.User
{
    public class InsertUserValidator : AbstractValidator<GrupoSelect.Domain.Entity.User>
    {
        public InsertUserValidator()
        {
            RuleFor(usuario => usuario.Name).NotEmpty().WithMessage("O nome é obrigatório.");
            RuleFor(usuario => usuario.Cnpj).NotEmpty().WithMessage("Informe um email válido.");
            RuleFor(usuario => usuario.Representation).NotEmpty().WithMessage("Informe um representante válido.");
            RuleFor(usuario => usuario.Login).NotEmpty().WithMessage("Informe um login válido.");
            RuleFor(usuario => usuario.Password).NotEmpty().WithMessage("Informe uma senha válido.");
            RuleFor(usuario => usuario.Email).NotEmpty().EmailAddress().WithMessage("Informe um email válido.");
            RuleFor(usuario => usuario.Profile).NotEmpty().WithMessage("Informe um perfil válido.");
        }
    }
}
