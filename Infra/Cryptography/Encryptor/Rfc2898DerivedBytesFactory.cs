using System.Security.Cryptography;

namespace Infra.Cryptography
{
    public class Rfc2898DerivedBytesFactory : BaseRfc2898DerivedBytesFactory, IRfc2898DerivedBytesFactory
    {
        const int StandardIterations = 1000;

        private readonly Rfc2898DeriveBytes DerivedBytes;

        public Rfc2898DerivedBytesFactory(string key)
        {
            this.DerivedBytes = new Rfc2898DeriveBytes(key, base.Salt, StandardIterations, HashAlgorithmName.SHA256);
        }

        public byte[] GetIV()
        {
            return this.DerivedBytes.GetBytes(16);
        }

        public byte[] GetKey()
        {
            return this.DerivedBytes.GetBytes(32);
        }
    }
}