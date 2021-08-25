using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IPagingRepository<T> where T : class, new()
    {
        /// <summary>
        /// Get total number of employees. Employees with flag IsDeleted will ignored 
        /// </summary>
        /// /// <param name="filter">Filter</param>
        /// <returns>Number of employees</returns>
        Task<int> Count(string filter);
        /// <summary>
        /// Fetch specified number of employees with specified offset matching specified filter
        /// </summary>
        /// <param name="skip">Offset</param>
        /// <param name="take">Fetch</param>
        /// <param name="filter">Filter</param>
        /// <returns>Collection of employees</returns>
        Task<IEnumerable<T>> GetSortedPage(int skip, int take, string filter);
    }
}