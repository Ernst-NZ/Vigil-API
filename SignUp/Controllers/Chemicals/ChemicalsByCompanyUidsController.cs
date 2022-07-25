using SignUp.Models;
using System.Linq;
using System.Web.Http;


namespace SignUp.Controllers.Chemicals
{
    public class ChemicalsByCompanyUidsController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult GetChemicalsByCompanyUid(string id)
    {
      var chemicals = from chem in db.Chemicals
                        where chem.CompanyUid == id
                        select chem;
      chemicals.OrderBy(x => x.ChemicalName);
      if (chemicals == null)
      {
        return NotFound();
      }
      return Ok(chemicals);
    }
  }
}
