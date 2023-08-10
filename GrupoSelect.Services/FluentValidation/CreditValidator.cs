using FluentValidation;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;
using System.Globalization;

namespace GrupoSelect.Services.FluentValidation
{
    public class CreditValidator : AbstractValidator<Domain.Entity.Credit>
    {
        private IUnitOfWork _unitOfWork;

        public CreditValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet(Constants.FLUENT_INSERT, () =>
            {
                RuleFor(x => x.ProductTypeId).NotEmpty().WithMessage("Selecione o Tipo Produto");
                RuleFor(x => x.TableTypeId).NotEmpty().WithMessage("Selecione o Tipo Tabela");
                RuleFor(x => x.FinancialAdminId).NotEmpty().WithMessage("Selecione a Administradora");
                RuleFor(x => x.Months).NotEmpty().WithMessage("Quantidade de meses inválida.");
                RuleFor(x => x.Months).GreaterThan(0).WithMessage("Quantidade de meses deve ser maior que 0.");
                RuleFor(x => x.CreditValue).NotEmpty().WithMessage("Valor Crédito inválido.");
                RuleFor(x => x.PortionValue).NotEmpty().WithMessage("Valor Parcela inválido.");
                RuleFor(x => x.MembershipValue).NotEmpty().WithMessage("Valor Adesão inválido.");
                RuleFor(x => x.TotalValue).NotEmpty().WithMessage("Valor Total inválido.");
                RuleFor(x => x).Custom(InsertUniqueCredit);
                RuleFor(x => x).Custom(ValidateMoney);
            });

            RuleSet(Constants.FLUENT_UPDATE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do usuário é inválido.");
                RuleFor(x => x.ProductTypeId).NotEmpty().WithMessage("Selecione o Tipo Produto");
                RuleFor(x => x.TableTypeId).NotEmpty().WithMessage("Selecione o Tipo Tabela");
                RuleFor(x => x.FinancialAdminId).NotEmpty().WithMessage("Selecione a Administradora");
                RuleFor(x => x.Months).NotEmpty().WithMessage("Quantidade de meses inválida.");
                RuleFor(x => x.Months).GreaterThan(0).WithMessage("Quantidade de meses deve ser maior que 0.");
                RuleFor(x => x.CreditValue).NotEmpty().WithMessage("Valor Crédito inválido.");
                RuleFor(x => x.PortionValue).NotEmpty().WithMessage("Valor Parcela inválido.");
                RuleFor(x => x.MembershipValue).NotEmpty().WithMessage("Valor Adesão inválido.");
                RuleFor(x => x.TotalValue).NotEmpty().WithMessage("Valor Total inválido.");
                RuleFor(x => x).Custom(EditUniqueCredit);
                RuleFor(x => x).Custom(ValidateMoney);
            });

            RuleSet(Constants.FLUENT_DELETE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do Crédito é inválido.");
            });
        }

        private void InsertUniqueCredit(Domain.Entity.Credit model, ValidationContext<Domain.Entity.Credit> context)
        {
            var result = _unitOfWork.Credits.GetAll(filter => filter.ProductTypeId == model.ProductTypeId &&
                                                            filter.TableTypeId == model.TableTypeId &&
                                                            filter.FinancialAdminId == model.FinancialAdminId &&
                                                            filter.Months == model.Months &&
                                                            filter.PortionValue == model.PortionValue);

            if (result.Any())
            {
                context.AddFailure("Já existe Crédito com os mesmos parâmetros cadastrado.");
            }
        }

        private void EditUniqueCredit(Domain.Entity.Credit model, ValidationContext<Domain.Entity.Credit> context)
        {
            var result = _unitOfWork.Credits.GetAll(filter => filter.ProductTypeId == model.ProductTypeId &&
                                                            filter.TableTypeId == model.TableTypeId &&
                                                            filter.FinancialAdminId == model.FinancialAdminId &&
                                                            filter.Months == model.Months &&
                                                            filter.PortionValue == model.PortionValue &&
                                                            filter.Id != model.Id);

            if (result.Any())
            {
                context.AddFailure("Já existe Crédito com os mesmos parâmetros cadastrado.");
            }
        }
        private void ValidateMoney(Domain.Entity.Credit model, ValidationContext<Domain.Entity.Credit> context)
        {
            decimal creditValue = 0;
            if (Decimal.TryParse(model.CreditValue, NumberStyles.Any, new CultureInfo("pt-BR"), out creditValue))
            {
                if (creditValue <= 0)
                {
                    context.AddFailure("Valor Crédito deve ser maior que 0,00.");
                }
            }
            else
            {
                context.AddFailure("Valor Crédito inválido.");
            }

            decimal portionValue = 0;
            if (Decimal.TryParse(model.PortionValue, NumberStyles.Any, new CultureInfo("pt-BR"), out portionValue))
            {
                if (portionValue <= 0)
                {
                    context.AddFailure("Valor Parcela deve ser maior que 0,00.");
                }
            }
            else
            {
                context.AddFailure("Valor Parcela inválido.");
            }

            decimal membershipValue = 0;
            if (Decimal.TryParse(model.MembershipValue, NumberStyles.Any, new CultureInfo("pt-BR"), out membershipValue))
            {
                if (membershipValue <= 0)
                {
                    context.AddFailure("Valor Adesão deve ser maior que 0,00.");
                }
            }
            else
            {
                context.AddFailure("Valor Adesão inválido.");
            }

            decimal totalValue = 0;
            if (Decimal.TryParse(model.TotalValue, NumberStyles.Any, new CultureInfo("pt-BR"), out totalValue))
            {
                if (totalValue <= 0)
                {
                    context.AddFailure("Valor Total deve ser maior que 0,00.");
                }
            }
            else
            {
                context.AddFailure("Valor Total inválido.");
            }
        }
    }
}
