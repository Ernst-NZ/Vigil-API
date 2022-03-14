using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers.StepBack
{
    public class StepBackByCompanyCodesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult StepBackByCompanyCode(string id)
    {
      var list = from s in db.Stepbacks
                 where s.CompanyCode == id
                 select s;
      list.OrderBy(x => x.CompletionDate);
      if (list == null)
      {
        return NotFound();
      }
      return Ok(list);
    }
  }
}
