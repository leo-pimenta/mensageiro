using System;
using System.Threading.Tasks;
using System.Linq;
using App.Factories;
using Domain;
using Infra.Cryptography;
using Infra.Database;
using Microsoft.EntityFrameworkCore;

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

        public async Task<User> GetUserAsync(Guid guid)
        {
            return await this.UnitOfWork.ExecuteAsync(async context => 
                await context.Users.FindAsync(guid));
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await this.UnitOfWork.ExecuteAsync(async context => 
                await context.Users.FirstOrDefaultAsync(user => user.Email == email));
        }

        public async Task<bool> ValidateLogin(User user, string password)
        {
            UserAccount account = await this.UnitOfWork.ExecuteAsync(async context => 
                await context.UserAccounts.FindAsync(user.Guid));

            return this.PasswordHashing.Verify(password, account.HashedPassword);
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