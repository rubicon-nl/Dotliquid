namespace Rubicon.Demo.Api.Controllers.V1.Dto
{
    public class ContactForUpsert
    {
        public ContactForUpsert()
        {
            Skills = new List<string>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Skills { get; set; }
    }
}
