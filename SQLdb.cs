using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem
{
    class SQLdb
    {
        Hash hash = new Hash();

        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LogInSystemDB;Trusted_Connection=True;";

        public bool CreateUser(string username, byte[] password)
        {
            byte[] salt = hash.GenerateSalt();
            string passwordToDB = Encoding.UTF8.GetString(hash.HashPasswordWithSalt(password, salt));
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO Users (Username, Password, Salt) VALUES(@username, @password, @salt);", connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", passwordToDB);
                command.Parameters.AddWithValue("@salt", Encoding.UTF8.GetString(salt));
                connection.Open();
                command.ExecuteReader();
            }

            return true;
        }

        public bool CheckPassword(string username, byte[] password)
        {
            byte[] salt = Encoding.UTF8.GetBytes((GetSalt(username)));
            byte[] passwordFromDB = Encoding.UTF8.GetBytes(GetPassword(username));

            if (passwordFromDB == hash.HashPasswordWithSalt(password, salt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        string GetPassword(string username)
        {
            string password = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT Password FROM Users WHERE Username = @username;", connection);
                command.Parameters.AddWithValue("@username", username);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    reader.Read();
                    password = Convert.ToString(reader[0]);
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return password;
        }
        string GetSalt(string username)
        {
            string salt = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT Salt FROM Users WHERE Username = @username", connection);
                command.Parameters.AddWithValue("@username", username);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    reader.Read();
                    salt = Convert.ToString(reader[0]);
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            Console.WriteLine(salt);
            return salt;
        }
    }
}
