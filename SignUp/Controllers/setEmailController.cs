using Newtonsoft.Json.Linq;
using SignUp.Models;
using System.Web.Http;
using System.Web.Http.Description;

using System.Linq;
using System;

namespace SignUp.Controllers
{
    public class setEmailController : ApiController
  {
    [Authorize]
    [AllowAnonymous]
    [HttpGet]
    [ResponseType(typeof(void))]
    public IHttpActionResult setEmail(string id)
    {
      string Result = "";
      using (var db = new DBModelBorn())        
      {
        try
        {
          var Users = db.AspNetUsers.Single(u => u.Id == id);
          Users.EmailConfirmed = true;
          db.SaveChanges();
          Result = "Email Confirmed";
        }
        catch (Exception e){
          Result = "Email not Confirmed: " + e.Message;
        }
      }
      return Ok(Result);
    }
  }
}
