using System.Linq;
using System.Security.Cryptography;
using System.Text;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Utilities
{
    [PublicAPI]
    public static class Hasher
    {
        [PublicAPI, Pure]
        public static string Hash(string s)
        {
            using (var sha256 = new SHA256Managed())
            {
                var plainBytes = Encoding.UTF8.GetBytes(s);
                var plainLength = Encoding.UTF8.GetByteCount(s);
                const int offset = 0;
                var hashBytes = sha256.ComputeHash(plainBytes, offset, plainLength);
                const string hexFormat = "X2";
                var byteAsHex = Func.Infer((byte b) => b.ToString(hexFormat));
                var hashString = new string(hashBytes.SelectMany(byteAsHex).ToArray());
                const string prefix = "SHA256";
                const string separator = ":";
                var result = string.Join(separator, prefix, hashString);
                return result;
            }
        }
    }
}