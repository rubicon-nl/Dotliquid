namespace Rubicon.Demo.Api.Repositories.SqlDb.Interfaces
{
    public interface IDatabaseActions
    {
        bool CheckConnection();
        void ExecuteMigrations();
    }
}
