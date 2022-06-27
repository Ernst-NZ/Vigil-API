
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/24/2022 15:50:28
-- Generated from EDMX file: E:\_Git\VIGIL-app\Vigil API\SignUp\Models\Vigil.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[HangFire].[FK_HangFire_JobParameter_Job]', 'F') IS NOT NULL
    ALTER TABLE [HangFire].[JobParameter] DROP CONSTRAINT [FK_HangFire_JobParameter_Job];
GO
IF OBJECT_ID(N'[HangFire].[FK_HangFire_State_Job]', 'F') IS NOT NULL
    ALTER TABLE [HangFire].[State] DROP CONSTRAINT [FK_HangFire_State_Job];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[CheckListLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CheckListLog];
GO
IF OBJECT_ID(N'[dbo].[ChecklistMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChecklistMaster];
GO
IF OBJECT_ID(N'[dbo].[CheckLogFull]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CheckLogFull];
GO
IF OBJECT_ID(N'[dbo].[Company]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Company];
GO
IF OBJECT_ID(N'[dbo].[CompanyDocs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyDocs];
GO
IF OBJECT_ID(N'[dbo].[CompanyStaff]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyStaff];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[FileData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileData];
GO
IF OBJECT_ID(N'[dbo].[Functions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Functions];
GO
IF OBJECT_ID(N'[dbo].[Images]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Images];
GO
IF OBJECT_ID(N'[dbo].[Incidents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Incidents];
GO
IF OBJECT_ID(N'[dbo].[LinkedModules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LinkedModules];
GO
IF OBJECT_ID(N'[dbo].[LinkedProjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LinkedProjects];
GO
IF OBJECT_ID(N'[dbo].[Meetings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Meetings];
GO
IF OBJECT_ID(N'[dbo].[Modules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Modules];
GO
IF OBJECT_ID(N'[dbo].[Profiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profiles];
GO
IF OBJECT_ID(N'[dbo].[ProjectDocs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectDocs];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO
IF OBJECT_ID(N'[dbo].[RawData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RawData];
GO
IF OBJECT_ID(N'[dbo].[SATSImages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SATSImages];
GO
IF OBJECT_ID(N'[dbo].[Stepback]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stepback];
GO
IF OBJECT_ID(N'[dbo].[Training]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Training];
GO
IF OBJECT_ID(N'[dbo].[VisitRegister]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VisitRegister];
GO
IF OBJECT_ID(N'[dbo].[WebUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WebUser];
GO
IF OBJECT_ID(N'[HangFire].[AggregatedCounter]', 'U') IS NOT NULL
    DROP TABLE [HangFire].[AggregatedCounter];
GO
IF OBJECT_ID(N'[HangFire].[Hash]', 'U') IS NOT NULL
    DROP TABLE [HangFire].[Hash];
GO
IF OBJECT_ID(N'[HangFire].[Job]', 'U') IS NOT NULL
    DROP TABLE [HangFire].[Job];
GO
IF OBJECT_ID(N'[HangFire].[JobParameter]', 'U') IS NOT NULL
    DROP TABLE [HangFire].[JobParameter];
GO
IF OBJECT_ID(N'[HangFire].[JobQueue]', 'U') IS NOT NULL
    DROP TABLE [HangFire].[JobQueue];
GO
IF OBJECT_ID(N'[HangFire].[List]', 'U') IS NOT NULL
    DROP TABLE [HangFire].[List];
GO
IF OBJECT_ID(N'[HangFire].[Schema]', 'U') IS NOT NULL
    DROP TABLE [HangFire].[Schema];
GO
IF OBJECT_ID(N'[HangFire].[Server]', 'U') IS NOT NULL
    DROP TABLE [HangFire].[Server];
GO
IF OBJECT_ID(N'[HangFire].[Set]', 'U') IS NOT NULL
    DROP TABLE [HangFire].[Set];
GO
IF OBJECT_ID(N'[HangFire].[State]', 'U') IS NOT NULL
    DROP TABLE [HangFire].[State];
GO
IF OBJECT_ID(N'[DBModelVigilStoreContainer].[Counter]', 'U') IS NOT NULL
    DROP TABLE [DBModelVigilStoreContainer].[Counter];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CompanyDocs'
CREATE TABLE [dbo].[CompanyDocs] (
    [CompanyDocId] int IDENTITY(1,1) NOT NULL,
    [CompanyCode] nvarchar(50)  NOT NULL,
    [CompanySubFolder] nvarchar(250)  NOT NULL,
    [CompanyDocName] nvarchar(250)  NOT NULL,
    [CompanyDocType] nvarchar(50)  NOT NULL,
    [CompanyDocDescription] nvarchar(500)  NULL,
    [AddedBy] nvarchar(250)  NOT NULL,
    [Date] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ProjectDocs'
CREATE TABLE [dbo].[ProjectDocs] (
    [ProjectDocId] int IDENTITY(1,1) NOT NULL,
    [ProjectCode] nvarchar(50)  NOT NULL,
    [ProjectSubFolder] nvarchar(250)  NOT NULL,
    [ProjectDocName] nvarchar(250)  NOT NULL,
    [ProjectDocType] nvarchar(50)  NOT NULL,
    [ProjectDocDescription] nvarchar(500)  NULL,
    [AddedBy] nvarchar(250)  NOT NULL,
    [Date] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Hashes'
CREATE TABLE [dbo].[Hashes] (
    [Key] nvarchar(100)  NOT NULL,
    [Field] nvarchar(100)  NOT NULL,
    [Value] nvarchar(max)  NULL,
    [ExpireAt] datetime  NULL
);
GO

-- Creating table 'Jobs'
CREATE TABLE [dbo].[Jobs] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [StateId] bigint  NULL,
    [StateName] nvarchar(20)  NULL,
    [InvocationData] nvarchar(max)  NOT NULL,
    [Arguments] nvarchar(max)  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ExpireAt] datetime  NULL
);
GO

-- Creating table 'JobParameters'
CREATE TABLE [dbo].[JobParameters] (
    [JobId] bigint  NOT NULL,
    [Name] nvarchar(40)  NOT NULL,
    [Value] nvarchar(max)  NULL
);
GO

-- Creating table 'JobQueues'
CREATE TABLE [dbo].[JobQueues] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [JobId] bigint  NOT NULL,
    [Queue] nvarchar(50)  NOT NULL,
    [FetchedAt] datetime  NULL
);
GO

-- Creating table 'Lists'
CREATE TABLE [dbo].[Lists] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Key] nvarchar(100)  NOT NULL,
    [Value] nvarchar(max)  NULL,
    [ExpireAt] datetime  NULL
);
GO

-- Creating table 'Schemata'
CREATE TABLE [dbo].[Schemata] (
    [Version] int  NOT NULL
);
GO

-- Creating table 'Servers'
CREATE TABLE [dbo].[Servers] (
    [Id] nvarchar(200)  NOT NULL,
    [Data] nvarchar(max)  NULL,
    [LastHeartbeat] datetime  NOT NULL
);
GO

-- Creating table 'Sets'
CREATE TABLE [dbo].[Sets] (
    [Key] nvarchar(100)  NOT NULL,
    [Score] float  NOT NULL,
    [Value] nvarchar(256)  NOT NULL,
    [ExpireAt] datetime  NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [JobId] bigint  NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [Reason] nvarchar(100)  NULL,
    [CreatedAt] datetime  NOT NULL,
    [Data] nvarchar(max)  NULL
);
GO

-- Creating table 'Counters'
CREATE TABLE [dbo].[Counters] (
    [Key] nvarchar(100)  NOT NULL,
    [Value] int  NOT NULL,
    [ExpireAt] datetime  NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [FirstName] nvarchar(150)  NULL,
    [LastName] nvarchar(150)  NULL
);
GO

-- Creating table 'WebUsers'
CREATE TABLE [dbo].[WebUsers] (
    [UserId] nvarchar(128)  NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [UserCompanyCode] nvarchar(50)  NULL,
    [UserFirstName] nvarchar(150)  NULL,
    [UserLastName] nvarchar(150)  NULL,
    [UserEmail] nvarchar(150)  NULL,
    [UserPhoneNumber] nvarchar(50)  NULL,
    [UserDepartmentCode] nvarchar(50)  NULL,
    [UserFunctionCode] nvarchar(50)  NULL,
    [UserProfileCode] nvarchar(50)  NULL,
    [UserAddedBy] varchar(25)  NULL,
    [UserNotActive] bit  NULL
);
GO

-- Creating table 'AggregatedCounters'
CREATE TABLE [dbo].[AggregatedCounters] (
    [Key] nvarchar(100)  NOT NULL,
    [Value] bigint  NOT NULL,
    [ExpireAt] datetime  NULL
);
GO

-- Creating table 'Profiles'
CREATE TABLE [dbo].[Profiles] (
    [ProfileId] int IDENTITY(1,1) NOT NULL,
    [ProfileName] nvarchar(50)  NOT NULL,
    [ProfileLevel] int  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [DepartmentId] int IDENTITY(1,1) NOT NULL,
    [DepartmentName] nvarchar(50)  NOT NULL,
    [CompanyCode] int  NOT NULL
);
GO

-- Creating table 'Functions'
CREATE TABLE [dbo].[Functions] (
    [FunctionId] int IDENTITY(1,1) NOT NULL,
    [FunctionName] nvarchar(50)  NOT NULL,
    [CompanyCode] int  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ProjectId] int IDENTITY(1,1) NOT NULL,
    [ProjectCode] nvarchar(50)  NOT NULL,
    [CompanyId] nvarchar(50)  NOT NULL,
    [ProjectName] nvarchar(250)  NOT NULL,
    [ContactFirstName] nvarchar(250)  NULL,
    [ContactLastName] nvarchar(250)  NULL,
    [ContactEmail] nvarchar(250)  NULL,
    [ContactPhone] nvarchar(250)  NULL,
    [ProjectLocation] nvarchar(500)  NULL,
    [ProjectStatus] nvarchar(250)  NULL,
    [AddedBy] nvarchar(250)  NULL,
    [Date] nvarchar(50)  NULL
);
GO

-- Creating table 'Trainings'
CREATE TABLE [dbo].[Trainings] (
    [TrainingId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(50)  NOT NULL,
    [CourseName] nvarchar(250)  NOT NULL,
    [CourseDate] nvarchar(50)  NULL,
    [ExpiryDate] nvarchar(50)  NULL,
    [CompetencyLevel] nvarchar(50)  NULL,
    [ProviderName] varchar(100)  NULL,
    [LastUpdateBy] varchar(100)  NULL,
    [UpdateDate] varchar(100)  NULL,
    [StudentNumber] varchar(50)  NULL
);
GO

-- Creating table 'LinkedProjects'
CREATE TABLE [dbo].[LinkedProjects] (
    [LinkedId] int IDENTITY(1,1) NOT NULL,
    [ProjectCode] nvarchar(50)  NOT NULL,
    [UserCode] nvarchar(250)  NOT NULL,
    [LinkedBy] nvarchar(250)  NULL,
    [Date] nvarchar(50)  NULL
);
GO

-- Creating table 'RawDatas'
CREATE TABLE [dbo].[RawDatas] (
    [Id] int  NOT NULL,
    [RawFile] varbinary(max)  NOT NULL
);
GO

-- Creating table 'FileDatas'
CREATE TABLE [dbo].[FileDatas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ParentId] int  NULL,
    [ParentName] nvarchar(250)  NULL,
    [SubFolder] nvarchar(250)  NULL,
    [FileDescription] nvarchar(500)  NULL,
    [FileTopic] nvarchar(250)  NULL,
    [FileName] nvarchar(500)  NOT NULL,
    [FileExtension] nvarchar(500)  NOT NULL,
    [FileSize] nvarchar(50)  NULL,
    [AddedBy] nvarchar(50)  NULL,
    [Date] nvarchar(250)  NULL
);
GO

-- Creating table 'CheckLogFulls'
CREATE TABLE [dbo].[CheckLogFulls] (
    [LogId] int IDENTITY(1,1) NOT NULL,
    [LineStatus] bit  NULL,
    [LineStatusOther] nvarchar(50)  NULL,
    [LineComments] nvarchar(max)  NULL,
    [SopId] bit  NULL,
    [AHeading] nvarchar(125)  NULL,
    [A1Text] nvarchar(125)  NULL,
    [A1Result] nvarchar(125)  NULL,
    [A1Comment] nvarchar(max)  NULL,
    [A2Text] nvarchar(125)  NULL,
    [A2Result] nvarchar(125)  NULL,
    [A2Comment] nvarchar(max)  NULL,
    [A3Text] nvarchar(150)  NULL,
    [A3Result] nvarchar(150)  NULL,
    [A3Comment] nvarchar(max)  NULL,
    [A4Text] nvarchar(150)  NULL,
    [A4Result] nvarchar(150)  NULL,
    [A4Comment] nvarchar(max)  NULL,
    [A5Text] nvarchar(125)  NULL,
    [A5Result] nvarchar(125)  NULL,
    [A5Comment] nvarchar(max)  NULL,
    [A6Text] nvarchar(125)  NULL,
    [A6Result] nvarchar(125)  NULL,
    [A6Comment] nvarchar(max)  NULL,
    [A7Text] nvarchar(125)  NULL,
    [A7Result] nvarchar(125)  NULL,
    [A7Comment] nvarchar(max)  NULL,
    [BHeading] nvarchar(125)  NULL,
    [B1Text] nvarchar(125)  NULL,
    [B1Result] nvarchar(125)  NULL,
    [B1Comment] nvarchar(max)  NULL,
    [B2Text] nvarchar(125)  NULL,
    [B2Result] nvarchar(125)  NULL,
    [B2Comment] nvarchar(max)  NULL,
    [B3Text] nvarchar(125)  NULL,
    [B3Result] nvarchar(125)  NULL,
    [B3Comment] nvarchar(max)  NULL,
    [B4Text] nvarchar(125)  NULL,
    [B4Result] nvarchar(125)  NULL,
    [B4Comment] nvarchar(max)  NULL,
    [B5Text] nvarchar(125)  NULL,
    [B5Result] nvarchar(125)  NULL,
    [B5Comment] nvarchar(max)  NULL,
    [B6Text] nvarchar(125)  NULL,
    [B6Result] nvarchar(125)  NULL,
    [B6Comment] nvarchar(max)  NULL,
    [B7Text] nvarchar(125)  NULL,
    [B7Result] nvarchar(125)  NULL,
    [B7Comment] nvarchar(max)  NULL,
    [B8Text] nvarchar(125)  NULL,
    [B8Result] nvarchar(125)  NULL,
    [B8Comment] nvarchar(max)  NULL,
    [CHeading] nvarchar(125)  NULL,
    [C1Text] nvarchar(125)  NULL,
    [C1Result] nvarchar(125)  NULL,
    [C1Comment] nvarchar(max)  NULL,
    [H_Spillage] bit  NULL,
    [H_Slips] bit  NULL,
    [H_ManualHandling] bit  NULL,
    [H_ConfinedSpace] bit  NULL,
    [H_Lifting] bit  NULL,
    [H_AccessWay] bit  NULL,
    [H_Vehicles] bit  NULL,
    [H_Heat] bit  NULL,
    [H_Lightning] bit  NULL,
    [H_WorkingAtHeights] bit  NULL,
    [H_Radiation] bit  NULL,
    [H_Vibration] bit  NULL,
    [H_Electrical] bit  NULL,
    [H_HazardousSubstances] bit  NULL,
    [H_Tools] bit  NULL,
    [H_FallingObjects] bit  NULL,
    [H_Dust] bit  NULL,
    [H_Pressure] bit  NULL,
    [H_Noise] bit  NULL,
    [H_GroundConditions] bit  NULL,
    [H_StoredEnergy] bit  NULL,
    [H_PinchPoint] bit  NULL,
    [H_OtherHazards] nvarchar(125)  NULL,
    [CompletionDate] nvarchar(125)  NULL,
    [GeneralComments] nvarchar(125)  NULL,
    [SupervisorCompany] nvarchar(125)  NULL,
    [SupervisorName] nvarchar(125)  NULL,
    [SupervisorSignature] nvarchar(125)  NULL,
    [EmployeeCompany] nvarchar(125)  NULL,
    [EmployeeName] nvarchar(125)  NULL,
    [EmployeeSignature] nvarchar(125)  NULL,
    [AddedBy] nvarchar(125)  NULL,
    [CompanyCode] nvarchar(125)  NULL,
    [AddedOn] nvarchar(125)  NULL,
    [Status] bit  NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [CompanyId] int IDENTITY(1,1) NOT NULL,
    [IsParentCompany] bit  NULL,
    [ParentCode] int  NOT NULL,
    [CompanyCode] nvarchar(50)  NOT NULL,
    [CompanyName] nvarchar(150)  NOT NULL,
    [CompanyAddress1] nvarchar(50)  NULL,
    [CompanyAddress2] nvarchar(50)  NULL,
    [CompanyTown] nvarchar(50)  NULL,
    [CompanyPostcode] decimal(18,0)  NULL,
    [CompanyContactPerson] varchar(100)  NULL,
    [CompanyContactEmail] varchar(100)  NULL,
    [CompanyContactPhone] varchar(100)  NULL,
    [CompanyContactPosition] varchar(50)  NULL,
    [CompanyHSManager] nvarchar(150)  NULL,
    [CompanyHSEmail] nvarchar(150)  NULL,
    [ReportPerson1] nvarchar(150)  NULL,
    [ReportEmail1] nvarchar(150)  NULL,
    [ReportPerson2] nvarchar(150)  NULL,
    [ReportEmail2] nvarchar(150)  NULL
);
GO

-- Creating table 'Incidents'
CREATE TABLE [dbo].[Incidents] (
    [IncidentId] int IDENTITY(1,1) NOT NULL,
    [IncidentDate] nvarchar(50)  NOT NULL,
    [IncidentTime] nvarchar(50)  NULL,
    [IncidentLocation] nvarchar(max)  NOT NULL,
    [PartyName] nvarchar(max)  NULL,
    [IncidentInjuries] bit  NULL,
    [IncidentDescription] nvarchar(max)  NULL,
    [ReportDate] nvarchar(250)  NULL,
    [EmailSent] bit  NULL,
    [EmailTo] nvarchar(250)  NULL,
    [Status] bit  NOT NULL,
    [StatusDetail] nvarchar(250)  NULL,
    [Acknowledged] bit  NULL,
    [AcknowledgedBy] nvarchar(50)  NULL,
    [AcknowledgeDate] nvarchar(50)  NULL,
    [IncidentType] nvarchar(50)  NULL,
    [TreatmentType] nvarchar(50)  NULL,
    [Treatment] nvarchar(max)  NULL,
    [TreatmentBy] nvarchar(max)  NULL,
    [UrgentAction] nvarchar(max)  NULL,
    [IncidentSeverity] nvarchar(125)  NULL,
    [ManagementComments] nvarchar(max)  NULL,
    [ManagementCommentsBy] nvarchar(125)  NULL,
    [ManagementCommentsDate] nvarchar(125)  NULL,
    [ReportableIncident] bit  NULL,
    [Reported] bit  NULL,
    [ReportedBy] varchar(250)  NULL,
    [ReportedDate] varchar(250)  NULL,
    [FeedbackToAffected] bit  NULL,
    [FeedbackDate] varchar(250)  NULL,
    [FeedbackBy] varchar(250)  NULL,
    [CompanyId] int  NOT NULL,
    [AddedBy] nvarchar(125)  NULL
);
GO

-- Creating table 'CompanyStaffs'
CREATE TABLE [dbo].[CompanyStaffs] (
    [StaffId] int IDENTITY(1,1) NOT NULL,
    [StaffName] nvarchar(50)  NOT NULL,
    [StaffSurname] nvarchar(50)  NOT NULL,
    [CompanyCode] nvarchar(50)  NOT NULL,
    [UserDepartmentCode] nvarchar(50)  NULL,
    [Inactive] bit  NOT NULL,
    [AddedBy] nvarchar(50)  NOT NULL,
    [LastUpdate] nvarchar(50)  NOT NULL,
    [AppUserId] nvarchar(128)  NULL
);
GO

-- Creating table 'Images'
CREATE TABLE [dbo].[Images] (
    [ImageId] int IDENTITY(1,1) NOT NULL,
    [ImageCat] nvarchar(150)  NULL,
    [ReferenceId] int  NULL,
    [ImageUrl] varbinary(max)  NULL,
    [ImageNotes] nvarchar(max)  NULL,
    [AddedBy] nvarchar(50)  NULL,
    [LastUpdate] nvarchar(50)  NULL
);
GO

-- Creating table 'Meetings'
CREATE TABLE [dbo].[Meetings] (
    [MeetingId] int IDENTITY(1,1) NOT NULL,
    [MeetingCompanyCode] nvarchar(50)  NULL,
    [MeetingType] nvarchar(50)  NULL,
    [MeetingDate] nvarchar(50)  NULL,
    [MeetingHeldBy] nvarchar(128)  NULL,
    [MeetingNotes] nvarchar(max)  NULL,
    [MeetingActionSteps] nvarchar(max)  NULL,
    [AddedBy] nvarchar(50)  NULL,
    [LastUpdate] nvarchar(50)  NULL
);
GO

-- Creating table 'Modules'
CREATE TABLE [dbo].[Modules] (
    [ModuleCode] nvarchar(50)  NOT NULL,
    [ModuleDescription] nvarchar(256)  NULL
);
GO

-- Creating table 'LinkedModules'
CREATE TABLE [dbo].[LinkedModules] (
    [LinkedId] int IDENTITY(1,1) NOT NULL,
    [ModuleCode] nvarchar(50)  NOT NULL,
    [CompanyCode] nvarchar(250)  NOT NULL,
    [LinkedBy] nvarchar(250)  NULL,
    [Date] nvarchar(50)  NULL
);
GO

-- Creating table 'Stepbacks'
CREATE TABLE [dbo].[Stepbacks] (
    [LogId] int IDENTITY(1,1) NOT NULL,
    [Reference] nvarchar(250)  NULL,
    [AHeading] nvarchar(125)  NULL,
    [A1Text] nvarchar(125)  NULL,
    [A1Result] nvarchar(125)  NULL,
    [A1Comment] nvarchar(max)  NULL,
    [A2Text] nvarchar(125)  NULL,
    [A2Result] nvarchar(125)  NULL,
    [A2Comment] nvarchar(max)  NULL,
    [A3Text] nvarchar(150)  NULL,
    [A3Result] nvarchar(150)  NULL,
    [A3Comment] nvarchar(max)  NULL,
    [A4Text] nvarchar(150)  NULL,
    [A4Result] nvarchar(150)  NULL,
    [A4Comment] nvarchar(max)  NULL,
    [A5Text] nvarchar(125)  NULL,
    [A5Result] nvarchar(125)  NULL,
    [A5Comment] nvarchar(max)  NULL,
    [A6Text] nvarchar(125)  NULL,
    [A6Result] nvarchar(125)  NULL,
    [A6Comment] nvarchar(max)  NULL,
    [A7Text] nvarchar(125)  NULL,
    [A7Result] nvarchar(125)  NULL,
    [A7Comment] nvarchar(max)  NULL,
    [BHeading] nvarchar(125)  NULL,
    [B1Text] nvarchar(125)  NULL,
    [B1Result] nvarchar(125)  NULL,
    [B1Comment] nvarchar(max)  NULL,
    [B2Text] nvarchar(125)  NULL,
    [B2Result] nvarchar(125)  NULL,
    [B2Comment] nvarchar(max)  NULL,
    [B3Text] nvarchar(125)  NULL,
    [B3Result] nvarchar(125)  NULL,
    [B3Comment] nvarchar(max)  NULL,
    [B4Text] nvarchar(125)  NULL,
    [B4Result] nvarchar(125)  NULL,
    [B4Comment] nvarchar(max)  NULL,
    [B5Text] nvarchar(125)  NULL,
    [B5Result] nvarchar(125)  NULL,
    [B5Comment] nvarchar(max)  NULL,
    [B6Text] nvarchar(125)  NULL,
    [B6Result] nvarchar(125)  NULL,
    [B6Comment] nvarchar(max)  NULL,
    [B7Text] nvarchar(125)  NULL,
    [B7Result] nvarchar(125)  NULL,
    [B7Comment] nvarchar(max)  NULL,
    [B8Text] nvarchar(125)  NULL,
    [B8Result] nvarchar(125)  NULL,
    [B8Comment] nvarchar(max)  NULL,
    [CHeading] nvarchar(125)  NULL,
    [C1Text] nvarchar(125)  NULL,
    [C1Result] nvarchar(125)  NULL,
    [C1Comment] nvarchar(max)  NULL,
    [H_Spillage] bit  NULL,
    [H_Slips] bit  NULL,
    [H_ManualHandling] bit  NULL,
    [H_ConfinedSpace] bit  NULL,
    [H_Lifting] bit  NULL,
    [H_AccessWay] bit  NULL,
    [H_Vehicles] bit  NULL,
    [H_Heat] bit  NULL,
    [H_Lightning] bit  NULL,
    [H_WorkingAtHeights] bit  NULL,
    [H_Radiation] bit  NULL,
    [H_Vibration] bit  NULL,
    [H_Electrical] bit  NULL,
    [H_HazardousSubstances] bit  NULL,
    [H_Tools] bit  NULL,
    [H_FallingObjects] bit  NULL,
    [H_Dust] bit  NULL,
    [H_Pressure] bit  NULL,
    [H_Noise] bit  NULL,
    [H_GroundConditions] bit  NULL,
    [H_StoredEnergy] bit  NULL,
    [H_PinchPoint] bit  NULL,
    [H_OtherHazards] nvarchar(125)  NULL,
    [CompletionDate] nvarchar(125)  NULL,
    [GeneralComments] nvarchar(125)  NULL,
    [SupervisorCompany] nvarchar(125)  NULL,
    [SupervisorName] nvarchar(125)  NULL,
    [SupervisorSignature] nvarchar(max)  NULL,
    [EmployeeCompany] nvarchar(125)  NULL,
    [EmployeeName] nvarchar(125)  NULL,
    [EmployeeSignature] nvarchar(max)  NULL,
    [AddedBy] nvarchar(125)  NULL,
    [CompanyCode] nvarchar(125)  NULL,
    [AddedOn] nvarchar(125)  NULL,
    [Status] bit  NULL
);
GO

-- Creating table 'ChecklistMasters'
CREATE TABLE [dbo].[ChecklistMasters] (
    [CheckListId] int IDENTITY(1,1) NOT NULL,
    [CompanyCode] nvarchar(50)  NULL,
    [CheckListUID] nvarchar(128)  NOT NULL,
    [CheckListName] nvarchar(128)  NOT NULL,
    [CheckListCategory] nvarchar(50)  NULL,
    [CheckListItem] nvarchar(50)  NULL,
    [CheckListDescription] nvarchar(128)  NULL,
    [AddedBy] nvarchar(50)  NULL,
    [LastUpdate] nvarchar(50)  NULL
);
GO

-- Creating table 'SATSImages'
CREATE TABLE [dbo].[SATSImages] (
    [ImageId] int IDENTITY(1,1) NOT NULL,
    [ImageCat] nvarchar(150)  NULL,
    [ReferenceId] int  NULL,
    [ImageUrl] varbinary(max)  NULL,
    [ImageNotes] nvarchar(max)  NULL,
    [UserCode] nvarchar(50)  NULL,
    [UploadDate] nvarchar(50)  NULL,
    [ReferenceUID] nvarchar(128)  NULL
);
GO

-- Creating table 'CheckListLogs'
CREATE TABLE [dbo].[CheckListLogs] (
    [CheckLogId] int IDENTITY(1,1) NOT NULL,
    [CheckListDate] nvarchar(128)  NULL,
    [CheckListName] nvarchar(128)  NULL,
    [CheckListUID] nvarchar(128)  NULL,
    [CheckListReference] nvarchar(128)  NULL,
    [CheckListCategory] nvarchar(50)  NULL,
    [CheckListItem] nvarchar(50)  NULL,
    [CheckListDescription] nvarchar(128)  NULL,
    [CheckListResult] nvarchar(50)  NULL,
    [CheckListLineNotes] nvarchar(max)  NULL,
    [CheckListComments] nvarchar(max)  NULL,
    [CheckListEmployeeName] nvarchar(128)  NULL,
    [CheckListSignature] nvarchar(max)  NULL,
    [CheckListStatus] bit  NULL,
    [CheckListCheckStatus] bit  NULL,
    [CheckListManagementComments] nvarchar(max)  NULL,
    [CheckListCompanyCode] nvarchar(50)  NULL,
    [AddedBy] nvarchar(50)  NULL,
    [LastUpdate] nvarchar(50)  NULL
);
GO

-- Creating table 'VisitRegisters'
CREATE TABLE [dbo].[VisitRegisters] (
    [VisitId] int IDENTITY(1,1) NOT NULL,
    [CompanyCode] nvarchar(50)  NOT NULL,
    [UserCode] nvarchar(50)  NOT NULL,
    [DateIn] nvarchar(50)  NOT NULL,
    [DateOut] nvarchar(50)  NULL,
    [Name] nvarchar(150)  NOT NULL,
    [Signature] nvarchar(max)  NULL,
    [Company] nvarchar(250)  NULL,
    [Purpose] nvarchar(500)  NULL,
    [Comments] nvarchar(max)  NULL,
    [WhoVisit] nvarchar(150)  NULL,
    [TimeIn] nvarchar(50)  NULL,
    [TimeOut] nvarchar(50)  NULL,
    [SignatureOut] nvarchar(max)  NULL,
    [DeviceId] nvarchar(250)  NULL,
    [IsStaff] nvarchar(max)  NULL,
    [IsLogin] bit  NULL,
    [Longitude] nvarchar(250)  NULL,
    [Latitude] nvarchar(250)  NULL,
    [DeviceType] nvarchar(250)  NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CompanyDocId] in table 'CompanyDocs'
ALTER TABLE [dbo].[CompanyDocs]
ADD CONSTRAINT [PK_CompanyDocs]
    PRIMARY KEY CLUSTERED ([CompanyDocId] ASC);
GO

-- Creating primary key on [ProjectDocId] in table 'ProjectDocs'
ALTER TABLE [dbo].[ProjectDocs]
ADD CONSTRAINT [PK_ProjectDocs]
    PRIMARY KEY CLUSTERED ([ProjectDocId] ASC);
GO

-- Creating primary key on [Key], [Field] in table 'Hashes'
ALTER TABLE [dbo].[Hashes]
ADD CONSTRAINT [PK_Hashes]
    PRIMARY KEY CLUSTERED ([Key], [Field] ASC);
GO

-- Creating primary key on [Id] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [PK_Jobs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [JobId], [Name] in table 'JobParameters'
ALTER TABLE [dbo].[JobParameters]
ADD CONSTRAINT [PK_JobParameters]
    PRIMARY KEY CLUSTERED ([JobId], [Name] ASC);
GO

-- Creating primary key on [Id], [Queue] in table 'JobQueues'
ALTER TABLE [dbo].[JobQueues]
ADD CONSTRAINT [PK_JobQueues]
    PRIMARY KEY CLUSTERED ([Id], [Queue] ASC);
GO

-- Creating primary key on [Id], [Key] in table 'Lists'
ALTER TABLE [dbo].[Lists]
ADD CONSTRAINT [PK_Lists]
    PRIMARY KEY CLUSTERED ([Id], [Key] ASC);
GO

-- Creating primary key on [Version] in table 'Schemata'
ALTER TABLE [dbo].[Schemata]
ADD CONSTRAINT [PK_Schemata]
    PRIMARY KEY CLUSTERED ([Version] ASC);
GO

-- Creating primary key on [Id] in table 'Servers'
ALTER TABLE [dbo].[Servers]
ADD CONSTRAINT [PK_Servers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Key], [Value] in table 'Sets'
ALTER TABLE [dbo].[Sets]
ADD CONSTRAINT [PK_Sets]
    PRIMARY KEY CLUSTERED ([Key], [Value] ASC);
GO

-- Creating primary key on [Id], [JobId] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([Id], [JobId] ASC);
GO

-- Creating primary key on [Key], [Value] in table 'Counters'
ALTER TABLE [dbo].[Counters]
ADD CONSTRAINT [PK_Counters]
    PRIMARY KEY CLUSTERED ([Key], [Value] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'WebUsers'
ALTER TABLE [dbo].[WebUsers]
ADD CONSTRAINT [PK_WebUsers]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [Key] in table 'AggregatedCounters'
ALTER TABLE [dbo].[AggregatedCounters]
ADD CONSTRAINT [PK_AggregatedCounters]
    PRIMARY KEY CLUSTERED ([Key] ASC);
GO

-- Creating primary key on [ProfileId] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [PK_Profiles]
    PRIMARY KEY CLUSTERED ([ProfileId] ASC);
GO

-- Creating primary key on [DepartmentId] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([DepartmentId] ASC);
GO

-- Creating primary key on [FunctionId] in table 'Functions'
ALTER TABLE [dbo].[Functions]
ADD CONSTRAINT [PK_Functions]
    PRIMARY KEY CLUSTERED ([FunctionId] ASC);
GO

-- Creating primary key on [ProjectId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ProjectId] ASC);
GO

-- Creating primary key on [TrainingId] in table 'Trainings'
ALTER TABLE [dbo].[Trainings]
ADD CONSTRAINT [PK_Trainings]
    PRIMARY KEY CLUSTERED ([TrainingId] ASC);
GO

-- Creating primary key on [LinkedId] in table 'LinkedProjects'
ALTER TABLE [dbo].[LinkedProjects]
ADD CONSTRAINT [PK_LinkedProjects]
    PRIMARY KEY CLUSTERED ([LinkedId] ASC);
GO

-- Creating primary key on [Id] in table 'RawDatas'
ALTER TABLE [dbo].[RawDatas]
ADD CONSTRAINT [PK_RawDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileDatas'
ALTER TABLE [dbo].[FileDatas]
ADD CONSTRAINT [PK_FileDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LogId] in table 'CheckLogFulls'
ALTER TABLE [dbo].[CheckLogFulls]
ADD CONSTRAINT [PK_CheckLogFulls]
    PRIMARY KEY CLUSTERED ([LogId] ASC);
GO

-- Creating primary key on [CompanyId] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([CompanyId] ASC);
GO

-- Creating primary key on [IncidentId] in table 'Incidents'
ALTER TABLE [dbo].[Incidents]
ADD CONSTRAINT [PK_Incidents]
    PRIMARY KEY CLUSTERED ([IncidentId] ASC);
GO

-- Creating primary key on [StaffId] in table 'CompanyStaffs'
ALTER TABLE [dbo].[CompanyStaffs]
ADD CONSTRAINT [PK_CompanyStaffs]
    PRIMARY KEY CLUSTERED ([StaffId] ASC);
GO

-- Creating primary key on [ImageId] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [PK_Images]
    PRIMARY KEY CLUSTERED ([ImageId] ASC);
GO

-- Creating primary key on [MeetingId] in table 'Meetings'
ALTER TABLE [dbo].[Meetings]
ADD CONSTRAINT [PK_Meetings]
    PRIMARY KEY CLUSTERED ([MeetingId] ASC);
GO

-- Creating primary key on [ModuleCode] in table 'Modules'
ALTER TABLE [dbo].[Modules]
ADD CONSTRAINT [PK_Modules]
    PRIMARY KEY CLUSTERED ([ModuleCode] ASC);
GO

-- Creating primary key on [LinkedId] in table 'LinkedModules'
ALTER TABLE [dbo].[LinkedModules]
ADD CONSTRAINT [PK_LinkedModules]
    PRIMARY KEY CLUSTERED ([LinkedId] ASC);
GO

-- Creating primary key on [LogId] in table 'Stepbacks'
ALTER TABLE [dbo].[Stepbacks]
ADD CONSTRAINT [PK_Stepbacks]
    PRIMARY KEY CLUSTERED ([LogId] ASC);
GO

-- Creating primary key on [CheckListId] in table 'ChecklistMasters'
ALTER TABLE [dbo].[ChecklistMasters]
ADD CONSTRAINT [PK_ChecklistMasters]
    PRIMARY KEY CLUSTERED ([CheckListId] ASC);
GO

-- Creating primary key on [ImageId] in table 'SATSImages'
ALTER TABLE [dbo].[SATSImages]
ADD CONSTRAINT [PK_SATSImages]
    PRIMARY KEY CLUSTERED ([ImageId] ASC);
GO

-- Creating primary key on [CheckLogId] in table 'CheckListLogs'
ALTER TABLE [dbo].[CheckListLogs]
ADD CONSTRAINT [PK_CheckListLogs]
    PRIMARY KEY CLUSTERED ([CheckLogId] ASC);
GO

-- Creating primary key on [VisitId] in table 'VisitRegisters'
ALTER TABLE [dbo].[VisitRegisters]
ADD CONSTRAINT [PK_VisitRegisters]
    PRIMARY KEY CLUSTERED ([VisitId] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [JobId] in table 'JobParameters'
ALTER TABLE [dbo].[JobParameters]
ADD CONSTRAINT [FK_HangFire_JobParameter_Job]
    FOREIGN KEY ([JobId])
    REFERENCES [dbo].[Jobs]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [JobId] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [FK_HangFire_State_Job]
    FOREIGN KEY ([JobId])
    REFERENCES [dbo].[Jobs]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HangFire_State_Job'
CREATE INDEX [IX_FK_HangFire_State_Job]
ON [dbo].[States]
    ([JobId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRole]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUser]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUser'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUser]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------