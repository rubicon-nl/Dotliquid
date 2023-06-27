namespace Rubicon.Demo.Api.Services.Models
{
    public class ContactDrop : DotLiquid.Drop
    {
        private readonly Domain.Contact _contact;

        public ContactDrop(Domain.Contact contact)
        {
            _contact = contact;
        }

        public string FirstName => _contact.FirstName;
        public string LastName => _contact.LastName;
        public List<string> Skills => _contact.Skills;
    }
}
