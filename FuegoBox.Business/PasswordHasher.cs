
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business
{
   public static class PasswordHasher
    {
        public static string PasswordHashing(string password)
        {
           return BCrypt.Net.BCrypt.HashPassword(password,10);
            
        }

        public static bool VerifyPassword(string passwordToCheck, string hashToMatch)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToCheck, hashToMatch);
        }

    }
}
