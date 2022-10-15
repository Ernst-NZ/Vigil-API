using System;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SignUp.Controllers.Briefings
{
    public class BriefingStatsByCompaniesController : ApiController
    {
    [HttpGet]
    public IHttpActionResult BriefingStatsByCompanyCode(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
        @"SET DATEFIRST 1
          SELECT 'Briefing 1  Week'
          ,Count(DISTINCT BriefingUid) AS Total
          ,(SELECT Count(DISTINCT BriefingUid)
             FROM   DailyBriefings
             WHERE  BriefingCompanyUid = '" + id + "' " +
       "      AND (CAST((SUBSTRING(BriefingDate, 1, CHARINDEX(',', BriefingDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
       "      AND (CAST((SUBSTRING(BriefingDate, 1, CHARINDEX(',', BriefingDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
       "      AND (BriefingChecked = 1) " +
       "      And BriefingDeleted is null or BriefingDeleted = 0 ) AS Done " +
       "  ,(SELECT Count(DISTINCT BriefingUid) " +
       "    FROM   DailyBriefings " +
       "     WHERE  BriefingCompanyUid = '" + id + "' " +
       "      AND (CAST((SUBSTRING(BriefingDate, 1, CHARINDEX(',', BriefingDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
       "      AND (CAST((SUBSTRING(BriefingDate, 1, CHARINDEX(',', BriefingDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
       "      AND Isnull(BriefingChecked, '') = '' " +
       "      and BriefingChecked <> 1 " +
       "      And BriefingDeleted is null or BriefingDeleted = 0) AS Outstanding " +
       "FROM   DailyBriefings " +
       "WHERE  BriefingCompanyUid = '" + id + "' " +
       " AND (CAST((SUBSTRING(BriefingDate, 1, CHARINDEX(',', BriefingDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
       " AND (CAST((SUBSTRING(BriefingDate, 1, CHARINDEX(',', BriefingDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
       " And BriefingDeleted is null or BriefingDeleted = 0 " +
       "UNION " +
       "SELECT 'Briefing 2 Month' " +
       "  ,Count(DISTINCT BriefingUid) AS Total  " +
       "   ,(SELECT Count(DISTINCT BriefingUid) " +
       "       FROM   DailyBriefings " +
       "       WHERE  BriefingCompanyUid = '" + id + "' " +
       "        AND Datepart(mm, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate) - 1) ) AS DATE) )) = Month(Getdate()) " +
       "        AND Datepart(yyyy, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate ) - 1) ) AS DATE) )) = Year(Getdate()) " +
       "        AND (BriefingChecked = 1) " +
       "        And BriefingDeleted is null or BriefingDeleted = 0) AS Done " +
       "   ,(SELECT Count(DISTINCT BriefingUid) " +
       "       FROM   DailyBriefings " +
       "       WHERE  BriefingCompanyUid = '" + id + "' " +
       "        AND Datepart(mm, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate) - 1) ) AS DATE) )) = Month(Getdate()) " +
       "        AND Datepart(yyyy, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate ) - 1) ) AS DATE) )) = Year(Getdate()) " +
       "        AND Isnull(BriefingChecked, '') = '' " +
       "        AND BriefingChecked <> 1 " +
       "        And BriefingDeleted is null or BriefingDeleted = 0) AS Outstanding " +
       "FROM   DailyBriefings " +
       "WHERE  BriefingCompanyUid = '" + id + "' " +
       "AND Datepart(mm, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate) - 1) ) AS DATE) )) = Month(Getdate()) " +
       "AND Datepart(yyyy, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate ) - 1) ) AS DATE) )) = Year(Getdate()) " +
       " And BriefingDeleted is null or BriefingDeleted = 0 " +
       "UNION " +
       "SELECT 'Briefing 3 Older' " +
       "  ,Count(DISTINCT BriefingUid) AS Total  " +
       "   ,(SELECT Count(DISTINCT BriefingUid) " +
       "       FROM   DailyBriefings " +
       "       WHERE  BriefingCompanyUid = '" + id + "' " +
       "        AND (Datepart(mm, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate) - 1) ) AS DATE) )) < Month(Getdate()) " +
       "             OR Datepart(yyyy, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate ) - 1) ) AS DATE) )) < Year(Getdate())) " +
       "        AND (BriefingChecked = 1) " +
       "        And BriefingDeleted is null or BriefingDeleted = 0) AS Done " +
       "   ,(SELECT Count(DISTINCT BriefingUid) " +
       "       FROM   DailyBriefings " +
       "       WHERE  BriefingCompanyUid = '" + id + "' " +
       "        AND (Datepart(mm, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate) - 1) ) AS DATE) )) < Month(Getdate()) " +
       "             OR Datepart(yyyy, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate ) - 1) ) AS DATE) )) < Year(Getdate())) " +
       "        AND Isnull(BriefingChecked, '') = '' " +
       "        AND BriefingChecked <> 1 " +
       "        And BriefingDeleted is null or BriefingDeleted = 0) AS Outstanding " +
       "FROM   DailyBriefings " +
       "WHERE  BriefingCompanyUid = '" + id + "' " +
       "AND (Datepart(mm, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate) - 1) ) AS DATE) )) < Month(Getdate()) " +
       "     OR Datepart(yyyy, ( Cast(( Substring(BriefingDate, 1, Charindex(',', BriefingDate ) - 1) ) AS DATE) )) < Year(Getdate())) " +
       " And BriefingDeleted is null or BriefingDeleted = 0 ";
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