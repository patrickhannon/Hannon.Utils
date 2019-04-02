using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hannon.Utils.encrypt
{
    public static class BCrypt
    {
        public static string HashPassword(string pwd)
        {
            return BCrypt.HashPassword(pwd);
        }

        public static bool VerifyPassword(string submittedPwd, string hashedPwd)
        {
            return BCrypt.VerifyPassword(submittedPwd, hashedPwd);
        }
    }
}
