using SignUp.Models;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;

namespace SignUp.Controllers
{
    public class TestController : ApiController
    {

    [HttpPost]
    [AllowAnonymous]
    [ResponseType(typeof(gmail))]
    public void PostEmail(string client, string EmailTo, string appointment)
    {
      string mailBody;
      mailBody = @"<h3>AnB Counselling Appointment: " + client + "<br>" +
                  "" + appointment + "</h3>" +
                  "<b>Good day " + client + "</b>,<br><br> " +
                  "Just a quick reminder of your upcoming appointment with Anneke.<br> " +
                  "Your appointment is scheduled for: <b><i>" + appointment + "</i></b>.<br><br> " +
                  "<u><h4>Please let us know in advance if you won't be able to attend the scheduled meeting.</h4></u>" +
                  "Kind regards.<br> " +
                  "Anneke.<br><br> " +
                  "<b><i>AnB Councelling<br> " +
                  "www:</i></b> https://anbcounselling.co.nz<br>" +
                  "<b><i>Email:</i></b> <a href='mailto: anneke@example.com'>anneke@example.com</a><br>" +
                  "<b><i>Phone:</i></b> 012 10111<br><br>" +
                  "Actual Email will go to: " + EmailTo;

      string messageResult;
      string EmailFrom = "admin@anbcounselling.co.nz";
      EmailTo = "ernst@hotmail.co.nz";
      MailMessage mm = new MailMessage(EmailFrom, EmailTo);
      mm.Subject = "AnB Counselling Appointment: " + client;
      mm.Body = mailBody;
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
      }
    }

    // Get Reminders from DB
    // Called as scheduled task and from void below
    // If found will sent Email
    [HttpGet]
    [ResponseType(typeof(calendar))]
    public IHttpActionResult GetRemindersFromDB()
    {
      DateTime today = DateTime.Now;
      DateTime ToDate = today.AddDays(3);
      string toDate = ToDate.ToString("yyyy-MM-dd");
      string fromDate = today.ToString("yyyy-MM-dd");

      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query = @"SELECT calendar.id " +
             " ,startDate " +
             " ,endDate " +
             " ,UserInfo.Email " +
             " ,Case when UserInfo.Firstname = '' or UserInfo.FirstName IS NULL then calendar.title else UserInfo.FirstName end FirstName " +
             " FROM calendar " +
             " inner join UserInfo on UserInfo.id = calendar.clientId " +
             " Where clientId between 1 and 99998 " +
             " and Upper(title) <> 'CANCELLED' " +
             " and EmailReminder = 'True' " +
             " and (RemiderSent = '' OR RemiderSent is NULL ) " +
             " and startDate > '" + fromDate + "' and startDate < '" + toDate + "' ";

      SqlConnection conn = new SqlConnection(connString);
      SqlCommand cmd = new SqlCommand(query, conn);
      SqlDataAdapter da = new SqlDataAdapter(cmd);
      try
      {
        conn.Open();
        da.Fill(dataTable);
      }
      catch (Exception ex)
      {
        myErrors(ex);
      }

      conn.Close();
      da.Dispose();
      string client;
      string appointment;
      string EmailTo;

      foreach (DataRow name in dataTable.Rows)
      {
        //client = Enc.Decryptword(name["FirstName"].ToString());
        //EmailTo = Enc.Decryptword(name["Email"].ToString());
        //string tempDate = name["startDate"].ToString();
        //var oDate = DateTime.Parse(tempDate);
        //appointment = oDate.ToString("dddd', 'dd' 'MMM' 'yyyy' at 'HH':'mm");
        //updateEmail(int.Parse(name["id"].ToString()));
        //PostEmail(client, EmailTo, appointment);
      }
      //  string result = dataTable.Rows.Count.ToString() + " Email/s Sent";
      string result = fromDate + "  " + toDate;  
      return Ok(result);
    }


    //   GET: api/SPGetText/ - Text per ID
    [HttpGet]
    [ResponseType(typeof(calendar))]
    public IHttpActionResult getReminders(string id)
    {
      var result = GetRemindersFromDB();
      //      var result = "test";
      return Ok(result);
    }


    public static string myErrors(object Error)
    {
      Console.WriteLine(Error);
      return "Error";
    }

    public static string updateEmail(int id)
    {
      using (var db = new DBModelVigil())
      {
        var Calendar = db.calendars.Single(u => u.id == id);
        Calendar.RemiderSent = true;
        db.SaveChanges();
      }
      return "OK";
    }

  }
}