using System;
using System.Threading.Tasks;
using App.Factories;
using Domain;
using Infra.Cryptography;
using Domain.Repositories;
using Infra.Database.Model;

namespace App.Services
{
    public class UserService : DbService, IUserService
    {
        private readonly IUsers Users;
        private readonly IUserAccounts UserAccounts;
        private readonly IPasswordHashing PasswordHashing;
        private readonly IUserAccountFactory UserAccountFactory;

        public UserService(
            MsgContext context,
            IUsers users,
            IUserAccounts userAccounts,
            IPasswordHashing passwordHashing, 
            IUserAccountFactory userAccountFactory) : base(context)
        {
            this.Users = users;
            this.UserAccounts = userAccounts;
            this.PasswordHashing = passwordHashing;
            this.UserAccountFactory = userAccountFactory;
        }

        public User GetUser(Guid guid) => this.Users.GetEntityById(guid);
        public async Task<User> GetUserAsync(Guid id) => await this.Users.GetEntityByIdAsync(id);
        public async Task<User> GetUserAsync(string email) => await this.Users.GetByEmailAsync(email);

        public async Task<bool> ValidateLogin(User user, string password)
        {
            UserAccount account = await this.UserAccounts.GetEntityByIdAsync(user.Id);
            return this.PasswordHashing.Verify(password, account.HashedPassword);
        }

        public async Task RegisterUserAsync(User user, string password)
        {
            UserAccount account = this.UserAccountFactory.Create(user, password, this.PasswordHashing);
            this.Users.Add(user);
            this.UserAccounts.Add(account);
            await this.SaveDbChangesAsync();
        }
    }
}