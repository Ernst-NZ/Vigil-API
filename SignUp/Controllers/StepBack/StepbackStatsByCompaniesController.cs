using SignUp.Models;
using System;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SignUp.Controllers.StepBack
{
    public class StepbackStatsByCompaniesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult StepBackStatsByCompanyCode(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
         @"SET DATEFIRST 1
          Select 'StepBack Week', Count(distinct LogId) as Total	
            From Stepback
            Where CompanyCode = '" + id + "' " +
         "   AND (CAST((SUBSTRING(AddedOn, 1, CHARINDEX(',', AddedOn)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
         "   AND (CAST((SUBSTRING(AddedOn, 1, CHARINDEX(',', AddedOn)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
         "   union " +
         "   Select 'Stepback Month', Count(distinct LogId) as Total " +
         "   From Stepback " +
         "   Where CompanyCode = '" + id + "' " +
         "   and datepart(mm,(CAST((SUBSTRING(AddedOn, 1, CHARINDEX(',', AddedOn)-1)) as date))) =month(getdate()) " +
         "   and datepart(yyyy,(CAST((SUBSTRING(AddedOn, 1, CHARINDEX(',', AddedOn)-1)) as date))) =year(getdate()) ";
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