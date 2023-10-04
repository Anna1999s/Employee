using Employees.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Abstractions
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> Get(string? filter = null);
        Task<List<EmployeeDto>> GetNames();
        Task<EmployeeDto> GetById(int Id);
        Task Add(EmployeeDto model);
        Task Delete(int id);
        Task Update(EmployeeDto model);
    }
}
