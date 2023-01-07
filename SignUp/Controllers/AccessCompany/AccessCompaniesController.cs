using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using SignUp.Models;


namespace SignUp.Controllers
{
    public class AccessCompaniesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/AccessCompanies
        public IQueryable<AccessCompany> GetAccessCompanies()
        {
            return db.AccessCompanies;
        }

        // GET: api/AccessCompanies/5
        [ResponseType(typeof(AccessCompany))]
        public IHttpActionResult GetAccessCompany(string id)
        {
            AccessCompany accessCompany = db.AccessCompanies.Find(id);
            if (accessCompany == null)
            {
                return NotFound();
            }

            return Ok(accessCompany);
        }

        // PUT: api/AccessCompanies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccessCompany(string id, AccessCompany accessCompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accessCompany.AccessUid)
            {
                return BadRequest();
            }

            db.Entry(accessCompany).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessCompanyExists(id))
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

        // POST: api/AccessCompanies
        [ResponseType(typeof(AccessCompany))]
        public IHttpActionResult PostAccessCompany(AccessCompany accessCompany)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccessCompanies.Add(accessCompany);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AccessCompanyExists(accessCompany.AccessUid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = accessCompany.AccessUid }, accessCompany);
        }

        // DELETE: api/AccessCompanies/5
        [ResponseType(typeof(AccessCompany))]
        public IHttpActionResult DeleteAccessCompany(string id)
        {
            AccessCompany accessCompany = db.AccessCompanies.Find(id);
            if (accessCompany == null)
            {
                return NotFound();
            }

            db.AccessCompanies.Remove(accessCompany);
            db.SaveChanges();

            return Ok(accessCompany);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccessCompanyExists(string id)
        {
            return db.AccessCompanies.Count(e => e.AccessUid == id) > 0;
        }
    }
}