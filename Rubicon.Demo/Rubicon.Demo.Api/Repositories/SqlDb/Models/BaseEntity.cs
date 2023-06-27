using System.ComponentModel.DataAnnotations;

namespace Rubicon.Demo.Api.Repositories.SqlDb.Models
{
    public class BaseEntity
    {
        [Key()]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
