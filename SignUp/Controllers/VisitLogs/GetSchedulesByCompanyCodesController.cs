using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;

namespace SignUp.Controllers.VisitLogs
{
    public class GetSchedulesByCompanyCodesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult BriefingStatsByCompanyCode(string id)
        {
            var code = id;
            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query =
                    @"select distinct WU.UserFirstName + ' ' + wu.UserLastName as UserName
	                ,wu.UserId
	                ,Case when count(LP.LinkedId) = 0 then 9999 else count(LP.LinkedId) end  SortId
                from WebUser WU
                Left outer join LinkedProjects LP on LP.UserCode = WU.UserId
                where usercompanyCode = '" + code + "' " +
              "  Group by WU.UserFirstName " +
              "      , wu.UserLastName " +
              "      ,wu.UserId " +
              "  Order by SortId, Username";

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