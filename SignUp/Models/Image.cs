//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SignUp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Image
    {
        public int ImageId { get; set; }
        public string ImageCat { get; set; }
        public Nullable<int> ReferenceId { get; set; }
        public byte[] ImageUrl { get; set; }
        public string ImageNotes { get; set; }
        public string UserCode { get; set; }
        public string UploadDate { get; set; }
    }
}