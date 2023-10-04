using AutoMapper;
using Employees.Abstractions;
using Employees.Data.Entities;
using Employees.Repositories.Base;
using Employees.Shared.Models;
using Microsoft.EntityFrameworkCore;
namespace Employees.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Experience> _repository;
        public ExperienceService(IMapper mapper, IBaseRepository<Experience> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<ExperienceDto>> Get()
        {
            var experiences = await _repository.Get().Where(_ => !_.IsDeleted).ToListAsync();
            return _mapper.Map<List<ExperienceDto>>(experiences);
        }
        public async Task<ExperienceDto> GetById(int Id)
        {
            var experience = await _repository.Get().FirstOrDefaultAsync(_ => _.Id == Id);
            return _mapper.Map<ExperienceDto>(experience);
        }
        public async Task Add(ExperienceDto model)
        {
            var entity = _mapper.Map<Experience>(model);
            await _repository.Create(entity);
            await _repository.Commit();
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
            await _repository.Commit();
        }
        public async Task Update(ExperienceDto model)
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
