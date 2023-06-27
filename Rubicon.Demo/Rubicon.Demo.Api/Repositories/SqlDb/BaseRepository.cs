using Microsoft.EntityFrameworkCore;
using Rubicon.Demo.Api.Repositories.SqlDb.Models;

namespace Rubicon.Demo.Api.Repositories.SqlDb
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected DatabaseContext Db { get; private set; }

        public BaseRepository(DatabaseContext db)
        {
            Db = db;
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            return await Db.Set<T>().SingleOrDefaultAsync(d => d.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetAsync()
        {
            return await Db.Set<T>().ToListAsync();
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await Db.Set<T>().AnyAsync(d => d.Id == id);
        }

        public virtual async Task<T> CreateAsync(T model)
        {
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }

            model.CreatedAt = DateTime.Now;

            Db.Set<T>().Add(model);

            await Db.SaveChangesAsync();

            return model;
        }

        public virtual async Task<T?> UpdateAsync(Guid id, T model)
        {
            bool exists = await ExistsAsync(id);
            if (!exists)
            {
                return null;                
            }

            model.Id = id;
            Db.Set<T>().Update(model);

            model.ModifiedAt = DateTime.Now;

            await Db.SaveChangesAsync();

            return model;
        }

        public virtual async Task<T?> DeleteAsync(Guid id)
        {
            T model = await GetAsync(id);

            if (model == null)
            {
                return null;
            }
            
            Db.Set<T>().Remove(model);

            await Db.SaveChangesAsync();

            return model;
        }
    }
}
