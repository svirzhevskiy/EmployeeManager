using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAll();
        Task<EmployeeDTO> Create(EmployeeDTO dto);
        Task<EmployeeDTO> Update(EmployeeDTO dto);
        Task Delete(int id);
    }
}