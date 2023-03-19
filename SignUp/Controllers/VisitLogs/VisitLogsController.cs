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
using SignUp.Models;

namespace SignUp.Controllers.VisitLogs
{
    public class VisitLogsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/VisitLogs
        public IQueryable<VisitLog> GetVisitLogs()
        {
            return db.VisitLogs;
        }

        // GET: api/VisitLogs/5
        [ResponseType(typeof(VisitLog))]
        public IHttpActionResult GetVisitLog(string id)
        {
            VisitLog visitLog = db.VisitLogs.Find(id);
            if (visitLog == null)
            {
                return NotFound();
            }

            return Ok(visitLog);
        }

        // PUT: api/VisitLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVisitLog(string id, VisitLog visitLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != visitLog.VisitUid)
            {
                return BadRequest();
            }

            db.Entry(visitLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitLogExists(id))
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

        // POST: api/VisitLogs
        [ResponseType(typeof(VisitLog))]
        public IHttpActionResult PostVisitLog(VisitLog visitLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VisitLogs.Add(visitLog);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VisitLogExists(visitLog.VisitUid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = visitLog.VisitUid }, visitLog);
        }

        // DELETE: api/VisitLogs/5
        [ResponseType(typeof(VisitLog))]
        public IHttpActionResult DeleteVisitLog(string id)
        {
            VisitLog visitLog = db.VisitLogs.Find(id);
            if (visitLog == null)
            {
                return NotFound();
            }

            db.VisitLogs.Remove(visitLog);
            db.SaveChanges();

            return Ok(visitLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VisitLogExists(string id)
        {
            return db.VisitLogs.Count(e => e.VisitUid == id) > 0;
        }
    }
}