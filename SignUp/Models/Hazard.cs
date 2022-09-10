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
    
    public partial class Hazard
    {
        public string HazardUID { get; set; }
        public string CompanyUID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string HazardDate { get; set; }
        public string Time { get; set; }
        public string Site { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string LocationOnSite { get; set; }
        public Nullable<int> RiskMatrix { get; set; }
        public Nullable<bool> ImmediateFix { get; set; }
        public string ImmediateDescription { get; set; }
        public Nullable<bool> Isolated { get; set; }
        public Nullable<bool> Reported { get; set; }
        public string SuggestedSolution { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorPosition { get; set; }
        public string SupervisorAction { get; set; }
        public Nullable<bool> InitiatorAdvised { get; set; }
        public string SupervisorSignOff { get; set; }
        public string InitiatorSignOff { get; set; }
        public string SupervisorSignOffDate { get; set; }
        public string InitiatorSignOffDate { get; set; }
        public string Status { get; set; }
        public string AddedBy { get; set; }
        public string Created_Date { get; set; }
        public string LastUpdate_By { get; set; }
        public string Updated_Date { get; set; }
        public string Deleted_By { get; set; }
        public string Deleted_Date { get; set; }
        public Nullable<bool> Deleted { get; set; }
    }
}
