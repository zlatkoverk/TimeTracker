using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TimeTracker.Shared
{
    public class HashUtil
    {
        private static HashAlgorithm md5 = MD5.Create();
        public static string GetHash(string inputString)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(inputString);

            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
