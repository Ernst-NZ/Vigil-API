using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.Hazards
{
    public class HazardsByCompanyIdsController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult HazardsByCompanyUID(string id)
    {
      var companyHazards = from i in db.Hazards
                             where i.CompanyUID == id
                             select new
                             {
                               i.HazardUID,
                               i.CompanyUID,
                               Name = i.Name ?? "",
                               Date = i.HazardDate ?? "",
                               Time = i.Time ?? "",
                               Site = i.Site ?? "",
                               i.Type,
                               i.Position,
                               Description = i.Description ?? "",
                               LocationOnSite = i.LocationOnSite ?? "",
                               i.RiskMatrix,
                               i.ImmediateFix,
                               i.ImmediateDescription,
                               i.Isolated,
                               i.Reported,
                               SuggestedSolution = i.SuggestedSolution ?? "",
                               i.SupervisorName,
                               i.SupervisorPosition,
                               i.SupervisorAction,
                               i.InitiatorAdvised,
                               SupervisorSignOff = i.SupervisorSignOff ?? "",
                               InitiatorSignOgg = i.InitiatorSignOff ?? "",
                               SupervisorSignOffDate = i.SupervisorSignOffDate ?? "",
                               i.InitiatorSignOffDate,
                               Status = i.Status ?? "",
                               AddedBy = i.AddedBy ?? "",
                               i.Created_Date,
                               i.LastUpdate_By,
                               Updated_Date = i.Updated_Date ?? "",
                               i.Deleted_By,
                               i.Deleted_Date,
                               Deleted = i.Deleted ?? false,
                               Longitude = i.Longitude ?? "",
                               Latitude = i.Latitude ?? ""
                             };
      companyHazards.OrderBy(x => x.Date);
      if (companyHazards == null)
      {
        return NotFound();
      }
      return Ok(companyHazards);
    }
  }
}
