using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;
using SignUp.Models;

namespace SignUp.Controllers.VisitLogs
{
    public class GetSchedulesByUserUidController : ApiController
    {
        [HttpPost]
        public IHttpActionResult BriefingStatsByCompanyCode(dynamic data)
        {
            string uid = data.userUid;
            string checkDate = data.checkDate;

            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query =
                    @"Select *
                    ,coalesce((Select Max(Format(vl.VisitTime, 'dd MMM yy HH:mm'))
		                From VisitLog vl
		                Where vl.SiteUid = l.ProjectCode
			                and vl.UserUid = L.UserCode), 'N/A') LastVisit
                    ,coalesce((Select Max(Format(vl.VisitTime, 'ddd - dd MMM yy HH:mm'))
		                From VisitLog vl
		                Where vl.SiteUid = l.ProjectCode
			                and vl.UserUid = L.UserCode), 'N/A') LastVisitFull
                    ,coalesce((Select Max(Format(vl.VisitTime, 'dd MMM yy HH:mm'))
		                From VisitLog vl
		                Where vl.SiteUid = l.ProjectCode
			               and vl.UserUid = L.UserCode
	  	                  and VisitTime > DATEADD(HOUR, -12, '" + checkDate + "')), 'N/A') Last24 " +
                    " From LinkedProjects L  " +
                    " inner join Projects P on P.ProjectId = L.ProjectCode " +
                    " Where Usercode = '" + uid + "' " +                   
                    " Order by Sequence ";

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