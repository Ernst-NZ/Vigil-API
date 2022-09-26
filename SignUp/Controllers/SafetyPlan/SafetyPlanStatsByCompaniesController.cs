using System;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SignUp.Controllers.SafetyPlan
{
    public class SafetyPlanStatsByCompaniesController : ApiController
  {
    [HttpGet]
    public IHttpActionResult SafetyPlanStatsByCompany(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
       @"SET DATEFIRST 1
        Select 'Paln 1', Count(distinct PlanningUid) as Total
        	,(Select Count(distinct PlanningUid)
            From AnnualSafetyPlans
            Where CompanyUid = '" + id + "' " +
        "      AND (CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "      AND (CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "      and DateCompleted IS NOT NULL) as Done " +
        "  ,(Select Count(distinct PlanningUid) " +
        "    From AnnualSafetyPlans " +
        "    Where CompanyUid = '" + id + "' " +
        "      AND (CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "      AND (CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "      and ISNULL(DateCompleted, '') = '') as Outstanding " +
        "From AnnualSafetyPlans " +
        "Where CompanyUid = '" + id + "' " +
        "  AND (CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "  AND (CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "UNION " +
        "Select 'AnnualSafetyPlans 2 Month', Count(distinct PlanningUid) as Total " +
        "	,(Select Count(distinct PlanningUid) " +
        "    From AnnualSafetyPlans " +
        "    Where CompanyUid = '" + id + "' " +
        "      and datepart(mm,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) =month(getdate()) " +
        "      and datepart(yyyy,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) =year(getdate()) " +
        "      and DateCompleted IS NOT NULL) as Done " +
        ",(Select Count(distinct PlanningUid) " +
        "  From AnnualSafetyPlans " +
        "  Where CompanyUid = '" + id + "' " +
        "    and datepart(mm,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) =month(getdate()) " +
        "    and datepart(yyyy,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) =year(getdate()) " +
        "    and ISNULL(DateCompleted, '') = '') as Outstanding " +
        "From AnnualSafetyPlans " +
        "Where CompanyUid = '" + id + "' " +
        "  and datepart(mm,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) =month(getdate()) " +
        "  and datepart(yyyy,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) =year(getdate()) " +
        "UNION " +
        "Select 'AnnualSafetyPlans 3 Older', Count(distinct PlanningUid) as Total " +
        "	,(Select Count(distinct PlanningUid) " +
        "    From AnnualSafetyPlans " +
        "    Where CompanyUid = '" + id + "' " +
        "      and (datepart(mm,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) < month(getdate()) " +
        "           OR datepart(yyyy,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) < year(getdate())) " +
        "      and DateCompleted IS NOT NULL) as Done " +
        ",(Select Count(distinct PlanningUid) " +
        "  From AnnualSafetyPlans " +
        "  Where CompanyUid = '" + id + "' " +
        "    and (datepart(mm,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) < month(getdate()) " +
        "         OR datepart(yyyy,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) < year(getdate())) " +
        "    and ISNULL(DateCompleted, '') = '') as Outstanding " +
        "From AnnualSafetyPlans " +
        "Where CompanyUid = '" + id + "' " +
        "  and (datepart(mm,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) < month(getdate()) " +
        "        OR datepart(yyyy,(CAST((SUBSTRING(DueDate, 1, CHARINDEX(',', DueDate)-1)) as date))) < year(getdate())) ";
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