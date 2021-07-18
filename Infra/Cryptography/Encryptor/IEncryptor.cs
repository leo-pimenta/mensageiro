namespace Infra.Cryptography
{
    public interface IEncryptor
    {
        string Encrypt(string key, string text);
        string Decrypt(string key, string encryptedText);
    }
}