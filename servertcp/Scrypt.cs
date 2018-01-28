using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptSharp.Utility;

namespace servertcp
{
    class Scrypt
    {
        public static string Hash(string secret, string salt)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secret);
            var saltBytes = Encoding.UTF8.GetBytes(salt);
            const int cost = 8192; // 4096, 8192, 16_384, 32_768, 65_536, 131_072, 262_144
            const int blockSize = 8;
            const int parallel = 1;
            var maxThreads = (int?)null;
            const int derivedKeyLength = 128;

            var bytes = SCrypt.ComputeDerivedKey(keyBytes, saltBytes, cost, blockSize, parallel, maxThreads, derivedKeyLength);
            return Convert.ToBase64String(bytes);
        }

        public static string GenerateSalt()
        {
            Random rnd = new Random();
            int length = rnd.Next(16, 24);
            return Guid.NewGuid().ToString("n").Substring(0, length);
        }
    }
}
