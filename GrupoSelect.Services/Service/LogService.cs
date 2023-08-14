using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using GrupoSelect.Domain.Models;

namespace GrupoSelect.Services.Service
{
    public class LogService : ILogService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private UserSession _userSession;

        public LogService(IUnitOfWork unitOfWork, ISessionProvider sessionProvider, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userSession = sessionProvider.Get();
            _configuration = configuration;
        }

        public void LogException(Exception ex)
        {
            if (Convert.ToBoolean(_configuration.GetSection(Constants.SYSTEM_SETTINGS)[Constants.SYSTEM_SETTINGS_REGISTERERRORLOG]))
            {
                LogError log = new LogError();

                log.Object = JsonSerializer.Serialize(ex.Data[Constants.SYSTEM_EXCEPTION_OBJ]);
                log.Method = ex.TargetSite.DeclaringType?.FullName;
                log.Message = ex.Message;
                log.Username = _userSession.UserName;

                _unitOfWork.ErrorLogs.Insert(log);
                _unitOfWork.ErrorLogs.Save();
            }
        }
    }
}
