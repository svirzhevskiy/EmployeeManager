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
                Company = new EnumItemDTO()
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

        public async Task<bool> Create(EmployeeDTO dto)
        {
            _unitOfWork.Employees.Create(EmployeeDTO.ToEntity(dto));

            return await _unitOfWork.Commit();
        }

        public async Task<bool> Update(EmployeeDTO dto)
        {
            _unitOfWork.Employees.Update(EmployeeDTO.ToEntity(dto));

            return await _unitOfWork.Commit();
        }

        public async Task<bool> Delete(int id)
        {
            _unitOfWork.Employees.Delete(id);

            return await _unitOfWork.Commit();
        }

        public async Task<EmployeeDTO> GetById(int id)
        {
            var entity = await _unitOfWork.Employees.GetById(id);

            return EmployeeDTO.ToDTO(entity);
        }

        public async Task<IEnumerable<EnumItemDTO>> GetPositions()
        {
            var entities = await _unitOfWork.Positions.GetAll();

            return entities.Select(x => new EnumItemDTO { Id = x.Id, Title = x.Title });
        }

        public async Task<IEnumerable<EnumItemDTO>> GetCompanies()
        {
            var entities = await _unitOfWork.Companies.GetAll();

            return entities.Select(x => new EnumItemDTO { Id = x.Id, Title = x.Title });
        }
    }
}