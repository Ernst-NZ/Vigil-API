using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers.CheckList
{
    public class ChecklistByUIDsController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult ChecklistByCompanyCode(string id)
    {
      var check = from c in db.ChecklistMasters
                 where c.CheckListUID == id
                 select c;
      check.OrderBy(x => x.CheckListId);
      if (check == null)
      {
        return NotFound();
      }
      return Ok(check);
    }
  }
}