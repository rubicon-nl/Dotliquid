namespace Rubicon.Demo.Api.Services.Interfaces
{
    public interface IHealthService
    {
        Task<bool> IsHealthy();
    }
}
