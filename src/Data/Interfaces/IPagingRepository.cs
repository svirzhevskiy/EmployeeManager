using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IPagingRepository<T> where T : class, new()
    {
        Task<int> Count();
        Task<IEnumerable<T>> GetPage(int skip, int take);
    }
}