using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDbContext
    {
        /// <summary>
        /// Adding new command to the command list for execution
        /// </summary>
        /// <param name="sqlCommand">Text SQL command</param>
        void AddCommand(string sqlCommand);
        /// <summary>
        /// Execute all commands added to the command list for execution
        /// </summary>
        /// <returns>True if the transaction was successful</returns>
        Task<bool> SaveChanges();
        /// <summary>
        /// Execute query and get result mapped in specified type
        /// </summary>
        /// <param name="query">Text SQL query</param>
        /// <typeparam name="T">Result object type</typeparam>
        /// <returns>Collection of objects received on request</returns>
        Task<IEnumerable<T>> ExecuteQuery<T>(string query) where T : class, new();
        /// <summary>
        /// Execute query and get first row of the result as object[]
        /// </summary>
        /// <param name="query">Text SQL query</param>
        /// <returns>First row of the result set</returns>
        Task<object[]> ExecuteQuery(string query);
    }
}