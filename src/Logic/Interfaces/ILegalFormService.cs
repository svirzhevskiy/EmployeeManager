using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic.Interfaces
{
    public interface ILegalFormService
    {
        Task<List<LegalFormDTO>> GetAll();
    }
}