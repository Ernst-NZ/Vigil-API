using SignUp.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignUp.Controllers
{
    public class ImageController : ApiController
    {
        private Entities db = new Entities();

        // POST: api/UploaData
        [ResponseType(typeof(UploadData))]
        public IHttpActionResult PostRawData(UploadData uploadData)
        {
            if (!ModelState.IsValid)
            {
                // return BadRequest(ModelState);
//                Console.WriteLine(ModelState);
            }
            FileData fileData = new FileData()
            {
                ParentId = uploadData.ParentId,
                ParentName = uploadData.ParentName,
                SubFolder = uploadData.SubFolder,
                FileDescription = uploadData.FileDescription,
                FileTopic = uploadData.FileTopic,
                FileName = uploadData.FileName,
                FileExtension = uploadData.FileExtension,
                FileSize = uploadData.FileSize,
                AddedBy = uploadData.AddedBy,
                Date = uploadData.Date,
            };
            db.FileDatas.Add(fileData);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtRoute("DefaultApi", new { id = fileData.Id }, fileData);
        }

    }
}
