using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using multijson.DbConnection;
using multijson.Configuration;
using System.Web.Http;

namespace multijson.Models
{
    public class DefaultHttpHandler : HttpMessageHandler
    {
        private string name;
        private string connectionStringName;
        private string storedProcedureName;

        public DefaultHttpHandler(string name, string connectionStringName, string storedProcedureName)
        {
            this.name = name;
            this.connectionStringName = connectionStringName;
            this.storedProcedureName = storedProcedureName;
        }

        //public static DefaultHttpHandler

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Create the response.
            var method = request.Method;
            string requestString = request.ToString();
            string[] segments = new string[request.RequestUri.Segments.Length];

            WindowsIdentity identity = HttpContext.Current.Request.LogonUserIdentity;
            // Med Windows Authentication får man ut Windows-namnet i identity.Name.

            // Kan man IP-filtrera här?

            string path = request.RequestUri.LocalPath;
            /*
            for (int i=0, j=0;i< request.RequestUri.Segments.Length; i++)
            {
                string s = request.RequestUri.Segments[i];
                if (s != "/")
                {
                    segments[j++] = s.Replace("/", "");
                }
            }
            */

            DbUtil dbUtil = new DbUtil();
            DataSet ds = dbUtil.GetDataSet(path, "", request.Method.ToString(), identity.Name, connectionStringName, storedProcedureName);

            string responsebody = "";
            int statusCode = 200;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                responsebody = (string) (dr["ResponseBody"] != DBNull.Value ? dr["ResponseBody"] : "");
                statusCode = (int) (dr["statusCode"] != DBNull.Value ? dr["statusCode"] : 404);
            }

            var response = new HttpResponseMessage((HttpStatusCode)statusCode)
            {
                Content = new StringContent(responsebody)
                
            };

            // Svara med headers.

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                DataRow row = ds.Tables[1].Rows[0];
                foreach (DataColumn dc in ds.Tables[1].Columns)
                {
                    response.Headers.Add(
                        dc.ColumnName,
                        (string)(row[dc.ColumnName])
                    );
                }
            }

            // DEBUG: Notera vilken "handler" som användes.
            response.Headers.Add("X-HandledBy", name);


            // Note: TaskCompletionSource creates a task that does not contain a delegate.
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);   // Also sets the task state to "RanToCompletion"
            return tsc.Task;
        }
    }

}