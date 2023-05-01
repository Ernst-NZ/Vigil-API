
using System;
using System.Web.Http;
using System.Net.Mail;
using System.Web.Http.Description;
using SignUp.Models;

namespace WebAPI.Controllers
{
  [AllowAnonymous]
  public class EmailController : ApiController
  {
    [HttpGet]
    public IHttpActionResult GetOrders()
    {
      dynamic xx;
      xx = "This is a tests";

     
      return Ok(xx);
      //  return Ok("Loaded: " + i + " Errors: " + err + " Duplicates: " + dup);
    }





    [HttpPost]
    [AllowAnonymous]
    [ResponseType(typeof(gmail))]
    public void PostBatchEmail(gmail gmail)
    {
      // gmail.EmailFrom = "noreply@ezy.kiwi";
      MailMessage mm = new MailMessage(gmail.EmailFrom, gmail.EmailTo);
        gmail.EmailBcc = "ernst.nz@outlook.com";
        mm.Subject = gmail.Subject;
        mm.Body = gmail.Body;
        mm.Bcc.Add(gmail.EmailBcc);
        mm.IsBodyHtml = true;
        try
        {
          SmtpClient smtp = new SmtpClient();
          smtp.Send(mm);
        }
        catch (Exception e)
        {
          Console.Write(e.Message);
        }
   
    }
  }
}

