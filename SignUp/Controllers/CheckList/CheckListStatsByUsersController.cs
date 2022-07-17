using SignUp.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace SignUp.Controllers.CheckList
{
  public class CheckListStatsByUsersController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult CheckListStatsByUserUid(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
        @"SET DATEFIRST 1
          SELECT 'Checks Week'
          ,Count(DISTINCT checklistuid) AS Total
          ,(SELECT Count(DISTINCT checklistuid)
             FROM   checklistlog
             WHERE  AddedBy = '" + id + "' " +
        "      AND (CAST((SUBSTRING(CheckListDate, 1, CHARINDEX(',', CheckListDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "      AND (CAST((SUBSTRING(CheckListDate, 1, CHARINDEX(',', ReportCheckListDateDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "        AND checklistfinalstatus = 1) AS Done " +
        "  ,(SELECT Count(DISTINCT checklistuid) " +
        "    FROM   checklistlog " +
        "     WHERE  AddedBy = '" + id + "' " +
        "     AND (CAST((SUBSTRING(CheckListDate, 1, CHARINDEX(',', CheckListDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "     AND (CAST((SUBSTRING(CheckListDate, 1, CHARINDEX(',', CheckListDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "        AND Isnull([checklistfinalstatus], '') = '') AS Outstanding " +
        "FROM   checklistlog " +
        "WHERE  AddedBy = '" + id + "' " +
        "  AND (CAST((SUBSTRING(CheckListDate, 1, CHARINDEX(',', CheckListDate)-1)) as date)) >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "  AND (CAST((SUBSTRING(CheckListDate, 1, CHARINDEX(',', CheckListDate)-1)) as date)) <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
        "UNION " +
        "SELECT 'Checks Month' " +
        "  ,Count(DISTINCT checklistuid) AS Total  " +
        "   ,(SELECT Count(DISTINCT checklistuid) " +
        "       FROM   checklistlog " +
        "       WHERE  AddedBy = '" + id + "' " +
        "        AND Datepart(mm, ( Cast(( Substring(checklistdate, 1, Charindex(',', checklistdate) - 1) ) AS DATE) )) = Month(Getdate()) " +
        "        AND Datepart(yyyy, ( Cast(( Substring(checklistdate, 1, Charindex(',', checklistdate ) - 1) ) AS DATE) )) = Year(Getdate()) " +
        "        AND [checklistfinalstatus] = 1) AS Done " +
        "   ,(SELECT Count(DISTINCT checklistuid) " +
        "       FROM   checklistlog " +
        "       WHERE  AddedBy = '" + id + "' " +
        "        AND Datepart(mm, ( Cast(( Substring(checklistdate, 1, Charindex(',', checklistdate) - 1) ) AS DATE) )) = Month(Getdate()) " +
        "        AND Datepart(yyyy, ( Cast(( Substring(checklistdate, 1, Charindex(',', checklistdate ) - 1) ) AS DATE) )) = Year(Getdate()) " +
        "        AND Isnull([checklistfinalstatus], '') = '') AS Outstanding " +
        "FROM   checklistlog " +
        "WHERE  AddedBy = '" + id + "' " +
        "AND Datepart(mm, ( Cast(( Substring(checklistdate, 1, Charindex(',', checklistdate) - 1) ) AS DATE) )) = Month(Getdate()) " +
        "AND Datepart(yyyy, ( Cast(( Substring(checklistdate, 1, Charindex(',', checklistdate ) - 1) ) AS DATE) )) = Year(Getdate()) "; ;
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