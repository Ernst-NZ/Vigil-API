using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers.Visits
{

  public class VisitsByCompanyCodesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult VisitsByCompanyCode(string id)
    {
      var visits = from m in db.VisitRegisters
                     where m.CompanyCode == id
                     select new
                     {
                       m.VisitId,
                       m.CompanyCode,
                       m.DateIn,
                       DateOut = m.DateOut ?? "",
                       m.Name,
                       Signature = m.Signature ?? "",
                       Company = m.Company ?? "",
                       Purpose = m.Purpose ?? "",
                       Comments = m.Comments ?? "",
                       WhoVisit = m.WhoVisit ?? "",
                       TimeIn = m.TimeIn ?? "",
                       TimeOut = m.TimeOut ?? "",
                       m.SignatureOut,
                       DeviceId = m.DeviceId ?? "",
                       Longitude = m.Longitude ?? "",
                       Latitude = m.Latitude ?? "",
                       m.DeviceType,
                       m.IsStaff,
                       m.IsLogin
                     };
      visits.OrderBy(x => x.DateIn);
      if (visits == null)
      {
        return NotFound();
      }
      return Ok(visits);
    }
  }
}
