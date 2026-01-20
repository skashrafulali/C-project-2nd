using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace C__project
{
    internal class DataAccess
    {
        private SqlConnection con;

        public DataAccess()
        {
            con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["OfficeDB"].ConnectionString
            );
        }

        public DataTable ExecuteQueryTable(string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public int ExecuteDMLQuery(string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                int row = cmd.ExecuteNonQuery();
                con.Close();
                return row;
            }
        }
    }
}
