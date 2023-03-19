using SignUp.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;

namespace SignUp.Controllers.Projects
{
    public class SiteSequenceController : ApiController
    {
        private Entities db = new Entities();
        [HttpPost]
        public IHttpActionResult PostSiteSequence(dynamic data)
        {
            var companyId = data.companyId;
            var userUid = data.userUid;
            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query =
              @"Select distinct p.ProjectId
                ,p.ProjectName
                ,L.LinkedId
                ,L.Sequence
                ,L.UserCode
                ,COALESCE(Sequence, Cast('9999' as int)) OrderId
                ,(Select max(Sequence)
	                From LinkedProjects 
	                Where UserCode = '" + userUid + "') maxId " +
              " from Projects p " +
              "  inner Join Company on company.CompanyId = P.CompanyId " +
              "  left join LinkedProjects L On L.ProjectCode = p.ProjectId " +
              "   and L.UserCode = '" + userUid + "' " +
              " Where p.CompanyId = " + companyId + " " +
              "order by OrderId, ProjectName";

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