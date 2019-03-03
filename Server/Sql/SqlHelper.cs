using System.Data.SqlClient;

namespace Server.Sql
{
    public class SqlHelper
    {
        private static string ConnectionString =
            "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\SdjDB.mdf;Integrated Security=True";


        public static int SqlNonQueryCommand(string command, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string command, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static SqlDataReader ExecuteDataReader(string command, params SqlParameter[] parameters)
        {
            var conn = new SqlConnection(ConnectionString);
            {
                using (SqlCommand cmd = new SqlCommand(command, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddRange(parameters);
                    var reader = cmd.ExecuteReader();
                    return reader;
                }

            }
        }
    }
}
