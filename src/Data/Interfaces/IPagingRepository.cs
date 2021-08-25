using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IPagingRepository<T> where T : class, new()
    {
        /// <summary>
        /// Get total number of employees. Employees with flag IsDeleted will ignored 
        /// </summary>
        /// <returns>Number of employees</returns>
        Task<int> Count();
        /// <summary>
        /// Fetch specified number of employees with specified offset
        /// </summary>
        /// <param name="skip">Offset</param>
        /// <param name="take">Fetch</param>
        /// <returns>Collection of employees</returns>
        Task<IEnumerable<T>> GetPage(int skip, int take);
    }
}