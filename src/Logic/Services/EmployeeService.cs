using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Logic.DTOs;
using Logic.Interfaces;

namespace Logic.Services;

public class EmployeeService(IUnitOfWork unitOfWork) : IEmployeeService
{
    public async Task<IEnumerable<EmployeeDTO>> GetAll(int page, int itemsPerPage, string filter)
    {
        var skip = (page - 1) * itemsPerPage;

        var employees = unitOfWork.Employees.GetSortedPage(skip, itemsPerPage, filter);
            
        var positions =  unitOfWork.Positions.GetAll();
        var companies = unitOfWork.Companies.GetAll(); 

        await Task.WhenAll(employees, positions, companies);

        return employees.Result.Select(employee => EmployeeDTO.ToDTO(
            employee,
            positions.Result.First(position => position.Id == employee.PositionId),
            companies.Result.First(company => company.Id == employee.CompanyId)
        ));
    }

    public Task<int> CountEmployees(string filter = "") => unitOfWork.Employees.Count(filter);

    public Task<bool> Create(EmployeeDTO dto)
    {
        unitOfWork.Employees.Create(EmployeeDTO.ToEntity(dto));

        return unitOfWork.Commit();
    }

    public Task<bool> Update(EmployeeDTO dto)
    {
        unitOfWork.Employees.Update(EmployeeDTO.ToEntity(dto));

        return unitOfWork.Commit();
    }

    public Task<bool> Delete(int id)
    {
        unitOfWork.Employees.Delete(id);

        return unitOfWork.Commit();
    }

    public async Task<EmployeeDTO> GetById(int id)
    {
        var entity = await unitOfWork.Employees.GetById(id);

        return EmployeeDTO.ToDTO(entity);
    }

    public async Task<IEnumerable<EnumItemDTO>> GetPositions()
    {
        var entities = await unitOfWork.Positions.GetAll();

        return entities.Select(position => new EnumItemDTO { Id = position.Id, Title = position.Title });
    }

    public async Task<IEnumerable<EnumItemDTO>> GetCompanies()
    {
        var entities = await unitOfWork.Companies.GetAll();

        return entities.Select(company => new EnumItemDTO { Id = company.Id, Title = company.Title });
    }
}