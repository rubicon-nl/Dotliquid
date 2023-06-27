using Rubicon.Demo.Api.Repositories.SqlDb.Interfaces;
using Rubicon.Demo.Api.Services.Interfaces;

namespace Rubicon.Demo.Api.Services
{
    public class HealthService : IHealthService
    {
        private readonly IDatabaseActions _databaseActions;
        private readonly ILogger<HealthService> _logger;

        public HealthService(
            IDatabaseActions databaseActions, 
            ILogger<HealthService> logger)
        {
            _databaseActions = databaseActions;
            _logger = logger;
        }

        public async Task<bool> IsHealthy()
        {
            var isHealthy = true;

            if (!SqlDatabaseConnected())
            {
                isHealthy = false;
            }

            return isHealthy;
        }

        private bool SqlDatabaseConnected()
        {
            try
            {
                return _databaseActions.CheckConnection();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HealthCheck SqlDatabaseConnected Failed");
                return false;
            }
        }
    }
}
