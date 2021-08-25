using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Logic.DTOs;
using Logic.Interfaces;

namespace Logic.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CompanyDTO>> GetAll()
        {
            var companies =  _unitOfWork.Companies.GetAll();
            var legalForms =  _unitOfWork.LegalForms.GetAll();

            await Task.WhenAll(companies, legalForms);

            return companies.Result.Select(x => new CompanyDTO
            {
                Id = x.Id,
                Title = x.Title,
                LegalForm = new EnumItemDTO
                {
                    Id = x.LegalFormId, 
                    Title = legalForms.Result.First(l => l.Id == x.LegalFormId).Title
                }
            });
        }

        public Task<bool> Create(CompanyDTO dto)
        {
            _unitOfWork.Companies.Create(CompanyDTO.ToEntity(dto));
            
            return _unitOfWork.Commit();
        }

        public Task<bool> Update(CompanyDTO dto)
        {
            _unitOfWork.Companies.Update(CompanyDTO.ToEntity(dto));
            
            return _unitOfWork.Commit();
        }

        public Task<bool> Delete(int id)
        {
            _unitOfWork.Companies.Delete(id);
            
            return _unitOfWork.Commit();
        }

        public async Task<CompanyDTO> GetById(int id)
        {
            var entity = await _unitOfWork.Companies.GetById(id);

            return CompanyDTO.ToDTO(entity);
        }

        public async Task<IEnumerable<EnumItemDTO>> GetLegalForms()
        {
            var entities = await _unitOfWork.LegalForms.GetAll();

            return entities.Select(x => new EnumItemDTO { Id = x.Id, Title = x.Title });
        }
    }
}