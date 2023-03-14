using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using SignUp.Models;

namespace SignUp.Controllers.Jsa
{
    public class JSAReviewedController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/JSAReviewed
        public IQueryable<JSAReviewed> GetJSARevieweds()
        {
            return db.JSARevieweds;
        }

        // GET: api/JSAReviewed/5
        [ResponseType(typeof(JSAReviewed))]
        public IHttpActionResult GetJSAReviewed(string id)
        {
            JSAReviewed jSAReviewed = db.JSARevieweds.Find(id);
            if (jSAReviewed == null)
            {
                return NotFound();
            }

            return Ok(jSAReviewed);
        }

        // PUT: api/JSAReviewed/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJSAReviewed(string id, JSAReviewed jSAReviewed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jSAReviewed.AcknowledgeUid)
            {
                return BadRequest();
            }

            db.Entry(jSAReviewed).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JSAReviewedExists(id))
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

        // POST: api/JSAReviewed
        [ResponseType(typeof(JSAReviewed))]
        public IHttpActionResult PostJSAReviewed(JSAReviewed jSAReviewed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JSARevieweds.Add(jSAReviewed);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JSAReviewedExists(jSAReviewed.AcknowledgeUid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = jSAReviewed.AcknowledgeUid }, jSAReviewed);
        }

        // DELETE: api/JSAReviewed/5
        [ResponseType(typeof(JSAReviewed))]
        public IHttpActionResult DeleteJSAReviewed(string id)
        {
            JSAReviewed jSAReviewed = db.JSARevieweds.Find(id);
            if (jSAReviewed == null)
            {
                return NotFound();
            }

            db.JSARevieweds.Remove(jSAReviewed);
            db.SaveChanges();

            return Ok(jSAReviewed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JSAReviewedExists(string id)
        {
            return db.JSARevieweds.Count(e => e.AcknowledgeUid == id) > 0;
        }
    }
}