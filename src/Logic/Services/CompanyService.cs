using System;
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

        public CompanyService(IBaseRepository<Company> repository)
        {
            _repository = repository;
        }

        public async Task<List<CompanyDTO>> GetAll()
        {
            var entities = await _repository.GetAll();

            return entities.Select(CompanyDTO.ToDTO).ToList();
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
    }
}