using FluentValidation;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;
using System.Globalization;

namespace GrupoSelect.Services.FluentValidation
{
    public class ClientValidator : AbstractValidator<Domain.Entity.Client>
    {
        private IUnitOfWork _unitOfWork;

        public ClientValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet(Constants.FLUENT_INSERT, () =>
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Informe o Nome do cliente");
                RuleFor(x => x.CPF).NotEmpty().Length(14).WithMessage("Informe o CPF");
                RuleFor(x => x.RG).NotEmpty().Length(13).WithMessage("Informe o RG");
                RuleFor(x => x.Sex).NotEmpty().WithMessage("Informe o Sexo");
                RuleFor(x => x.NaturalFrom).NotEmpty().WithMessage("Informe o campo Natural");
                RuleFor(x => x.Nationality).NotEmpty().WithMessage("Informe a Naturalidade");
                RuleFor(x => x.MaritalStatus).NotEmpty().WithMessage("Informe o Estado Civil");
                RuleFor(x => x.DateExp).NotEmpty().WithMessage("Informe a Data de Expedição do documento");
                RuleFor(x => x.OrganExp).NotEmpty().WithMessage("Informe o Orgão Expedidor do documento");
                RuleFor(x => x.Contact).NotEmpty().WithMessage("Informe o telefone para contato");
                RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Informe um Email válido");
                RuleFor(x => x.Profession).NotEmpty().WithMessage("Informe a Profissão");
                RuleFor(x => x.Income).NotEmpty().WithMessage("Informe a Renda");
                RuleFor(x => x.Address).NotEmpty().WithMessage("Informe o Endereço");
                RuleFor(x => x.Neighborhood).NotEmpty().WithMessage("Informe o Bairro");
                RuleFor(x => x.Cep).NotEmpty().WithMessage("Informe o CEP");
                RuleFor(x => x.City).NotEmpty().WithMessage("Informe a Cidade");
                RuleFor(x => x.State).NotEmpty().WithMessage("Informe o Estado");

                RuleFor(x => x).Custom(InsertUniqueClient);
                RuleFor(x => x).Custom(ValidateMoney);
                RuleFor(x => x).Custom(ValidateDateBirth);
                RuleFor(x => x).Custom(ValidateCpf);
            });

            RuleSet(Constants.FLUENT_UPDATE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do usuário é inválido.");
                RuleFor(x => x.Name).NotEmpty().WithMessage("Informe o Nome do cliente");
                RuleFor(x => x.CPF).NotEmpty().Length(14).WithMessage("Informe o CPF");
                RuleFor(x => x.RG).NotEmpty().Length(13).WithMessage("Informe o RG");
                RuleFor(x => x.Sex).NotEmpty().WithMessage("Informe o Sexo");
                RuleFor(x => x.NaturalFrom).NotEmpty().WithMessage("Informe o campo Natural");
                RuleFor(x => x.Nationality).NotEmpty().WithMessage("Informe a Naturalidade");
                RuleFor(x => x.MaritalStatus).NotEmpty().WithMessage("Informe o Estado Civil");
                RuleFor(x => x.DateExp).NotEmpty().WithMessage("Informe a Data de Expedição do documento");
                RuleFor(x => x.OrganExp).NotEmpty().WithMessage("Informe o Orgão Expedidor do documento");
                RuleFor(x => x.Contact).NotEmpty().WithMessage("Informe o telefone para contato");
                RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Informe um Email válido");
                RuleFor(x => x.Profession).NotEmpty().WithMessage("Informe a Profissão");
                RuleFor(x => x.Income).NotEmpty().WithMessage("Informe a Renda");
                RuleFor(x => x.Address).NotEmpty().WithMessage("Informe o Endereço");
                RuleFor(x => x.Neighborhood).NotEmpty().WithMessage("Informe o Bairro");
                RuleFor(x => x.Cep).NotEmpty().WithMessage("Informe o CEP");
                RuleFor(x => x.City).NotEmpty().WithMessage("Informe a Cidade");
                RuleFor(x => x.State).NotEmpty().WithMessage("Informe o Estado");

                RuleFor(x => x).Custom(EditUniqueClient);
                RuleFor(x => x).Custom(ValidateMoney);
                RuleFor(x => x).Custom(ValidateDateBirth);
                RuleFor(x => x).Custom(ValidateCpf);
            });

            RuleSet(Constants.FLUENT_DELETE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do Cliente é inválido.");
            });
        }

        private void InsertUniqueClient(Domain.Entity.Client model, ValidationContext<Domain.Entity.Client> context)
        {
            var result = _unitOfWork.Clients.GetAll(filter => filter.CPF == model.CPF);

            if (result.Any())
            {
                context.AddFailure("Já existe Cliente com este CPF cadastrado.");
            }
        }

        private void EditUniqueClient(Domain.Entity.Client model, ValidationContext<Domain.Entity.Client> context)
        {
            var result = _unitOfWork.Clients.GetAll(filter => filter.CPF == model.CPF &&
                                                            filter.Id != model.Id);

            if (result.Any())
            {
                context.AddFailure("Já existe Cliente com este CPF cadastrado.");
            }
        }
        private void ValidateMoney(Domain.Entity.Client model, ValidationContext<Domain.Entity.Client> context)
        {
            decimal incomeValue = 0;
            if (Decimal.TryParse(model.Income, NumberStyles.Any, new CultureInfo("pt-BR"), out incomeValue))
            {
                if (incomeValue <= 0)
                {
                    context.AddFailure("Valor da Renda deve ser maior que 0,00.");
                }
            }
            else
            {
                context.AddFailure("Valor da Renda inválido.");
            }
        }
        private void ValidateDateBirth(Domain.Entity.Client model, ValidationContext<Domain.Entity.Client> context)
        {
            DateTime datebirth = DateTime.Now;

            if (DateTime.TryParse(model.DateBirth, out datebirth))
            {
                if (datebirth == DateTime.MinValue)
                {
                    context.AddFailure("Data de Nascimento inválido");
                }
                else if (datebirth.Date > DateTime.Now.AddYears(-18))
                {
                    context.AddFailure("O Cliente deve ser maior de idade");
                }
            }
            else
            {
                context.AddFailure("Data de Nascimento inválido");
            }

        }
        private void ValidateCpf(Domain.Entity.Client model, ValidationContext<Domain.Entity.Client> context)
        {
            if (!Functions.IsCpf(model.CPF))
            {
                context.AddFailure("CPF Inválido");
            }
        }
    }
}
