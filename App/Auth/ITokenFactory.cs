using Domain;

namespace App.Auth
{
    public interface ITokenFactory
    {
        string Create(User user);
        string CreateRefreshToken(User user);
    }
}