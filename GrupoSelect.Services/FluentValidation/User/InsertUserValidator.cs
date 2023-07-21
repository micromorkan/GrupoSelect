using FluentValidation;

namespace GrupoSelect.Services.FluentValidation.User
{
    public class InsertUserValidator : AbstractValidator<GrupoSelect.Domain.Entity.User>
    {
        public InsertUserValidator()
        {
            RuleFor(usuario => usuario.Name).NotEmpty().WithMessage("O nome é obrigatório.");
            RuleFor(usuario => usuario.Cnpj).NotEmpty().WithMessage("Informe um email válido.");
            RuleFor(usuario => usuario.Representation).NotEmpty().WithMessage("O representante é obrigatório.");
            RuleFor(usuario => usuario.Login).NotEmpty().WithMessage("O login é obrigatório.");
            RuleFor(usuario => usuario.Password).NotEmpty().WithMessage("A senha é obrigatória.");
            RuleFor(usuario => usuario.Email).NotEmpty().EmailAddress().WithMessage("O email é obrigatório.");
            RuleFor(usuario => usuario.Profile).NotEmpty().WithMessage("O perfil é obrigatório.");
        }
    }
}
