using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;

namespace Data.Repositories;

public class BaseRepository<TEntity>(IDbContext context) : IBaseRepository<TEntity>
    where TEntity : class, new()
{
    private readonly string _table = typeof(TEntity).Name;

    public Task<IEnumerable<TEntity>> GetAll() =>
        context.ExecuteQuery<TEntity>(
            $"SELECT * FROM {_table} WHERE IsDeleted=0 OR IsDeleted IS NULL ORDER BY Id DESC");

    public void Create(TEntity entity)
    {
        var fieldsBuilder = new StringBuilder();
        var valuesBuilder = new StringBuilder();

        var props = typeof(TEntity).GetProperties();

        for (var propIndex = 0; propIndex < props.Length; propIndex++)
        {
            if (props[propIndex].Name == "Id")
            {
                continue;
            }
                
            fieldsBuilder.Append(props[propIndex].Name);

            if (props[propIndex].PropertyType != typeof(int))
            {
                if (props[propIndex].PropertyType == typeof(string))
                {
                    valuesBuilder.Append('N');
                }
                valuesBuilder.Append("'");
                
                if (props[propIndex].PropertyType == typeof(DateTime))
                {
                    var date = Convert.ToDateTime(props[propIndex].GetValue(entity)).ToString("yyyy-MM-dd");
                    valuesBuilder.Append(date);
                }
                else
                {
                    valuesBuilder.Append(props[propIndex].GetValue(entity));
                }
                valuesBuilder.Append("'");
            }
            else
            {
                valuesBuilder.Append(props[propIndex].GetValue(entity));
            }

            if (propIndex == props.Length - 1)
            {
                break;
            }
                
            fieldsBuilder.Append(", ");
            valuesBuilder.Append(", ");
        }
            
        context.AddCommand($"INSERT INTO {_table} ({fieldsBuilder}) VALUES ({valuesBuilder})");
    }

    public void Update(TEntity entity)
    {
        var updateCommand = new StringBuilder();
        var id = -1;

        var props = typeof(TEntity).GetProperties();

        for (var propIndex = 0; propIndex < props.Length; propIndex++)
        {
            if (props[propIndex].Name == "Id")
            {
                id = Convert.ToInt32(props[propIndex].GetValue(entity));
                continue;
            }
                
            updateCommand.Append(props[propIndex].Name);
            updateCommand.Append("=");

            if (props[propIndex].PropertyType != typeof(int))
            {
                if (props[propIndex].PropertyType == typeof(string))
                {
                    updateCommand.Append('N');
                }
                updateCommand.Append("'");
                
                if (props[propIndex].PropertyType == typeof(DateTime))
                {
                    var date = Convert.ToDateTime(props[propIndex].GetValue(entity)).ToString("yyyy-MM-dd");
                    updateCommand.Append(date);
                }
                else
                {
                    updateCommand.Append(props[propIndex].GetValue(entity));
                }
                updateCommand.Append("'");
            }
            else
            {
                updateCommand.Append(props[propIndex].GetValue(entity));
            }
                
            if (propIndex == props.Length - 1) break;
                
            updateCommand.Append(", ");
        }

        context.AddCommand($"UPDATE {_table} SET {updateCommand} where Id={id}");
    }
        
    public void Delete(int id) => context.AddCommand($"UPDATE {_table} SET IsDeleted=1 WHERE Id={id}");

    public async Task<TEntity> GetById(int id)
    {
        var entities = await context.ExecuteQuery<TEntity>($"SELECT * FROM {_table} WHERE Id={id}");
            
        return entities.Single();
    }
}