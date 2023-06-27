using Rubicon.Demo.Api.Domain;
using Rubicon.Demo.Api.Repositories.SqlDb.Interfaces;
using Rubicon.Demo.Api.Services.Interfaces;

namespace Rubicon.Demo.Api.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Result<Domain.Contact>> GetAsync(Guid Id)
        {
            Domain.Contact contact = await _contactRepository.GetAsync(Id);

            if (contact == null)
            {
                return new Result<Domain.Contact>(Domain.Enums.ResultStatus.NotFound);
            }

            return new Result<Domain.Contact>(contact);
        }

        public async Task<Result<IEnumerable<Domain.Contact>>> GetAsync()
        {
            IEnumerable<Domain.Contact> contactCollection = await _contactRepository.GetAsync();

            if (contactCollection == null)
            {
                return new Result<IEnumerable<Domain.Contact>>(Domain.Enums.ResultStatus.NotFound);
            }

            return new Result<IEnumerable<Domain.Contact>>(contactCollection);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _contactRepository.ExistsAsync(id);
        }


        public async Task<Result<Domain.Contact>> CreateAsync(Domain.Contact model)
        {
            Domain.Contact contact = await _contactRepository.CreateAsync(model);

            return new Result<Domain.Contact>(contact);
        }

        public async Task<Result<Domain.Contact?>> UpdateAsync(Guid id, Domain.Contact model)
        {
            Domain.Contact contact = await _contactRepository.UpdateAsync(id, model);

            if (contact == null)
            {
                return new Result<Domain.Contact>(Domain.Enums.ResultStatus.NotFound);
            }

            return new Result<Domain.Contact>(contact);
        }

        public async Task<Result<Domain.Contact>> DeleteAsync(Guid id)
        {
            Domain.Contact? contact = await _contactRepository.DeleteAsync(id);

            if (contact == null)
            {
                return new Result<Domain.Contact>(Domain.Enums.ResultStatus.NotFound);
            }

            return new Result<Domain.Contact>(contact);
        }
    }
}
