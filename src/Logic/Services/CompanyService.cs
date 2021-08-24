using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;
using Logic.Interfaces;

namespace Logic.Services
{
    public class CompanyService : ICompanyService
    {
        public async Task<List<CompanyDTO>> GetAll()
        {
            return new List<CompanyDTO>()
            {
                new()
                {
                    Id = 1, Title = "Epol", LegalForm = new LegalFormDTO() { Id = 1, Title = "OOO"}
                },
                new()
                {
                    Id = 2, Title = "Epol", LegalForm = new LegalFormDTO() { Id = 1, Title = "OOO"}
                },
                new()
                {
                    Id = 3, Title = "Epol", LegalForm = new LegalFormDTO() { Id = 1, Title = "OOO"}
                },
                new()
                {
                    Id = 4, Title = "Epol", LegalForm = new LegalFormDTO() { Id = 1, Title = "OOO"}
                }
            };
        }

        public async Task<CompanyDTO> Create(CompanyDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CompanyDTO> Update(CompanyDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}