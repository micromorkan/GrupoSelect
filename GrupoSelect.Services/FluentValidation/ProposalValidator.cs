using FluentValidation;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;
using System.Globalization;

namespace GrupoSelect.Services.FluentValidation
{
    public class ProposalValidator : AbstractValidator<Domain.Entity.Proposal>
    {
        private IUnitOfWork _unitOfWork;

        public ProposalValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet(Constants.FLUENT_INSERT, () =>
            {
                RuleFor(x => x.ClientId).NotEmpty().WithMessage("Selecione o Cliente.");
                RuleFor(x => x.UserId).NotEmpty().WithMessage("Selecione o Usuário.");
                RuleFor(x => x.ProductTypeName).NotEmpty().WithMessage("Selecione o Tipo Produto.");
                RuleFor(x => x.TableTypeTax).NotEmpty().WithMessage("Selecione o Tipo Tabela.");
                RuleFor(x => x.FinancialAdminName).NotEmpty().WithMessage("Selecione a Administradora.");
                RuleFor(x => x.CreditValue).NotEmpty().WithMessage("Selecione o Crédito.");
                RuleFor(x => x.CreditTotalValue).NotEmpty().WithMessage("Valor Total inválido.");
                RuleFor(x => x).Custom(CreditTotalValue);
            });

            RuleSet(Constants.FLUENT_UPDATE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do usuário é inválido.");
                RuleFor(x => x.ClientId).NotEmpty().WithMessage("Selecione o Cliente.");
                RuleFor(x => x.UserId).NotEmpty().WithMessage("Selecione o Usuário.");
                RuleFor(x => x.ProductTypeName).NotEmpty().WithMessage("Selecione o Tipo Produto.");
                RuleFor(x => x.TableTypeTax).NotEmpty().WithMessage("Selecione o Tipo Tabela.");
                RuleFor(x => x.FinancialAdminName).NotEmpty().WithMessage("Selecione a Administradora.");
                RuleFor(x => x.CreditValue).NotEmpty().WithMessage("Selecione o Crédito.");
                RuleFor(x => x.CreditTotalValue).NotEmpty().WithMessage("Valor Total inválido.");
                RuleFor(x => x).Custom(CreditTotalValue);
            });

            RuleSet(Constants.FLUENT_DELETE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id da Proposta é inválido.");
                RuleFor(x => x.Id).Custom(BlockDelete);
            });
        }
        
        private void CreditTotalValue(Domain.Entity.Proposal model, ValidationContext<Domain.Entity.Proposal> context)
        {
            decimal creditTotalValue = 0;
            if (Decimal.TryParse(model.CreditTotalValue, NumberStyles.Any, new CultureInfo("pt-BR"), out creditTotalValue))
            {
                decimal creditPortionValue = decimal.Parse(model.CreditPortionValue, NumberStyles.Any, new CultureInfo("pt-BR"));
                decimal creditMembershipValue = decimal.Parse(model.CreditMembershipValue, NumberStyles.Any, new CultureInfo("pt-BR"));
                decimal valueMinimalRequired = creditPortionValue + creditMembershipValue;

                if (creditTotalValue < valueMinimalRequired)
                {
                    string formatedValue = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valueMinimalRequired);
                    context.AddFailure("Valor Total deve ser igual ou superior a " + formatedValue);
                }
            }
            else
            {
                context.AddFailure("Valor Total inválido.");
            }
        }

        private async void BlockDelete(int id, ValidationContext<Domain.Entity.Proposal> context)
        {
            var proposal = _unitOfWork.Proposals.GetAll(x => x.Id == id).FirstOrDefault();
         
            if (proposal == null) 
            {
                context.AddFailure("O id da Proposta é inválido.");
            }
            else
            {
                if (proposal.UserChecked > 0 || proposal.DateChecked != null)
                {
                    context.AddFailure("A proposta não pode ser excluida pois já foi conferida.");
                }

                if (proposal.Aproved)
                {
                    context.AddFailure("A proposta não pode ser excluida pois já foi aprovada.");
                }
            }
        }
    }
}
