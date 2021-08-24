using System;
using System.Collections.Generic;
using Data.Interfaces;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data.DatabaseProvider
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly string _connectionString;

        public DatabaseProvider(IDbSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T>(string sqlCommand) where T : class, new()
        {
            await using var connection = new SqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                    
                await using var command = new SqlCommand(sqlCommand, connection);
                await using var reader = await command.ExecuteReaderAsync();

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

        public async Task<bool> ExecuteCommand(string sqlCommand)
        {
            await using var connection = new SqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                
                await using var command = new SqlCommand(sqlCommand, connection);
                var affectedRows = await command.ExecuteNonQueryAsync();
                
                return affectedRows >= 1;
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

            for (var i = 0; i < data.Length; i++)
            {
                props[i].SetValue(entity, Convert.ChangeType(data[i], props[i].PropertyType));
            }
            
            return entity;
        }
    }
}