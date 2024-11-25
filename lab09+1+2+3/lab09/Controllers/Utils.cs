﻿using System.Text;
using System.Security.Cryptography;
namespace lab09.Utilities
{
    public static class Utils
    {
        public static string GetSHA26Hash(string input)
        {
            string hash = "";
            using (var sha256 = new SHA256Managed())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input)); hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            return hash;
        }
    }
}

