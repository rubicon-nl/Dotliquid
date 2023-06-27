namespace Rubicon.Demo.Api.Controllers.V1.Dto.Mappers
{
    public static class ContactMapper
    {
        public static IEnumerable<Dto.Contact> Map(IEnumerable<Domain.Contact> source)
        {
            if (source == null)
            {
                return new List<Dto.Contact>();
            }

            return source.Select(sourceItem => Map(sourceItem));
        }


        public static Dto.Contact Map(Domain.Contact source)
        {
            return new Dto.Contact
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Skills = source.Skills,
            };
        }

        public static Domain.Contact Map(Dto.Contact source)
        {
            return new Domain.Contact
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Skills = source.Skills,
            };
        }

        public static Domain.Contact Map(Dto.ContactForUpsert source)
        {
            return new Domain.Contact
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                Skills = source.Skills,
            };
        }
    }
}
