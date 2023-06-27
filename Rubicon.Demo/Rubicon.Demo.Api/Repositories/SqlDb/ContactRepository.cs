using Rubicon.Demo.Api.Repositories.SqlDb.Interfaces;

namespace Rubicon.Demo.Api.Repositories.SqlDb
{
    public class ContactRepository : BaseRepository<Domain.Contact>, IContactRepository
    {
        public ContactRepository(DatabaseContext db) : base(db)
        {
        }
    }
}
