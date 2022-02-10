using SignUp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SignUp.Controllers
{
    public class ImageController : ApiController
    {
        private Entities db = new Entities();
        private readonly string AppDirectory = HttpContext.Current.Server.MapPath("~/Images/");
        [HttpPost]       
        public HttpResponseMessage UploadImage()
        {
            string FileName = null;
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            var postedFile = httpRequest.Files["Image"];
            // Create Custom File Name
            FileName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            FileName = FileName + DateTime.Now.ToString("dd MMM yy") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Images/" + FileName);
            postedFile.SaveAs(filePath);
            using (db)
            {
                FileData image = new FileData()
                {
                    FileName = httpRequest["ImageCaption"],
                    FileExtension = Path.GetExtension(postedFile.FileName),
                    MimeType = "x",
                    FilePath = filePath
                };
                db.FileDatas.Add(image);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpGet]
        public List<FileRecord> GetAllFiles()
        {
            //getting data from inmemory obj
            //return fileDB;
            //getting data from SQL DB
            return db.FileDatas.Select(n => new FileRecord
            {
                Id = n.Id,
                ContentType = n.MimeType,
                FileFormat = n.FileExtension,
                FileName = n.FileName,
                FilePath = n.FilePath
            }).ToList();
        }

        [HttpGet]
        public HttpResponseMessage DownloadFile(int id)
        {
            if (!Directory.Exists(AppDirectory))
                Directory.CreateDirectory(AppDirectory);

            //getting file from inmemory obj
            //var file = fileDB?.Where(n => n.Id == id).FirstOrDefault();
            //getting file from DB
            var file = db.FileDatas.Where(n => n.Id == id).FirstOrDefault();

            var fullPath = Path.Combine(AppDirectory, file?.FilePath);

            if (File.Exists(fullPath))
            {

                //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                //var fileStream = new FileStream(fullPath, FileMode.Open);
                //response.Content = new StreamContent(fileStream);
                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                //response.Content.Headers.ContentDisposition.FileName = file.FileName;
                //return response;

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(new FileStream(fullPath, FileMode.Open, FileAccess.Read));
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = file.FileName;

                return response;


            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
