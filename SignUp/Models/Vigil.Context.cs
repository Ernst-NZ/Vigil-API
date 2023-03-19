﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CompanyDoc> CompanyDocs { get; set; }
        public virtual DbSet<Hash> Hashes { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobParameter> JobParameters { get; set; }
        public virtual DbSet<JobQueue> JobQueues { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<Schema> Schemata { get; set; }
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Counter> Counters { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<WebUser> WebUsers { get; set; }
        public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<RawData> RawDatas { get; set; }
        public virtual DbSet<CompanyStaff> CompanyStaffs { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<LinkedModule> LinkedModules { get; set; }
        public virtual DbSet<Stepback> Stepbacks { get; set; }
        public virtual DbSet<SATSImage> SATSImages { get; set; }
        public virtual DbSet<VisitRegister> VisitRegisters { get; set; }
        public virtual DbSet<MyLog> MyLogs { get; set; }
        public virtual DbSet<ChecklistMaster> ChecklistMasters { get; set; }
        public virtual DbSet<CheckLogFull> CheckLogFulls { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Training> Trainings { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Chemical> Chemicals { get; set; }
        public virtual DbSet<CheckListLog> CheckListLogs { get; set; }
        public virtual DbSet<FileData> FileDatas { get; set; }
        public virtual DbSet<RiskRegister> RiskRegisters { get; set; }
        public virtual DbSet<AnnualSafetyPlan> AnnualSafetyPlans { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<AccessCompany> AccessCompanies { get; set; }
        public virtual DbSet<AccessUser> AccessUsers { get; set; }
        public virtual DbSet<AppVersion> AppVersions { get; set; }
        public virtual DbSet<DailyBriefing> DailyBriefings { get; set; }
        public virtual DbSet<Hazard> Hazards { get; set; }
        public virtual DbSet<ProjectDoc> ProjectDocs { get; set; }
        public virtual DbSet<LinkedProject> LinkedProjects { get; set; }
        public virtual DbSet<JSAReviewed> JSARevieweds { get; set; }
        public virtual DbSet<JSAData> JSADatas { get; set; }
        public virtual DbSet<VisitLog> VisitLogs { get; set; }
    }
}
