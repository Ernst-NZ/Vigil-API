using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers.StepBack
{
    public class StepBackByIdsController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult StepBackById(int id)
    {
      var list = from s in db.Stepbacks
                 join user in db.WebUsers on s.AddedBy equals user.Username
                 where s.LogId == id
                 select new
                 {
                   s, 
                   user.UserFirstName,
                 };
      if (list == null)
      {
        return NotFound();
      }
      return Ok(list);
    }
  }
}