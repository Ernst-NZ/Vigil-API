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
    
    public partial class AnnualSafetyPlan
    {
        public System.Guid PlanningUid { get; set; }
        public string CompanyUid { get; set; }
        public string DueDate { get; set; }
        public string Objective { get; set; }
        public string ActionRequired { get; set; }
        public string ResponsiblePersonUid { get; set; }
        public string DateCompleted { get; set; }
        public string Comments { get; set; }
        public string LastCheck { get; set; }
        public string CheckedBy { get; set; }
        public string AddedBy { get; set; }
        public string DateAdded { get; set; }
        public string LastUpdate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string DeletedBy { get; set; }
    }
}