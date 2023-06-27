using Rubicon.Demo.Api.Repositories.SqlDb.Models;

namespace Rubicon.Demo.Api.Repositories.SqlDb.Interfaces
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAsync();
        Task<bool> ExistsAsync(Guid id);
        Task<T> CreateAsync(T model);
        Task<T?> UpdateAsync(Guid id, T model);
        Task<T?> DeleteAsync(Guid id);
    }
}
