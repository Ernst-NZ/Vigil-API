using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;

namespace SignUp.Controllers
{
    public class AccessByCompanyUidsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult ChemicalStatsByCompanies(string id)
        {
            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query =
             @"Select t.[NoOfUsers]
              ,t.[ExpiryDate]
	          ,Format(DateAdd(month,1, CAST(ExpiryDate AS DATE)), 'dd-MMM-yyyy' ) Yellow
              ,Format(DateAdd(month,2, CAST(ExpiryDate AS DATE)), 'dd-MMM-yyyy' ) Red
	          ,(SELECT Count([UserId])
                  FROM [Vigil].[dbo].[WebUser]
                  inner join Company on Company.CompanyCode = WebUser.userCompanyCode
                  Where Company.CompanyUid = t.CompanyUid
                  and
                  (UserNotActive is null OR UserNotActive <> '1')) activeUsers
                FROM [Vigil].[dbo].[AccessCompany] t
	                inner join (
                    Select distinct companyUid, max(CAST(ExpiryDate AS DATE)) as maxDate
		                FROM [Vigil].[dbo].[AccessCompany]
                        Group by [CompanyUid]
                    ) tm on t.companyuid = tm.companyUid and t.ExpiryDate = tm.maxDate
                Where t.companyuid = '" + id + "' ";
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