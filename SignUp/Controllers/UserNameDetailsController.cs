using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SignUp.Models
{
  public class UserNameDetailsController : ApiController
    {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult GetWebUser(string id)
    {
      var webUser = from user in db.WebUsers
                where user.Username == id 
                select user;
      if (webUser == null)
      {
        return NotFound();
      }
      return Ok(webUser);
    }
  }
}
