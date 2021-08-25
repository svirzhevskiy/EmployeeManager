using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Logic.DTOs;
using Logic.Interfaces;

namespace Logic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAll()
        {
            var employees =  _unitOfWork.Employees.GetAll();
            var positions =  _unitOfWork.Positions.GetAll();
            var companies = _unitOfWork.Companies.GetAll(); 

            await Task.WhenAll(employees, positions, companies);

            return employees.Result.Select(x => new EmployeeDTO
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