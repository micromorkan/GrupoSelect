using FluentValidation;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;

namespace GrupoSelect.Services.FluentValidation
{
    public class FinancialAdminValidator : AbstractValidator<Domain.Entity.FinancialAdmin>
    {
        private IUnitOfWork _unitOfWork;

        public FinancialAdminValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet(Constants.FLUENT_INSERT, () =>
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("O nome é obrigatório.");
                RuleFor(x => x.Name).Length(1, 50).WithMessage("O nome deve conter no máximo 50 caracteres.");
                RuleFor(x => x.Active).NotEmpty().WithMessage("O status é obrigatório.");
                RuleFor(x => x).Custom(InsertUniqueName);
            });

            RuleSet(Constants.FLUENT_UPDATE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id da administradora é inválido.");
                RuleFor(x => x.Name).NotEmpty().WithMessage("O nome é obrigatório.");
                RuleFor(x => x.Name).Length(1, 50).WithMessage("O nome deve conter no máximo 50 caracteres.");
                RuleFor(x => x.Active).NotEmpty().WithMessage("O status é obrigatório.");
                RuleFor(x => x).Custom(EditUniqueName);
            });

            RuleSet(Constants.FLUENT_DELETE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id da administradora é inválido.");
                RuleFor(x => x).Custom(DeleteBlock);
            });
        }

        private void InsertUniqueName(Domain.Entity.FinancialAdmin model, ValidationContext<Domain.Entity.FinancialAdmin> context)
        {
            var result = _unitOfWork.FinancialAdmins.GetAll(x => x.Name == model.Name);

            if (result.Any())
            {
                context.AddFailure("Já existe uma administradora com mesmo nome cadastrada.");
            }
        }

        private void EditUniqueName(Domain.Entity.FinancialAdmin model, ValidationContext<Domain.Entity.FinancialAdmin> context)
        {
            var result = _unitOfWork.FinancialAdmins.GetAll(x => x.Name == model.Name && x.Id != model.Id);

            if (result.Any())
            {
                context.AddFailure("Já existe uma administradora com mesmo nome cadastrada.");
            }
        }
        private void DeleteBlock(Domain.Entity.FinancialAdmin model, ValidationContext<Domain.Entity.FinancialAdmin> context)
        {
            var result = _unitOfWork.Credits.GetAll(filter => filter.FinancialAdminId == model.Id);

            if (result.Any())
            {
                context.AddFailure("Não é possível deletar este registro que está em uso na tabela de Crédito.");
            }
        }
    }
}
