using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using System.Reflection;

namespace GrupoSelect.Services.Service
{
    public class BorderoService : IDisposable, IBorderoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContractRepository _contractRepository;

        public BorderoService(IUnitOfWork unitOfWork, IContractRepository contractRepository)
        {
            _unitOfWork = unitOfWork;
            _contractRepository = contractRepository;
        }

        public async Task<Result<IEnumerable<Contract>>> GetAll(int userId, DateTime startDate, DateTime endDate)
        {
            string messageError = string.Empty;

            if (userId == 0)
            {
                messageError = "Selecione um Representante!";
            }
            else if (startDate == DateTime.MinValue)
            {
                messageError = "Data Início é inválida.";
            }
            else if (endDate == DateTime.MinValue)
            {
                messageError = "Data Fim é inválida.";
            }

            if (!string.IsNullOrEmpty(messageError))
            {
                return new Result<IEnumerable<Contract>>
                {
                    Success = false,
                    Object = null,
                    Message = messageError
                };
            }

            return new Result<IEnumerable<Contract>>
            {
                Success = true,
                Object = await _contractRepository.GetAllBordero(userId, startDate, endDate.AddDays(1).AddSeconds(-1)),
            };
        }

        public async Task<Result<IEnumerable<Contract>>> GetAllManager(int userId, DateTime startDate, DateTime endDate)
        {
            string messageError = string.Empty;

            if (userId == 0)
            {
                messageError = "Selecione um Representante!";
            }
            else if (startDate == DateTime.MinValue)
            {
                messageError = "Data Início é inválida.";
            }
            else if (endDate == DateTime.MinValue)
            {
                messageError = "Data Fim é inválida.";
            }

            if (!string.IsNullOrEmpty(messageError))
            {
                return new Result<IEnumerable<Contract>>
                {
                    Success = false,
                    Object = null,
                    Message = messageError
                };
            }

            return new Result<IEnumerable<Contract>>
            {
                Success = true,
                Object = await _contractRepository.GetAllBorderoManager(userId, startDate, endDate.AddDays(1).AddSeconds(-1)),
            };
        }

        public async Task<Result<IEnumerable<Contract>>> GetAllLawyer(int userId, DateTime startDate, DateTime endDate)
        {
            string messageError = string.Empty;

            if (userId == 0)
            {
                messageError = "Selecione um Advogado!";
            }
            else if (startDate == DateTime.MinValue)
            {
                messageError = "Data Início é inválida.";
            }
            else if (endDate == DateTime.MinValue)
            {
                messageError = "Data Fim é inválida.";
            }

            if (!string.IsNullOrEmpty(messageError))
            {
                return new Result<IEnumerable<Contract>>
                {
                    Success = false,
                    Object = null,
                    Message = messageError
                };
            }

            return new Result<IEnumerable<Contract>>
            {
                Success = true,
                Object = await _contractRepository.GetAllBorderoManager(userId, startDate, endDate.AddDays(1).AddSeconds(-1)),
            };
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}