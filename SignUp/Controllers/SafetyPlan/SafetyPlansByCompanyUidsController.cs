using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.SafetyPlan
{
    public class SafetyPlansByCompanyUidsController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult SafetyPlansByCompanyUid(string id)
    {
      var safetyPlans = from i in db.AnnualSafetyPlans
                        join w in db.WebUsers on i.ResponsiblePersonUid equals w.UserId
                        where i.CompanyUid == id
                           select new
                           {
                             i.PlanningUid,
                             i.CompanyUid,
                             DueDate = i.DueDate ?? "",
                             Objective = i.Objective ?? "",
                             ActionRequired = i.ActionRequired ?? "",
                             ResponsiblePersonUid = i.ResponsiblePersonUid ?? "",
                             ResponsiblePersonName = w.UserFirstName + " " + w.UserLastName,
                             DateCompleted = i.DateCompleted ?? "",
                             Comments = i.Comments ?? "",
                             LastCheck = i.LastCheck ?? "",
                             CheckedBy = i.CheckedBy ?? "",
                             AddedBy = i.AddedBy ?? "",
                             i.DateAdded,
                             LastUpdate = i.LastUpdate ?? "",
                             UpdateBy = i.UpdateBy ?? "",
                             i.DeletedBy,
                             Deleted = i.Deleted ?? false,
                           };
      safetyPlans.OrderBy(x => x.DueDate);
      if (safetyPlans == null)
      {
        return NotFound();
      }
      return Ok(safetyPlans);
    }
  }
}
