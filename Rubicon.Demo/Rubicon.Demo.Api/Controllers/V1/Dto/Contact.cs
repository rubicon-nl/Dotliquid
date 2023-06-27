using System.ComponentModel.DataAnnotations;

namespace Rubicon.Demo.Api.Controllers.V1.Dto
{
    public class Contact
    {
        public Contact()
        {
            Skills = new List<string>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Skills { get; set; }
    }
}
