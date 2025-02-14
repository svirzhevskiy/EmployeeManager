using System;

namespace Data.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public DateTime EmploymentDate { get; set; }
    public int PositionId { get; set; }
    public int CompanyId { get; set; }
}