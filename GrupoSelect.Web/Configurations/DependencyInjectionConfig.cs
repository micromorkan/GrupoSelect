using FluentValidation;
using GrupoSelect.Data.Repository;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Services.FluentValidation.User;
using GrupoSelect.Services.Interface;
using GrupoSelect.Services.Interface.Helpers;
using GrupoSelect.Services.Service;
using GrupoSelect.Services.Service.Helpers;

namespace GrupoSelect.Web.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccessService, AccessService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddValidatorsFromAssemblyContaining<UserValidator>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ILogExceptions, LogExceptions>();

            return services;
        }
    }
}
