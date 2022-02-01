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
    public class ProjectDocsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/ProjectDocs
        public IQueryable<ProjectDoc> GetProjectDocs()
        {
            return db.ProjectDocs;
        }

        // GET: api/ProjectDocs/5
        [ResponseType(typeof(ProjectDoc))]
        public IHttpActionResult GetProjectDoc(int id)
        {
            ProjectDoc projectDoc = db.ProjectDocs.Find(id);
            if (projectDoc == null)
            {
                return NotFound();
            }

            return Ok(projectDoc);
        }

        // PUT: api/ProjectDocs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjectDoc(int id, ProjectDoc projectDoc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectDoc.ProjectDocId)
            {
                return BadRequest();
            }

            db.Entry(projectDoc).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectDocExists(id))
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

        // POST: api/ProjectDocs
        [ResponseType(typeof(ProjectDoc))]
        public IHttpActionResult PostProjectDoc(ProjectDoc projectDoc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectDocs.Add(projectDoc);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = projectDoc.ProjectDocId }, projectDoc);
        }

        // DELETE: api/ProjectDocs/5
        [ResponseType(typeof(ProjectDoc))]
        public IHttpActionResult DeleteProjectDoc(int id)
        {
            ProjectDoc projectDoc = db.ProjectDocs.Find(id);
            if (projectDoc == null)
            {
                return NotFound();
            }

            db.ProjectDocs.Remove(projectDoc);
            db.SaveChanges();

            return Ok(projectDoc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectDocExists(int id)
        {
            return db.ProjectDocs.Count(e => e.ProjectDocId == id) > 0;
        }
    }
}