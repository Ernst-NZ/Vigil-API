using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace SignUp.Controllers
{
  public class LinkedProjectsByUsernameController : ApiController
  {
    [HttpGet]
    public IHttpActionResult GetLinkedProjectsByUsername(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query = @"Select Distinct Projects.ProjectName,
                      LinkedProjects.LinkedId
                    From LinkedProjects
                    Inner Join Projects on Projects.ProjectId = LinkedProjects.ProjectCode
                    Where LinkedProjects.UserCode = '" + id + "' ";

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
