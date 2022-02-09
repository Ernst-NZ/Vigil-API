using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace SignUp.Controllers
{
    public class WebUsersByCompaniesController : ApiController
    {
    [HttpGet]
    public IHttpActionResult GetUsers(int id)
    {
 
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query = @"Select webuser.UserId,
        	UserFirstName + ' ' + coalesce(UserLastName, '') as Username,
    	    UserEmail,
        	UserPhoneNumber,
        	DepartmentName,
            FunctionName,
        	UserNotActive,
        	Profiles.ProfileName,
        	Company.CompanyName,
        	count(linkedprojects.ProjectCode) as Projects,
        	count(Training.TrainingId) as Training
        from WebUser	
          inner join Company on WebUser.UserCompanyCode = Company.CompanyCode
          inner join profiles on WebUser.UserProfileCode = Profiles.ProfileLevel
          Left Join Departments on Departments.DepartmentId = WebUser.UserDepartmentCode
          Left Join Functions on WebUser.UserFunctionCode = Functions.FunctionId
          left join LinkedProjects on WebUser.userId = LinkedProjects.UserCode
          Left Join Training on Training.UserId = WebUser.UserId
        Where Company.CompanyId = '" + id + "'  " +
        "Group by webUser.UserId, " +
          "UserFirstName, " +
          "UserlastName, " +
          "UserEmail, " +
          "UserPhoneNumber, " +
          "UserDepartmentCode, " +
          "UserFunctionCode, " +
           "UserProfileCode, " +
          "UserNotActive, " +
          "CompanyName, " +
          "ProfileName, " +
          "DepartmentName, " +
          "FunctionName";
  
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
