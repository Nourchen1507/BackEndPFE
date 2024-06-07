using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Services
{
    public class PasswordService
    {
        public PasswordService()
        {
        }

        public static string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            byte[] hashedPasswordBytes = Encoding.UTF8.GetBytes(hashedPassword);
            return Convert.ToBase64String(hashedPasswordBytes);
        }

        public static bool VerifyPassword(string passwordHash, string providedPassword)
        {
            byte[] hashedPasswordBytes = Convert.FromBase64String(passwordHash);
            string hashedPassword = Encoding.UTF8.GetString(hashedPasswordBytes);
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
    }