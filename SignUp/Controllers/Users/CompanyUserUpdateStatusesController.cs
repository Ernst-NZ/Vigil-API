using SignUp.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignUp.Controllers.Users
{
  public class CompanyUserUpdateStatusesController : ApiController
    {
    // PUT: api/CompanyUserUpdateStatuses/
    [ResponseType(typeof(void))]
    public IHttpActionResult PutCompanyUser(int id, dynamic companyUser)
    {
      var status = companyUser["Status"];

      using (var db = new Entities())
      {
        var User = db.CompanyStaffs.Single(u => u.StaffId == id);
        User.Inactive = status;
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
