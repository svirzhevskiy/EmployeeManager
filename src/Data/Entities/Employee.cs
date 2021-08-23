using System;

namespace Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Surname { get; set; } = "";
        public string Name { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public DateTime EmploymentDate { get; set; }
        public int PositionId { get; set; }
        public int CompanyId { get; set; }
    }
}