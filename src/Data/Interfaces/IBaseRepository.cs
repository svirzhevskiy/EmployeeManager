using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Collection of entities</returns>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Insert new entity
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        void Create(T entity);
        /// <summary>
        /// Change the data of entity
        /// </summary>
        /// <param name="entity">Entity with updated fields</param>
        void Update(T entity);
        /// <summary>
        /// Delete entity. Entity will marked as deleted in db
        /// </summary>
        /// <param name="id">Id of entity</param>
        void Delete(int id);
        /// <summary>
        /// Get entity by specified id. This method ignores IsDeleted flag
        /// </summary>
        /// <param name="id">Id of entity</param>
        /// <returns>Entity with specified Id</returns>
        Task<T> GetById(int id);
    }
}