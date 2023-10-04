using AutoMapper;
using Employees.Data.Entities;
using Employees.Shared.Models;

namespace Employees.Infrastructure
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<Language, LanguageDto>()
                .ReverseMap();

            CreateMap<Department, DepartmentDto>()
                .ReverseMap();

            CreateMap<Experience, ExperienceDto>()
                .ReverseMap();

            CreateMap<Employee, EmployeeDto>()
                .ReverseMap();
        }
    }
}
