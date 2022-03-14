using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignUp.Models
{
    public class gmail
    {
        public string EmailType { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public string EmailBcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] Attachments { get; set; }

    //    public System.Net.Mail.AttachmentCollection Attachments { get; }
    //        public string[] array1 = new int[5]
    public string file { get; set; }
  }
}