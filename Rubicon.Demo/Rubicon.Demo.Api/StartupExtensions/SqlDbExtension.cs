using Microsoft.EntityFrameworkCore;
using Rubicon.Demo.Api.Configuration;
using Rubicon.Demo.Api.Repositories.SqlDb.Interfaces;
using Rubicon.Demo.Api.Repositories.SqlDb;

namespace Rubicon.Demo.Api.StartupExtensions
{
    public static class SqlDbExtension
    {
        public static void AddSqlDb(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlDbOptions = configuration.GetSection("Settings").GetSection("SqlDb").Get<SqlDbOptions>();

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(
                    sqlDbOptions.ConnectionString,
                    options => options.EnableRetryOnFailure()), ServiceLifetime.Scoped);
        }

        public static void UseSqlDb(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var databaseActions = scope.ServiceProvider.GetRequiredService<IDatabaseActions>() ?? throw new ArgumentNullException();

                databaseActions.ExecuteMigrations();
            }
        }
    }
}
