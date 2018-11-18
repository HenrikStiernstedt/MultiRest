using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace multijson.DbConnection
{
    public class DbUtil
    {

        public DataSet GetDataSet(string path, string[] segments, string body, string method)
        {
            string sqlCommand = "dbo.Web_MultiJSON ";
            string connectionString = ConfigurationManager.ConnectionStrings["MultijsonConnectionString"].ConnectionString;

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = sqlCommand;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Path", path);
                    cmd.Parameters.AddWithValue("@body", body);
                    cmd.Parameters.AddWithValue("@Method", method);
                    cmd.Parameters.AddWithValue("@Segment1", (segments.Length > 0 ? segments[0] : null));
                    cmd.Parameters.AddWithValue("@Segment2", (segments.Length > 1 ? segments[1] : null));
                    cmd.Parameters.AddWithValue("@Segment3", (segments.Length > 2 ? segments[2] : null));

                    conn.Open();

                    System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    conn.Close();

                    return ds;
                }
            }

        }



    }
}