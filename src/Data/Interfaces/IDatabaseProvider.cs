using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDatabaseProvider
    {
        Task<IEnumerable<T>> ExecuteQuery<T>(string sqlCommand) where T : class, new();
        Task<bool> ExecuteCommand(string sqlCommand);
    }
}