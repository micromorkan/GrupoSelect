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
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do Contrato é inválido.");
                RuleFor(x => x.ContractConsultancy).NotEmpty().WithMessage("Nenhum Contrato foi anexado ao registro.");
                RuleFor(x => x.ContractFinancialAdmin).NotEmpty().WithMessage("Nenhum Documento foi anexado ao registro.");

                RuleFor(x => x).Custom(ValidateUpdateStatus);
            });

            RuleSet(Constants.FLUENT_CHECK, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do Contrato é inválido.");
                RuleFor(x => x.Status).NotEmpty().WithMessage("O resultado da análise não foi informado.");

                RuleFor(x => x).Custom(ValidateResultStatus);
            });

            RuleSet(Constants.FLUENT_CANCEL, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do Contrato é inválido.");
                RuleFor(x => x).Custom(ValidateCancelStatus);
            });
        }

        private void ValidateUpdateStatus(Domain.Entity.Contract model, ValidationContext<Domain.Entity.Contract> context)
        {
            if (model.Status != Constants.CONTRACT_STATUS_AD)
            {
                context.AddFailure("O Contrato não pode ser editado pois possui o status de " + model.Status + ".");
            }
            else if (model.Proposal.User.BranchWithoutAdm && model.VideoAgree == null)
            {
                context.AddFailure("Nenhum Vídeo foi anexado ao registro.");
            }
        }

        private void ValidateResultStatus(Domain.Entity.Contract model, ValidationContext<Domain.Entity.Contract> context)
        {
            if (model.Status == Constants.CONTRACT_STATUS_CR && string.IsNullOrEmpty(model.ReprovedReason))
            {
                context.AddFailure("Informe o motivo da reprovação do Contrato.");
            }

            if (model.Status == Constants.CONTRACT_STATUS_CR && string.IsNullOrEmpty(model.ReprovedExplain))
            {
                context.AddFailure("Descreva qual/quais documentos precisam ser verificados.");
            }
        }

        private void ValidateCancelStatus(Domain.Entity.Contract model, ValidationContext<Domain.Entity.Contract> context)
        {
            var contract = _unitOfWork.Contracts.GetAll(x => x.Id == model.Id).FirstOrDefault();

            if (contract == null)
            {
                if (contract.Status == Constants.CONTRACT_STATUS_CA || contract.Status == Constants.CONTRACT_STATUS_CC)
                {
                    context.AddFailure("O Contrato não pode ser cancelado pois possui o status de " + contract.Status + ".");
                }
            }
            else
            {
                context.AddFailure("O registro do Contrato não foi encontrado.");
            }
        }
    }
}
