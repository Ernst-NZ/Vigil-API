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
using System.Security.Cryptography;
using System.Reflection;

namespace SignUp.Controllers
{
  public class UserInfoController : ApiController
  {
    private DBModelBorn db = new DBModelBorn();

    // GET: api/UserInfo
    public IQueryable<UserInfo> GetUserInfoes()
    {
      //var data = db.UserInfoes;
      //foreach (var userInfo in data)
      //{
      //  userInfo.FirstName = userInfo.LastName; //Enc.Decryptword(gG8lIZeoKh4=);
      //  var xx = Enc.Decryptword("gG8lIZeoKh4=");
      //  userInfo.LastName = xx; // Enc.Decryptword(userInfo.FirstName.ToString());
      //}
      //return data;
      return db.UserInfoes;
    }

    // GET: api/UserInfo/5
    [ResponseType(typeof(UserInfo))]
    public IHttpActionResult GetUserInfo(int id)
    {
      UserInfo userInfo = db.UserInfoes.Find(id);
      if (userInfo == null)
      {
        return NotFound();
      }

      return Ok(userInfo);
    }

    // PUT: api/UserInfo/5
    [ResponseType(typeof(void))]
    public IHttpActionResult PutUserInfo(int id, UserInfo userInfo)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != userInfo.id)
      {
        return BadRequest();
      }
      db.Entry(userInfo).State = EntityState.Modified;


      //foreach (PropertyInfo prop in typeof(UserInfo).GetProperties())
      //{
      //  var dd = prop.GetValue(userInfo, null);
      //  //        Console.WriteLine("{0} = {1}", prop.Name, prop.GetValue(userinfo, null));
      //  if (prop.Name != "UserId" && prop.Name != "id" && prop.Name != "UserName" && prop.PropertyType == typeof(string))
      //  {
      //    if (dd != null)
      //    {
      //      var ss = dd.ToString();
      //      var xx = Enc.Encryptword(ss);
      //      prop.SetValue(userInfo, xx, null);
      //    }
      //  }
      //}

