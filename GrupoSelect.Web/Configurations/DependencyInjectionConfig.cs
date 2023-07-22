using FluentValidation;
using GrupoSelect.Data.Repository;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Util;
using GrupoSelect.Services.FluentValidation.User;
using GrupoSelect.Services.Interface;
using GrupoSelect.Services.Service;

namespace GrupoSelect.Web.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccessService, AccessService>();
            services.AddScoped<IProfileService, ProfileService>();
            
            services.AddScoped<ISessionProvider, SessionProvider>();
            services.AddValidatorsFromAssemblyContaining<UserValidator>();
            
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ILogService, LogService>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1800);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            return services;
        }
    }
}
