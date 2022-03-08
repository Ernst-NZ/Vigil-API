using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers.Modules
{
    public class ModulesOutstandingController : ApiController
    {
    [HttpGet]
    public IHttpActionResult GetOutstandingProjectsByUsername(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
        @"Select Distinct Modules.ModuleCode,
	          Modules.ModuleDescription
          From Modules
          Where Modules.ModuleCode NOT IN(
	          Select Distinct LinkedModules.ModuleCode 
		          From LinkedModules
                  where LinkedModules.companyCode = '" + id + "' )";

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
