using SignUp.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;

namespace SignUp.Controllers.VisitLogs
{
    public class GetUserDayScheduleByUidsController : ApiController
    {
        private Entities db = new Entities();
        [HttpPost]
        public IHttpActionResult GetUserDayScheduleByUid(dynamic data)
        {
            string userUid = data.userUid;
            string fromDate = data.fromDate;
            string toDate = data.toDate;

            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query =
                @"SELECT distinct Created_by
	                  ,format(VisitTime, 'ddd dd MMM yy') day
	                  ,VisitTime
	                  ,Result
	                  ,notes
	                  ,p.ProjectName
	                  ,p.ProjectLocation
	                  ,lp.Sequence      
                      ,vl.Longitude
                      ,vl.Latitude
	                  ,vl.Notes
                  FROM [Vigil].[dbo].[VisitLog] vl
                  inner Join Projects P on P.ProjectId = vl.SiteUid
                  inner join LinkedProjects LP on LP.ProjectCode = vl.SiteUid
                    and NOT lp.Sequence IS NULL
                    and Lp.UserCode = vl.UserUid
                  Where UserUid = '" + userUid + "' " +
                "  and VisitTime >= '" + fromDate + "' " +
                "  and VisitTime <= '" + toDate + "' " +
                "  " +
                "  Order by VisitTime, lp.Sequence";

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