using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Data.Interfaces;

namespace Data
{
    public class DbContext : IDbContext
    {
        private readonly List<string> _commands;
        private readonly string _connectionString;

        public DbContext(IDbSettings settings)
        {
            _connectionString = settings.ConnectionString;
            _commands = new List<string>();
        }
        
        public void AddCommand(string sqlCommand)
        {
            _commands.Add(sqlCommand);
        }
        
        public async Task<bool> SaveChanges()
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var transaction = connection.BeginTransaction();

            try
            {
                var commandTasks = new List<Task>();

                foreach (var cmdText in _commands)
                {
                    await using var sqlCommand = new SqlCommand(cmdText, connection, transaction);
                    commandTasks.Add(sqlCommand.ExecuteNonQueryAsync());
                }

                await Task.WhenAll(commandTasks);

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        
        public async Task<IEnumerable<T>> ExecuteQuery<T>(string query) where T : class, new()
        {
            await using var connection = new SqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                    
                await using var sqlCommand = new SqlCommand(query, connection);
                await using var reader = await sqlCommand.ExecuteReaderAsync();

                var result = new List<T>();
                    
                while (await reader.ReadAsync())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);

                    result.Add(CreateEntity<T>(values));
                }

                return result;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        
        private static T CreateEntity<T>(object[] data) where T : class, new()
        {
            var entity = new T();

            var props = entity.GetType().GetProperties();

            for (var i = 0; i < props.Length; i++)
            {
                props[i].SetValue(entity, Convert.ChangeType(data[i], props[i].PropertyType));
            }
            
            return entity;
        }
    }
}