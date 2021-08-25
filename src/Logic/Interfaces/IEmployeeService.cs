using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAll();
        Task<bool> Create(EmployeeDTO dto);
        Task<bool> Update(EmployeeDTO dto);
        Task<bool> Delete(int id);
        Task<EmployeeDTO> GetById(int id);
        Task<IEnumerable<EnumItemDTO>> GetPositions();
        Task<IEnumerable<EnumItemDTO>> GetCompanies();
    }
}