using FluentValidation;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;
using System.Globalization;

namespace GrupoSelect.Services.FluentValidation
{
    public class ContractValidator : AbstractValidator<Domain.Entity.Contract>
    {
        private IUnitOfWork _unitOfWork;

        public ContractValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet(Constants.FLUENT_UPDATE, () =>
            {
                //RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do usuário é inválido.");
                //RuleFor(x => x.ContractConsultancy).NotEmpty().WithMessage("Selecione o Cliente.");
                //RuleFor(x => x.ContractFinancialAdmin).NotEmpty().WithMessage("Selecione o Usuário.");
                //RuleFor(x => x.VideoAgree).NotEmpty().WithMessage("Selecione o Tipo Produto.");

                //RuleFor(x => x).Custom(CreditTotalValue);
            });

            RuleSet(Constants.FLUENT_CHECK, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do contrato é inválido.");
                RuleFor(x => x.Status).NotEmpty().WithMessage("O resultado da análise não foi informada.");

                RuleFor(x => x).Custom(ValidateResultStatus);
            });
        }

        private void ValidateResultStatus(Domain.Entity.Contract model, ValidationContext<Domain.Entity.Contract> context)
        {
            if (model.Status == Constants.CONTRACT_STATUS_CR && string.IsNullOrEmpty(model.ReprovedReason))
            {
                context.AddFailure("Informe o motivo da reprovação do contrato.");
            }

            if (model.Status == Constants.CONTRACT_STATUS_CR && string.IsNullOrEmpty(model.ReprovedExplain))
            {
                context.AddFailure("Descreva qual/quais documentos precisam ser verificados.");
            }
        }
    }
}
