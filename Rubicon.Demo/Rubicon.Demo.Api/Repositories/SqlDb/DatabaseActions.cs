using Microsoft.EntityFrameworkCore;
using Rubicon.Demo.Api.Repositories.SqlDb.Interfaces;

namespace Rubicon.Demo.Api.Repositories.SqlDb
{
    public class DatabaseActions : IDatabaseActions
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<DatabaseActions> _logger;

        public DatabaseActions(
            DatabaseContext databaseContext, 
            ILogger<DatabaseActions> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public bool CheckConnection()
        {
            try
            {
                _databaseContext.Database.OpenConnection();
                _databaseContext.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Connection check to SQL Database failed");
                return false;
            }
            return true;
        }

        public void ExecuteMigrations()
        {
            var success = CheckConnection();

            if (!success)
            {
                _logger.LogError("Entity Framework Migration CheckConnection Failed");
                return;
            }

            try
            {
                _databaseContext.Database.SetCommandTimeout(1000);
                _databaseContext.Database.Migrate();
                _databaseContext.Database.SetCommandTimeout(30);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Entity Framework Migration Failed");
            }
        }
    }
}
