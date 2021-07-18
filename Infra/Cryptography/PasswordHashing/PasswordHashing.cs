using BCryptNet = BCrypt.Net.BCrypt;

namespace Infra.Cryptography
{
    public class BCryptPasswordHashing : IPasswordHashing
    {
        public string Generate(string password) => BCryptNet.HashPassword(password);

        public bool Verify(string password, string hash) => BCryptNet.Verify(password, hash);
    }
}