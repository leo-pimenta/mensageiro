using System.Threading.Tasks;
using App.Factories;
using Domain;
using Infra.Cryptography;
using Infra.Database;

namespace App.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IPasswordHashing PasswordHashing;
        private readonly IUserAccountFactory UserAccountFactory;

        public UserService(
            IUnitOfWork unitOfWork,
            IPasswordHashing passwordHashing, 
            IUserAccountFactory userAccountFactory)
        {
            this.UnitOfWork = unitOfWork;
            this.PasswordHashing = passwordHashing;
            this.UserAccountFactory = userAccountFactory;
        }

        public async Task RegisterUserAsync(User user, string password)
        {
            UserAccount account = this.UserAccountFactory.Create(user, password, this.PasswordHashing);
            
            await this.UnitOfWork.ExecuteAsync(context => 
            {
                context.Users.Add(user);
                context.UserAccounts.Add(account);
            });
        }
    }
}