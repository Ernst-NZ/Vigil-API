using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers
{
    public class CompaniesParentsController : ApiController
    {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult GetCompanies(int id)
    {
      var companies = from i in db.Companies
                      where i.CompanyId == id || i.ParentCode == id
                      select i;
      companies.OrderBy(x => x.CompanyName);
      if (companies == null)
      {
        return NotFound();
      }

      return Ok(companies);
    }
  }
}
