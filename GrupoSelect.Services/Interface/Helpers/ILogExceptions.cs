using GrupoSelect.Domain.Interface;

namespace GrupoSelect.Services.Interface.Helpers
{
    public interface ILogExceptions
    {
        void Log(Exception ex);
    }
}
