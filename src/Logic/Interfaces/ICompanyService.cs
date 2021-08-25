using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic.Interfaces
{
    public interface ICompanyService
    {
        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns>Collection of companies</returns>
        Task<IEnumerable<CompanyDTO>> GetAll();
        /// <summary>
        /// Create new company
        /// </summary>
        /// <param name="dto">Company data transfer object</param>
        /// <returns>True if company was successfully added</returns>
        Task<bool> Create(CompanyDTO dto);
        /// <summary>
        /// Update data for company
        /// </summary>
        /// <param name="dto">Company data transfer object</param>
        /// <returns>True if company was successfully updated</returns>
        Task<bool> Update(CompanyDTO dto);
        /// <summary>
        /// Delete company
        /// </summary>
        /// <param name="id">Id of company</param>
        /// <returns>True if company was successfully deleted</returns>
        Task<bool> Delete(int id);
        /// <summary>
        /// Get company by id
        /// </summary>
        /// <param name="id">Id of company</param>
        /// <returns>Company data transfer object</returns>
        Task<CompanyDTO> GetById(int id);
        /// <summary>
        /// Get possible legal forms of company 
        /// </summary>
        /// <returns>Legal forms for companies</returns>
        Task<IEnumerable<EnumItemDTO>> GetLegalForms();
    }
}