using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;

namespace Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private readonly IDbContext _context;
        private readonly string _table;

        public BaseRepository(IDbContext context)
        {
            _context = context;
            _table = typeof(T).Name;
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return _context.ExecuteQuery<T>($"SELECT * FROM {_table} ORDER BY Id DESC");
        }

        public void Create(T entity)
        {
            var fieldsBuilder = new StringBuilder();
            var valuesBuilder = new StringBuilder();

            var props = typeof(T).GetProperties();

            for (var i = 0; i < props.Length; i++)
            {
                if (props[i].Name == "Id") continue;
                
                fieldsBuilder.Append(props[i].Name);

                if (props[i].PropertyType != typeof(int))
                {
                    valuesBuilder.Append("'");
                    valuesBuilder.Append(props[i].GetValue(entity));
                    valuesBuilder.Append("'");
                }
                else
                {
                    valuesBuilder.Append(props[i].GetValue(entity));
                }
                
                if (i == props.Length - 1) break;
                
                fieldsBuilder.Append(", ");
                valuesBuilder.Append(", ");
            }
            
            _context.AddCommand($"INSERT INTO {_table} ({fieldsBuilder}) VALUES ({valuesBuilder})");
        }

        public void Update(T entity)
        {
            var updateCommand = new StringBuilder();
            var id = -1;

            var props = typeof(T).GetProperties();

            for (var i = 0; i < props.Length; i++)
            {
                if (props[i].Name == "Id")
                {
                    id = Convert.ToInt32(props[i].GetValue(entity));
                    continue;
                }
                
                updateCommand.Append(props[i].Name);
                updateCommand.Append("=");

                if (props[i].PropertyType != typeof(int))
                {
                    updateCommand.Append("'");
                    updateCommand.Append(props[i].GetValue(entity));
                    updateCommand.Append("'");
                }
                else
                {
                    updateCommand.Append(props[i].GetValue(entity));
                }
                
                if (i == props.Length - 1) break;
                
                updateCommand.Append(", ");
            }

            _context.AddCommand($"UPDATE {_table} SET {updateCommand} where Id={id}");
        }
        
        public void Delete(int id)
        {
            _context.AddCommand($"DELETE FROM {_table} WHERE Id={id}");
        }

        public async Task<T> GetById(int id)
        {
            var entities = await _context.ExecuteQuery<T>($"SELECT * FROM {_table} WHERE Id={id}");
            
            return entities.Single();
        }
    }
}