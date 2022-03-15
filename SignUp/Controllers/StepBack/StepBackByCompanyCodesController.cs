using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.Meetings
{
  public class StepBackByCompanyCodesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult StepBackByCompanyCode(string id)
    {
      //var meetings = from m in db.Meetings
      //               where m.MeetingCompanyCode == id
      //               select new
      //               {
      //                 m.MeetingId,
      //                 m.MeetingCompanyCode,
      //                 m.MeetingType,
      //                 m.MeetingDate,
      //                 MeetingHeldBy = m.MeetingHeldBy ?? "",
      //                 MeetingNotes = m.MeetingNotes ?? "",
      //                 MeetingActionSteps = m.MeetingActionSteps ?? "",
      //                 AddedBy = m.AddedBy ?? "",
      //                 m.LastUpdate
      //               };
      //meetings.OrderBy(x => x.MeetingDate);
      //if (meetings == null)
      //{
      //  return NotFound();
      //}
      //return Ok(meetings);
      var list = from s in db.Stepbacks
                 join user in db.WebUsers on s.AddedBy equals user.Username
                 where s.CompanyCode == id
                 select new
                 {
                   s.LogId,
                   Reference = s.Reference ?? "",
                   s.CompletionDate,
                   GeneralComments = s.GeneralComments ?? "",
                   SupervisorName = s.SupervisorName ?? "",
                   EmployeeName = s.EmployeeName ?? "",
                   AddedBy = user.UserFirstName,
                   s.CompanyCode,
                   s.Status
                 };
      list.OrderBy(x => x.CompletionDate);
      if (list == null)
      {
        return NotFound();
      }
      return Ok(list);
    }
  }
}




//using SignUp.Models;
//using System.Linq;
//using System.Web.Http;

//namespace SignUp.Controllers.StepBack
//{
//  public class StepBackByCompanyCodesController : ApiController
//  {
//    private Entities db = new Entities();
//    [HttpGet]
//    public IHttpActionResult StepBackByCompanyCode(string id)
//    {
//      var list = from s in db.Stepbacks
//                 join user in db.WebUsers on s.AddedBy equals user.Username
//                 where s.CompanyCode == id
//                 select new {
//                   s.LogId,
//                   Reference = s.Reference ?? "",
//                   s.CompletionDate,
//                   GeneralComments = s.GeneralComments ?? "",
//                   s.SupervisorName,
//                   s.EmployeeName,
//                   AddedBy = user.UserFirstName + ' ' + user.UserLastName,
//                   s.CompanyCode,
//                   s.Status
//                 };
//      list.OrderBy(x => x.CompletionDate);
//      if (list == null)
//      {
//        return NotFound();
//      }
//      return Ok(list);
//    }
//  }
//}
