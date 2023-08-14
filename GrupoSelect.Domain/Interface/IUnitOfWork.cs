using GrupoSelect.Domain.Entity;

namespace GrupoSelect.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> Users { get; }
        IBaseRepository<Profile> Profiles { get; }
        IBaseRepository<LogSystem> SystemLogs { get; }
        IBaseRepository<LogError> ErrorLogs { get; }
        IBaseRepository<FinancialAdmin> FinancialAdmins { get; }
        IBaseRepository<ProductType> ProductTypes { get; }
        IBaseRepository<TableType> TableTypes { get; }
        IBaseRepository<Credit> Credits { get; }
        IBaseRepository<Client> Clients { get; }
        IBaseRepository<Proposal> Proposals { get; }
        IBaseRepository<Contract> Contracts { get; }
        IBaseRepository<ContractHistoric> ContractHistorics { get; }
        IBaseRepository<ContractConfig> ContractConfigs { get; }
        void SaveAllChanges();
    }
}
