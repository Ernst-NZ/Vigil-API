using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignUp.Models
{
    public class UploadData
    {
        public int Id { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public string SubFolder { get; set; }
        public string FileDescription { get; set; }
        public string FileTopic { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileSize { get; set; }
        public string AddedBy { get; set; }
        public string Date { get; set; }
    }
}