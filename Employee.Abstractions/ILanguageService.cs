using Employees.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Abstractions
{
    public interface ILanguageService
    {
        Task<List<LanguageDto>> Get();
        Task<LanguageDto> GetById(int Id);
        Task Add(LanguageDto model);
        Task Delete(int id);
        Task Update(LanguageDto model);
    }
}
