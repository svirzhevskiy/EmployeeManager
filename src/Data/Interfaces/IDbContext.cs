using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDbContext
    {
        void AddCommand(string sqlCommand);
        Task<bool> SaveChanges();
        Task<IEnumerable<T>> ExecuteQuery<T>(string query) where T : class, new();
        Task<object[]> ExecuteQuery(string query);
    }
}