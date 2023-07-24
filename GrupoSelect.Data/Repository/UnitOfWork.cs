using GrupoSelect.Data.Context;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;

namespace GrupoSelect.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GSDbContext _context;
        private IBaseRepository<SystemLog> _systemLogs;
        private IBaseRepository<ErrorLog> _errorLogs;
        private IBaseRepository<User> _usuarios;
        private IBaseRepository<Profile> _profiles;

        public UnitOfWork(GSDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<ErrorLog> ErrorLogs => _errorLogs ??= new BaseRepository<ErrorLog>(_context);
        public IBaseRepository<SystemLog> SystemLogs => _systemLogs ??= new BaseRepository<SystemLog>(_context);
        public IBaseRepository<User> Users => _usuarios ??= new BaseRepository<User>(_context);
        public IBaseRepository<Profile> Profiles => _profiles ??= new BaseRepository<Profile>(_context);

        public void SaveAllChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
