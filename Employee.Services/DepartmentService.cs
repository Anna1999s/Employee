using AutoMapper;
using Employees.Abstractions;
using Employees.Data.Entities;
using Employees.Repositories.Base;
using Employees.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Department> _repository;
        public DepartmentService(IMapper mapper, IBaseRepository<Department> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<DepartmentDto>> Get()
        {
            var departments = await _repository.Get().Where(_ => !_.IsDeleted).ToListAsync();
            return _mapper.Map<List<DepartmentDto>>(departments);
        }
        public async Task<DepartmentDto> GetById(int Id)
        {
            var department = await _repository.Get().FirstOrDefaultAsync(_ => _.Id == Id);
            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task Add(DepartmentDto model)
        {
            var entity = _mapper.Map<Department>(model);
            await _repository.Create(entity);
            await _repository.Commit();
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
            await _repository.Commit();
        }
        public async Task Update(DepartmentDto model)
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
