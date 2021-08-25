using System.Threading.Tasks;
using Data.Entities;

namespace Data.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Applies changes to database
        /// </summary>
        /// <returns>True if changes were successfully applied</returns>
        Task<bool> Commit();
        /// <summary>
        /// Collection of companies stored in database
        /// </summary>
        IBaseRepository<Company> Companies { get; }
        /// <summary>
        /// Collection of positions stored in database
        /// </summary>
        IBaseRepository<Position> Positions { get; }
        /// <summary>
        /// Collection of employees stored in database
        /// </summary>
        IEmployeeRepository Employees { get; }
        /// <summary>
        /// Collection of legal forms stored in database
        /// </summary>
        IBaseRepository<LegalForm> LegalForms { get; }
    }
}