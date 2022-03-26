using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using SignUp.Models;

namespace SignUp.Controllers.CheckList
{
  public class CheckListLogsController : ApiController
  {
    private Entities db = new Entities();

    // GET: api/CheckListLogs
    public IQueryable<CheckListLog> GetCheckListLogs()
    {
      return db.CheckListLogs;
    }

    // GET: api/CheckListLogs/5
    [ResponseType(typeof(CheckListLog))]
    public IHttpActionResult GetCheckListLog(int id)
    {
      CheckListLog checkListLog = db.CheckListLogs.Find(id);
      if (checkListLog == null)
      {
        return NotFound();
      }

      return Ok(checkListLog);
    }

    // PUT: api/CheckListLogs/5
    [ResponseType(typeof(void))]
    public IHttpActionResult PutCheckListLog(int id, CheckListLog checkListLog)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != checkListLog.CheckLogId)
      {
        return BadRequest();
      }

      db.Entry(checkListLog).State = EntityState.Modified;

      try
      {
        db.SaveChanges();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CheckListLogExists(id))
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




    // POST: api/CheckListLogs
    [ResponseType(typeof(CheckListLog))]
    public IHttpActionResult Post(JArray objData)
    {
      List<CheckListLog> lstItemDetails = new List<CheckListLog>();
      JArray itemDetailsJson = objData;
      foreach (var item in itemDetailsJson)
      {
        lstItemDetails.Add(item.ToObject<CheckListLog>());
      }
      foreach (CheckListLog itemDetail in lstItemDetails)
      {
        db.CheckListLogs.Add(itemDetail);
      }
      dynamic xx = null;
      xx = "";
      try
      {
        db.SaveChanges();
        System.Threading.Thread.Sleep(500);
        return Ok();
      }
      catch (WebException ex)
      {
        return BadRequest(ex.Message);
      }
    }
    //public IHttpActionResult PostCheckListLog(CheckListLog checkListLog)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    db.CheckListLogs.Add(checkListLog);
    //    db.SaveChanges();

    //    return CreatedAtRoute("DefaultApi", new { id = checkListLog.CheckLogId }, checkListLog);
    //}





    // DELETE: api/CheckListLogs/5
    [ResponseType(typeof(CheckListLog))]
    public IHttpActionResult DeleteCheckListLog(int id)
    {
      CheckListLog checkListLog = db.CheckListLogs.Find(id);
      if (checkListLog == null)
      {
        return NotFound();
      }

      db.CheckListLogs.Remove(checkListLog);
      db.SaveChanges();

      return Ok(checkListLog);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    private bool CheckListLogExists(int id)
    {
      return db.CheckListLogs.Count(e => e.CheckLogId == id) > 0;
    }
  }
}