using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.Meetings
{
  public class MeetingsByCompanyCodesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult IncidentsByCompanyId(string id)
    {
      var meetings = from m in db.Meetings
                             where m.MeetingCompanyCode == id
                             select new
                             {
                               m.MeetingId,
                               m.MeetingCompanyCode,
                               m.MeetingType,
                               m.MeetingDate,
                               MeetingHeldBy = m.MeetingHeldBy ?? "",
                               MeetingNotes = m.MeetingNotes ?? "",
                               MeetingActionSteps = m.MeetingActionSteps ?? "",
                               AddedBy = m.AddedBy ?? "",
                               m.LastUpdate
                             };
      meetings.OrderBy(x => x.MeetingDate);
      if (meetings == null)
      {
        return NotFound();
      }
      return Ok(meetings);
    }
  }
}
