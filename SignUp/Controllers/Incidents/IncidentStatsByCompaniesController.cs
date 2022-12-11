using SignUp.Models;
using System;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SignUp.Controllers.Incidents
{
    public class IncidentStatsByCompaniesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult IncidentStatsByCompanyCode(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
         @"SET DATEFIRST 1
        Select 'Incidents 1 Week', Count(distinct IncidentID) as Total
        	,(Select Count(distinct IncidentID)
            From Incidents
            Where CompanyId = " + id + " " +
        "      AND (CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "      AND (CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "      and Status = 1 and (Deleted = 0 OR Deleted is null)) as Done " +
        "  ,(Select Count(distinct IncidentID) " +
        "    From Incidents " +
        "    Where CompanyId = " + id + " " +
        "      AND (CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "      AND (CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "      and ISNULL(Status, '') = '' and (Deleted = 0 OR Deleted is null)) as Outstanding " +
        "From Incidents " +
        "Where CompanyId = " + id + " and (Deleted = 0 OR Deleted is null) " +
        "  AND (CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "  AND (CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "UNION " +
        "Select 'Incidents 2 Month', Count(distinct IncidentID) as Total " +
        "	,(Select Count(distinct IncidentID) " +
        "    From Incidents " +
        "    Where CompanyId = " + id + " " +
        "      and datepart(mm,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) =month(getdate()) " +
        "      and datepart(yyyy,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) =year(getdate()) " +
        "      and Status = 1  and (Deleted = 0 OR Deleted is null)) as Done " +
        ",(Select Count(distinct IncidentID) " +
        "  From Incidents " +
        "  Where CompanyId = " + id + " " +
        "    and datepart(mm,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) =month(getdate()) " +
        "    and datepart(yyyy,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) =year(getdate()) " +
        "    and ISNULL(Status, '') = '' and (Deleted = 0 OR Deleted is null)) as Outstanding " +
        "From Incidents " +
        "Where CompanyId = " + id + " and (Deleted = 0 OR Deleted is null) " +
        "  and datepart(mm,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) =month(getdate()) " +
        "  and datepart(yyyy,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) =year(getdate()) " +
        "UNION " +
        "Select 'Incidents 3 Older', Count(distinct IncidentID) as Total " +
        "	,(Select Count(distinct IncidentID) " +
        "    From Incidents " +
        "    Where CompanyId = " + id + " " +
        "      and (datepart(mm,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) < month(getdate()) " +
        "           OR datepart(yyyy,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) < year(getdate())) " +
        "      and Status = 1  and (Deleted = 0 OR Deleted is null)) as Done " +
        ",(Select Count(distinct IncidentID) " +
        "  From Incidents " +
        "  Where CompanyId = " + id + " " +
        "    and (datepart(mm,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) < month(getdate()) " +
        "         OR datepart(yyyy,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) < year(getdate())) " +
        "    and ISNULL(Status, '') = ''  and (Deleted = 0 OR Deleted is null)) as Outstanding " +
        "From Incidents " +
        "Where CompanyId = " + id + "  and (Deleted = 0 OR Deleted is null) " +
        "  and (datepart(mm,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) < month(getdate()) " +
        "        OR datepart(yyyy,(CAST((SUBSTRING(ReportDate, 1, CHARINDEX(',', ReportDate)-1)) as date))) < year(getdate())) ";
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