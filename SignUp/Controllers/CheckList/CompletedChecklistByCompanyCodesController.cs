using SignUp.Models;
using System.Linq;
using System.Web.Http;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SignUp.Controllers.CheckList
{
  public class CompletedChecklistByCompanyCodesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult CompletedChecklistByCompanyCode(string id)
    {

      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
        @"Select distinct CheckListName
              ,coalesce(CheckListReference, '') CheckListReference
	            ,CheckListStatus
	            ,coalesce(CheckListEmployeeName, '') CheckListEmployeeName
	            ,coalesce(CheckListDate, '') CheckListDate
	            ,CheckListUID
	            ,Case when CheckListFinalStatus = 1 then 'Checked' when CheckListStatus = 0 then 'Fail' else 'Pass' end as StatusString 
	            ,coalesce(CheckListComments, '') CheckListComments
              ,coalesce(CheckListFinalComments, '') CheckListFinalComments
              ,coalesce(Deleted, 'false') Deleted
              ,coalesce(DeletedBy, '') DeletedBy
              ,AddedBy
	            ,(Select max(S.CheckLogId)
	              From  CheckListLog S
	              Where s.CheckListUID = CheckListLog.CheckListUID) myTime
          From CheckListLog
          Where CheckListCompanyCode = '" + id + "' " +
          "Order by myTime desc";

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