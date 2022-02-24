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

namespace SignUp.Controllers.Incidents
{
    public class IncidentsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Incidents
        public IQueryable<Incident> GetIncidents()
        {
            return db.Incidents;
        }

        // GET: api/Incidents/5
        [ResponseType(typeof(Incident))]
        public IHttpActionResult GetIncident(int id)
        {
            Incident incident = db.Incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }

            return Ok(incident);
        }

        // PUT: api/Incidents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIncident(int id, Incident incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != incident.IncidentId)
            {
                return BadRequest();
            }

            db.Entry(incident).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentExists(id))
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

        // POST: api/Incidents
        [ResponseType(typeof(Incident))]
        public IHttpActionResult PostIncident(Incident incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Incidents.Add(incident);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = incident.IncidentId }, incident);
        }

        // DELETE: api/Incidents/5
        [ResponseType(typeof(Incident))]
        public IHttpActionResult DeleteIncident(int id)
        {
            Incident incident = db.Incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }

            db.Incidents.Remove(incident);
            db.SaveChanges();

            return Ok(incident);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IncidentExists(int id)
        {
            return db.Incidents.Count(e => e.IncidentId == id) > 0;
        }
    }
}