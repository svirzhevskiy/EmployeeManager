using System;
using System.Collections.Generic;
using Logic.DTOs;

namespace Web.Models.Employee
{
    public class IndexModel
    {
        public IEnumerable<EmployeeDTO> Employees { get; init; }
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}