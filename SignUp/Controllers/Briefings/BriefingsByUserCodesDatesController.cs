using SignUp.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Http.Description;


namespace SignUp.Controllers.Briefings
{
    public class BriefingsByUserCodesDatesController : ApiController
  {

    private Entities db = new Entities();
    //  [Authorize]
    [AllowAnonymous]
    [HttpPost]
    [ResponseType(typeof(void))]
    public IHttpActionResult BriefingsByUserCodesDates(dynamic data)
    {
      string uid = data.uid;
      string dateFrom = data.fromDate;
      string dateTo = data.toDate;


      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
        @"SELECT *
       FROM   DailyBriefings 
       WHERE  BriefingAddedBy = '" + uid + "' " +
       " AND (CAST(BriefingDate as date)) >= '" + dateFrom + " ' and (CAST(BriefingDate as date)) <= '" + dateTo + " '   " +
       " Order by CAST(BriefingDate as date) desc, BriefingLocation";
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