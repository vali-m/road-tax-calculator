using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace hacktm.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _set;

        public GenericRepository(TContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public virtual async Task AddOrUpdateAsync(TEntity entity)
        {
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrUpdateAsync(IEnumerable<TEntity> entities)
        {
            _set.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await this.GetAsync(id);
            await this.DeleteAsync(entity);
        }

        public async Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            bool asNoTracking = false,
            params string[] includeProperties)
        {
            var query = this.GetQuery();

            if (includeProperties is { Length: > 0 })
            {
                query = includeProperties.Aggregate(query, (current, prop) => current.Include(prop));
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(object id, bool asNoTracking = false)
        {
            var entity = await _set.FindAsync(id);

            if (entity != null && asNoTracking)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            _set.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        protected IQueryable<TEntity> GetQuery()
        {
            return _set.AsQueryable().AsNoTracking();
        }
    }
}
