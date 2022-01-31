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
  [AllowAnonymous]
  public class UserNameDetailsController : ApiController
    {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult GetWebUser(string id)
    {
      var webUser = from user in db.WebUsers
                    join comp in db.Companies on user.UserCompanyCode equals comp.CompanyCode
                    where user.Username == id
                    select new { user, comp };
      webUser.OrderBy(x => x.user.UserFirstName);
      if (webUser == null)
      {
        return NotFound();
      }
     
      return Ok(webUser);
    }
  }
}
