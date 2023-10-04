using Employees.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Abstractions
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> Get();
        Task<DepartmentDto> GetById(int Id);
        Task Add(DepartmentDto model);
        Task Delete(int id);
        Task Update(DepartmentDto model);
    }
}
