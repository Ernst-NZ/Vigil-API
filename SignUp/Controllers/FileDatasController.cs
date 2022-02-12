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
    public class FileDatasController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/FileDatas
        public IQueryable<FileData> GetFileDatas()
        {
            return db.FileDatas;
        }

        // GET: api/FileDatas/5
        [ResponseType(typeof(FileData))]
        public IHttpActionResult GetFileData(int id)
        {
            FileData fileData = db.FileDatas.Find(id);
            if (fileData == null)
            {
                return NotFound();
            }

            return Ok(fileData);
        }

        // PUT: api/FileDatas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFileData(int id, FileData fileData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fileData.Id)
            {
                return BadRequest();
            }

            db.Entry(fileData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileDataExists(id))
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

        // POST: api/FileDatas
        [ResponseType(typeof(FileData))]
        public IHttpActionResult PostFileData(FileData fileData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FileDatas.Add(fileData);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fileData.Id }, fileData);
        }

        // DELETE: api/FileDatas/5
        [ResponseType(typeof(FileData))]
        public IHttpActionResult DeleteFileData(int id)
        {
            FileData fileData = db.FileDatas.Find(id);
            if (fileData == null)
            {
                return NotFound();
            }

            db.FileDatas.Remove(fileData);
            db.SaveChanges();

            return Ok(fileData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FileDataExists(int id)
        {
            return db.FileDatas.Count(e => e.Id == id) > 0;
        }
    }
}