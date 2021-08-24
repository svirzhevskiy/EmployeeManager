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
        public DateTime EmploymentDate { get; set; }
        public EnumItemDTO Position { get; set; } = new ();
        public CompanyDTO Company { get; set; } = new ();

        public EmployeeDTO ToDTO(Employee entity)
        {
            return new EmployeeDTO
            {
                Id = entity.Id,
                Surname = entity.Surname,
                Name = entity.Name,
                Patronymic = entity.Patronymic,
                EmploymentDate = entity.EmploymentDate,
                Company = new CompanyDTO { Id = entity.CompanyId },
                Position = new EnumItemDTO { Id = entity.PositionId }
            };
        }
        
        public Employee ToEntity(EmployeeDTO dto)
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