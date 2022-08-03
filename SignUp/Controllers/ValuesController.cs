
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using System.Web.Http.Description;
using SignUp.Models;
using System.Data;
using System.IO;
using System.Text;

namespace WebAPI.Controllers
{
  [AllowAnonymous]
  public class ValuesController : ApiController
  {
  //  private DBModelVigil db = new DBModelVigil();

    //public List<Persoon> GetEmails()
    //{
    //    IQueryable<Persoon> users = db.Persoons;
    //    string searchString = "@";

    //    if (!String.IsNullOrEmpty(searchString))
    //    {
    //        users = users.Where(s => s.Email.Contains(searchString) && s.IsActive.Contains("True"));
    //    }
    //    return (users.ToList());
    //}


    [HttpPost]
    [AllowAnonymous]
    [ResponseType(typeof(gmail))]
    //   public IHttpActionResult PostBatchEmail(gmail gmail)
    public void PostEmail(gmail gmail)
    {
      //var bytes = Convert.FromBase64String(gmail.Attachments[0]);
      //MemoryStream strm = new MemoryStream(bytes);
      //Attachment data = new Attachment(strm, "Test.pdf");

      if (gmail.EmailTo2.Length > 5) {
        gmail.EmailTo = gmail.EmailTo + " , " + gmail.EmailTo2;
      }
  

      string messageResult;
      gmail.EmailFrom = "info@nzsats.co.nz";
      gmail.EmailBcc = "ernst@nzsats.co.nz";
      MailMessage mm = new MailMessage(gmail.EmailFrom, gmail.EmailTo);
      mm.Subject = gmail.Subject;
      mm.Body = gmail.Body;
      mm.Bcc.Add(gmail.EmailBcc);
      mm.IsBodyHtml = true;
//      mm.Attachments.Add(data);
      try
      {
        SmtpClient smtp = new SmtpClient();
        smtp.Send(mm);
      }
      catch (Exception e)
      {
        Console.Write(e.Message);
        messageResult = e.Message;
      }


    }
      //if (gmail.EmailType == "Navraag")
      //{
      //  gmail.EmailFrom = "info@nzsats.co.nz";
      //  MailMessage mm = new MailMessage(gmail.EmailFrom, gmail.EmailTo);
      //  mm.Subject = gmail.Subject;
      //  mm.Body = gmail.Body;
      //  mm.IsBodyHtml = true;
      //  try
      //  {
      //    SmtpClient smtp = new SmtpClient(); 
      //    smtp.Send(mm);
      //    messageResult = "OK";
      //  }
      //  catch (Exception e)
      //  {
      //    Console.Write(e.Message);
      //  }
      //}
      //else
      //{
      //  gmail.EmailFrom = "info@nzsats.co.nz";
      //  MailMessage mm = new MailMessage(gmail.EmailFrom, gmail.EmailTo);
      //  mm.Subject = gmail.Subject;
      //  mm.Body = gmail.Body;
      //  mm.IsBodyHtml = true;
      //  try
      //  {
      //    SmtpClient smtp = new SmtpClient(); 
      //    smtp.Send(mm);        
      //  }
      //  catch (Exception e)
      //  {
      //    Console.Write(e.Message);
      //    messageResult = e.Message;
      //  }
      //  //string emailTo;
      //  //string emailName;
      //  //List<Persoon> lstEmail = GetEmails();

      //  //emailTo = "ernst@hotmail.co.nz";
      //  //emailName = string.Concat("<h3>Goeie dag ", "Admin", ",</h3><br>Die eerste van die Emails<br>");
      //  //MailMessage mm1 = new MailMessage(gmail.EmailFrom, emailTo);
      //  //mm1.Subject = gmail.Subject;
      //  //mm1.Body = string.Concat(emailName, gmail.Body);
      //  //mm1.IsBodyHtml = true;
      //  //SmtpClient smtp1 = new SmtpClient();
      //  //smtp1.Send(mm1);

      //  //foreach (var email in lstEmail)
      //  //{
      //  //    try
      //  //    {
      //  //        emailTo = email.Email;
      //  //        //  emailTo = "ernst@hotmail.co.nz";
      //  //    }
      //  //    catch (FormatException)
      //  //    {
      //  //        //do nothing, illformed address. screw it.
      //  //    }

      //  //    System.Threading.Thread.Sleep(5000);
      //  //    if (n % 5 == 0)
      //  //    {
      //  //        System.Threading.Thread.Sleep(3000);
      //  //    }

      //  //    emailName = string.Concat("<h3>Goeie dag ", email.FirstName, ",</h3><br>");
      //  //    //emailName = string.Concat("<h3>Goeie dag ", "Test", ",</h3><br>");

      //  //    MailMessage mm = new MailMessage(gmail.EmailFrom, emailTo);
      //  //    mm.Subject = gmail.Subject;
      //  //    mm.Body = string.Concat(emailName, gmail.Body);
      //  //    mm.IsBodyHtml = true;
      //  //    w = w + emailTo + "<br>";
      //  //    //   SmtpClient smtp = new SmtpClient(); smtp.Send(mm);
      //  //    try
      //  //    {
      //  //        mm1.Body = string.Concat(emailTo, "<br/>", emailName, gmail.Body);
      //  //        smtp1.Send(mm1);
      //  //        SmtpClient smtp = new SmtpClient(); smtp.Send(mm);
      //  //    }
      //  //    catch (SmtpFailedRecipientsException ex)
      //  //    {
      //  //        x++;
      //  //        errorMessage = errorMessage + ex.Message;
      //  //        File.WriteAllText("errorlog.txt", ex.Message);
      //  //        continue;
      //  //        //    //                  return Ok(errorMessage);
      //  //    }
      //  //    catch (FormatException ex)
      //  //    {
      //  //        //do nothing, illformed address. screw it.
      //  //        x++;
      //  //        errorMessage = errorMessage + ex.Message;
      //  //    }
      //  //    catch (SmtpException ex)
      //  //    {
      //  //        x++;
      //  //        errorMessage = errorMessage + ex.Message;
      //  //        File.WriteAllText("errorlog.txt", ex.Message);
      //  //        continue;
      //  //        //    //                  return Ok(errorMessage);
      //  //    }
      //  //    catch (Exception e)
      //  //    {
      //  //        x++;
      //  //        errorMessage = errorMessage + e.Message;
      //  //        File.WriteAllText("errorlog.txt", e.Message);
      //  //        continue;
      //  //    }

      //  //    n++;
      //  //}

      //}


   // }
  }
}

