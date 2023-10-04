using AutoMapper;
using Employees.Abstractions;
using Employees.Data.Entities;
using Employees.Repositories.Base;
using Employees.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Language> _repository;
        public LanguageService(IMapper mapper, IBaseRepository<Language> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<LanguageDto>> Get()
        {
            var languages = await _repository.Get().ToListAsync();
            return _mapper.Map<List<LanguageDto>>(languages);
        }
        public async Task<LanguageDto> GetById(int Id)
        {
            var language = await _repository.Get().FirstOrDefaultAsync(_ => _.Id == Id);
            return _mapper.Map<LanguageDto>(language);
        }
        public async Task Add(LanguageDto model)
        {
            var entity = _mapper.Map<Language>(model);
            await _repository.Create(entity);
            await _repository.Commit();
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
            await _repository.Commit();
        }
        public async Task Update(LanguageDto model)
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
