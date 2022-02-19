using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers
{
    public class TrainingByUserIdController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult GetTrainingByUserId(string id)
    {
      var trainings = from i in db.Trainings
                      where i.UserId == id
                      select new
                      {
                        i.UserId,
                        i.TrainingId,
                        CompetencyLevel = i.CompetencyLevel ?? "",
                        ExpiryDate = i.ExpiryDate ?? "",
                        UpdateDate = i.UpdateDate ?? "",
                        LastUpdateBy = i.LastUpdateBy ?? "",
                        CourseDate = i.CourseDate ?? "",
                        CourseName = i.CourseName ?? "",
                        ProviderName = i.ProviderName ?? ""
                      };
      trainings.OrderBy(x => x.CourseName);
      if (trainings == null)
      {
        return NotFound();
      }

      return Ok(trainings);
    }
  }
}