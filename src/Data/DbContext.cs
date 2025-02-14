using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Data.Interfaces;

namespace Data;

public class DbContext : IDbContext
{
    private readonly List<string> _commands;
    private readonly string _connectionString;

    public DbContext(IDbSettings settings)
    {
        _connectionString = settings.ConnectionString;
        _commands = new List<string>();
    }

    public DbContext(List<string> commands, string connectionString)
    {
        _commands = commands;
        _connectionString = connectionString;
    }

    public void AddCommand(string sqlCommand) => _commands.Add(sqlCommand);

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
        
    public async Task<IEnumerable<TEntity>> ExecuteQuery<TEntity>(string query) where TEntity : class, new()
    {
        await using var connection = new SqlConnection(_connectionString);
        try
        {
            await connection.OpenAsync();
                    
            await using var sqlCommand = new SqlCommand(query, connection);
            await using var reader = await sqlCommand.ExecuteReaderAsync();

            var result = new List<TEntity>();
                    
            while (await reader.ReadAsync())
            {
                var values = new object[reader.FieldCount];
                reader.GetValues(values);

                result.Add(CreateEntity<TEntity>(values));
            }

            return result;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task<object[]> ExecuteQuery(string query)
    {
        await using var connection = new SqlConnection(_connectionString);
        try
        {
            await connection.OpenAsync();
                    
            await using var sqlCommand = new SqlCommand(query, connection);
            await using var reader = await sqlCommand.ExecuteReaderAsync();

            await reader.ReadAsync();

            var values = new object[reader.FieldCount];
            reader.GetValues(values);
                
            return values;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
        
    private static TEntity CreateEntity<TEntity>(object[] data) where TEntity : class, new()
    {
        var entity = new TEntity();

        var props = entity.GetType().GetProperties();

        for (var propIndex = 0; propIndex < props.Length; propIndex++)
        {
            props[propIndex].SetValue(entity, Convert.ChangeType(data[propIndex], props[propIndex].PropertyType));
        }
            
        return entity;
    }
}