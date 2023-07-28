using FluentValidation;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;

namespace GrupoSelect.Services.FluentValidation
{
    public class TableTypeValidator : AbstractValidator<Domain.Entity.TableType>
    {
        private IUnitOfWork _unitOfWork;

        public TableTypeValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet(Constants.FLUENT_INSERT, () =>
            {
                RuleFor(x => x.TableTax).NotEmpty().WithMessage("Tipo Tabela é obrigatório.");
                RuleFor(x => x.TableTax).Length(1, 50).WithMessage("Tipo Tabela deve conter no máximo 50 caracteres.");
                RuleFor(x => x.MembershipFee).NotEmpty().WithMessage("Taxa Adesão é obrigatória.");
                RuleFor(x => x.RemainingRate).NotEmpty().WithMessage("Taxa Restante é obrigatória.");

                RuleFor(x => x).Custom(IsNumber);
                RuleFor(x => x).Custom(InsertUniqueName);
            });

            RuleSet(Constants.FLUENT_UPDATE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id to Tipo Tabela é inválido.");
                RuleFor(x => x.TableTax).NotEmpty().WithMessage("Tipo Tabela é obrigatório.");
                RuleFor(x => x.TableTax).Length(1, 50).WithMessage("Tipo Tabela deve conter no máximo 50 caracteres.");
                RuleFor(x => x.MembershipFee).NotEmpty().WithMessage("Taxa Adesão é obrigatória.");
                RuleFor(x => x.RemainingRate).NotEmpty().WithMessage("Taxa Restante é obrigatória.");

                RuleFor(x => x).Custom(IsNumber);
                RuleFor(x => x).Custom(EditUniqueName);
            });

            RuleSet(Constants.FLUENT_DELETE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id Tipo Tabela é inválido.");
                RuleFor(x => x).Custom(DeleteBlock);
            });
        }

        private void InsertUniqueName(Domain.Entity.TableType model, ValidationContext<Domain.Entity.TableType> context)
        {
            var result = _unitOfWork.TableTypes.GetAll(x => x.TableTax == model.TableTax);

            if (result.Any())
            {
                context.AddFailure("Já existe um Tipo Tabela com mesmo nome cadastrado.");
            }
        }

        private void EditUniqueName(Domain.Entity.TableType model, ValidationContext<Domain.Entity.TableType> context)
        {
            var result = _unitOfWork.TableTypes.GetAll(x => x.TableTax == model.TableTax && x.Id != model.Id);

            if (result.Any())
            {
                context.AddFailure("Já existe um Tipo Tabela com mesmo nome cadastrado.");
            }
        }
        private void IsNumber(Domain.Entity.TableType model, ValidationContext<Domain.Entity.TableType> context)
        {
            Decimal result = 0;

            if (!Decimal.TryParse(model.MembershipFee, out result))
            {
                context.AddFailure("Informe um valor numérico para Taxa Adesão.");
            }
            else if (result < 0) 
            {
                context.AddFailure("Taxa Adesão inválida.");
            }

            Decimal result2 = 0;

            if (!Decimal.TryParse(model.RemainingRate, out result2))
            {
                context.AddFailure("Informe um valor numérico para Taxa Restante.");
            }
            else if (result2 < 0)
            {
                context.AddFailure("Taxa Restante inválida.");
            }
        }
        private void DeleteBlock(Domain.Entity.TableType model, ValidationContext<Domain.Entity.TableType> context)
        {
            var result = _unitOfWork.Credits.GetAll(filter => filter.TableTypeId == model.Id);

            if (result.Any())
            {
                context.AddFailure("Não é possível deletar este registro que está em uso na tabela de Crédito.");
            }
        }
    }
}
