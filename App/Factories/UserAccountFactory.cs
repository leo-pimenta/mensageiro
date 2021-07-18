using Domain;
using Infra.Cryptography;

namespace App.Factories
{
    public class UserAccountFactory : IUserAccountFactory
    {
        public UserAccount Create(User user, string password, IPasswordHashing passwordHashing)
        {
            return new UserAccount()
            {
                User = user,
                HashedPassword = passwordHashing.Generate(password)
            };
        }
    }
}