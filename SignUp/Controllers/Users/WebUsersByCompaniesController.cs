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
      string query =
        @"Select webuser.UserId,
        	UserFirstName as Username,
          UserLastName,
    	    UserEmail,
        	coalesce(UserPhoneNumber, '') UserPhoneNumber,
        	coalesce(DepartmentName, '') DepartmentName,
          coalesce(FunctionName,'') FunctionName,
        	coalesce(UserNotActive, '0') UserNotActive,
        	Profiles.ProfileName,
        	Company.CompanyName,
        	count(distinct linkedprojects.ProjectCode) as Projects,
        	count(distinct Training.TrainingId) as Training,
          1 as AppUser
        from WebUser	
          inner join Company on WebUser.UserCompanyCode = Company.CompanyCode
          inner join profiles on WebUser.UserProfileCode = Profiles.ProfileLevel
          Left Join Departments on Departments.DepartmentId = WebUser.UserDepartmentCode
          Left Join Functions on WebUser.UserFunctionCode = Functions.FunctionId
          left join LinkedProjects on WebUser.userId = LinkedProjects.UserCode
          Left Join Training on Training.UserId = WebUser.UserId
        Where Company.CompanyId = '" + id + "' " +
        "Group by webUser.UserId,  " +
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
          "FunctionName " +
        "Union " +
        "Select cast (StaffId as nvarchar(4)), " +
          "StaffName + ' ' + coalesce(StaffSurName, '') as Username, " +
          "StaffSurName, " +
          "'', " +
          "'', " +
          "coalesce(DepartmentName, '') DepartmentName, " +
          "'', " +
          "coalesce(InActive, '0') UserNotActive, " +
          "'', " +
          "Company.CompanyName, " +
          "'' as Projects, " +
          "count(distinct Training.TrainingId) as Training, " +
          "0 as AppUser " +
        "from CompanyStaff " +
        "  inner join Company on CompanyStaff.CompanyCode = Company.CompanyCode " +
        "  Left Join Departments on Departments.DepartmentId = CompanyStaff.UserDepartmentCode " +
        "  Left Join Training on Training.UserId = cast(CompanyStaff.StaffId as varchar(6))  " +
        "Where Company.CompanyId = '" + id + "' " +
         "Group by StaffId,  " +
          "StaffName, " +
          "StaffSurName, " +
          "UserDepartmentCode, " +
          "Inactive, " +
          "CompanyName, " +
          "DepartmentName " +          
        "Order By AppUser desc, Username ";
  
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
