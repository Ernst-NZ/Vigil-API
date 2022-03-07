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
        @"Select Distinct Company.companyId,
	        Module.ModuleCode,
          Module.ModuleDescription          
        From Company
          Inner Join Modules on modules.CompanyCode = Company.companyCode
        Where Company.companyCode = '" + id + "' " +
        "  and Module.ModuleCode NOT IN( " +
        "   select distinct LinkedModules.ModuleCode " +
        "   From LinkedProjects	" +
        "   where LinkedModules.ModuleCode = '" + id + "' )";

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
