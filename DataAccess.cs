using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace C__project
{
    internal class DataAccess
    {
        private readonly string _conString;

        public DataAccess()
        {
            _conString = ConfigurationManager
                .ConnectionStrings["OfficeDB"].ConnectionString;
        }

        // SELECT
        public DataTable ExecuteQueryTable(string query)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // INSERT / UPDATE / DELETE
        public int ExecuteDMLQuery(string query)
        {
            using (SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["OfficeDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
