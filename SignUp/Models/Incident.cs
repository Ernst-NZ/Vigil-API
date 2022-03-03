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
    
    public partial class Incident
    {
        public int IncidentId { get; set; }
        public string IncidentDate { get; set; }
        public string IncidentTime { get; set; }
        public string IncidentLocation { get; set; }
        public string PartyName { get; set; }
        public Nullable<bool> IncidentInjuries { get; set; }
        public string IncidentDescription { get; set; }
        public string ReportDate { get; set; }
        public Nullable<bool> EmailSent { get; set; }
        public string EmailTo { get; set; }
        public bool Status { get; set; }
        public string StatusDetail { get; set; }
        public Nullable<bool> Acknowledged { get; set; }
        public string AcknowledgedBy { get; set; }
        public string AcknowledgeDate { get; set; }
        public string IncidentType { get; set; }
        public string TreatmentType { get; set; }
        public string Treatment { get; set; }
        public string TreatmentBy { get; set; }
        public string UrgentAction { get; set; }
        public string IncidentSeverity { get; set; }
        public string ManagementComments { get; set; }
        public string ManagementCommentsBy { get; set; }
        public string ManagementCommentsDate { get; set; }
        public Nullable<bool> ReportableIncident { get; set; }
        public Nullable<bool> Reported { get; set; }
        public string ReportedBy { get; set; }
        public string ReportedDate { get; set; }
        public Nullable<bool> FeedbackToAffected { get; set; }
        public string FeedbackDate { get; set; }
        public string FeedbackBy { get; set; }
        public int CompanyId { get; set; }
        public string AddedBy { get; set; }
    }
}
