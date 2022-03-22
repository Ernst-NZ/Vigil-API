using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers.CheckList
{
  public class DeleteChecklistMastersController : ApiController
  {
    [HttpDelete]
    public IHttpActionResult DeleteChecklistMaster(string id)
    {
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query = @"Delete from ChecklistMaster
                    Where CheckListUID = '" + id + "' ";

      SqlConnection conn = new SqlConnection(connString);
      SqlCommand cmd = new SqlCommand(query, conn);
     
      try
      {
        conn.Open();
        cmd.ExecuteNonQuery();
        return Ok();
      }
      catch (Exception ex)
      {
        conn.Close();
        return BadRequest(ex.Message);
      }      
    }
  }
}
