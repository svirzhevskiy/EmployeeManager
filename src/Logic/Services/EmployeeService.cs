using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;
using Logic.Interfaces;

namespace Logic.Services
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<List<EmployeeDTO>> GetAll()
        {
            throw new Exception();
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
    }
}