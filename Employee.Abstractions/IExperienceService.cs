using Employees.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Abstractions
{
    public interface IExperienceService
    {
        Task<List<ExperienceDto>> Get();
        Task<ExperienceDto> GetById(int Id);
        Task Add(ExperienceDto model);
        Task Delete(int id);
        Task Update(ExperienceDto model);
    }
}
