using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace SignUp.Controllers
{
    public class LinkedProjectsOutstandingForUserNameController : ApiController
  {
    [HttpGet]
    public IHttpActionResult LinkedProjectsOutstandingForUserName(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query = @"Select Distinct Company.companyId,
	                      Projects.ProjectName,
                        Projects.ProjectId
                      From Webuser
                      Inner Join Company on Company.CompanyCode = WebUser.UserCompanyCode
                      Inner Join Projects on Projects.CompanyId = Company.companyId
                      Where WebUser.UserId = '" + id + "' " +
                        "and Projects.ProjectId NOT IN( " +
                            "select distinct LinkedProjects.ProjectCode " +
                                "From LinkedProjects	" +
                                "where LinkedProjects.UserCode = WebUser.UserId)";

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
