using App.Dtos;
using Domain;
using Infra.Cryptography;

namespace App.Factories
{
    public interface IUserAccountFactory
    {
        UserAccount Create(User user, string password, IPasswordHashing passwordHashing);
    }
}