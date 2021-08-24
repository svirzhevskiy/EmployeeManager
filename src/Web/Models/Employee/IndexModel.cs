using System.Collections.Generic;
using Logic.DTOs;

namespace Web.Models.Employee
{
    public class IndexModel
    {
        public IEnumerable<EmployeeDTO> Employees { get; init; }
    }
}