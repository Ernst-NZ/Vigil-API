using SignUp.Models;
using System.Linq;
using System.Web.Http;

namespace SignUp.Controllers
{
  public class DepartmentsByCompaniesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult GetDepartments(int id)
    {
      var departments = from dep in db.Departments
                        where dep.CompanyCode == id
                        select dep;
      departments.OrderBy(x => x.DepartmentName);
      if (departments == null)
      {
        return NotFound();
      }

      return Ok(departments);
    }
  }
}
