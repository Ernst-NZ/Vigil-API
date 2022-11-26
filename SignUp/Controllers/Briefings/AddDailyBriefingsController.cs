using SignUp.Models;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System;

namespace SignUp.Controllers.Briefings
{
  public class AddDailyBriefingsController : ApiController
    {
    private Entities db = new Entities();
    [HttpPost]
    [AllowAnonymous]
    [ResponseType(typeof(gmail))]
    public IHttpActionResult PostDailyMeetings(dynamic rawData)
    {
      dynamic data = new DailyBriefing();
      data = rawData["body"];
      try
      {
        data = rawData["body"];
        Console.WriteLine(data);
        test(data);        
      }
      catch (SyntaxErrorException ex)
      {
        return BadRequest(ex.Message);
      }      

      //if (!ModelState.IsValid)
      //{
      //  return BadRequest(ModelState);
      //}


      db.DailyBriefings.Add(data);

      try
      {
        db.SaveChanges();
      }
      catch (DbUpdateException)
      {
        if (DailyBriefingExists(data.BriefingUid))
        {
          return Conflict();
        }
        else
        {
          throw;
        }
      }

      return CreatedAtRoute("DefaultApi", new { id = data.BriefingUid }, data);
    }

    private bool DailyBriefingExists(string id)
    {
      return db.DailyBriefings.Count(e => e.BriefingUid == id) > 0;
    }

    private string test(dynamic xx)
    {
      Console.WriteLine(xx);
      return null;
    }

  }
}

