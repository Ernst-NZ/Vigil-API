using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.Incidents
{
    public class IncidentsByCompanyIdsController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult IncidentsByCompanyId(int id)
    {
      var companyIncidents = from i in db.Incidents
                            where i.CompanyId == id
                            select new
                            {
                              i.IncidentId,
                              i.IncidentType,
                              i.IncidentLocation,
                              i.IncidentDate,
                              IncidentTime = i.IncidentTime ?? "",
                              PartyName = i.PartyName ?? "",
                              i.IncidentInjuries,
                              TreatmentType = i.TreatmentType ?? "",
                              UrgentAction = i.UrgentAction ?? "",
                              IncidentSeverity = i.IncidentSeverity ?? "",
                              i.IncidentDescription,
                              TreatmentBy = i.TreatmentBy ?? "",
                              i.Status,
                              StatusDetail = i.StatusDetail ?? "",
                              i.EmailSent,
                              EmailTo = i.EmailTo ?? "",
                              i.Acknowledged,
                              AcknowledgedBy = i.AcknowledgedBy ?? "",
                              AcknowledgeDate = i.AcknowledgeDate ?? "",
                              ManagementComments = i.ManagementComments ?? "",
                              ReportDate = i.ReportDate ?? "",
                              i.ReportableIncident,
                              i.Reported,
                              ReportedBy = i.ReportedBy ?? "",
                              ReportedDate = i.ReportedDate ?? "",
                              i.CompanyId,
                              AddedBy = i.AddedBy ?? "",
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
