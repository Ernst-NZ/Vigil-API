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
        public string RoleCode { get; set; }
    public string PhoneNumber { get; set; }
    public string firstName { get; set; }


    public string LoggedOn
        {
            get; set;
        }
    }
}