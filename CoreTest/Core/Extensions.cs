using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CoreTest.Core
{
    public static class Extensions
    {
        public static string Format(this string @this, params object[] args)
        {
            return string.Format(@this, args);
        }

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(this string @this)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(@this))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        
        public static bool IsNull(this object @this)
        {
            return @this == null;
        }

        public static bool IsNotNull(this object @this)
        {
            return @this.IsNull() == false;
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source.Any() == false;
        }
    }
}
