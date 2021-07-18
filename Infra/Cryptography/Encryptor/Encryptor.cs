using System;
using System.Security.Cryptography;
using System.IO;

namespace Infra.Cryptography
{
    public class Encryptor : IEncryptor
    {
        public string Encrypt(string key, string text)
        {
            ValidateInput(key, text);
            IRfc2898DerivedBytesFactory derivedBytesFactory = new Rfc2898DerivedBytesFactory(key);
            byte[] derivedKey = derivedBytesFactory.GetKey();
            byte[] iv = derivedBytesFactory.GetIV();
            return Transform(text, derivedKey, iv);
        }

        public string Decrypt(string key, string encryptedText)
        {
            ValidateInput(key, encryptedText);
            IRfc2898DerivedBytesFactory derivedBytesFactory = new Rfc2898DerivedBytesFactory(key);
            byte[] derivedKey = derivedBytesFactory.GetKey();
            byte[] iv = derivedBytesFactory.GetIV();
            return Restore(encryptedText, derivedKey, iv);
        }

        private static string Restore(string encryptedText, byte[] key, byte[] iv)
        {
            byte[] bytes = Convert.FromBase64String(encryptedText);

            using (var aesService = new AesCryptoServiceProvider() {  Key = key, IV = iv})
            {
                ICryptoTransform decryptor = aesService.CreateDecryptor(aesService.Key, aesService.IV);

                using (var stream = new MemoryStream(bytes))
                using (var criptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(criptoStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static string Transform(string text, byte[] key, byte[] iv)
        {
            using (var aesService = new AesCryptoServiceProvider() { Key = key, IV = iv })
            {
                ICryptoTransform encryptor = aesService.CreateEncryptor(aesService.Key, aesService.IV);

                using (var stream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                {
                    WriteToStream(text, cryptoStream);
                    return Convert.ToBase64String(stream.ToArray());
                }
            }
        }

        private static void WriteToStream(string text, CryptoStream cryptoStream)
        {
            using (var writer = new StreamWriter(cryptoStream))
            {
                writer.Write(text);
            }
        }

        private void ValidateInput(string key, string text)
        {
            ValidateKey(key);
            ValidateText(text);
        }

        private static void ValidateText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Invalid text.");
            }
        }

        private static void ValidateKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Invalid key.");
            }
        }
    }
}
