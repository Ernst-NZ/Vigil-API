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
    
    public partial class getCalendarWeekClient_Result
    {
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public string FirstName { get; set; }
        public bool allDay { get; set; }
        public bool booked { get; set; }
        public string clientId { get; set; }
        public bool completed { get; set; }
        public decimal price { get; set; }
        public bool cancelled { get; set; }
        public int id { get; set; }
        public string Notes { get; set; }
        public string AdminNotes { get; set; }
        public Nullable<decimal> Payments { get; set; }
    }
}
