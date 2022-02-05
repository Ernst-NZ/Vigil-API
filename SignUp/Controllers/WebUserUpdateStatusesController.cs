using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignUp.Controllers
{
    public class WebUserUpdateStatusesController : ApiController
    {

    // PUT: api/WebUsers/5
    [ResponseType(typeof(void))]
    public IHttpActionResult PutWebUser(string id, dynamic webUser)
    {
      //if (!ModelState.IsValid)
      //{
      //  return BadRequest(ModelState);
      //}

      //if (id != webUser["UserId"])
      //{
      //  return BadRequest();
      //}
      var status = webUser["Status"];

      using (var db = new Entities())
      {
        var User = db.WebUsers.Single(u => u.UserId == id);
        User.UserNotActive = status;
        try
        {
          db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
          return BadRequest(ex.Message);
        }
      }

      return StatusCode(HttpStatusCode.NoContent);
    }

  }
}
