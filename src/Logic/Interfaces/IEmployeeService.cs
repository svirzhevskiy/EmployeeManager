using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>Collection of employees</returns>
        Task<IEnumerable<EmployeeDTO>> GetAll();
        /// <summary>
        /// Get employees for specified page
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="itemsPerPage">Number of employees on page</param>
        /// <returns>Collection of employees</returns>
        Task<IEnumerable<EmployeeDTO>> GetAll(int page, int itemsPerPage);
        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="dto">Employee data transfer object</param>
        /// <returns>True if employee was successfully created</returns>
        Task<bool> Create(EmployeeDTO dto);
        /// <summary>
        /// Update data for employee
        /// </summary>
        /// <param name="dto">Employee data transfer object</param>
        /// <returns>True if employee was successfully updated</returns>
        Task<bool> Update(EmployeeDTO dto);
        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id">Id of employee</param>
        /// <returns>True if employee was successfully deleted</returns>
        Task<bool> Delete(int id);
        /// <summary>
        /// Get employee with specified id
        /// </summary>
        /// <param name="id">Id of employee</param>
        /// <returns>Employee data transfer object</returns>
        Task<EmployeeDTO> GetById(int id);
        /// <summary>
        /// Get possible positions for employee
        /// </summary>
        /// <returns>Collection of employee positions</returns>
        Task<IEnumerable<EnumItemDTO>> GetPositions();
        /// <summary>
        /// Get companies
        /// </summary>
        /// <returns>Collection of companies</returns>
        Task<IEnumerable<EnumItemDTO>> GetCompanies();
        /// <summary>
        /// Get total number of employees
        /// </summary>
        /// <returns>Number of employees</returns>
        Task<int> CountEmployees();
    }
}