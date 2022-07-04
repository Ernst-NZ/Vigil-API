﻿using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.Meetings
{
  public class StepBackByUserCodesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult StepBackByUserCode(string id)
    {
      var list = from s in db.Stepbacks
                 join user in db.WebUsers on s.AddedBy equals user.Username
                 where s.AddedBy == id
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