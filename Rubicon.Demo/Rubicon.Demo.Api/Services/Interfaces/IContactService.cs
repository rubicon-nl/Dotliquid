using Rubicon.Demo.Api.Domain;

namespace Rubicon.Demo.Api.Services.Interfaces
{
    public interface IContactService
    {
        Task<Result<Contact>> GetAsync(Guid Id);
        Task<Result<IEnumerable<Contact>>> GetAsync();
        Task<bool> ExistsAsync(Guid id);
        Task<Result<Contact>> CreateAsync(Contact model);
        Task<Result<Contact>> UpdateAsync(Guid id, Contact model);
        Task<Result<Contact>> DeleteAsync(Guid id);
    }
}
