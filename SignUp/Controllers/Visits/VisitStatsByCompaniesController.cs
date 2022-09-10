using SignUp.Models;
using System;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SignUp.Controllers.Visits
{
    public class VisitStatsByCompaniesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult VisitStatsByByCompanyCode(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
         @"SET DATEFIRST 1
          Select 'Visit 1 Week', Count(distinct VisitId) as Total
	          ,(Select Count(distinct VisitId)
            From VisitRegister
            Where CompanyCode = '" + id + "' " +
         "   AND DateIn >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
         "   AND DateIn <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
         "   and DateOut IS NOT NULl) as Done " +
         "   ,(Select Count(distinct VisitId) " +
         "   From VisitRegister " +
         "   Where CompanyCode = '" + id + "' " +
         "   AND DateIn >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
         "   AND DateIn <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
         "   and ISNULL(DateOut, '') = '') as Outstanding " +
         "   From VisitRegister " +
         "   Where CompanyCode = '" + id + "' " +
         "   AND DateIn >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
         "   AND DateIn <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
         "   union " +
         "   Select 'Visit 2 Month', Count(distinct VisitId) as Total " +
         "   ,(Select Count(distinct VisitId) " +
         "   From VisitRegister " +
         "   Where CompanyCode = '" + id + "' " +
         "   and datepart(mm,DateIn) =month(getdate()) " +
         "   and datepart(yyyy,DateIn) =year(getdate()) " +
         "   and DateOut IS NOT NULl) as Done " +
         "   ,(Select Count(distinct VisitId) " +
         "   From VisitRegister " +
         "   Where CompanyCode = '" + id + "' " +
         " and datepart(mm,DateIn) =month(getdate()) " +
         " and datepart(yyyy,DateIn) =year(getdate()) " +
         "   and ISNULL(DateOut, '') = '') as Outstanding " +
         "   From VisitRegister " +
         "   Where CompanyCode = '" + id + "' " +
         "   and datepart(mm,DateIn) =month(getdate()) " +
         " and datepart(yyyy,DateIn) =year(getdate())  " +
         "   union " +
         "   Select 'Visit 3 Older', Count(distinct VisitId) as Total " +
         "   ,(Select Count(distinct VisitId) " +
         "   From VisitRegister " +
         "   Where CompanyCode = '" + id + "' " +
         "   and (datepart(mm,DateIn) < month(getdate()) " +
         "        OR datepart(yyyy,DateIn) < year(getdate())) " +
         "   and DateOut IS NOT NULl) as Done " +
         "   ,(Select Count(distinct VisitId) " +
         "   From VisitRegister " +
         "   Where CompanyCode = '" + id + "' " +
         " and (datepart(mm,DateIn) < month(getdate()) " +
         "      OR datepart(yyyy,DateIn) < year(getdate())) " +
         "   and ISNULL(DateOut, '') = '') as Outstanding " +
         "   From VisitRegister " +
         "   Where CompanyCode = '" + id + "' " +
         "   and (datepart(mm,DateIn) < month(getdate()) " +
         "        OR datepart(yyyy,DateIn) < year(getdate()))  ";
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