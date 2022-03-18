using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers.CheckList
{
    public class ChecklistNameUpdateController : ApiController
  {
    private Entities db = new Entities();
    [HttpPut]
    public IHttpActionResult PutChecklistNameUpdate(string id, dynamic myParams)
    {
      var newName = myParams["newName"];
      (from c in db.ChecklistMasters
       where c.CheckListUID == id
       select c).ToList()
          .ForEach(x => x.CheckListName = newName);
      try
      {
        db.SaveChanges();
      }
      catch (DbUpdateException ex)
      {
        return BadRequest(ex.Message);
      }
      return Ok();
    }
  }
}