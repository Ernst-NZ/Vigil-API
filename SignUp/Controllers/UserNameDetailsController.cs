using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;


namespace SignUp.Models
{
  public class UserNameDetailsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetUsers(string id)
        {

            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query = @"SELECT TOP (1000) UserId
                              ,UserName
	                          ,UserName as UserCode
	                          ,UserProfileCode as UserRole
	                          ,UserFirstName as FirstName
                              ,UserLastName as UserSurname
                              ,UserCompanyCode as CompanyCode
                              ,CompanyName
                              ,CompanyId
                              ,case when UserNotActive = 1 then 'viewOnly' else 'active' end ViewOnly
                          FROM WebUser
                          inner join Company on Company.companyCode = Webuser.UserCompanyCode
                          Where WebUser.username = '" + id + "' ";

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
