using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using SignUp.Models;

namespace SignUp.Controllers.Jsa
{
    public class JSADatasController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/JSADatas
        public IQueryable<JSAData> GetJSADatas()
        {
            return db.JSADatas;
        }

        // GET: api/JSADatas/5
        [ResponseType(typeof(JSAData))]
        public IHttpActionResult GetJSAData(string id)
        {
            JSAData jSAData = db.JSADatas.Find(id);
            if (jSAData == null)
            {
                return NotFound();
            }

            return Ok(jSAData);
        }

        // PUT: api/JSADatas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJSAData(string id, JArray objData)
        {
            var TaskUid = "";
            List<JSAData> lstItemDetails = new List<JSAData>();
            JArray itemDetailsJson = objData;
            foreach (var item in itemDetailsJson)
            {
                lstItemDetails.Add(item.ToObject<JSAData>());
            }
            foreach (JSAData itemDetail in lstItemDetails)
            {
                //db.CheckListLogs.Add(itemDetail);
                TaskUid = itemDetail.TaskUid;
                if (TaskUid != itemDetail.TaskUid)
                {
                    return BadRequest();
                }
                db.Entry(itemDetail).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JSADataExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != jSAData.TaskUid)
            //{
            //    return BadRequest();
            //}

            //db.Entry(jSAData).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!JSADataExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/JSADatas
        [ResponseType(typeof(JSAData))]
        public IHttpActionResult PostJSAData(JArray objData)
        {
            List<JSAData> lstItemDetails = new List<JSAData>();
            JArray itemDetailsJson = objData;
            foreach (var item in itemDetailsJson)
            {
                int email = item.ToString().IndexOf("Reported Job Safety Analysis");
                if (email > 0)
                {
                    PostEmail(item);
                }
                else {
                    lstItemDetails.Add(item.ToObject<JSAData>());
                }
                
            }
            foreach (JSAData itemDetail in lstItemDetails)
            {
                db.JSADatas.Add(itemDetail);
            }
            try
            {
                db.SaveChanges();
                System.Threading.Thread.Sleep(500);                
                return Ok();
            }
            catch (WebException ex)
            {
                return BadRequest(ex.Message);
            }
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.JSADatas.Add(jSAData);

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateException)
            //{
            //    if (JSADataExists(jSAData.TaskUid))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return CreatedAtRoute("DefaultApi", new { id = jSAData.TaskUid }, jSAData);
        }

        // DELETE: api/JSADatas/5
        [ResponseType(typeof(JSAData))]
        public IHttpActionResult DeleteJSAData(string id)
        {
            JSAData jSAData = db.JSADatas.Find(id);
            if (jSAData == null)
            {
                return NotFound();
            }

            db.JSADatas.Remove(jSAData);
            db.SaveChanges();

            return Ok(jSAData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JSADataExists(string id)
        {
            return db.JSADatas.Count(e => e.TaskUid == id) > 0;
        }


        private void PostEmail(dynamic incident)
        {
            string messageResult = "";
            incident.EmailTo = incident.EmailTo;
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("noreply@ezy.kiwi");
            mm.To.Add("ernst@hotmail.co.nz");
            mm.Subject = "Job Safety Anallysis registered";
            mm.Body = incident.EmailMessage;
            mm.Bcc.Add("ernst@ezy.kiwi");
            mm.IsBodyHtml = true;
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Send(mm);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                messageResult = e.Message;
                test(messageResult);
            }
            Console.WriteLine(messageResult);
        }

        private void test(dynamic xx)
        {
            Console.Write(xx);
            Console.WriteLine(xx);
        }

    }
}