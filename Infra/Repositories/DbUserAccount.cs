using Domain;
using Domain.Repositories;
using Infra.Database.Model;

namespace Infra.Repositories
{
    public class DbUserAccount : Repository<UserAccount>, IUserAccounts
    {
        public DbUserAccount(MsgContext context) : base(context) {}
    }
}