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

namespace SignUp.Controllers.Users
{
    public class CompanyStaffsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/CompanyStaffs
        public IQueryable<CompanyStaff> GetCompanyStaffs()
        {
            return db.CompanyStaffs;
        }

        // GET: api/CompanyStaffs/5
        [ResponseType(typeof(CompanyStaff))]
        public IHttpActionResult GetCompanyStaff(int id)
        {
            CompanyStaff companyStaff = db.CompanyStaffs.Find(id);
            if (companyStaff == null)
            {
                return NotFound();
            }

            return Ok(companyStaff);
        }

        // PUT: api/CompanyStaffs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompanyStaff(int id, CompanyStaff companyStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companyStaff.StaffId)
            {
                return BadRequest();
            }

            db.Entry(companyStaff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyStaffExists(id))
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

        // POST: api/CompanyStaffs
        [ResponseType(typeof(CompanyStaff))]
        public IHttpActionResult PostCompanyStaff(CompanyStaff companyStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CompanyStaffs.Add(companyStaff);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = companyStaff.StaffId }, companyStaff);
        }

        // DELETE: api/CompanyStaffs/5
        [ResponseType(typeof(CompanyStaff))]
        public IHttpActionResult DeleteCompanyStaff(int id)
        {
            CompanyStaff companyStaff = db.CompanyStaffs.Find(id);
            if (companyStaff == null)
            {
                return NotFound();
            }

            db.CompanyStaffs.Remove(companyStaff);
            db.SaveChanges();

            return Ok(companyStaff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyStaffExists(int id)
        {
            return db.CompanyStaffs.Count(e => e.StaffId == id) > 0;
        }
    }
}