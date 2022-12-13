using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.Contracts.Common
{
    public static class HashPassword
    {
        public static string GetPasswordHash(string password)
        {
            var sha = SHA512.Create();
            var asByteArray = Encoding.UTF8.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}
