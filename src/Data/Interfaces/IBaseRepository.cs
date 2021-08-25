using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
        Task<T> GetById(int id);
    }
}