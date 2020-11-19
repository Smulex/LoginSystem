using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem
{
    class AccountManger
    {
        SQLdb sqlDB = new SQLdb();
        Hash hash = new Hash();

        public bool LogIn(string username, string password)
        {
            byte[] bytePassword = Encoding.UTF8.GetBytes(password);

            return sqlDB.CheckPassword(username, bytePassword);
        }

        public bool Register(string username, string password)
        {
            byte[] salt = hash.GenerateSalt();
            byte[] bytePassword = Encoding.UTF8.GetBytes(password);

            return sqlDB.CreateUser(username, hash.HashPasswordWithSalt(bytePassword, salt));
        }
    }
}
