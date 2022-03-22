using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.CheckList
{
  public class CompletedChecklistByUidsController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult CompletedChecklistByUid(string id)
    {
      var check = from c in db.CheckListLogs
                  where c.CheckListUID == id
                  select c;
      check.OrderBy(x => x.CheckLogId);
      if (check == null)
      {
        return NotFound();
      }
      return Ok(check);
    }
  }
}