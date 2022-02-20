using SignUp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignUp.Controllers
{
    public class TrainingByUserIdController : ApiController
  {
    private Entities db = new Entities();
    [HttpGet]
    //public IHttpActionResult GetTrainingByUserId(string id)
    //{
    //  var trainings = from i in db.Trainings
    //                  where i.UserId == id
    //                  select new
    //                  {
    //                    i.UserId,
    //                    i.TrainingId,
    //                    CompetencyLevel = i.CompetencyLevel ?? "",
    //                    ExpiryDate = i.ExpiryDate ?? "",
    //                    UpdateDate = i.UpdateDate ?? "",
    //                    LastUpdateBy = i.LastUpdateBy ?? "",
    //                    CourseDate = i.CourseDate ?? "",
    //                    CourseName = i.CourseName ?? "",
    //                    ProviderName = i.ProviderName ?? ""
    //                  };
    //  trainings.OrderBy(x => x.CourseName);
    //  if (trainings == null)
    //  {
    //    return NotFound();
    //  }

    //  return Ok(trainings);
    //}
    public IHttpActionResult TrainingByCompanyId(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query = @"SELECT WebUser.UserFirstName
                              ,WebUser.UserLastName
                              ,train.TrainingId
                              ,train.UserId
                              ,coalesce(train.CourseName, '') CourseName
                              ,coalesce(train.CourseDate, '') CourseDate
                              ,coalesce(train.ExpiryDate, '') ExpiryDate
                              ,coalesce(train.CompetencyLevel, '') CompetencyLevel
                              ,coalesce(train.ProviderName, '') ProviderName
                              ,train.LastUpdateBy
                              ,coalesce(train.UpdateDate, '') UpdateDate
  	                          ,count(FD.Id) as Docs
                          FROM WebUser 
                            Inner Join Company on Company.CompanyCode = WebUser.UsercompanyCode
                            Inner Join Training train on train.UserId = WebUser.UserId
                            left Join FileData as FD on FD.ParentId = train.TrainingId 
                                 AND FD.ParentName = 'Training'   
                          Where WebUser.UserId = " + id + " " +
                         "Group by WebUser.UserFirstName " +
                              ", WebUser.UserLastName " +
                              ",train.TrainingId " +
                              ",train.UserId " +
                              ",train.CourseName" +
                              ",train.CourseDate" +
                              ",train.ExpiryDate" +
                              ",train.CompetencyLevel" +
                              ",train.ProviderName" +
                              ",train.LastUpdateBy " +
                              ",train.UpdateDate";

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