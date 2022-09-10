using System;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace SignUp.Controllers.Chemicals
{
    public class ChemicalsByCompanyUidsController : ApiController
  {   
    [HttpGet]
    public IHttpActionResult TrainingByCompanyId(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
        @"SELECT Chemical.Id 
          ,Chemical.CompanyUid 
          ,Chemical.userCode 
          ,coalesce(ChemicalName, '') ChemicalName 
          ,coalesce(Manufacturer, '') Manufacturer 
          ,coalesce(SDSDate, '') SDSDate 
          ,MaxQty 
          ,OnSiteQty 
          ,Labelled 
          ,RiskAssessment 
          ,Controls 
          ,SafetySheet 
          ,SafetyLink 
          ,coalesce(Chemical.Comments, '') Comments 
          ,coalesce(LastCheck, '') LastCheck 
          ,coalesce(CheckBy, '') CheckBy 
          ,Chemical.AddedBy 
          ,Chemical.LastUpdate 
          ,Chemical.LastUpdateBy 
          ,count(FD.Id) as Docs
        FROM Chemical
          left Join FileData as FD on FD.ParentId = Chemical.Id 
            AND FD.ParentName = 'Chemical'   
        Where Chemical.CompanyUId =  '" + id + "' " +
        "Group by Chemical.id, Chemical.CompanyUid " +
        "  , Chemical.userCode " +
        "  ,ChemicalName " +
        "  ,Manufacturer " +
        "  ,SDSDate " +
        "  ,MaxQty " +
        "  ,OnSiteQty " +
        "  ,Labelled " +
        "  ,RiskAssessment " +
        "  ,Controls " +
        "  ,SafetySheet " +
        "  ,SafetyLink " +
        "  ,Chemical.Comments " +
        "  ,LastCheck " +
        "  ,CheckBy " +
        "  ,Chemical.AddedBy " +
        "  ,Chemical.LastUpdate " +
        "  ,Chemical.LastUpdateBy " +
        " Order by ChemicalName";

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
    //public IHttpActionResult GetChemicalsByCompanyUid(string id)
    //{
    //  var chemicals = from chem in db.Chemicals
    //                    where chem.CompanyUid == id
    //                    select chem;
    //  chemicals.OrderBy(x => x.ChemicalName);
    //  if (chemicals == null)
    //  {
    //    return NotFound();
    //  }
    //  return Ok(chemicals);
    //}
  }
}
