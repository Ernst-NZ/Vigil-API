using SignUp.Models;
using System.Web.Http;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace SignUp.Controllers.Jsa
{
    public class JsaByUserUidsController : ApiController
    {
        private Entities db = new Entities();
        [HttpPost]
        public IHttpActionResult JsaByCompanyUid(dynamic data)
        {
            string uid = data.uid;
            string dateFrom = data.fromDate;
            string dateTo = data.toDate;

            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query =
              @"Select distinct JsaUid
                ,CompanyUid
                ,AddedByUid
                ,AddedByName
                ,JobName
                ,Location
                ,Date
                ,CAST(Date as Date) JsaDate
                ,coalesce(Status, '') Status
                ,AddedOn
                ,LastUpdate
                ,coalesce(Latitude, '') Latitide
                ,coalesce(LongiTude, '') Longitude
                ,coalesce(Deleted, 0) Deleted
                ,coalesce(DeletedBy, '') DeletedBy
                ,(select Count(AcknowledgeUid)
					from JSAReviewed Review
					Where Review.JSAUid = jsa.JsaUid
				 ) acknowledge
          From JSAData jsa
          Where AddedByUid = '" + uid + "' " +
             " AND (CAST(Date as date)) >= '" + dateFrom + " ' and (CAST(Date as date)) <= '" + dateTo + " '   " +
             " Order by CAST(Date as date) desc, LastUpdate desc, JobName";

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