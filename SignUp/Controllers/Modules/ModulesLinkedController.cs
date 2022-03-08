using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers.Modules
{
  public class ModulesLinkedController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult GetLinkedModules(string id)
    {
      var modules = from module in db.LinkedModules
                        where module.CompanyCode == id
                        select module;
      modules.OrderBy(x => x.ModuleCode);
      if (modules == null)
      {
        return NotFound();
      }

      return Ok(modules);
    }
  }
}
