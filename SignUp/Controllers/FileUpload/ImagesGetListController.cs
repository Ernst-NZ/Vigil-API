using SignUp.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SignUp.Controllers
{
  public class ImagesGetListController : ApiController
  {
    private Entities db = new Entities();
    [HttpPost]
    [ResponseType(typeof(void))]
    public IHttpActionResult PostStatement(imageSearch fileData)
    {
      string ParentName = fileData.ParentName;
      string ParentId = fileData.ParentId;
      DataTable dataTable = new DataTable();

      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      if (ParentName == "Training")
      {
        string query =
          @"select files.Id,
	            WebUser.UserFirstName + ' ' + WebUser.UserLastName as username,
	            files.ParentId,
	            files.ParentName,
	            files.SubFolder,
	            files.FileDescription,
	            files.FileTopic,
	            files.FileName,
	            files.FileExtension,
	            files.FileSize,
	            files.AddedBy,
	            files.Date
            from filedata as files
            inner join rawdata on files.id = RawData.Id
            inner join training on training.trainingid = files.parentid
            inner join WebUser on WebUser.UserId = Training.UserId
            Where ParentName = '" + ParentName + "' and files.ParentId = " + ParentId + " Order by files.SubFolder, FileTopic, FileDescription";
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
      else 
      {
        var imageFiles = from files in db.FileDatas
                         join raw in db.RawDatas on files.Id equals raw.Id
                         where files.ParentName == ParentName && files.ParentId == ParentId
                         select new
                         {
                           files.Id,
                           files.ParentId,
                           files.ParentName,
                           SubFolder = files.SubFolder ?? " ",
                           FileDescription = files.FileDescription ?? " ",
                           files.FileTopic,
                           files.FileName,
                           files.FileExtension,
                           files.FileSize,
                           files.AddedBy,
                           files.Date
                         };
        imageFiles.OrderBy(x => x.SubFolder).ThenBy(z => z.FileTopic).ThenBy(z => z.FileDescription);

        if (imageFiles == null)
        {
          return NotFound();
        }
        return Ok(imageFiles);
      }
    }
  }
}
