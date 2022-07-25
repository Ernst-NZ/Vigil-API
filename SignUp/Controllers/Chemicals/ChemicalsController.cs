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

namespace SignUp.Controllers.Chemicals
{
    public class ChemicalsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Chemicals
        public IQueryable<Chemical> GetChemicals()
        {
            return db.Chemicals;
        }

        // GET: api/Chemicals/5
        [ResponseType(typeof(Chemical))]
        public IHttpActionResult GetChemical(string id)
        {
            Chemical chemical = db.Chemicals.Find(id);
            if (chemical == null)
            {
                return NotFound();
            }

            return Ok(chemical);
        }

        // PUT: api/Chemicals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChemical(string id, Chemical chemical)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chemical.Id)
            {
                return BadRequest();
            }

            db.Entry(chemical).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChemicalExists(id))
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

        // POST: api/Chemicals
        [ResponseType(typeof(Chemical))]
        public IHttpActionResult PostChemical(Chemical chemical)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Chemicals.Add(chemical);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ChemicalExists(chemical.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = chemical.Id }, chemical);
        }

        // DELETE: api/Chemicals/5
        [ResponseType(typeof(Chemical))]
        public IHttpActionResult DeleteChemical(string id)
        {
            Chemical chemical = db.Chemicals.Find(id);
            if (chemical == null)
            {
                return NotFound();
            }

            db.Chemicals.Remove(chemical);
            db.SaveChanges();

            return Ok(chemical);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChemicalExists(string id)
        {
            return db.Chemicals.Count(e => e.Id == id) > 0;
        }
    }
}