using System.Threading.Tasks;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;
        public IBaseRepository<Company> Companies { get; }
        public IBaseRepository<Position> Positions { get; }
        public IBaseRepository<Employee> Employees { get; }
        public IBaseRepository<LegalForm> LegalForms { get; }

        public UnitOfWork(IDbContext context)
        {
            _context = context;
            Companies = new BaseRepository<Company>(_context);
            Positions = new BaseRepository<Position>(_context);
            Employees = new BaseRepository<Employee>(_context);
            LegalForms = new BaseRepository<LegalForm>(_context);
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChanges();
        }
    }
}