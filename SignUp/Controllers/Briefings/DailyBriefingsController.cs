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

namespace SignUp.Controllers.Briefings
{
    public class DailyBriefingsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/DailyBriefings
        public IQueryable<DailyBriefing> GetDailyBriefings()
        {
            return db.DailyBriefings;
        }

        // GET: api/DailyBriefings/5
        [ResponseType(typeof(DailyBriefing))]
        public IHttpActionResult GetDailyBriefing(string id)
        {
            DailyBriefing dailyBriefing = db.DailyBriefings.Find(id);
            if (dailyBriefing == null)
            {
                return NotFound();
            }

            return Ok(dailyBriefing);
        }

        // PUT: api/DailyBriefings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDailyBriefing(string id, DailyBriefing dailyBriefing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dailyBriefing.BriefingUid)
            {
                return BadRequest();
            }

            db.Entry(dailyBriefing).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyBriefingExists(id))
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

        // POST: api/DailyBriefings
        [ResponseType(typeof(DailyBriefing))]
        public IHttpActionResult PostDailyBriefing(DailyBriefing dailyBriefing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DailyBriefings.Add(dailyBriefing);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DailyBriefingExists(dailyBriefing.BriefingUid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dailyBriefing.BriefingUid }, dailyBriefing);
        }

        // DELETE: api/DailyBriefings/5
        [ResponseType(typeof(DailyBriefing))]
        public IHttpActionResult DeleteDailyBriefing(string id)
        {
            DailyBriefing dailyBriefing = db.DailyBriefings.Find(id);
            if (dailyBriefing == null)
            {
                return NotFound();
            }

            db.DailyBriefings.Remove(dailyBriefing);
            db.SaveChanges();

            return Ok(dailyBriefing);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DailyBriefingExists(string id)
        {
            return db.DailyBriefings.Count(e => e.BriefingUid == id) > 0;
        }
    }
}