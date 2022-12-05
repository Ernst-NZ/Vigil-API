using SignUp.Models;
using System.Linq;
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
    public IHttpActionResult GetImagesByRefAndId(imagesParams data)
    {
      var category = data.category;
      var id = data.id;
      var uid = data.uid;
      dynamic images; 
      if (id > 0) {
        images = from i in db.SATSImages
                     where i.ImageCat == category && i.ReferenceId == id
                     select i;
      } else {
        images = from i in db.SATSImages
                     where i.ImageCat == category && i.ReferenceUID == uid
                     select i;
      }
      
      if (images == null)
      {
        return NotFound();
      }
      return Ok(images);
    }
  }
}
