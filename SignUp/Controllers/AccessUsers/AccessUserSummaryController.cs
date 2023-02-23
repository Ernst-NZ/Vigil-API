using SignUp.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Http.Description;


namespace SignUp.Controllers.AccessUsers
{
    public class AccessUserSummaryController : ApiController
    {
        private Entities db = new Entities();
        //  [Authorize]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(void))]
        public IHttpActionResult GetImagesByRefAndId()
        {
            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query =
              @"SELECT Distinct CompanyName
	                ,(Select Count(distinct ac.UserUid)
		                From AccessUser ac
                        Where ac.CompanyUid = AccessUser.companyUid ) as Comptotal
	                ,UserFirstName + ' ' + UserLastName userName
                    ,(Select (max(App.Appversion)) 
		                From AccessUser App
		                Where App.UserUid = accessuser.UserUid) as AppVersion
	                ,(Select Count(au.UserAccessUid)
		                From AccessUser au
                        Where au.UserUid = AccessUser.UserUid ) as total
	                ,(Select Format(max(ud.actiondate), 'dd-MMM-yy  HH:MM') 
		                From AccessUser ud
		                Where ud.UserUid = accessuser.UserUid) as lastAccess
	                ,cast((Select Format(max(ud.actiondate), 'dd-MMM-yy  HH:MM') 
		                From AccessUser ud
		                Where ud.UserUid = accessuser.UserUid) as datetime) as lastdate
                FROM [Vigil].[dbo].[AccessUser]
	                inner join company on company.CompanyUid = AccessUser.companyuid
                    inner join WebUser on WebUser.UserId = AccessUser.UserUid              
                order by CompanyName, userName";
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