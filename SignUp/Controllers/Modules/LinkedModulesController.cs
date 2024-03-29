﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
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
        public IHttpActionResult GetLinkedModule(int id)
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
        public IHttpActionResult PutLinkedModule(int id, LinkedModule linkedModule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != linkedModule.LinkedId)
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
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = linkedModule.LinkedId }, linkedModule);
        }

        // DELETE: api/LinkedModules/5
        [ResponseType(typeof(LinkedModule))]
        public IHttpActionResult DeleteLinkedModule(int id)
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

        private bool LinkedModuleExists(int id)
        {
            return db.LinkedModules.Count(e => e.LinkedId == id) > 0;
        }
    }
}