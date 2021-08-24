using System.Collections.Generic;
using Logic.DTOs;

namespace Web.Models.Employee
{
    public class IndexModel
    {
        public List<EmployeeDTO> Employees { get; init; } = new();
    }
}