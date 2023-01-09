using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using SignUp.Models;

namespace SignUp.Controllers.AppVersions
{
    public class AppVersionsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/AppVersions
        public IQueryable<AppVersion> GetAppVersions()
        {
            return db.AppVersions;
        }

        // GET: api/AppVersions/5
        [ResponseType(typeof(AppVersion))]
        public IHttpActionResult GetAppVersion(string id)
        {
            AppVersion appVersion = db.AppVersions.Find(id);
            if (appVersion == null)
            {
                return NotFound();
            }

            return Ok(appVersion);
        }

        // PUT: api/AppVersions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppVersion(string id, AppVersion appVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appVersion.VersionUid)
            {
                return BadRequest();
            }

            db.Entry(appVersion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppVersionExists(id))
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

        // POST: api/AppVersions
        [ResponseType(typeof(AppVersion))]
        public IHttpActionResult PostAppVersion(AppVersion appVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppVersions.Add(appVersion);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AppVersionExists(appVersion.VersionUid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = appVersion.VersionUid }, appVersion);
        }

        // DELETE: api/AppVersions/5
        [ResponseType(typeof(AppVersion))]
        public IHttpActionResult DeleteAppVersion(string id)
        {
            AppVersion appVersion = db.AppVersions.Find(id);
            if (appVersion == null)
            {
                return NotFound();
            }

            db.AppVersions.Remove(appVersion);
            db.SaveChanges();

            return Ok(appVersion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppVersionExists(string id)
        {
            return db.AppVersions.Count(e => e.VersionUid == id) > 0;
        }
    }
}