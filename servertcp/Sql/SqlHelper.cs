using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace servertcp.Sql
{
    public class SqlHelper
    {
        public static int ExecuteNonQuery(string stringConnection, string command, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static SqlDataReader ExecuteDataReader(string stringConnection, string command, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(stringConnection);
            using (SqlCommand cmd = new SqlCommand(command, conn))
            {
                conn.Open();
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
    }
}
