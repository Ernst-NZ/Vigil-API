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
  public class FunctionsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Functions
        public IQueryable<Function> GetFunctions()
        {
            return db.Functions.OrderBy(c => c.FunctionName);
        }

        // GET: api/Functions/5
        [ResponseType(typeof(Function))]
        public IHttpActionResult GetFunction(int id)
        {
            Function function = db.Functions.Find(id);
            if (function == null)
            {
                return NotFound();
            }

            return Ok(function);
        }

        // PUT: api/Functions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFunction(int id, Function function)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != function.FunctionId)
            {
                return BadRequest();
            }

            db.Entry(function).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FunctionExists(id))
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

        // POST: api/Functions
        [ResponseType(typeof(Function))]
        public IHttpActionResult PostFunction(Function function)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Functions.Add(function);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = function.FunctionId }, function);
        }

        // DELETE: api/Functions/5
        [ResponseType(typeof(Function))]
        public IHttpActionResult DeleteFunction(int id)
        {
            Function function = db.Functions.Find(id);
            if (function == null)
            {
                return NotFound();
            }

            db.Functions.Remove(function);
            db.SaveChanges();

            return Ok(function);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FunctionExists(int id)
        {
            return db.Functions.Count(e => e.FunctionId == id) > 0;
        }
    }
}