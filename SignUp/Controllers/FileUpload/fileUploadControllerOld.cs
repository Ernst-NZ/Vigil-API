using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;

namespace SignUp.Controllers
{
    [AllowAnonymous]
  public class fileUploadControllerOld : ApiController
  {


    [HttpGet]
    public IHttpActionResult getImagesCheckList()
    {

      DirectoryInfo directory = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Uploads"));
      var files = directory.GetFiles().ToList();
      return Ok(files);
    }

    [HttpPost]
    public IHttpActionResult getFiles()
    {
      int i = 0;
      int cntSuccess = 0;
      var uploadedFileNames = new List<string>();
      string result = string.Empty;

      HttpResponseMessage response = new HttpResponseMessage();

      var httpRequest = HttpContext.Current.Request;
      if (httpRequest.Files.Count > 0)
      {
        foreach (string file in httpRequest.Files)
        {
          var postedFile = httpRequest.Files[i];
          var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + postedFile.FileName);
          try
          {
            postedFile.SaveAs(filePath);
            uploadedFileNames.Add(httpRequest.Files[i].FileName);
            cntSuccess++;
          }
          catch (Exception ex)
          {
            throw ex;
          }
          i++;
        }
      }

      result = cntSuccess.ToString() + " files uploaded succesfully.<br/>";

      result += "<ul>";

      foreach (var f in uploadedFileNames)
      {
        result += "<li>" + f + "</li>";
      }

      result += "</ul>";

      return Json(result);
    }

  }
}
