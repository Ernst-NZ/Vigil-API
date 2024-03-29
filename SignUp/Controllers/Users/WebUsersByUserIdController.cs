﻿using SignUp.Models;
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
    public class WebUsersByUserIdController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetUsers(string id)
        {

            DataTable dataTable = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["IdentityDemoConnection"].ConnectionString;
            string query =
        @"Select webuser.UserId,
          1 as AppUser,
        	UserFirstName + ' ' + coalesce(UserLastName, '') as Username,
          UserLastName,
    	    UserEmail,
        	UserPhoneNumber,
        	DepartmentName,
          FunctionName,
        	coalesce(UserNotActive, '0') UserNotActive,
        	Profiles.ProfileName,
        	Company.CompanyName,
        	count(distinct linkedprojects.ProjectCode) as Projects,
        	count(distinct Training.TrainingId) as Training
        from WebUser	
          inner join Company on WebUser.UserCompanyCode = Company.CompanyCode
          inner join profiles on WebUser.UserProfileCode = Profiles.ProfileLevel
          Left Join Departments on Departments.DepartmentId = WebUser.UserDepartmentCode
          Left Join Functions on WebUser.UserFunctionCode = Functions.FunctionId
          left join LinkedProjects on WebUser.userId = LinkedProjects.UserCode
          Left Join Training on Training.UserId = WebUser.UserId
        Where webuser.Username = '" + id + "'  " +
              "Group by webUser.UserId, " +
                "UserFirstName, " +
                "UserlastName, " +
                "UserEmail, " +
                "UserPhoneNumber, " +
                "UserDepartmentCode, " +
                "UserFunctionCode, " +
                 "UserProfileCode, " +
                "UserNotActive, " +
                "CompanyName, " +
                "ProfileName, " +
                "DepartmentName, " +
                "FunctionName";

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
