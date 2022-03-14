using SignUp.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace SignUp.Controllers
{
  public class ProjectsByCompanyIdController : ApiController
  {
    [HttpGet]
    public IHttpActionResult GeProjectByCompanyId(string id)
    {
            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query = @"SELECT Projects.ProjectId
                              ,Projects.ProjectCode
                              ,Projects.CompanyId
                              ,Projects.ProjectName
                              ,coalesce(Projects.ContactFirstName, '') ContactFirstName
                              ,coalesce(Projects.ContactLastName, '') ContactLastName
                              ,coalesce(Projects.ContactEmail, '') ContactEmail
                              ,coalesce(Projects.ContactPhone, '') ContactPhone
                              ,coalesce(Projects.ProjectLocation, '') ProjectLocation
                              ,coalesce(Projects.ProjectStatus, '') ProjectStatus
                              ,coalesce(Projects.AddedBy, '') AddedBy
                              ,coalesce(Projects.Date, '') Date
	                          ,count(FD.Id) as Docs
                          FROM Projects
                            Left Join FileData as FD on FD.ParentId = Projects.ProjectId 
                                 AND FD.ParentName = 'Project'   
                          Where Projects.CompanyId = " + id + " " +
                          "Group by Projects.ProjectId" +
                               ", Projects.ProjectCode" +
                               ", Projects.CompanyId" +
                               ", Projects.ProjectName" +
                               ", Projects.ContactFirstName" +
                               ", Projects.ContactLastName" +
                               ", Projects.ContactEmail" +
                               ", Projects.ContactPhone" +
                               ", Projects.ProjectLocation" +
                               ", Projects.ProjectStatus" +
                               ", Projects.AddedBy" +
                               ", Projects.Date";

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
