using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDatabaseProvider
    {
        Task<List<T>> ExecuteQuery<T>(string sqlCommand) where T : class, new();
        Task<bool> ExecuteCommand(string sqlCommand);
    }
}