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
using Newtonsoft.Json.Linq;
using SignUp.Models;

namespace SignUp.Controllers.Images
{
    public class SATSImagesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/SATSImages
        public IQueryable<SATSImage> GetSATSImages()
        {
            return db.SATSImages;
        }

        // GET: api/SATSImages/5
        [ResponseType(typeof(SATSImage))]
        public IHttpActionResult GetSATSImage(int id)
        {
            SATSImage sATSImage = db.SATSImages.Find(id);
            if (sATSImage == null)
            {
                return NotFound();
            }

            return Ok(sATSImage);
        }

        // PUT: api/SATSImages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSATSImage(int id, SATSImage sATSImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sATSImage.ImageId)
            {
                return BadRequest();
            }

            db.Entry(sATSImage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SATSImageExists(id))
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

    // POST: api/SATSImages
    //[ResponseType(typeof(SATSImage))]
    //public IHttpActionResult PostSATSImage(SATSImage sATSImage)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    db.SATSImages.Add(sATSImage);
    //    db.SaveChanges();

    //    return CreatedAtRoute("DefaultApi", new { id = sATSImage.ImageId }, sATSImage);
    //}
    [ResponseType(typeof(SATSImage))]
    public IHttpActionResult PostSATSImage(JArray objData)
    {
      List<SATSImage> lstItemDetails = new List<SATSImage>();
      JArray itemDetailsJson = objData;
      foreach (var item in itemDetailsJson)
      {
        Console.Write(item);
        lstItemDetails.Add(item.ToObject<SATSImage>());
      }
      foreach (SATSImage itemDetail in lstItemDetails)
      {
        db.SATSImages.Add(itemDetail);
      }
      db.SaveChanges();
      return Ok();
    }


    // DELETE: api/SATSImages/5
    [ResponseType(typeof(SATSImage))]
        public IHttpActionResult DeleteSATSImage(int id)
        {
            SATSImage sATSImage = db.SATSImages.Find(id);
            if (sATSImage == null)
            {
                return NotFound();
            }

            db.SATSImages.Remove(sATSImage);
            db.SaveChanges();

            return Ok(sATSImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SATSImageExists(int id)
        {
            return db.SATSImages.Count(e => e.ImageId == id) > 0;
        }
    }
}