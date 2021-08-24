using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAll();
        Task<EmployeeDTO> Create(EmployeeDTO dto);
        Task<EmployeeDTO> Update(EmployeeDTO dto);
        Task Delete(int id);
        Task<EmployeeDTO> GetById(int id);
        Task<IEnumerable<EnumItemDTO>> GetPositions();
    }
}