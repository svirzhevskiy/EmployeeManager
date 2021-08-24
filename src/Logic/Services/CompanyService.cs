using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Interfaces;
using Logic.DTOs;
using Logic.Interfaces;

namespace Logic.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IBaseRepository<Company> _repository;
        private readonly IBaseRepository<LegalForm> _legalFormRepository;

        public CompanyService(IBaseRepository<Company> repository, 
            IBaseRepository<LegalForm> legalFormRepository)
        {
            _repository = repository;
            _legalFormRepository = legalFormRepository;
        }

        public async Task<IEnumerable<CompanyDTO>> GetAll()
        {
            var entities = await _repository.GetAll();
            var legalForms = await _legalFormRepository.GetAll();

            return entities.Select(x => new CompanyDTO
            {
                Id = x.Id,
                Title = x.Title,
                LegalForm = new EnumItemDTO
                {
                    Id = x.LegalFormId, 
                    Title = legalForms.First(l => l.Id == x.LegalFormId).Title
                }
            });
        }

        public Task<bool> Create(CompanyDTO dto)
        {
            return _repository.Create(CompanyDTO.ToEntity(dto));
        }

        public Task<bool> Update(CompanyDTO dto)
        {
            return _repository.Update(CompanyDTO.ToEntity(dto));
        }

        public Task<bool> Delete(int id)
        {
            return _repository.Delete(id);
        }

        public async Task<CompanyDTO> GetById(int id)
        {
            var entity = await _repository.GetById(id);

            return CompanyDTO.ToDTO(entity);
        }

        public async Task<IEnumerable<EnumItemDTO>> GetLegalForms()
        {
            var entities = await _legalFormRepository.GetAll();

            return entities.Select(x => new EnumItemDTO { Id = x.Id, Title = x.Title });
        }
    }
}