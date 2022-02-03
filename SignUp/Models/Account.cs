using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignUp.Models
{
  public class AccountModel
  {
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserProfile { get; set; }
    public string UserDepartment { get; set; }
    public string UserFunction { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string PhoneNumber { get; set; }
    public string UserCompanyCode { get; set; }
    public string AddedBy { get; set; }


    public string LoggedOn
    {
      get; set;
    }
  }
}