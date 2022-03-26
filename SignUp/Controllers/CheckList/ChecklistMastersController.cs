using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using SignUp.Models;

namespace SignUp.Controllers
{
  public class ChecklistMastersController : ApiController
  {
    private Entities db = new Entities();

    // GET: api/ChecklistMasters
    public IQueryable<ChecklistMaster> GetChecklistMasters()
    {
      return db.ChecklistMasters;
    }

    // GET: api/ChecklistMasters/5
    [ResponseType(typeof(ChecklistMaster))]
    public IHttpActionResult GetChecklistMaster(int id)
    {
      ChecklistMaster checklistMaster = db.ChecklistMasters.Find(id);
      if (checklistMaster == null)
      {
        return NotFound();
      }

      return Ok(checklistMaster);
    }

    // PUT: api/ChecklistMasters/5
    [ResponseType(typeof(void))]
    public IHttpActionResult PutChecklistMaster(int id, ChecklistMaster checklistMaster)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != checklistMaster.CheckListId)
      {
        return BadRequest();
      }

      db.Entry(checklistMaster).State = EntityState.Modified;

      try
      {
        db.SaveChanges();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ChecklistMasterExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return StatusCode(HttpStatusCode.NoContent);
    }

    // POST: api/ChecklistMasters
    [ResponseType(typeof(ChecklistMaster))]
    public IHttpActionResult PostChecklistMaster(ChecklistMaster checklistMaster)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      db.ChecklistMasters.Add(checklistMaster);
      db.SaveChanges();

      return CreatedAtRoute("DefaultApi", new { id = checklistMaster.CheckListId }, checklistMaster);
    }

    // DELETE: api/ChecklistMasters/5
    [ResponseType(typeof(ChecklistMaster))]
    public IHttpActionResult DeleteChecklistMaster(int id)
    {
      ChecklistMaster checklistMaster = db.ChecklistMasters.Find(id);
      if (checklistMaster == null)
      {
        return NotFound();
      }

      db.ChecklistMasters.Remove(checklistMaster);
      db.SaveChanges();

      return Ok(checklistMaster);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    private bool ChecklistMasterExists(int id)
    {
      return db.ChecklistMasters.Count(e => e.CheckListId == id) > 0;
    }
  }
}