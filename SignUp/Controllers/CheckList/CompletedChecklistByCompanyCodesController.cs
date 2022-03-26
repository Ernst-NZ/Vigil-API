using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.CheckList
{
  public class CompletedChecklistByCompanyCodesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult CompletedChecklistByCompanyCode(string id)
    {
      var list = (from m in db.CheckListLogs
                  where m.CheckListCompanyCode == id
                  select new {
                    m.CheckListName,
                    CheckListReference = m.CheckListReference ?? "",
                    CheckListStatus = m.CheckListStatus ?? false,
                    CheckListEmployeeName = m.CheckListEmployeeName ?? "",
                    CheckListDate = m.CheckListDate ?? "",
                    m.CheckListUID
                  }).Distinct();
      list.OrderByDescending(x => x.CheckListDate).ThenBy(x => x.CheckListName);
      if (list == null)
      {
        return NotFound();
      }
      return Ok(list);
    }
  }
}