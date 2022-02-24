using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignUp.Controllers.Images
{
    public class GetImagesByRefAndIdsController : ApiController
    {

    private Entities db = new Entities();
    //  [Authorize]
    [AllowAnonymous]
    [HttpPost]
    [ResponseType(typeof(void))]
    public IHttpActionResult PostStatement(imagesParams data)
    {
      var category = data.category;
      var id = data.id;
      var images = from i in db.SATSImages
                   where i.ImageCat == category && i.ReferenceId == id
                   select i;
      if (images == null)
      {
        return NotFound();
      }
      return Ok(images);
    }
  }
}
