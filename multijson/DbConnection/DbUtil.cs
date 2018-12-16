using multijson.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using static System.Collections.Generic.Dictionary<string, string>;

namespace multijson.DbConnection
{
    public class DbUtil
    {

        public DataSet GetDataSet(RoutingGroupElement config, HttpRequestMessage request, string userName)
        {
            string sqlCommand = config.StoredProcedureName;
            string connectionString = ConfigurationManager.ConnectionStrings[config.ConnectionStringName].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sqlCommand;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Path", request.RequestUri.LocalPath);
                    cmd.Parameters.AddWithValue("body", "");
                    cmd.Parameters.AddWithValue("Method", request.Method.Method.ToString());
                    cmd.Parameters.AddWithValue("UserName", userName);
                    cmd.Parameters.AddWithValue("IsDebug", config.IsDebug);

                    if(config.DoUseTableValuedParameters)
                    {
                        DataTable HeaderParameter = new DataTable();
                        HeaderParameter.Columns.Add("KeyText", typeof(string));
                        HeaderParameter.Columns.Add("ValueText", typeof(string));

                        Dictionary<string, string> headers = request.Headers.ToDictionary(a => a.Key.ToString(), a => string.Join(";", a.Value));
                        Enumerator enumerator = headers.GetEnumerator();
                        while(enumerator.MoveNext())
                        {
                            DataRow dr = HeaderParameter.NewRow();
                            dr["KeyText"] = enumerator.Current.Key;
                            dr["ValueText"] = enumerator.Current.Value;
                            HeaderParameter.Rows.Add(dr);
                        }
                        cmd.Parameters.AddWithValue("Headers", HeaderParameter);
                    }
                    
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    conn.Close();

                    return ds;
                }
            }

        }



    }
}