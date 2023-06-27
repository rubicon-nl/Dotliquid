using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Rubicon.Demo.Api.Repositories.SqlDb
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public virtual DbSet<Domain.Contact> Contacts{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Contact>()
                .HasKey(contact => contact.Id);

            modelBuilder.Entity<Domain.Contact>()
                .Property(contact => contact.Skills)
                .HasConversion(
                    from => string.Join(";", from),
                    to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList(),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                ));               

            base.OnModelCreating(modelBuilder);
        }
    }
}
