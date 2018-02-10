using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servertcp.Sql
{
    class SqlUserCommands
    {
        public void CreateUser(string strConnection, string login, string password, string salt, string username)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Login", login));
            parameters.Add(new SqlParameter("@Password", password));
            parameters.Add(new SqlParameter("@Salt", salt));
            parameters.Add(new SqlParameter("@Username", username));

            SqlHelper.ExecuteNonQuery(strConnection, "INSERT INTO Users (Login, Username, Password, Salt) " + 
                                                     "VALUES('@Login', '@Username', '@Password', '@Salt')", parameters.ToArray());
        }

        public bool CheckPassword(string strConnection, string password, string login)
        {
            SqlParameter parameter = new SqlParameter("@Login", login);
            using (var reader = SqlHelper.ExecuteDataReader(strConnection, "SELECT Users.Password" +
                                                                      "FROM Users WHERE @Login = Users.Login", parameter))
                return reader["Password"] == password;
        }

        public string GetSalt(string strConnection, string login)
        {
            SqlParameter parameter = new SqlParameter("@Login", login);
            using (var reader = SqlHelper.ExecuteDataReader(strConnection, "SELECT Users.Salt " +
                                                                           "FROM Users WHERE @Login = Users.Login", parameter))
                return reader["Salt"].ToString();
        }


    }
}
