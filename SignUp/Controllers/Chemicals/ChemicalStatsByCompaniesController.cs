using System;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SignUp.Controllers.Chemicals
{
  public class ChemicalStatsByCompaniesController : ApiController
  {
    [HttpGet]
    public IHttpActionResult ChemicalStatsByCompanies(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
       @"SET DATEFIRST 1
        Select 'Chemical 1 SDS Date', Count(distinct Id) as Total
            ,(Select Count(distinct ID)
        From Chemical
        Where CompanyUid = '" + id + "' " +
        "AND SDSDate < DATEADD(MONTH, -54, GETDATE()) " +
        "      ) as Done " +
        "  ,(Select Count(distinct ID) " +
        "    From Chemical " +
        "    Where CompanyUid = '" + id + "' " +
        "      AND (SDSDate < DATEADD(MONTH, -114, GETDATE())) OR ISNULL(SDSDate, '') = '' ) as Outstanding " +
        "From Chemical " + 
        "Where CompanyUid = '" + id + "' " +
        "  and CAST(OnsiteQty as decimal) > 0 " +
        "UNION " +
        "Select 'Chemical 2 Qty', Count(distinct ID) as Total " +
        "	,(Select Count(distinct ID) " +
        "    From Chemical " +
        "    Where CompanyUid = '" + id + "' " +
        "      AND Cast(OnSiteQty As Decimal) > cast((maxqty*80/100) as decimal)    ) as warning " +
        ",(Select Count(distinct ID) " +
        "  From Chemical " +
        "  Where CompanyUid = '" + id + "' " +
        "     AND Cast(OnSiteQty As Decimal) > Cast(maxqty as Decimal) ) as Danger " +
        "From Chemical " +
        "Where CompanyUid = '" + id + "' " +
        "  and CAST(MaxQty as decimal) > 0 " +
        "UNION " +
        "Select 'Chemical 3 Checks', Count(distinct ID) as Total " +
        "	,(Select Count(distinct ID) " +
        "    From Chemical " +
        "    Where CompanyUid = '" + id + "' " +
        "      AND LastCheck < DATEADD(MONTH, -12, GETDATE())  ) as Done " +
        ",(Select Count(distinct ID) " +
        "  From Chemical " +
        "  Where CompanyUid = '" + id + "' " +
        "    AND LastCheck < DATEADD(MONTH, -6, GETDATE())  ) as Outstanding " +
        "From Chemical " +
        "Where CompanyUid = '" + id + "' " +
        "  and CAST(OnsiteQty as decimal) > 0  ";
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