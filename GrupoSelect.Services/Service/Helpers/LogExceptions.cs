using GrupoSelect.Domain.Interface;
using GrupoSelect.Services.Interface.Helpers;

namespace GrupoSelect.Services.Service.Helpers
{
    public class LogExceptions : ILogExceptions
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogExceptions(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Log(Exception ex)
        {
            string message = ex.Message;
        }
    }
}
