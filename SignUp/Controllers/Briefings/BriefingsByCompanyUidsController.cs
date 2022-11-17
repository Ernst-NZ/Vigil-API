using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.Briefings
{
    public class BriefingsByCompanyUidsController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult BriefingsByCompanyUid(string id)
    {
      var briefings = from b in db.DailyBriefings
                     where b.BriefingCompanyUid == id
                     select new
                     {
                       b.BriefingAddedBy,
                       BriefingChecked = b.BriefingChecked ?? false,
                       BriefingCheckedBy = b.BriefingCheckedBy ?? "",
                       BriefingComments = b.BriefingComments ?? "",
                       b.BriefingCompanyUid,
                       BriefingCompletedBy = b.BriefingCompletedBy ?? "",
                       b.BriefingCreatedAt,
                       BriefingDate = b.BriefingDate ?? "",
                       BriefingDeleted = b.BriefingDeleted ?? false,
                       b.BriefingDeletedBy,
                       BriefingEndOne = b.BriefingEndOne ?? "",
                       BriefingEndThree = b.BriefingEndThree ?? "",
                       BriefingEndTwo = b.BriefingEndTwo ?? "",
                       BriefingLocation = b.BriefingLocation ?? "",
                       BriefingSafeToWork  = b.BriefingSafeToWork ?? false,
                       BriefingStartOne = b.BriefingStartOne ?? "",
                       BriefingStartThree = b.BriefingStartThree ?? "",
                       BriefingStarttwo = b.BriefingStarttwo ?? "",
                       BriefingTotalHours = b.BriefingTotalHours ?? "",
                       BriefingInReachMessage = b.BriefingInReachMessage ?? false,
                       BriefingInReachTracking = b.BriefingInReachTracking ?? false,
                       b.BriefingUid
                     };
      briefings.OrderBy(x => x.BriefingDate).ThenBy(n => n.BriefingLocation);
      if (briefings == null)
      {
        return NotFound();
      }
      return Ok(briefings);
    }
  }
}

