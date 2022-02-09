using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignUp.Controllers
{
    public class ProjectsByCompanyIdController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult GeProjectByCompanyId(string id)
    {
            //var projects = from p in db.Projects
            //               join docs in db.ProjectDocs on p.ProjectCode equals docs.ProjectCode
            //               where p.CompanyId == id
            //                select p;
            //projects.OrderBy(x => x.ProjectName);
            //if (projects == null)
            //{
            //  return NotFound();
            //}
            //return Ok(projects);
            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query = @"SELECT Projects.ProjectId
                              ,Projects.ProjectCode
                              ,Projects.CompanyId
                              ,Projects.ProjectName
                              ,Projects.ContactFirstName
                              ,Projects.ContactLastName
                              ,Projects.ContactEmail
                              ,Projects.ContactPhone
                              ,Projects.ProjectLocation
                              ,Projects.ProjectStatus
                              ,Projects.AddedBy
                              ,Projects.Date
	                          ,count(PD.ProjectDocId) as Docs
                          FROM Projects
                            inner Join ProjectDocs as PD on PD.ProjectCode = Projects.ProjectId
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
