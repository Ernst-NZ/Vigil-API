using SignUp.Models;
using System;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SignUp.Controllers.Incidents
{
  public class HazardStatsByUsersController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult IncidentStatsByUserCode(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
         @"SET DATEFIRST 1
        Select 'Hazard 1 Week', Count(distinct HazardUID) as Total
	          ,(Select Count(distinct HazardUID)
            From Hazards
            Where AddedBy = '" + id + "' and (Deleted = 0 OR Deleted is null) " +
            "AND HazardDate >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
            "AND HazardDate <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
            "and InitiatorSignOffDate IS NOT NULl " +
            "AND InitiatorSignOffDate <> '' " +
            ") as Done " +
            "     ,(Select Count(distinct HazardUID) " +
            "     From Hazards " +
            "     Where AddedBy = '" + id + "' and (Deleted = 0 OR Deleted is null) " +
            "     AND HazardDate >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
            "     AND HazardDate <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
            "     and ISNULL(InitiatorSignOffDate, '') = '') as Outstanding " +
            "     From Hazards " +
            "     Where AddedBy = '" + id + "' and (Deleted = 0 OR Deleted is null) " +
            "     AND HazardDate >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
            "     AND HazardDate <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) " +
            "     union " +
            "     Select 'Hazard 2 Month', Count(distinct HazardUID) as Total " +
            "     ,(Select Count(distinct HazardUID) " +
            "     From Hazards " +
            "     Where AddedBy = '" + id + "' and (Deleted = 0 OR Deleted is null) " +
            "     and datepart(mm,HazardDate) =month(getdate()) " +
            "     and datepart(yyyy,HazardDate) =year(getdate()) " +
            "     and InitiatorSignOffDate IS NOT NULl " +
            " AND InitiatorSignOffDate <> '') as Done " +
            "       ,(Select Count(distinct HazardUID) " +
            "       From Hazards " +
            "       Where AddedBy = '" + id + "' and (Deleted = 0 OR Deleted is null) " +
            "     and datepart(mm,HazardDate) =month(getdate()) " +
            "     and datepart(yyyy,HazardDate) =year(getdate()) " +
            "       and ISNULL(InitiatorSignOffDate, '') = '') as Outstanding " +
            "       From Hazards   " +
            "       Where AddedBy = '" + id + "' and (Deleted = 0 OR Deleted is null) " +
            "       and datepart(mm,HazardDate) =month(getdate()) " +
            "     and datepart(yyyy,HazardDate) =year(getdate()) " +
            "       union " +
            "       Select 'Hazard 3 Older', Count(distinct HazardUID) as Total " +
            "       ,(Select Count(distinct HazardUID) " +
            "       From Hazards " +
            "       Where AddedBy = '" + id + "' and (Deleted = 0 OR Deleted is null) " +
            "       and (datepart(mm,HazardDate) <month(getdate()) " +
            "       OR datepart(yyyy,HazardDate) <year(getdate())) " +
            "       and InitiatorSignOffDate IS NOT NULl " +
            "      AND InitiatorSignOffDate <> '') as Done " +
            "           ,(Select Count(distinct HazardUID) " +
            "          From Hazards " +
            "         Where AddedBy = '" + id + "' and (Deleted = 0 OR Deleted is null)  " +
            "      and (datepart(mm,HazardDate) <month(getdate())   " +
            "     OR datepart(yyyy,HazardDate) <year(getdate()))   " +
            "       and ISNULL(InitiatorSignOffDate, '') = '') as Outstanding   " +
            "       From Hazards   " +
            "       Where AddedBy = '" + id + "' and (Deleted = 0 OR Deleted is null)  " +
            "       and (datepart(mm,HazardDate) <month(getdate())   " +
            "     OR datepart(yyyy,HazardDate) <year(getdate()))";
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