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
        private IBaseRepository<FinancialAdmin> _financialAdmins;
        private IBaseRepository<ProductType> _productTypes;
        private IBaseRepository<TableType> _tableTypes;
        private IBaseRepository<Credit> _credits;
        private IBaseRepository<Proposal> _proposals;
        private IBaseRepository<Client> _clients;
        private IBaseRepository<Contract> _contracts;

        public UnitOfWork(GSDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<ErrorLog> ErrorLogs => _errorLogs ??= new BaseRepository<ErrorLog>(_context);
        public IBaseRepository<SystemLog> SystemLogs => _systemLogs ??= new BaseRepository<SystemLog>(_context);
        public IBaseRepository<User> Users => _usuarios ??= new BaseRepository<User>(_context);
        public IBaseRepository<Profile> Profiles => _profiles ??= new BaseRepository<Profile>(_context);
        public IBaseRepository<FinancialAdmin> FinancialAdmins => _financialAdmins ??= new BaseRepository<FinancialAdmin>(_context);
        public IBaseRepository<ProductType> ProductTypes => _productTypes ??= new BaseRepository<ProductType>(_context);
        public IBaseRepository<TableType> TableTypes => _tableTypes ??= new BaseRepository<TableType>(_context);
        public IBaseRepository<Credit> Credits => _credits ??= new BaseRepository<Credit>(_context);
        public IBaseRepository<Client> Clients => _clients ??= new BaseRepository<Client>(_context);
        public IBaseRepository<Proposal> Proposals => _proposals ??= new BaseRepository<Proposal>(_context);
        public IBaseRepository<Contract> Contracts => _contracts ??= new BaseRepository<Contract>(_context);

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
