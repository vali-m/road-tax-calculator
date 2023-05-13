using System.Linq.Expressions;

namespace hacktm.Repositories
{
    /// <summary>
    /// A generic repository to be used for overriding in a custom repository, or directly in a service
    /// </summary>
    /// <typeparam name="TEntity">The entity type for the repository</typeparam>
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Adds and entity if it doesn't already exist, otherwise updates it
        /// </summary>
        /// <param name="entity">The entity to be added/updated</param>
        Task AddOrUpdateAsync(TEntity entity);

        /// <summary>
        /// Adds a collection of entities, or updates them if they already exist in the database
        /// </summary>
        /// <param name="entities">The entities to be added/updated</param>
        Task AddOrUpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes an entity from the database
        /// </summary>
        /// <param name="entity">The entity to be deleted</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Retrieves an entity from the database, then deletes it
        /// </summary>
        /// <param name="id">The id of the entity to be deleted</param>
        Task DeleteAsync(object id);

        /// <summary>
        /// Gets a list of entities from the database
        /// </summary>
        /// <param name="filter">A lambda function to filter the entities</param>
        /// <param name="orderBy">A lambda function to sort the entities</param>
        /// <param name="includeProperties">A params list of names of navigation properties to be included</param>
        /// <param name="asNoTracking">Indicates whether the results should be tracked and cached by EF</param>
        /// <returns>A list of entities</returns>
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool asNoTracking = false,
            params string[] includeProperties);

        /// <summary>
        /// Gets an entity from the database by filtering using it's id
        /// </summary>
        /// <param name="id">The id of the property to filter</param>
        /// <returns>The entity</returns>
        Task<TEntity> GetAsync(object id, bool asNoTracking = false);

        /// <summary>
        /// Deletes a list of entities from the database
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task DeleteAsync(IEnumerable<TEntity> entities);
    }
}
