using AutoMapper;
using Employees.Abstractions;
using Employees.Data.Entities;
using Employees.Repositories.Base;
using Employees.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<User> _repository;

        public UserService(IMapper mapper, IBaseRepository<User> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<UserDto>> Get()
        {
            var users = await _repository.Get().ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
        public async Task<UserDto> GetById(int Id)
        {
            var user = await _repository.Get().FirstOrDefaultAsync(_ => _.Id == Id);
            return _mapper.Map<UserDto>(user);
        }
        public async Task Add(UserDto model)
        {
            var entity = _mapper.Map<User>(model);
            await _repository.Create(entity);
            await _repository.Commit();
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
            await _repository.Commit();
        }
        public async Task Update(UserDto model)
        {
            var entity = await _repository.Get().FirstOrDefaultAsync(_ => _.Id == model.Id);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                _repository.Update(entity);
                await _repository.Commit();
            }
        }
        public async Task UpdateAction(string login)
        {
            var entity = await _repository.Get().FirstOrDefaultAsync(_ => _.Login == login);
            if (entity != null)
            {
                entity.ActionDate = DateTime.Now;
                _repository.Update(entity);
                await _repository.Commit();
            }
        }
        public async Task<bool> Authenticate(string login, string password)
        {
            if (await Task.FromResult(_repository.Get(x => x.Login == login && x.Password == password).FirstOrDefault()) != null)
            {
                return true;
            }
            return false;
        }
    }
}
