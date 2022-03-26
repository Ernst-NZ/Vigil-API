using Newtonsoft.Json.Linq;
using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignUp.Controllers.CheckList
{
  public class DuplicateCheckListMastersController : ApiController
  {
    private Entities db = new Entities();
      
    // POST: api/CheckListLogs
    [ResponseType(typeof(ChecklistMaster))]
    public IHttpActionResult PostDuplicateCheckListMaster(JArray objData)
    {
      List<ChecklistMaster> lstItemDetails = new List<ChecklistMaster>();
      JArray itemDetailsJson = objData;
      foreach (var item in itemDetailsJson)
      {
        lstItemDetails.Add(item.ToObject<ChecklistMaster>());
      }
      foreach (ChecklistMaster itemDetail in lstItemDetails)
      {
        db.ChecklistMasters.Add(itemDetail);
      }
      try
      {
        db.SaveChanges();
        System.Threading.Thread.Sleep(500);
        return Ok();
      }
      catch (WebException ex)
      {
        return BadRequest(ex.Message);
      }
    }






  }
}
