using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servertcp.Sql
{
    class SqlUserCommands
    {
        public static bool CreateUser(string login, string password, string salt, string username)
        {
            try
            {
                if (!LoginExists(login))
                {
                    DateTime dt = DateTime.Now;
                    var sqlDt = dt.ToString("s");

                    var parameters = new[] { new SqlParameter("@Login", login),
                        new SqlParameter("@Password", password),
                        new SqlParameter("@Username", username),
                        new SqlParameter("@Salt", salt),
                        new SqlParameter("@Date", sqlDt), 
                    };


                    SqlHelper.SqlNonQueryCommand("INSERT INTO Users (Login, Username, Password, Salt, AccountCreationTime) " +
                                                             "VALUES(@Login, @Username, @Password, @Salt, @Date)", parameters);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static int GetUserId(string login)
        {
            var parameter = new SqlParameter("@Login", login);
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader("SELECT Users.Id FROM Users WHERE Users.Login LIKE @Login", parameter))
                while (reader.Read())
                    return (int)reader["Id"];
            return -1;
        }

        public static bool LoginExists(string login)
        {
            var parameter = new SqlParameter("@Login", login);
            int count = (int)SqlHelper.ExecuteScalar("SELECT COUNT(*) FROM Users WHERE Users.Login LIKE @Login", parameter);
            return count > 0;
        }

        public static bool CheckPassword(string password, string login)
        {
            var parameter = new SqlParameter("@Login", login);
            using (var reader = SqlHelper.ExecuteDataReader("SELECT Users.Password FROM Users WHERE @Login LIKE Users.Login", parameter))
                while (reader.Read())
                    return reader["Password"].ToString() == password;
            return false;
        }

        public static bool AddLoginInfo(string login, string ip)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string sqlDt = dt.ToString("s");
                var userId = GetUserId(login);

                var parameters = new[]
                {
                    new SqlParameter("@UserId",userId),
                    new SqlParameter("@Ip", ip),
                    new SqlParameter("@Date", sqlDt), 
                };

                SqlHelper.SqlNonQueryCommand("INSERT INTO LoginInfo (UserId, Ip, Time) VALUES (@UserId, @Ip, @Date)", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public static string GetSalt(string login)
        {
            var parameter = new SqlParameter("@Login", login);
            using (var reader = SqlHelper.ExecuteDataReader("SELECT Users.Salt FROM Users WHERE Users.Login LIKE @Login", parameter))
                while (reader.Read())
                {
                    return reader["Salt"].ToString();
                }
            return string.Empty;

        }
    }
}
