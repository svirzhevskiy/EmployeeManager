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

            return employees.Result.Select(x => EmployeeDTO.ToDTO(
                x,
                positions.Result.First(p => p.Id == x.PositionId),
                companies.Result.First(c => c.Id == x.CompanyId)
            ));
        }
        
        public async Task<IEnumerable<EmployeeDTO>> GetAll(int page, int itemsPerPage)
        {
            var skip = (page - 1) * itemsPerPage;
            
            var employees =  _unitOfWork.Employees.GetPage(skip < 0 ? 0 : skip, itemsPerPage);
            var positions =  _unitOfWork.Positions.GetAll();
            var companies = _unitOfWork.Companies.GetAll(); 

            await Task.WhenAll(employees, positions, companies);

            return employees.Result.Select(x => EmployeeDTO.ToDTO(
                x,
                positions.Result.First(p => p.Id == x.PositionId),
                companies.Result.First(c => c.Id == x.CompanyId)
                ));
        }

        public Task<int> CountEmployees()
        {
            return _unitOfWork.Employees.Count();
        }

        public Task<bool> Create(EmployeeDTO dto)
        {
            _unitOfWork.Employees.Create(EmployeeDTO.ToEntity(dto));

            return _unitOfWork.Commit();
        }

        public Task<bool> Update(EmployeeDTO dto)
        {
            _unitOfWork.Employees.Update(EmployeeDTO.ToEntity(dto));

            return _unitOfWork.Commit();
        }

        public Task<bool> Delete(int id)
        {
            _unitOfWork.Employees.Delete(id);

            return _unitOfWork.Commit();
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