using Domain;
using Infra.Cryptography;

namespace App.Factories
{
    public class UserAccountFactory : IUserAccountFactory
    {
        public UserAccount Create(User user, string password, IPasswordHashing passwordHashing) => 
            new UserAccount(user, passwordHashing.Generate(password));
    }
}