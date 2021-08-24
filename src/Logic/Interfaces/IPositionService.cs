using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic.Interfaces
{
    public interface IPositionService
    {
        Task<List<PositionDTO>> GetAll();
    }
}