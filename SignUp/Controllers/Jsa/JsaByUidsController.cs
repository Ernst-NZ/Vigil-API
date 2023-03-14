using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;

namespace SignUp.Controllers.Jsa
{
    public class JsaByUidsController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult JsaByUid(dynamic data)
        {
            string uid = data.uid;            

            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query =
              @"Select *
                From JSAData
                Where JsaUid = '" + uid + "' " +
                " Order by Sequence";

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                conn.Open();
                da.Fill(dataTable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            conn.Close();
            da.Dispose();

            if (dataTable == null)
            {
                return NotFound();
            }

            return Ok(dataTable);

        }
    }
}
