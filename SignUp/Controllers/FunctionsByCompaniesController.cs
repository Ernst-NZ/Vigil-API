using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers
{
  public class FunctionsByCompaniesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult GetFunctions(int id)
    {
      var functions = from f in db.Functions
                      where f.CompanyCode == id
                      select f;
      functions.OrderBy(x => x.FunctionName);
      if (functions == null)
      {
        return NotFound();
      }

      return Ok(functions);
    }
  }
}
