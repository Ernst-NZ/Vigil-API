using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignUp.Controllers
{
    public class ProjectsByCompanyIdController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult GeProjectByCompanyId(string id)
    {
      var projects = from p in db.Projects
                      where p.CompanyId == id
                      select p;
      projects.OrderBy(x => x.ProjectName);
      if (projects == null)
      {
        return NotFound();
      }
      return Ok(projects);
    }
  }
}
