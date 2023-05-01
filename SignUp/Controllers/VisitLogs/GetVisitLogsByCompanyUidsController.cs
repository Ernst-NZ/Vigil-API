using SignUp.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;

namespace SignUp.Controllers.VisitLogs
{
    public class GetVisitLogsByCompanyUidsController : ApiController
    {
        private Entities db = new Entities();
        [HttpPost]
        public IHttpActionResult PostVisitLogsByCompanyUid(dynamic data)
        {
            string companyUid = data.companyUid;
            string fromDate = data.fromDate;
            string toDate = data.toDate;

            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query =
                @"SELECT
                  Created_by as UserName
                  ,VL.UserUid  
	              ,format(VisitTime, 'ddd dd MMM yy') VisitDay
	              ,Count(distinct VisitUid) Visits
                  ,Count(distinct SiteUid) Sites
	              ,Min(VisitTime) FirstVisit
                  ,Max(VisitTime) LastVisit
                  ,(Select Count(VP.Result)
		            From Visitlog VP
		            Where CompanyUid =  '" + companyUid + "' " +
                "    and format(VP.VisitTime, 'ddd dd MMM yy') = format(vl.VisitTime, 'ddd dd MMM yy') " +
                "    and Result = 1 " +
		        "    ) NoIssues " +
                " ,(Select Count(Result) " +
                "    From Visitlog VN " +
                "    Where VN.CompanyUid = '" + companyUid + "' " +
                "    and format(VN.VisitTime, 'ddd dd MMM yy') = format(vl.VisitTime, 'ddd dd MMM yy') " +
                "    and Result = 0) Issues " +
                "  FROM [Vigil].[dbo].[VisitLog] vl " +
                "  inner Join Projects P on P.ProjectId = vl.SiteUid " +
                "  Where CompanyUid = '" + companyUid + "' " +
                "  and VisitTime >= '" + fromDate + "' " +
                "  and VisitTime <= '" + toDate + "'  " +
                "  group by Created_by ,format(VisitTime, 'ddd dd MMM yy'), VL.UserUid " +
                "  Order by Created_by, LastVisit desc";

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