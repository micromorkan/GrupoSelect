using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.Interface;
using System.Text.Json;

namespace GrupoSelect.Services.Service
{
    public class LogService : ILogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserSession _userSession;
        public LogService(IUnitOfWork unitOfWork, ISessionProvider sessionProvider)
        {
            _unitOfWork = unitOfWork;
            _userSession = sessionProvider.Get();
        }

        public void LogException(Exception ex)
        {
            SystemLog log = new SystemLog();

            log.Object = JsonSerializer.Serialize(ex.Data[Constants.SYSTEM_EXCEPTION_OBJ]);
            log.Method = ex.TargetSite.DeclaringType?.FullName;
            log.Message = ex.Message;
            log.Username = _userSession.UserName;

            _unitOfWork.SystemLogs.Insert(log);
            _unitOfWork.SystemLogs.Save();
        }

        public void LogUser(User user)
        {
        }

        public void LogAction(User user)
        {
        }
    }
}
