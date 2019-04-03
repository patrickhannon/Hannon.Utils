using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hannon.Utils.encrypt
{
    public static class BCryptHelper
    {


        public static string HashPassword(string pwd)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            string myHash = BCrypt.Net.BCrypt.HashPassword(pwd, salt);
            return myHash;
        }

        public static bool VerifyPassword(string submittedPwd, string hashedPwd)
        {
            return BCrypt.Net.BCrypt.Verify(submittedPwd, hashedPwd);
        }
    }
}
