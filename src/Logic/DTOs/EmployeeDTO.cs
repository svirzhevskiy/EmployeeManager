using System;
using Data.Entities;

namespace Logic.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Surname { get; set; } = "";
        public string Name { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public DateTime EmploymentDate { get; set; } = DateTime.Now;
        public EnumItemDTO Position { get; set; } = new ();
        public EnumItemDTO Company { get; set; } = new ();

        public static EmployeeDTO ToDTO(Employee entity)
        {
            return new EmployeeDTO
            {
                Id = entity.Id,
                Surname = entity.Surname,
                Name = entity.Name,
                Patronymic = entity.Patronymic,
                EmploymentDate = entity.EmploymentDate,
                Company = new EnumItemDTO { Id = entity.CompanyId },
                Position = new EnumItemDTO { Id = entity.PositionId }
            };
        }

        public static EmployeeDTO ToDTO(Employee employee, Position position, Company company)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                Surname = employee.Surname,
                Name = employee.Name,
                Patronymic = employee.Patronymic,
                EmploymentDate = employee.EmploymentDate,
                Company = new EnumItemDTO
                {
                    Id = employee.CompanyId,
                    Title = company.Title
                },
                Position = new EnumItemDTO
                {
                    Id = employee.PositionId,
                    Title = position.Title
                }
            };
        }
        
        public static Employee ToEntity(EmployeeDTO dto)
        {
            return new Employee
            {
                Id = dto.Id,
                Surname = dto.Surname,
                Name = dto.Name,
                Patronymic = dto.Patronymic,
                EmploymentDate = dto.EmploymentDate,
                CompanyId = dto.Company.Id,
                PositionId = dto.Position.Id
            };
        }
    }
}