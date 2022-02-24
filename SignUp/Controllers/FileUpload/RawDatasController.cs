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
    public class RawDatasController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/RawDatas
        public IQueryable<RawData> GetRawDatas()
        {
            return db.RawDatas;
        }

        // GET: api/RawDatas/5
        [ResponseType(typeof(RawData))]
        public IHttpActionResult GetRawData(int id)
        {
            RawData rawData = db.RawDatas.Find(id);
            if (rawData == null)
            {
                return NotFound();
            }

            return Ok(rawData);
        }

        // PUT: api/RawDatas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRawData(int id, RawData rawData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rawData.Id)
            {
                return BadRequest();
            }

            db.Entry(rawData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RawDataExists(id))
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

        // POST: api/RawDatas
        [ResponseType(typeof(RawData))]
        public IHttpActionResult PostRawData(RawData rawData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RawDatas.Add(rawData);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RawDataExists(rawData.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rawData.Id }, rawData);
        }

        // DELETE: api/RawDatas/5
        [ResponseType(typeof(RawData))]
        public IHttpActionResult DeleteRawData(int id)
        {
            RawData rawData = db.RawDatas.Find(id);
            if (rawData == null)
            {
                return NotFound();
            }

            db.RawDatas.Remove(rawData);
            db.SaveChanges();

            return Ok(rawData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RawDataExists(int id)
        {
            return db.RawDatas.Count(e => e.Id == id) > 0;
        }
    }
}