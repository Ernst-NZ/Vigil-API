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
    public class WebUsersController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/WebUsers
        public IQueryable<WebUser> GetWebUsers()
        {
            return db.WebUsers;
        }

        // GET: api/WebUsers/5
        [ResponseType(typeof(WebUser))]
        public IHttpActionResult GetWebUser(string id)
        {
            WebUser webUser = db.WebUsers.Find(id);
            if (webUser == null)
            {
                return NotFound();
            }

            return Ok(webUser);
        }

        // PUT: api/WebUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWebUser(string id, WebUser webUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != webUser.UserId)
            {
                return BadRequest();
            }

            db.Entry(webUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebUserExists(id))
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

        // POST: api/WebUsers
        [ResponseType(typeof(WebUser))]
        public IHttpActionResult PostWebUser(WebUser webUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WebUsers.Add(webUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (WebUserExists(webUser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = webUser.UserId }, webUser);
        }

        // DELETE: api/WebUsers/5
        [ResponseType(typeof(WebUser))]
        public IHttpActionResult DeleteWebUser(string id)
        {
            WebUser webUser = db.WebUsers.Find(id);
            if (webUser == null)
            {
                return NotFound();
            }

            db.WebUsers.Remove(webUser);
            db.SaveChanges();

            return Ok(webUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WebUserExists(string id)
        {
            return db.WebUsers.Count(e => e.UserId == id) > 0;
        }
    }
}