using SignUp.Models;
using System;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace SignUp.Controllers.CheckList
{
  public class TrainingStatsByCompaniesController : ApiController
  {
    [HttpGet]
    public IHttpActionResult TrainingStatsByCompanyCode(string id)
    {
      DataTable dataTable = new DataTable();
      string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
      string query =
       @"SELECT count(TrainingId) as red
        	  ,(SELECT count(TrainingId)
            FROM Training
            inner join WebUser on webuser.userid = training.userId
            Where DATEDIFF(day, getDate(), ExpiryDate) between 0 and 30
            and UserCompanyCode = '" + id + "') as orange " +
            ",(SELECT count(TrainingId) " +
            "FROM Training " +
            "inner join WebUser on webuser.userid = training.userId " +
            "Where DATEDIFF(day, getDate(), ExpiryDate) between 31 and 60  " +
            "and UserCompanyCode = '" + id + "') as blue " +
            "FROM Training " +
            "inner join WebUser on webuser.userid = training.userId " +
            "Where DATEDIFF(day, getDate(), ExpiryDate) < 0 " +
            "and UserCompanyCode = '" + id + "'"; 
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