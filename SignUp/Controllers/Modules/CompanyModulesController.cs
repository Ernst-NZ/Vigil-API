using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers.Modules
{
  public class CompanyModulesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult GetCompanyModules(string id)
    {
      var companyCode = id;
      var companyModules = from module in db.LinkedModules
                           join comp in db.Companies on module.CompanyCode equals comp.CompanyCode
                           where module.CompanyCode == companyCode || comp.CompanyId.ToString() == companyCode                           
                           select new
                            {
                              module.ModuleCode,
                            };
      companyModules.OrderBy(x => x.ModuleCode);
      if (companyModules == null)
      {
        return NotFound();
      }

      return Ok(companyModules);
    }
  }
  }
