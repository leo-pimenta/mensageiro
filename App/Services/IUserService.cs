using System;
using System.Threading.Tasks;
using Domain;

namespace App.Services
{
    public interface IUserService
    {
        Task RegisterUserAsync(User user, string password);
        Task<User> GetUserAsync(Guid guid);
        Task<User> GetUserAsync(string email);
        User GetUser(Guid guid);
        Task<bool> ValidateLogin(User user, string password);
    }
}