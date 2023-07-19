using GrupoSelect.Data.Context;
using GrupoSelect.Data.Repository;
using GrupoSelect.Domain.Interface;
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

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
