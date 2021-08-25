using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly IDbContext _context;
        private readonly string _table;

        public EmployeeRepository(IDbContext context) : base(context)
        {
            _context = context;
            _table = nameof(Employee);
        }
        
        public async Task<int> Count()
        {
            var result = await _context.ExecuteQuery(
                $"SELECT COUNT(*) FROM {_table} WHERE IsDeleted=0 or IsDeleted IS NULL");

            return Convert.ToInt32(result[0]);
        }

        public Task<IEnumerable<Employee>> GetPage(int skip, int take)
        {
            return _context.ExecuteQuery<Employee>(
                $"SELECT * FROM {_table} WHERE IsDeleted=0 or IsDeleted IS NULL ORDER BY Id OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY");
        }
    }
}