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
            return new List<EmployeeDTO>()
            {
                new ()
                {
                    Id = 1, Name = "Larry", Surname = "qwe", Company = new CompanyDTO() {Id = 1, Title = "Quilix"},
                    EmploymentDate = new DateTime(2019, 01, 10), Patronymic = "asd", 
                    Position =new PositionDTO(){ Id = 1, Title = "qwe"} 
                },
                new ()
                {
                    Id = 2, Name = "Larry", Surname = "qwe", Company = new CompanyDTO() {Id = 1, Title = "Quilix"},
                    EmploymentDate = new DateTime(2019, 01, 10), Patronymic = "asd", 
                    Position =new PositionDTO(){ Id = 1, Title = "qwe"} 
                },
                new ()
                {
                    Id = 3, Name = "Larry", Surname = "qwe", Company = new CompanyDTO() {Id = 1, Title = "Quilix"},
                    EmploymentDate = new DateTime(2019, 01, 10), Patronymic = "asd", 
                    Position =new PositionDTO(){ Id = 1, Title = "qwe"} 
                },
                new ()
                {
                    Id = 4, Name = "Larry", Surname = "qwe", Company = new CompanyDTO() {Id = 1, Title = "Quilix"},
                    EmploymentDate = new DateTime(2019, 01, 10), Patronymic = "asd", 
                    Position =new PositionDTO(){ Id = 1, Title = "qwe"} 
                },
                new ()
                {
                    Id = 5, Name = "Larry", Surname = "qwe", Company = new CompanyDTO() {Id = 1, Title = "Quilix"},
                    EmploymentDate = new DateTime(2019, 01, 10), Patronymic = "asd", 
                    Position =new PositionDTO(){ Id = 1, Title = "qwe"} 
                },
            };
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