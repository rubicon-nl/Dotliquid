using Rubicon.Demo.Api.Repositories.SqlDb.Interfaces;
using Rubicon.Demo.Api.Repositories.SqlDb;
using Rubicon.Demo.Api.Services.Interfaces;
using Rubicon.Demo.Api.Services;

namespace Rubicon.Demo.Api.StartupExtensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            // Services
            services.AddTransient<IHealthService, HealthService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<ITemplateService, TemplateService>();

            // Repositories
            services.AddTransient<IDatabaseActions, DatabaseActions>();
            services.AddTransient<IContactRepository, ContactRepository>();        }
    }
}
