using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic.Interfaces
{
    public interface ICompanyService
    {
        Task<List<CompanyDTO>> GetAll();
        Task<CompanyDTO> Create(CompanyDTO dto);
        Task<CompanyDTO> Update(CompanyDTO dto);
        Task Delete(int id);
    }
}