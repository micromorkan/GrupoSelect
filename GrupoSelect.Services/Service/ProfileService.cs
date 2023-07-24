using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Services.Interface;
using GrupoSelect.Domain.Model;
using GrupoSelect.Domain.Util;

namespace GrupoSelect.Services.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;

        public ProfileService(IUnitOfWork unitOfWork, ILogService logService)
        {
            _unitOfWork = unitOfWork;
            _logService = logService;
        }

        public async Task<Result<IEnumerable<Profile>>> GetAll(Profile filter)
        {
            try
            {
                return new Result<IEnumerable<Profile>>
                {
                    Success = true,
                    Object = _unitOfWork.Profiles.GetAll(f => (string.IsNullOrEmpty(filter.Name) || f.Name.Contains(filter.Name)) && f.Active == filter.Active),
                };
            }
            catch (Exception ex)
            {
                ex.Data.Add(Constants.SYSTEM_EXCEPTION_OBJ, filter);
                _logService.LogException(ex);

                return new Result<IEnumerable<Profile>>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Result<Profile>> GetById(int id)
        {
            try
            {
                Profile profile = _unitOfWork.Profiles.GetAll(f => f.Id == id).FirstOrDefault();

                if (profile != null)
                {
                    return new Result<Profile>
                    {
                        Success = true,
                        Object = profile
                    };
                }
                else
                {
                    return new Result<Profile>
                    {
                        Success = false,
                        Message = "O id informado não foi encontrado."
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Profile>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}