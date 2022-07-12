using SignUp.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.Incidents
{
  public class IncidentsByUserIdsController : ApiController
  {
    private Entities db = new Entities();
    [AllowAnonymous]
    [HttpPut] 
    public IHttpActionResult IncidentsByUserId(string id, dynamic webUser)
    {
      string userID = (webUser["UserId"]);
      var companyIncidents = from i in db.Incidents
                             join w in db.WebUsers on i.AddedBy equals w.UserId
                             where i.AddedBy == userID
                             select new
                             {
                               i.IncidentId,
                               i.IncidentDate,
                               IncidentTime = i.IncidentTime ?? "",
                               i.IncidentLocation,
                               PartyName = i.PartyName ?? "",
                               i.IncidentInjuries,
                               i.IncidentDescription,
                               ReportDate = i.ReportDate ?? "",
                               FirstReportBy = w.UserFirstName + " " + w.UserLastName ?? "",
                               i.EmailSent,
                               EmailTo = i.EmailTo ?? "",
                               i.Status,
                               StatusDetail = i.StatusDetail ?? "",
                               i.Acknowledged,
                               AcknowledgedBy = i.AcknowledgedBy ?? "",
                               AcknowledgeDate = i.AcknowledgeDate ?? "",
                               IncidentType = i.IncidentType ?? "",
                               TreatmentType = i.TreatmentType ?? "",
                               Treatment = i.Treatment ?? "",
                               TreatmentBy = i.TreatmentBy ?? "",
                               UrgentAction = i.UrgentAction ?? "",
                               IncidentSeverity = i.IncidentSeverity ?? "",
                               ManagementComments = i.ManagementComments ?? "",
                               ManagementCommentsBy = i.ManagementCommentsBy ?? "",
                               ManagementCommentsDate = i.ManagementCommentsDate ?? "",
                               i.ReportableIncident,
                               i.Reported,
                               ReportedBy = i.ReportedBy ?? "",
                               ReportedDate = i.ReportedDate ?? "",
                               i.FeedbackToAffected,
                               FeedbackDate = i.FeedbackDate ?? "",
                               FeedbackBy = i.FeedbackBy ?? "",
                               i.CompanyId,
                               AddedBy = i.AddedBy ?? "",
                               Deleted = i.Deleted ?? false,
                               DeletedBy = i.DeletedBy ?? ""
                             };
      companyIncidents.OrderBy(x => x.ReportedDate);
      if (companyIncidents == null)
      {
        return NotFound();
      }
      return Ok(companyIncidents);
    }
  }
}
