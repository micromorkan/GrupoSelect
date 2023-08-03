﻿using GrupoSelect.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> Users { get; }
        IBaseRepository<Profile> Profiles { get; }
        IBaseRepository<SystemLog> SystemLogs { get; }
        IBaseRepository<ErrorLog> ErrorLogs { get; }
        IBaseRepository<FinancialAdmin> FinancialAdmins { get; }
        IBaseRepository<ProductType> ProductTypes { get; }
        IBaseRepository<TableType> TableTypes { get; }
        IBaseRepository<Credit> Credits { get; }
        IBaseRepository<Client> Clients { get; }
        IBaseRepository<Proposal> Proposals { get; }
        IBaseRepository<Contract> Contracts { get; }
        void SaveAllChanges();
    }
}
