using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using SignUp.Models;

namespace SignUp.Controllers.Visits
{
  [AllowAnonymous]
    public class VisitRegistersController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/VisitRegisters
        public IQueryable<VisitRegister> GetVisitRegisters()
        {
            return db.VisitRegisters;
        }

        // GET: api/VisitRegisters/5
        [ResponseType(typeof(VisitRegister))]
        public IHttpActionResult GetVisitRegister(int id)
        {
            VisitRegister visitRegister = db.VisitRegisters.Find(id);
            if (visitRegister == null)
            {
                return NotFound();
            }

            return Ok(visitRegister);
        }

        // PUT: api/VisitRegisters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVisitRegister(int id, VisitRegister visitRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != visitRegister.VisitId)
            {
                return BadRequest();
            }

            db.Entry(visitRegister).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitRegisterExists(id))
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

        // POST: api/VisitRegisters
        [AllowAnonymous]
        [ResponseType(typeof(VisitRegister))]
        public IHttpActionResult PostVisitRegister(VisitRegister visitRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VisitRegisters.Add(visitRegister);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = visitRegister.VisitId }, visitRegister);
        }

        // DELETE: api/VisitRegisters/5
        [ResponseType(typeof(VisitRegister))]
        public IHttpActionResult DeleteVisitRegister(int id)
        {
            VisitRegister visitRegister = db.VisitRegisters.Find(id);
            if (visitRegister == null)
            {
                return NotFound();
            }

            db.VisitRegisters.Remove(visitRegister);
            db.SaveChanges();

            return Ok(visitRegister);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VisitRegisterExists(int id)
        {
            return db.VisitRegisters.Count(e => e.VisitId == id) > 0;
        }
    }
}