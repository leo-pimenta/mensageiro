using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using Infra.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class DbUsers : Repository<User>, IUsers
    {
        public DbUsers(MsgContext context) : base(context) {}

        public async Task<User> GetByEmailAsync(string email)
        {
            IQueryable<User> query = from user in base.Context.Users
                where user.Email == email
                select user;
            
            return await query.FirstOrDefaultAsync();
        }
    }
}