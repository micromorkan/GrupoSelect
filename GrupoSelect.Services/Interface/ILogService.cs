using GrupoSelect.Domain.Interface;

namespace GrupoSelect.Services.Interface
{
    public interface ILogService
    {
        void LogException(Exception ex);
    }
}