      userInfo.Email = Enc.Encryptword(userInfo.Email);
      userInfo.PhoneNumber = userInfo.PhoneNumber != null ? Enc.Encryptword(userInfo.PhoneNumber) : userInfo.PhoneNumber;
      userInfo.Notes = userInfo.Notes != null ? Enc.Encryptword(userInfo.Notes) : userInfo.Notes;
      userInfo.FirstName = userInfo.FirstName != null ? Enc.Encryptword(userInfo.FirstName) : userInfo.FirstName;
      userInfo.DateOfBirth = userInfo.DateOfBirth != null ? Enc.Encryptword(userInfo.DateOfBirth) : userInfo.DateOfBirth;
      userInfo.BirthCountry = userInfo.BirthCountry != null ? Enc.Encryptword(userInfo.BirthCountry) : userInfo.BirthCountry;
      userInfo.LastName = userInfo.LastName != null ? Enc.Encryptword(userInfo.LastName) : userInfo.LastName;
      userInfo.Address1 = userInfo.Address1 != null ? Enc.Encryptword(userInfo.Address1) : userInfo.Address1;
      userInfo.Address2 = userInfo.Address2 != null ? Enc.Encryptword(userInfo.Address2) : userInfo.Address2;
      userInfo.Suburb = userInfo.Suburb != null ? Enc.Encryptword(userInfo.Suburb) : userInfo.Suburb;
      userInfo.City = userInfo.City != null ? Enc.Encryptword(userInfo.City) : userInfo.City;
      userInfo.OtherContact = userInfo.OtherContact != null ? Enc.Encryptword(userInfo.OtherContact) : userInfo.OtherContact;
      userInfo.ParentProblems = userInfo.ParentProblems != null ? Enc.Encryptword(userInfo.ParentProblems) : userInfo.ParentProblems;
      userInfo.FamilyComments = userInfo.FamilyComments != null ? Enc.Encryptword(userInfo.FamilyComments) : userInfo.FamilyComments;
      userInfo.MedicalNotes = userInfo.MedicalNotes != null ? Enc.Encryptword(userInfo.MedicalNotes) : userInfo.MedicalNotes;
      userInfo.LastCheckup = userInfo.LastCheckup != null ? Enc.Encryptword(userInfo.LastCheckup) : userInfo.LastCheckup;
      userInfo.Checkupdate = userInfo.Checkupdate != null ? Enc.Encryptword(userInfo.Checkupdate) : userInfo.Checkupdate;
      userInfo.MedicationName = userInfo.MedicationName != null ? Enc.Encryptword(userInfo.MedicationName) : userInfo.MedicationName;
      userInfo.MedicationReason = userInfo.MedicationReason != null ? Enc.Encryptword(userInfo.MedicationReason) : userInfo.MedicationReason;
      userInfo.PsycNotes = userInfo.PsycNotes != null ? Enc.Encryptword(userInfo.PsycNotes) : userInfo.PsycNotes;
      userInfo.PsycProblems = userInfo.PsycProblems != null ? Enc.Encryptword(userInfo.PsycProblems) : userInfo.PsycProblems;
      userInfo.PsycPreviousDates = userInfo.PsycPreviousDates != null ? Enc.Encryptword(userInfo.PsycPreviousDates) : userInfo.PsycPreviousDates;
      userInfo.PsycPreviousTreated = userInfo.PsycPreviousTreated != null ? Enc.Encryptword(userInfo.PsycPreviousTreated) : userInfo.PsycPreviousTreated;
      userInfo.ViolenceExplain = userInfo.ViolenceExplain != null ? Enc.Encryptword(userInfo.ViolenceExplain) : userInfo.ViolenceExplain;
      userInfo.GreatestFear = userInfo.GreatestFear != null ? Enc.Encryptword(userInfo.GreatestFear) : userInfo.GreatestFear;
      userInfo.GreatestHope = userInfo.GreatestHope != null ? Enc.Encryptword(userInfo.GreatestHope) : userInfo.GreatestHope;
      userInfo.SeekingHelpFor = userInfo.SeekingHelpFor != null ? Enc.Encryptword(userInfo.SeekingHelpFor) : userInfo.SeekingHelpFor;
      userInfo.WhoKnows = userInfo.WhoKnows != null ? Enc.Encryptword(userInfo.WhoKnows) : userInfo.WhoKnows;
      userInfo.OutcomeWanted = userInfo.OutcomeWanted != null ? Enc.Encryptword(userInfo.OutcomeWanted) : userInfo.OutcomeWanted;
      userInfo.MeetingAssist = userInfo.MeetingAssist != null ? Enc.Encryptword(userInfo.MeetingAssist) : userInfo.MeetingAssist;
      userInfo.AdditionalComments = userInfo.AdditionalComments != null ? Enc.Encryptword(userInfo.AdditionalComments) : userInfo.AdditionalComments;
      userInfo.AdminNotes = userInfo.AdminNotes != null ? Enc.Encryptword(userInfo.AdminNotes) : userInfo.AdminNotes;

      try
      {
        db.SaveChanges();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserInfoExists(id))
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

    // POST: api/UserInfo
    [ResponseType(typeof(UserInfo))]
    public IHttpActionResult PostUserInfo(UserInfo userInfo)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      db.UserInfoes.Add(userInfo);
      db.SaveChanges();

      return CreatedAtRoute("DefaultApi", new { id = userInfo.id }, userInfo);
    }

    // DELETE: api/UserInfo/5
    [ResponseType(typeof(UserInfo))]
    public IHttpActionResult DeleteUserInfo(int id)
    {
      UserInfo userInfo = db.UserInfoes.Find(id);
      if (userInfo == null)
      {
        return NotFound();
      }

      db.UserInfoes.Remove(userInfo);
      db.SaveChanges();

      return Ok(userInfo);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    private bool UserInfoExists(int id)
    {
      return db.UserInfoes.Count(e => e.id == id) > 0;
    }

    public string Decryptword(string DecryptText)
    {
      int req = Convert.ToInt32(DecryptText);
      if (req == 1)
      {
        var cleanText = Enc.Decryptword(DecryptText);
        return cleanText;
      }
      else
      {
        return DecryptText;
      }
    }
  }
}