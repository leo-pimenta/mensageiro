using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUsers : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}