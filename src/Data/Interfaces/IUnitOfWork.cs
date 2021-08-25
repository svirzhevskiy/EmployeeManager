using System.Threading.Tasks;
using Data.Entities;

namespace Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        IBaseRepository<Company> Companies { get; }
        IBaseRepository<Position> Positions { get; }
        IBaseRepository<Employee> Employees { get; }
        IBaseRepository<LegalForm> LegalForms { get; }
    }
}