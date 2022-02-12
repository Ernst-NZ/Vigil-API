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
            int ParentId = fileData.ParentId;

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
