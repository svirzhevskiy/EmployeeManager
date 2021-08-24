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
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepository<Employee> _repository;
        private readonly IBaseRepository<Position> _positionRepository;
        private readonly IBaseRepository<Company> _companyRepository;

        public EmployeeService(IBaseRepository<Employee> repository, 
            IBaseRepository<Position> positionRepository, 
            IBaseRepository<Company> companyRepository)
        {
            _repository = repository;
            _positionRepository = positionRepository;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAll()
        {
            var entities =  _repository.GetAll();
            var positions =  _positionRepository.GetAll();
            var companies = _companyRepository.GetAll(); 

            await Task.WhenAll(entities, positions, companies);

            return entities.Result.Select(x => new EmployeeDTO
            {
                Id = x.Id,
                Surname = x.Surname,
                Name = x.Name,
                Patronymic = x.Patronymic,
                EmploymentDate = x.EmploymentDate,
                Company = new CompanyDTO
                {
                    Id = x.CompanyId,
                    Title = companies.Result.First(c => c.Id == x.CompanyId).Title
                },
                Position = new EnumItemDTO
                {
                    Id = x.PositionId,
                    Title = positions.Result.First(p => p.Id == x.PositionId).Title
                }
            });
        }

        public async Task<EmployeeDTO> Create(EmployeeDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<EmployeeDTO> Update(EmployeeDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<EmployeeDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EnumItemDTO>> GetPositions()
        {
            throw new NotImplementedException();
        }
    }
}