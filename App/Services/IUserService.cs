using System.Threading.Tasks;
using Domain;

namespace App.Services
{
    public interface IUserService
    {
        Task RegisterUserAsync(User user, string password);
    }
}