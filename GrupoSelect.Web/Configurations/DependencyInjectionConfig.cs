﻿using FluentValidation;
using GrupoSelect.Data.Repository;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Services.FluentValidation;
using GrupoSelect.Services.Interface;
using GrupoSelect.Services.Service;
using GrupoSelect.Web.Helpers;

namespace GrupoSelect.Web.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccessService, AccessService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IFinancialAdminService, FinancialAdminService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<ITableTypeService, TableTypeService>();
            services.AddScoped<ICreditService, CreditService>();
            services.AddScoped<IProposalService, ProposalService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IBorderoService, BorderoService>();

            services.AddScoped<ISessionProvider, SessionProvider>();
            services.AddValidatorsFromAssemblyContaining<UserValidator>();
            services.AddTransient<SecurityAttribute>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ILogService, LogService>();

            return services;
        }
    }
}
