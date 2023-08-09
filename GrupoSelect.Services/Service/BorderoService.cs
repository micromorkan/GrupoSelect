using FluentValidation;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using System.Reflection;

namespace GrupoSelect.Services.Service
{
    public class BorderoService : IBorderoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BorderoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                Object = _unitOfWork.Contracts.GetAll(f => f.Proposal.UserId == userId &&
                                                           f.Status == Constants.CONTRACT_STATUS_CA &&
                                                           f.DateAproved >= startDate && 
                                                           f.DateAproved <= endDate, o => o.OrderBy(x => x.ContractNum), i => i.Proposal.User, i => i.Proposal.Client),
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
                Object = _unitOfWork.Contracts.GetAll(f => (f.Proposal.UserId == userId || f.Proposal.User.Profile == Constants.PROFILE_REPRESENTANTE) &&
                                                           f.Status == Constants.CONTRACT_STATUS_CA &&
                                                           f.DateAproved >= startDate &&
                                                           f.DateAproved <= endDate, o => o.OrderBy(x => x.ContractNum), i => i.Proposal.User, i => i.Proposal.Client),
            };
        }
    }
}