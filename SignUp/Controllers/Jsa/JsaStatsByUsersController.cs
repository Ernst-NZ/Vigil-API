using SignUp.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;

namespace SignUp.Controllers.Jsa
{
    public class JsaStatsByUsersController : ApiController
    {
        private Entities db = new Entities();
        [HttpPost]
        public IHttpActionResult JsaStatsByCompanyUid(dynamic data)
        {
            string uid = data.uid;
            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query =
              @"SET DATEFIRST 1
        Select 'JSA 1 Week', Count(distinct JsaUid) as Total
	          ,(Select Count(distinct JsaUid)
            From JSAData
            Where AddedByUid = '" + uid + "' " +
            "AND (JSAData.Date >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
            "AND JSAData.Date <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate()))  OR JSAData.Date IS NULL ) " +
            "and JSAData.Status IS NOT NULl " +
            "AND JSAData.Status <> '' " +
            "And JSAData.Status <> 'Entered'  " +
            ") as Done " +
            "     ,(Select Count(distinct JsaUid) " +
            "     From JSAData " +
            "     Where AddedByUid = '" + uid + "'  and  (Deleted = 0 or (ISNULL(JSAData.Deleted, '') = '')) " +
            "     AND (JSAData.Date >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
            "     AND JSAData.Date <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate())) OR JSAData.Date IS NULL ) " +
            "     and (ISNULL(JSAData.Status, '') = '' or JSAData.Status = 'Entered')) as Outstanding " +
            "     From JSAData " +
            "     Where AddedByUid = '" + uid + "'  and Deleted = 0 " +
            "     AND (JSAData.Date >= dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
            "     AND JSAData.Date <  dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate()))  OR JSAData.Date IS NULL ) " +
            "     union " +
            "     Select 'Jsa 2 Month', Count(distinct JsaUid) as Total " +
            "     ,(Select Count(distinct JsaUid) " +
            "     From JSAData " +
            "     Where AddedByUid = '" + uid + "'  and  (Deleted = 0 or (ISNULL(JSAData.Deleted, '') = '')) " +
            "     and datepart(mm,JSAData.Date) =month(getdate()) " +
            "     and datepart(yyyy,JSAData.Date) =year(getdate()) " +
            "     and JSAData.Status IS NOT NULl " +
            "    AND JSAData.Status <> '' " +
            "    and JSAData.Status <> 'Entered') as Done " +
            "       ,(Select Count(distinct JsaUid) " +
            "       From JSAData " +
            "       Where AddedByUid = '" + uid + "'  and  (Deleted = 0 or (ISNULL(JSAData.Deleted, '') = '')) " +
            "     and datepart(mm,JSAData.Date) =month(getdate()) " +
            "     and datepart(yyyy,JSAData.Date) =year(getdate()) " +
            "     and (ISNULL(JSAData.Status, '') = '' or JSAData.Status = 'Entered')) as Outstanding " +
            "       From JSAData " +
            "       Where AddedByUid = '" + uid + "'  and  (Deleted = 0 or (ISNULL(JSAData.Deleted, '') = '')) " +
            "       and datepart(mm,JSAData.Date) =month(getdate()) " +
            "     and datepart(yyyy,JSAData.Date) =year(getdate()) " +
            "       union " +
            "       Select 'JSA 3 Older', Count(distinct JsaUid) as Total " +
            "       ,(Select Count(distinct JsaUid) " +
            "       From JSAData " +
            "       Where AddedByUid = '" + uid + "'  and  (Deleted = 0 or (ISNULL(JSAData.Deleted, '') = '')) " +
            "       and (datepart(mm,JSAData.Date) <month(getdate()) " +
            "       OR datepart(yyyy,JSAData.Date) <year(getdate())) " +
            "       and JSAData.Status IS NOT NULl " +
            "      AND JSAData.Status <> '' " +
            "      and JSAData.Status <> 'Entered') as Done " +
            "           ,(Select Count(distinct JsaUid) " +
            "          From JSAData " +
            "         Where AddedByUid = '" + uid + "'  and  (Deleted = 0 or (ISNULL(JSAData.Deleted, '') = '')) " +
            "      and (datepart(mm,JSAData.Date) <month(getdate()) " +
            "        OR datepart(yyyy,JSAData.Date) <year(getdate())) " +
            "       and (ISNULL(JSAData.Status, '') = '' or JSAData.Status = 'Entered')) as Outstanding " +
            "       From JSAData " +
            "       Where AddedByUid = '" + uid + "'  and  (Deleted = 0 or (ISNULL(JSAData.Deleted, '') = '')) " +
            "       and (datepart(mm,JSAData.Date) <month(getdate()) " +
            "     OR datepart(yyyy,JSAData.Date) <year(getdate()))";


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