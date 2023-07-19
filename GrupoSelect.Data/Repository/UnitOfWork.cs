using GrupoSelect.Data.Context;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;

namespace GrupoSelect.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GSDbContext _context;
        private IBaseRepository<User> _usuarios;

        public UnitOfWork(GSDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<User> Users => _usuarios ??= new BaseRepository<User>(_context);

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
