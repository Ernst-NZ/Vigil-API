using System;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SignUp.Controllers
{
  public class TrainingByCompanyIdController : ApiController
  {
   // private Entities db = new Entities();
    [HttpGet]
    //public IHttpActionResult TrainingByCompanyId(int id)
    //{
    //  var companyTraining = from WebUser in db.WebWebUsers
    //                join comp in db.Companies on WebUser.WebUserCompanyCode equals comp.CompanyCode
    //                join train in db.Trainings on WebUser.WebUserId equals train.WebUserId
    //                where comp.CompanyId == id
    //                select new { WebUser.WebUserFirstName, 
    //                  WebUser.WebUserLastName,
    //                  train.TrainingId,
    //                  train.WebUserId,
    //                  CourseName = train.CourseName ?? "", 
    //                  CourseDate = train.CourseDate ?? "", 
    //                  ExpiryDate = train.ExpiryDate ?? "",
    //                  CompetencyLevel = train.CompetencyLevel ?? "",
    //                  ProviderName = train.ProviderName ?? "",
    //                  train.LastUpdateBy,
    //                  UpdateDate = train.UpdateDate ?? "",
    //                };
    //  companyTraining.OrderBy(x => x.WebUserFirstName).ThenBy(z => z.CourseName) ;
    //  if (companyTraining == null)
    //  {
    //    return NotFound();
    //  }

    //  return Ok(companyTraining);
    //}

   
    public IHttpActionResult TrainingByCompanyId(int id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
        @"SELECT WebUser.UserFirstName
          ,WebUser.UserLastName
          ,train.TrainingId
          ,train.UserId
          ,coalesce(train.CourseName, '') CourseName
          ,coalesce(train.CourseDate, '') CourseDate
          ,coalesce(train.ExpiryDate, '') ExpiryDate
          ,coalesce(train.CompetencyLevel, '') CompetencyLevel
          ,coalesce(train.ProviderName, '') ProviderName
          ,coalesce(train.StudentNumber, '') StudentNumber
          ,coalesce(train.NZQANumber, '') NZQANumber
          ,train.LastUpdateBy
          ,coalesce(train.UpdateDate, '') UpdateDate
  	      ,(Select Count(FD.Id) from 
    				FileData as FD
		    		Inner join RawData on RawData.Id = fd.Id
	    			where FD.ParentId = train.TrainingId
			    	and ParentName = 'Training') as Docs
        FROM WebUser 
          Inner Join Company on Company.CompanyCode = WebUser.UsercompanyCode
          Inner Join Training train on train.UserId = WebUser.UserId
        Where Company.CompanyId =  '" + id + "' " +
        "Group by WebUser.UserFirstName " +
        "  , WebUser.UserLastName " +
        "  ,train.TrainingId " +
        "  ,train.UserId " +
        "  ,train.CourseName " +
        "  ,train.CourseDate " +
        "  ,train.ExpiryDate " +
        "  ,train.CompetencyLevel " +
        "  ,train.ProviderName " +
        "  ,train.StudentNumber " +
        "  ,train.NZQANumber " +
        "  ,train.LastUpdateBy " +
        "  ,train.UpdateDate " +
        "Union " +
        "SELECT CompanyStaff.StaffName " +
        "  , CompanyStaff.StaffSurname " +
        "  ,train.TrainingId " +
        "  ,cast(train.UserId as varchar(6)) " +
        "  ,coalesce(train.CourseName, '') CourseName " +
        "  ,coalesce(train.CourseDate, '') CourseDate " +
        "  ,coalesce(train.ExpiryDate, '') ExpiryDate " +
        "  ,coalesce(train.CompetencyLevel, '') CompetencyLevel " +
        "  ,coalesce(train.ProviderName, '') ProviderName " +
        "  , coalesce(train.StudentNumber, '') StudentNumber " +
        " , coalesce(train.NZQANumber, '') NZQANumber " +
        "  ,train.LastUpdateBy " +
        "  ,coalesce(train.UpdateDate, '') UpdateDate " +
        "  ,(Select Count(FD.Id) from " +
        "   FileData as FD " +
        "   Inner join RawData on RawData.Id = fd.Id " +
        "   where FD.ParentId = train.TrainingId " +
        "   and ParentName = 'Training') as Docs " +
        "FROM CompanyStaff " +
        "  Inner Join Company on Company.CompanyCode = CompanyStaff.CompanyCode " +
        "  Inner Join Training train on train.UserId = cast(CompanyStaff.StaffId as varchar(6)) " +
        "Where Company.CompanyId =  '" + id + "' " +
        "Group by CompanyStaff.StaffName " +
        "  , CompanyStaff.StaffSurName " +
        "  ,train.TrainingId " +
        "  ,train.UserId " +
        "  ,train.CourseName " +
        "  ,train.CourseDate " +
        "  ,train.ExpiryDate " +
        "  ,train.CompetencyLevel " +
        "  ,train.ProviderName " +
        "  ,train.StudentNumber " +
        "  ,train.NZQANumber " +
        "  ,train.LastUpdateBy " +
        "  ,train.UpdateDate";

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
