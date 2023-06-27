using Rubicon.Demo.Api.Repositories.SqlDb.Models;
using System.ComponentModel.DataAnnotations;

namespace Rubicon.Demo.Api.Domain
{
    public class Contact : BaseEntity
    {
        public Contact()
        {
            Skills = new List<string>();
        }

        [MaxLength(128)]
        public string FirstName { get; set; }

        [MaxLength(128)]
        public string LastName { get; set; }
        public List<string> Skills { get; set; }
    }
}
