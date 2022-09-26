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

namespace SignUp.Controllers.SafetyPlan
{
    public class AnnualSafetyPlansController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/AnnualSafetyPlans
        public IQueryable<AnnualSafetyPlan> GetAnnualSafetyPlans()
        {
            return db.AnnualSafetyPlans;
        }

        // GET: api/AnnualSafetyPlans/5
        [ResponseType(typeof(AnnualSafetyPlan))]
        public IHttpActionResult GetAnnualSafetyPlan(Guid id)
        {
            AnnualSafetyPlan annualSafetyPlan = db.AnnualSafetyPlans.Find(id);
            if (annualSafetyPlan == null)
            {
                return NotFound();
            }

            return Ok(annualSafetyPlan);
        }

        // PUT: api/AnnualSafetyPlans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnnualSafetyPlan(Guid id, AnnualSafetyPlan annualSafetyPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != annualSafetyPlan.PlanningUid)
            {
                return BadRequest();
            }

            db.Entry(annualSafetyPlan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnualSafetyPlanExists(id))
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

        // POST: api/AnnualSafetyPlans
        [ResponseType(typeof(AnnualSafetyPlan))]
        public IHttpActionResult PostAnnualSafetyPlan(AnnualSafetyPlan annualSafetyPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AnnualSafetyPlans.Add(annualSafetyPlan);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AnnualSafetyPlanExists(annualSafetyPlan.PlanningUid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = annualSafetyPlan.PlanningUid }, annualSafetyPlan);
        }

        // DELETE: api/AnnualSafetyPlans/5
        [ResponseType(typeof(AnnualSafetyPlan))]
        public IHttpActionResult DeleteAnnualSafetyPlan(Guid id)
        {
            AnnualSafetyPlan annualSafetyPlan = db.AnnualSafetyPlans.Find(id);
            if (annualSafetyPlan == null)
            {
                return NotFound();
            }

            db.AnnualSafetyPlans.Remove(annualSafetyPlan);
            db.SaveChanges();

            return Ok(annualSafetyPlan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnnualSafetyPlanExists(Guid id)
        {
            return db.AnnualSafetyPlans.Count(e => e.PlanningUid == id) > 0;
        }
    }
}