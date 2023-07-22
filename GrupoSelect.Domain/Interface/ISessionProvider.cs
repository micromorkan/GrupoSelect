using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Util;

namespace GrupoSelect.Domain.Interface
{
    public interface ISessionProvider
    {
        void Set(int userId, string userName, string profile);

        UserSession Get();
    }
}
