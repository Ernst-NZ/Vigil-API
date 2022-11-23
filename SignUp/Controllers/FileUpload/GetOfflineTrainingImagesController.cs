using SignUp.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace SignUp.Controllers
{
  public class GetOfflineTrainingImagesController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    public IHttpActionResult TrainingByUserId(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
        @"select files.Id,
	        WebUser.UserFirstName + ' ' + WebUser.UserLastName as username,
	        files.ParentId,
	        files.ParentName,
	        files.SubFolder,
	        files.FileDescription,
	        files.FileTopic,
	        files.FileName,
	        files.FileSize,
          rawData.RawFile
        from filedata as files
        inner join rawdata on files.id = RawData.Id
        inner join training on training.trainingid = files.parentid
        inner join WebUser on WebUser.UserId = Training.UserId
        Where ParentName = 'Training'
        and webuser.UserId = '" + id + "' " +
        "Order by files.SubFolder, FileTopic, FileDescription";
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
        return BadRequest(ex.Message);
      }

      conn.Close();
      da.Dispose();

      if (dataTable == null)
      {
        return NotFound();
      }

      return Ok(dataTable);
    }


  }
}