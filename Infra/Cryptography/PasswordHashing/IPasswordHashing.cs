namespace Infra.Cryptography
{
    public interface IPasswordHashing
    {
        string Generate(string password);
        bool Verify(string password, string hash);
    }
}