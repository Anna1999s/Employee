using Employees.Shared.Models;

namespace Employees.Abstractions
{
    public interface IUserService
    {
        Task<List<UserDto>> Get();
        Task<UserDto> GetById(int Id);
        Task Add(UserDto model);
        Task Delete(int id);
        Task Update(UserDto model);
        Task UpdateAction(string login);
        Task<bool> Authenticate(string login, string password);
    }
}
