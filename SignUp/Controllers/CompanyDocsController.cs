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
    public class CompanyDocsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/CompanyDocs
        public IQueryable<CompanyDoc> GetCompanyDocs()
        {
            return db.CompanyDocs;
        }

        // GET: api/CompanyDocs/5
        [ResponseType(typeof(CompanyDoc))]
        public IHttpActionResult GetCompanyDoc(int id)
        {
            CompanyDoc companyDoc = db.CompanyDocs.Find(id);
            if (companyDoc == null)
            {
                return NotFound();
            }

            return Ok(companyDoc);
        }

        // PUT: api/CompanyDocs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompanyDoc(int id, CompanyDoc companyDoc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companyDoc.CompanyDocId)
            {
                return BadRequest();
            }

            db.Entry(companyDoc).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyDocExists(id))
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

        // POST: api/CompanyDocs
        [ResponseType(typeof(CompanyDoc))]
        public IHttpActionResult PostCompanyDoc(CompanyDoc companyDoc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CompanyDocs.Add(companyDoc);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = companyDoc.CompanyDocId }, companyDoc);
        }

        // DELETE: api/CompanyDocs/5
        [ResponseType(typeof(CompanyDoc))]
        public IHttpActionResult DeleteCompanyDoc(int id)
        {
            CompanyDoc companyDoc = db.CompanyDocs.Find(id);
            if (companyDoc == null)
            {
                return NotFound();
            }

            db.CompanyDocs.Remove(companyDoc);
            db.SaveChanges();

            return Ok(companyDoc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyDocExists(int id)
        {
            return db.CompanyDocs.Count(e => e.CompanyDocId == id) > 0;
        }
    }
}