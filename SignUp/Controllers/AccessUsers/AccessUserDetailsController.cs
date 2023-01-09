using SignUp.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignUp.Controllers.AccessUsers
{
    public class AccessUserDetailsController : ApiController
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
              @"SELECT Distinct [UserAccessUid]
                  ,UserFirstName + ' ' + UserLastName userName 
                  ,CompanyName
                  ,Format(ActionDate, 'dd-MMM-yyyy HH:mm:ss') as 'ActionDate'
                  ,[Action]
                  ,[Device]
                  ,[DeviceType]
                  ,[DeviceUserAgent]
                  ,[DeviceOs]
                  ,[DeviceOsVersion]
                  ,[DeviceBrowser]
                  ,[Latitude]
                  ,[Longitude]
                  ,[AppVersion]
                  ,(Select Count(au.UserAccessUid) 
                      From AccessUser au
                      Where au.UserUid = AccessUser.UserUid and au.Action = AccessUser.Action) as total
              FROM [Vigil].[dbo].[AccessUser]
              inner join company on company.CompanyUid = AccessUser.companyuid
              inner join WebUser on WebUser.UserId = AccessUser.UserUid
              order by CompanyName, userName, ActionDate desc";
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