using SignUp.Models;
using System;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SignUp.Controllers.Meetings
{
    public class MeetingStatsByCompaniesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult MeetingStatsByCompanyCode(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
         @"SET DATEFIRST 1
          Select 'Meeting 1 Week', Count(distinct MeetingId) as Total	
            From Meetings
            Where MeetingCompanyCode = '" + id + "' " +
         "   AND (CAST((SUBSTRING(MeetingDate, 1, CHARINDEX(',', MeetingDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
         "   AND (CAST((SUBSTRING(MeetingDate, 1, CHARINDEX(',', MeetingDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
         "   And Deleted is Null " +
         "   union " +
         "   Select 'Meeting 2 Month', Count(distinct MeetingId) as Total " +
         "   From Meetings " +
         "   Where MeetingCompanyCode = '" + id + "' " +
         "   and datepart(mm,(CAST((SUBSTRING(MeetingDate, 1, CHARINDEX(',', MeetingDate)-1)) as date))) =month(getdate()) " +
         "   and datepart(yyyy,(CAST((SUBSTRING(MeetingDate, 1, CHARINDEX(',', MeetingDate)-1)) as date))) =year(getdate()) " +
         " And Deleted is Null " +
         "   union " +
         "   Select 'Meeting 3 Older', Count(distinct MeetingId) as Total " +
         "   From Meetings " +
         "   Where MeetingCompanyCode = '" + id + "' " +         
         "   and datepart(yyyy,(CAST((SUBSTRING(MeetingDate, 1, CHARINDEX(',', MeetingDate)-1)) as date))) =year(getdate()) " +
         " And Deleted is Null ";
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