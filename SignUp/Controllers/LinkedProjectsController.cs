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

namespace SignUp.Controllers
{
    public class LinkedProjectsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/LinkedProjects
        public IQueryable<LinkedProject> GetLinkedProjects()
        {
            return db.LinkedProjects;
        }

        // GET: api/LinkedProjects/5
        [ResponseType(typeof(LinkedProject))]
        public IHttpActionResult GetLinkedProject(int id)
        {
            LinkedProject linkedProject = db.LinkedProjects.Find(id);
            if (linkedProject == null)
            {
                return NotFound();
            }

            return Ok(linkedProject);
        }

        // PUT: api/LinkedProjects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLinkedProject(int id, LinkedProject linkedProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != linkedProject.LinkedId)
            {
                return BadRequest();
            }

            db.Entry(linkedProject).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkedProjectExists(id))
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

        // POST: api/LinkedProjects
        [ResponseType(typeof(LinkedProject))]
        public IHttpActionResult PostLinkedProject(LinkedProject linkedProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LinkedProjects.Add(linkedProject);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = linkedProject.LinkedId }, linkedProject);
        }

        // DELETE: api/LinkedProjects/5
        [ResponseType(typeof(LinkedProject))]
        public IHttpActionResult DeleteLinkedProject(int id)
        {
            LinkedProject linkedProject = db.LinkedProjects.Find(id);
            if (linkedProject == null)
            {
                return NotFound();
            }

            db.LinkedProjects.Remove(linkedProject);
            db.SaveChanges();

            return Ok(linkedProject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LinkedProjectExists(int id)
        {
            return db.LinkedProjects.Count(e => e.LinkedId == id) > 0;
        }
    }
}