using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers
{
    public class TrainingByCompanyIdController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult TrainingByCompanyId(int id)
    {
      var companyTraining = from user in db.WebUsers
                    join comp in db.Companies on user.UserCompanyCode equals comp.CompanyCode
                    join train in db.Trainings on user.UserId equals train.UserId
                    where comp.CompanyId == id
                    select new { user.UserFirstName, 
                      user.UserLastName,
                      train.TrainingId,
                      train.UserId,
                      CourseName = train.CourseName ?? "", 
                      CourseDate = train.CourseDate ?? "", 
                      ExpiryDate = train.ExpiryDate ?? "",
                      CompetencyLevel = train.CompetencyLevel ?? "",
                      ProviderName = train.ProviderName ?? "",
                      train.LastUpdateBy,
                      UpdateDate = train.UpdateDate ?? "",
                    };
      companyTraining.OrderBy(x => x.UserFirstName).ThenBy(z => z.CourseName) ;
      if (companyTraining == null)
      {
        return NotFound();
      }

      return Ok(companyTraining);
    }
  }
}
