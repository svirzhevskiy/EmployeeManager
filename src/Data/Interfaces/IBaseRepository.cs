using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {
        Task<List<T>> GetAll();
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
        Task<T> GetById(int id);
    }
}