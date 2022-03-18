using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.CheckList
{
  public class ChecklistByCompanyCodesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult ChecklistByCompanyCode(string id)
    {
      var list = from m in db.ChecklistMasters.Distinct()
                 where m.CompanyCode == id
                 select new
                 {
                   m.CheckListName,
                   m.CheckListUID,
                   m.LastUpdate
                 };
      list.OrderBy(x => x.CheckListName);
      if (list == null)
      {
        return NotFound();
      }
      return Ok(list);
    }
  }
}