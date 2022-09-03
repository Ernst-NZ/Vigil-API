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

namespace SignUp.Controllers.Hazards
{
    public class HazardsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Hazards
        public IQueryable<Hazard> GetHazards()
        {
            return db.Hazards;
        }

        // GET: api/Hazards/5
        [ResponseType(typeof(Hazard))]
        public IHttpActionResult GetHazard(string id)
        {
            Hazard hazard = db.Hazards.Find(id);
            if (hazard == null)
            {
                return NotFound();
            }

            return Ok(hazard);
        }

        // PUT: api/Hazards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHazard(string id, Hazard hazard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hazard.HazardUID)
            {
                return BadRequest();
            }

            db.Entry(hazard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HazardExists(id))
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

        // POST: api/Hazards
        [ResponseType(typeof(Hazard))]
        public IHttpActionResult PostHazard(Hazard hazard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hazards.Add(hazard);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (HazardExists(hazard.HazardUID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hazard.HazardUID }, hazard);
        }

        // DELETE: api/Hazards/5
        [ResponseType(typeof(Hazard))]
        public IHttpActionResult DeleteHazard(string id)
        {
            Hazard hazard = db.Hazards.Find(id);
            if (hazard == null)
            {
                return NotFound();
            }

            db.Hazards.Remove(hazard);
            db.SaveChanges();

            return Ok(hazard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HazardExists(string id)
        {
            return db.Hazards.Count(e => e.HazardUID == id) > 0;
        }
    }
}