using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
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

        [HttpGet]
        public List<FileRecord> GetAllFilesPerParentNameAndId()
        {
            //getting data from inmemory obj
            //return fileDB;
            //getting data from SQL DB
            return db.FileDatas.Select(n => new FileRecord
            {
                Id = n.Id,
                FileFormat = n.FileExtension,
                FileName = n.FileName,
            }).ToList();
        }


        // POST: api/RawDatas
        [HttpGet]
        [ResponseType(typeof(FileData))]
        public IHttpActionResult getRawData(dynamic fileData)
        {
            string ParentName = fileData["ParentName"];
            int ParentId = fileData["ParentId"];

            var imageFiles = from files in db.FileDatas
                                  where files.ParentName == ParentName && files.ParentId == ParentId
                             select files;
            imageFiles.OrderBy(x => x.SubFolder).ThenBy(z => z.FileTopic).ThenBy(z => z.FileDescription);
            if (imageFiles == null)
            {
                return NotFound();
            }

            return Ok(imageFiles);
        }
    }
}
