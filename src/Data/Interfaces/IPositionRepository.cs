using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Interfaces
{
    public interface IPositionRepository
    {
        Task<List<Position>> GetAll();
    }
}