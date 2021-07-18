using System.Security.Cryptography;

namespace Infra.Cryptography
{
    public interface IRfc2898DerivedBytesFactory
    {
        byte[] GetKey();
        byte[] GetIV();
    }
}