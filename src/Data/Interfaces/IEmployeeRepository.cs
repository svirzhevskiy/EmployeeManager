﻿using Data.Entities;

namespace Data.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>, IPagingRepository<Employee>
    {
        
    }
}