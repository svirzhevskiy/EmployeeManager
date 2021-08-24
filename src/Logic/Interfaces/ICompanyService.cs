using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic.Interfaces
{
    public interface ICompanyService
    {
        Task<List<CompanyDTO>> GetAll();
        Task<bool> Create(CompanyDTO dto);
        Task<bool> Update(CompanyDTO dto);
        Task<bool> Delete(int id);
        Task<CompanyDTO> GetById(int id);
    }
}