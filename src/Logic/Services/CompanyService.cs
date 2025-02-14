using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Logic.DTOs;
using Logic.Interfaces;

namespace Logic.Services;

public class CompanyService(IUnitOfWork unitOfWork) : ICompanyService
{
    public async Task<IEnumerable<CompanyDTO>> GetAll()
    {
        var companies =  unitOfWork.Companies.GetAll();
        var legalForms =  unitOfWork.LegalForms.GetAll();

        await Task.WhenAll(companies, legalForms);

        return companies.Result.Select(company => new CompanyDTO
        {
            Id = company.Id,
            Title = company.Title,
            LegalForm = new EnumItemDTO
            {
                Id = company.LegalFormId, 
                Title = legalForms.Result.First(l => l.Id == company.LegalFormId).Title
            }
        });
    }

    public Task<bool> Create(CompanyDTO dto)
    {
        unitOfWork.Companies.Create(CompanyDTO.ToEntity(dto));
            
        return unitOfWork.Commit();
    }

    public Task<bool> Update(CompanyDTO dto)
    {
        unitOfWork.Companies.Update(CompanyDTO.ToEntity(dto));
            
        return unitOfWork.Commit();
    }

    public Task<bool> Delete(int id)
    {
        unitOfWork.Companies.Delete(id);
            
        return unitOfWork.Commit();
    }

    public async Task<CompanyDTO> GetById(int id)
    {
        var entity = await unitOfWork.Companies.GetById(id);

        return CompanyDTO.ToDTO(entity);
    }

    public async Task<IEnumerable<EnumItemDTO>> GetLegalForms()
    {
        var entities = await unitOfWork.LegalForms.GetAll();

        return entities.Select(legalForm => new EnumItemDTO { Id = legalForm.Id, Title = legalForm.Title });
    }
}