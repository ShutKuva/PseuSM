using System.Security.Cryptography;
using System.Text;
using Tools.Abstractions;

namespace Tools
{
    public class HasherSHA256 : IHasher
    {
        public string Hash(string str)
        {
            using HashAlgorithm sha = SHA256.Create();
            return Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(str)));
        }
    }
}
