using AutoMapper;
using Employees.Abstractions;
using Employees.Data.Entities;
using Employees.Repositories.Base;
using Employees.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Employee> _repository;
        public EmployeeService(IMapper mapper, IBaseRepository<Employee> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<EmployeeDto>> Get(string? filter = null)
        {
            var query = _repository.Get().Where(_ => !_.IsDeleted);
            if (!string.IsNullOrEmpty(filter))
            {
                var listfilters = filter.ToLower().Split(" ");
                query = query.Where(_ => listfilters.Contains(_.Name.ToLower()) || listfilters.Contains(_.LastName.ToLower()));
            }
            var experiences = await query.ToListAsync();
            return _mapper.Map<List<EmployeeDto>>(experiences);
        }
        public async Task<List<EmployeeDto>> GetNames()
        {
            var experiences = await _repository.Get().Where(_ => !_.IsDeleted).ToListAsync();
            var names = _mapper.Map<List<EmployeeDto>>(experiences);
            foreach (var item in names)
            {
                item.Name = item.Name + " " + item.LastName;
            }
            return names;
        }
        public async Task<EmployeeDto> GetById(int Id)
        {
            var experience = await _repository.Get().FirstOrDefaultAsync(_ => _.Id == Id);
            return _mapper.Map<EmployeeDto>(experience);
        }
        public async Task Add(EmployeeDto model)
        {
            var entity = _mapper.Map<Employee>(model);
            await _repository.Create(entity);
            await _repository.Commit();
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
            await _repository.Commit();
        }
        public async Task Update(EmployeeDto model)
        {
            var entity = await _repository.Get().FirstOrDefaultAsync(_ => _.Id == model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                _repository.Update(entity);
                await _repository.Commit();
            }
        }
    }
}
