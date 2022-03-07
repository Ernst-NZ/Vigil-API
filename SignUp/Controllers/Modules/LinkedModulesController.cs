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

namespace SignUp.Controllers.Modules
{
    public class LinkedModulesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/LinkedModules
        public IQueryable<LinkedModule> GetLinkedModules()
        {
            return db.LinkedModules;
        }

        // GET: api/LinkedModules/5
        [ResponseType(typeof(LinkedModule))]
        public IHttpActionResult GetLinkedModule(string id)
        {
            LinkedModule linkedModule = db.LinkedModules.Find(id);
            if (linkedModule == null)
            {
                return NotFound();
            }

            return Ok(linkedModule);
        }

        // PUT: api/LinkedModules/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLinkedModule(string id, LinkedModule linkedModule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != linkedModule.ModuleCode)
            {
                return BadRequest();
            }

            db.Entry(linkedModule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkedModuleExists(id))
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

        // POST: api/LinkedModules
        [ResponseType(typeof(LinkedModule))]
        public IHttpActionResult PostLinkedModule(LinkedModule linkedModule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LinkedModules.Add(linkedModule);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LinkedModuleExists(linkedModule.ModuleCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = linkedModule.ModuleCode }, linkedModule);
        }

        // DELETE: api/LinkedModules/5
        [ResponseType(typeof(LinkedModule))]
        public IHttpActionResult DeleteLinkedModule(string id)
        {
            LinkedModule linkedModule = db.LinkedModules.Find(id);
            if (linkedModule == null)
            {
                return NotFound();
            }

            db.LinkedModules.Remove(linkedModule);
            db.SaveChanges();

            return Ok(linkedModule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LinkedModuleExists(string id)
        {
            return db.LinkedModules.Count(e => e.ModuleCode == id) > 0;
        }
    }
}