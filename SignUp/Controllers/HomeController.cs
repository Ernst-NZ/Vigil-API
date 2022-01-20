using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using SignUp.Models;
using System.Text;
using System.Configuration;

namespace SignUp.Controllers
{
  //  [Authorize]
  public class HomeController : Controller
  {
    string key = ConfigurationManager.AppSettings["EncryptKey"];
    public ActionResult Index()
    {
      ViewBag.Title = "Home Page";
      EModel obj = new EModel();
      return View(obj);

      //return View();
    }

    [HttpPost]
    public ActionResult Index(EModel obj)
    {
      int req = Convert.ToInt32(Request.Form["type"]);
      if (req == 1)
      {
        ViewBag.Result = Encryptword(obj.word);
      }
      else
      {
        ViewBag.Result = Decryptword(obj.word);
      }
      return View(obj);
    }
    public string Encryptword(string Encryptval)
    {
      byte[] SrctArray;
      byte[] EnctArray = UTF8Encoding.UTF8.GetBytes(Encryptval);
      SrctArray = UTF8Encoding.UTF8.GetBytes(key);
      TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();
      MD5CryptoServiceProvider objcrpt = new MD5CryptoServiceProvider();
      SrctArray = objcrpt.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
      objcrpt.Clear();
      objt.Key = SrctArray;
      objt.Mode = CipherMode.ECB;
      objt.Padding = PaddingMode.PKCS7;
      ICryptoTransform crptotrns = objt.CreateEncryptor();
      byte[] resArray = crptotrns.TransformFinalBlock(EnctArray, 0, EnctArray.Length);
      objt.Clear();
      return Convert.ToBase64String(resArray, 0, resArray.Length);
    }
    public string Decryptword(string DecryptText)
    {
      byte[] SrctArray;
      byte[] DrctArray = Convert.FromBase64String(DecryptText);
      SrctArray = UTF8Encoding.UTF8.GetBytes(key);
      TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();
      MD5CryptoServiceProvider objmdcript = new MD5CryptoServiceProvider();
      SrctArray = objmdcript.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
      objmdcript.Clear();
      objt.Key = SrctArray;
      objt.Mode = CipherMode.ECB;
      objt.Padding = PaddingMode.PKCS7;
      ICryptoTransform crptotrns = objt.CreateDecryptor();
      byte[] resArray = crptotrns.TransformFinalBlock(DrctArray, 0, DrctArray.Length);
      objt.Clear();
      return UTF8Encoding.UTF8.GetString(resArray);
    }

  }
}
