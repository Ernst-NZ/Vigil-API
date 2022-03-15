using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using SignUp.Models;

namespace SignUp.Controllers
{
  public class StepbacksController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Stepbacks
        public IQueryable<Stepback> GetStepbacks()
        {
            return db.Stepbacks;
        }

        // GET: api/Stepbacks/5
        [ResponseType(typeof(Stepback))]
        public IHttpActionResult GetStepback(int id)
        {
            Stepback stepback = db.Stepbacks.Find(id);
            if (stepback == null)
            {
                return NotFound();
            }

            return Ok(stepback);
        }

        // PUT: api/Stepbacks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStepback(int id, Stepback stepback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stepback.LogId)
            {
                return BadRequest();
            }

            db.Entry(stepback).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StepbackExists(id))
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

        // POST: api/Stepbacks
        [ResponseType(typeof(Stepback))]
        public IHttpActionResult PostStepback(Stepback stepback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stepbacks.Add(stepback);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stepback.LogId }, stepback);
        }

        // DELETE: api/Stepbacks/5
        [ResponseType(typeof(Stepback))]
        public IHttpActionResult DeleteStepback(int id)
        {
            Stepback stepback = db.Stepbacks.Find(id);
            if (stepback == null)
            {
                return NotFound();
            }

            db.Stepbacks.Remove(stepback);
            db.SaveChanges();

            return Ok(stepback);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StepbackExists(int id)
        {
            return db.Stepbacks.Count(e => e.LogId == id) > 0;
        }
    }
}