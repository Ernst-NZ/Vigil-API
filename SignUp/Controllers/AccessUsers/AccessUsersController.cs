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

namespace SignUp.Controllers.AccessUsers
{
    public class AccessUsersController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/AccessUsers
        public IQueryable<AccessUser> GetAccessUsers()
        {
            return db.AccessUsers;
        }

        // GET: api/AccessUsers/5
        [ResponseType(typeof(AccessUser))]
        public IHttpActionResult GetAccessUser(string id)
        {
            AccessUser accessUser = db.AccessUsers.Find(id);
            if (accessUser == null)
            {
                return NotFound();
            }

            return Ok(accessUser);
        }

        // PUT: api/AccessUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccessUser(string id, AccessUser accessUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accessUser.UserAccessUid)
            {
                return BadRequest();
            }

            db.Entry(accessUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessUserExists(id))
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

        // POST: api/AccessUsers
        [ResponseType(typeof(AccessUser))]
        public IHttpActionResult PostAccessUser(AccessUser accessUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccessUsers.Add(accessUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AccessUserExists(accessUser.UserAccessUid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = accessUser.UserAccessUid }, accessUser);
        }

        // DELETE: api/AccessUsers/5
        [ResponseType(typeof(AccessUser))]
        public IHttpActionResult DeleteAccessUser(string id)
        {
            AccessUser accessUser = db.AccessUsers.Find(id);
            if (accessUser == null)
            {
                return NotFound();
            }

            db.AccessUsers.Remove(accessUser);
            db.SaveChanges();

            return Ok(accessUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccessUserExists(string id)
        {
            return db.AccessUsers.Count(e => e.UserAccessUid == id) > 0;
        }
    }
}