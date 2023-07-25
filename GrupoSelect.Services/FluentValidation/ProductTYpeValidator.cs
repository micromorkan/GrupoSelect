using FluentValidation;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;

namespace GrupoSelect.Services.FluentValidation
{
    public class ProductTypeValidator : AbstractValidator<Domain.Entity.ProductType>
    {
        private IUnitOfWork _unitOfWork;

        public ProductTypeValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet(Constants.FLUENT_INSERT, () =>
            {
                RuleFor(x => x.ProductName).NotEmpty().WithMessage("O nome é obrigatório.");
                RuleFor(x => x.ProductName).Length(1, 50).WithMessage("O nome deve conter no máximo 50 caracteres.");
                RuleFor(x => x).Custom(InsertUniqueName);
            });

            RuleSet(Constants.FLUENT_UPDATE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do produto é inválido.");
                RuleFor(x => x.ProductName).NotEmpty().WithMessage("O nome é obrigatório.");
                RuleFor(x => x.ProductName).Length(1, 50).WithMessage("O nome deve conter no máximo 50 caracteres.");
                RuleFor(x => x).Custom(EditUniqueName);
            });

            RuleSet(Constants.FLUENT_DELETE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do produto é inválido.");
                //todo-lucas validacao de exclusao se o registro estiver sendo usado na tabela de credito
            });
        }

        private void InsertUniqueName(Domain.Entity.ProductType model, ValidationContext<Domain.Entity.ProductType> context)
        {
            var result = _unitOfWork.ProductTypes.GetAll(x => x.ProductName == model.ProductName);

            if (result.Any())
            {
                context.AddFailure("Já existe um produto com mesmo nome cadastrado.");
            }
        }

        private void EditUniqueName(Domain.Entity.ProductType model, ValidationContext<Domain.Entity.ProductType> context)
        {
            var result = _unitOfWork.ProductTypes.GetAll(x => x.ProductName == model.ProductName && x.Id != model.Id);

            if (result.Any())
            {
                context.AddFailure("Já existe um produto com mesmo nome cadastrado.");
            }
        }
    }
}
