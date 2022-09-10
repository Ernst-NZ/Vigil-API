using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

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

            var imageFiles = from files in db.FileDatas
                             join raw in db.RawDatas on files.Id equals raw.Id
                             where files.ParentName == ParentName && files.ParentId == ParentId
                             select new { 
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
