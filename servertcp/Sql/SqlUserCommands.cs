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
                    var parameters = new[] { new SqlParameter("@Login", login),
                        new SqlParameter("@Password", password),
                        new SqlParameter("@Username", username),
                        new SqlParameter("@Salt", salt),
                        new SqlParameter("@Date", GetSqlDateTime()),
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

        public static long GetUserId(string login)
        {
            var parameter = new SqlParameter("@Login", login);
            using (SqlDataReader reader = SqlHelper.ExecuteDataReader("SELECT Users.Id FROM Users WHERE Users.Login LIKE @Login", parameter))
                while (reader.Read())
                    return (long)reader["Id"];
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

        public static bool AddActionInfo(long id, string ip, Actions action)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@UserId",id),
                    new SqlParameter("@Ip", ip),
                    new SqlParameter("@Date", GetSqlDateTime()),
                    new SqlParameter("@Action", action)
                };

                SqlHelper.SqlNonQueryCommand("INSERT INTO ActionInfo (UserId, Ip, Time, Action) VALUES (@UserId, @Ip, @Date, @Action)", parameters);
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
                    return reader["Salt"].ToString();
            return string.Empty;
        }

        public class DataChange
        {
            public static bool ChangeLogin(long id, string newLogin)
            {
                try
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@Id", id),
                        new SqlParameter("@NewLogin", newLogin),
                    };

                    SqlHelper.SqlNonQueryCommand("UPDATE Users SET Users.Login=@NewLogin WHERE Id = @Id", parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return true;
            }

            public static bool ChangeUsername(long id, string newUsername)
            {
                try
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@Id", id),
                        new SqlParameter("@NewUsername", newUsername),
                    };

                    SqlHelper.SqlNonQueryCommand("UPDATE Users SET Users.Username=@NewUsername WHERE Id = @Id", parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return true;
            }

            public static bool ChangePassword(long id, string newPassword)
            {
                try
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@Id", id),
                        new SqlParameter("@NewPassword", newPassword),
                    };

                    SqlHelper.SqlNonQueryCommand("UPDATE Users SET Users.Password=@NewPassword WHERE Id = @Id", parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return true;
            }

            public static bool ChangeEmail(long id , string newEmail)
            {
                try
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@Id", id),
                        new SqlParameter("@NewEmail", newEmail),
                    };

                    SqlHelper.SqlNonQueryCommand("UPDATE Users SET Users.Password=@NewEmail WHERE Id = @Id", parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return true;
            }

            public static bool ChangeAvatar(long id, string newAvatarUrl)
            {
                try
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@Id", id),
                        new SqlParameter("@NewAvatarUrl", newAvatarUrl),
                    };

                    SqlHelper.SqlNonQueryCommand("UPDATE Users SET Users.Password=@NewAvatarUrl WHERE Id = @Id", parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return true;
            }
        }

        private static string GetSqlDateTime()
        {
            return DateTime.Now.ToString("s");
        }

        public enum Actions : int
        {
            Login,
            Logout,
            Register,
            ChangeLogin,
            ChangeUsername,
            ChangePassword,
            ChangeEmail,
            ChangeAvatar
        }
    }
}
