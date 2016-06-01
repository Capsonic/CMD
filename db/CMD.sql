-- DATABASE

ALTER DATABASE [CMD] SET COMPATIBILITY_LEVEL = 100;
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CMD].[dbo].[sp_fulltext_database] @action = 'enable'
end;
ALTER DATABASE [CMD] SET ANSI_NULL_DEFAULT OFF;
ALTER DATABASE [CMD] SET ANSI_NULLS OFF;
ALTER DATABASE [CMD] SET ANSI_PADDING OFF;
ALTER DATABASE [CMD] SET ANSI_WARNINGS OFF;
ALTER DATABASE [CMD] SET ARITHABORT OFF;
ALTER DATABASE [CMD] SET AUTO_CLOSE OFF;
ALTER DATABASE [CMD] SET AUTO_CREATE_STATISTICS ON;
ALTER DATABASE [CMD] SET AUTO_SHRINK OFF;
ALTER DATABASE [CMD] SET AUTO_UPDATE_STATISTICS ON;
ALTER DATABASE [CMD] SET CURSOR_CLOSE_ON_COMMIT OFF;
ALTER DATABASE [CMD] SET CURSOR_DEFAULT  GLOBAL;
ALTER DATABASE [CMD] SET CONCAT_NULL_YIELDS_NULL OFF;
ALTER DATABASE [CMD] SET NUMERIC_ROUNDABORT OFF;
ALTER DATABASE [CMD] SET QUOTED_IDENTIFIER OFF;
ALTER DATABASE [CMD] SET RECURSIVE_TRIGGERS OFF;
ALTER DATABASE [CMD] SET  DISABLE_BROKER;
ALTER DATABASE [CMD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF;
ALTER DATABASE [CMD] SET DATE_CORRELATION_OPTIMIZATION OFF;
ALTER DATABASE [CMD] SET TRUSTWORTHY OFF;
ALTER DATABASE [CMD] SET ALLOW_SNAPSHOT_ISOLATION OFF;
ALTER DATABASE [CMD] SET PARAMETERIZATION SIMPLE;
ALTER DATABASE [CMD] SET READ_COMMITTED_SNAPSHOT OFF;
ALTER DATABASE [CMD] SET HONOR_BROKER_PRIORITY OFF;
ALTER DATABASE [CMD] SET  READ_WRITE;
ALTER DATABASE [CMD] SET RECOVERY FULL;
ALTER DATABASE [CMD] SET  MULTI_USER;
ALTER DATABASE [CMD] SET PAGE_VERIFY CHECKSUM;
ALTER DATABASE [CMD] SET DB_CHAINING OFF;
EXEC [CMD].dbo.sp_changedbowner @loginame = N'CMD_APP', @map = false;
USE CMD;
-- TABLES

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cat_ComparatorMethod]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[cat_ComparatorMethod](
	[ComparatorMethodKey] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_cat_ComparatorMethod] PRIMARY KEY CLUSTERED 
(
	[ComparatorMethodKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[cat_ComparatorMethod] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cat_MetricBasis]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[cat_MetricBasis](
	[MetricBasisKey] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_cat_MetricBasis] PRIMARY KEY CLUSTERED 
(
	[MetricBasisKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[cat_MetricBasis] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cat_MetricFormat]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[cat_MetricFormat](
	[MetricFormatKey] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_cat_MetricFormat] PRIMARY KEY CLUSTERED 
(
	[MetricFormatKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[cat_MetricFormat] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cross_Dashboard_Objective]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[cross_Dashboard_Objective](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DashboardKey] [int] NOT NULL,
	[ObjectiveKey] [int] NOT NULL,
 CONSTRAINT [PK_cross_Dashboard_Objective] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[cross_Dashboard_Objective] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cross_Objective_Initiative]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[cross_Objective_Initiative](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectiveKey] [int] NOT NULL,
	[InitiativeKey] [int] NOT NULL,
 CONSTRAINT [PK_cross_Objective_Initiative] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[cross_Objective_Initiative] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cross_Objective_Metric]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[cross_Objective_Metric](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectiveKey] [int] NOT NULL,
	[MetricKey] [int] NOT NULL,
 CONSTRAINT [PK_cross_Objective_Metric] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[cross_Objective_Metric] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dashboard]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dashboard](
	[DashboardKey] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[sys_active] [bit] NOT NULL,
 CONSTRAINT [PK_Dashboard] PRIMARY KEY CLUSTERED 
(
	[DashboardKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[Dashboard] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gant]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Gant](
	[GantKey] [int] IDENTITY(1,1) NOT NULL,
	[InitiativeKey] [int] NOT NULL,
	[GantData] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[sys_active] [bit] NOT NULL,
 CONSTRAINT [PK_Gant] PRIMARY KEY CLUSTERED 
(
	[GantKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[Gant] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Initiative]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Initiative](
	[InitiativeKey] [int] IDENTITY(1,1) NOT NULL,
	[ProgressValue] [decimal](15, 4) NOT NULL,
	[Description] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DueDate] [datetime] NULL,
	[ActualDate] [datetime] NULL,
	[sys_active] [bit] NOT NULL,
 CONSTRAINT [PK_Initiative] PRIMARY KEY CLUSTERED 
(
	[InitiativeKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[Initiative] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Metric]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Metric](
	[MetricKey] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CurrentValue] [decimal](15, 4) NULL,
	[GoalValue] [decimal](15, 4) NULL,
	[FormatKey] [int] NULL,
	[BasisKey] [int] NULL,
	[ComparatorMethodKey] [int] NULL,
	[sys_active] [bit] NOT NULL,
 CONSTRAINT [PK_Metric] PRIMARY KEY CLUSTERED 
(
	[MetricKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[Metric] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Objective]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Objective](
	[ObjectiveKey] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[sys_status] [bit] NOT NULL,
 CONSTRAINT [PK_Objective] PRIMARY KEY CLUSTERED 
(
	[ObjectiveKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[Objective] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sort]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Sort](
	[SortKey] [int] IDENTITY(1,1) NOT NULL,
	[Sort_Entity_ID] [int] NOT NULL,
	[Sort_Entity_Kind] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Sort_User_ID] [int] NOT NULL,
	[Sort_Edited_On] [datetime] NULL,
	[Sort_Sequence] [int] NULL,
 CONSTRAINT [PK_Sort] PRIMARY KEY CLUSTERED 
(
	[SortKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[Sort] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sysdiagrams]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sysdiagrams](
	[name] [sysname] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[principal_id] [int] NOT NULL,
	[diagram_id] [int] IDENTITY(1,1) NOT NULL,
	[version] [int] NULL,
	[definition] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[diagram_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_principal_name] UNIQUE NONCLUSTERED 
(
	[principal_id] ASC,
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[sysdiagrams] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Track]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Track](
	[TrackKey] [int] IDENTITY(1,1) NOT NULL,
	[Entity_ID] [int] NOT NULL,
	[Entity_Kind] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[User_CreatedBy] [int] NOT NULL,
	[Date_CreatedOn] [datetime] NOT NULL,
	[Date_EditedOn] [datetime] NULL,
	[Date_RemovedOn] [datetime] NULL,
	[Date_LastTimeUsed] [datetime] NULL,
	[User_LastEditedBy] [int] NULL,
	[User_RemovedBy] [int] NULL,
	[User_AssignedTo] [int] NULL,
	[User_AssignedBy] [int] NULL,
 CONSTRAINT [PK_Track] PRIMARY KEY CLUSTERED 
(
	[TrackKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[Track] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[UserKey] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Role] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone1] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[sys_active] [bit] NOT NULL,
	[Identicon] [varbinary](max) NULL,
	[Identicon64] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END;
 ALTER AUTHORIZATION ON [dbo].[User] TO  SCHEMA OWNER;
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Dashboard_Objective_Dashboard]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Dashboard_Objective]'))
ALTER TABLE [dbo].[cross_Dashboard_Objective]  WITH CHECK ADD  CONSTRAINT [FK_cross_Dashboard_Objective_Dashboard] FOREIGN KEY([DashboardKey])
REFERENCES [Dashboard] ([DashboardKey]);
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Dashboard_Objective_Dashboard]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Dashboard_Objective]'))
ALTER TABLE [dbo].[cross_Dashboard_Objective] CHECK CONSTRAINT [FK_cross_Dashboard_Objective_Dashboard];
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Dashboard_Objective_Objective]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Dashboard_Objective]'))
ALTER TABLE [dbo].[cross_Dashboard_Objective]  WITH CHECK ADD  CONSTRAINT [FK_cross_Dashboard_Objective_Objective] FOREIGN KEY([ObjectiveKey])
REFERENCES [Objective] ([ObjectiveKey])
ON DELETE CASCADE;
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Dashboard_Objective_Objective]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Dashboard_Objective]'))
ALTER TABLE [dbo].[cross_Dashboard_Objective] CHECK CONSTRAINT [FK_cross_Dashboard_Objective_Objective];
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Objective_Initiative_Initiative]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Objective_Initiative]'))
ALTER TABLE [dbo].[cross_Objective_Initiative]  WITH CHECK ADD  CONSTRAINT [FK_cross_Objective_Initiative_Initiative] FOREIGN KEY([InitiativeKey])
REFERENCES [Initiative] ([InitiativeKey])
ON DELETE CASCADE;
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Objective_Initiative_Initiative]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Objective_Initiative]'))
ALTER TABLE [dbo].[cross_Objective_Initiative] CHECK CONSTRAINT [FK_cross_Objective_Initiative_Initiative];
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Objective_Initiative_Objective]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Objective_Initiative]'))
ALTER TABLE [dbo].[cross_Objective_Initiative]  WITH CHECK ADD  CONSTRAINT [FK_cross_Objective_Initiative_Objective] FOREIGN KEY([ObjectiveKey])
REFERENCES [Objective] ([ObjectiveKey]);
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Objective_Initiative_Objective]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Objective_Initiative]'))
ALTER TABLE [dbo].[cross_Objective_Initiative] CHECK CONSTRAINT [FK_cross_Objective_Initiative_Objective];
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Objective_Metric_Metric]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Objective_Metric]'))
ALTER TABLE [dbo].[cross_Objective_Metric]  WITH CHECK ADD  CONSTRAINT [FK_cross_Objective_Metric_Metric] FOREIGN KEY([MetricKey])
REFERENCES [Metric] ([MetricKey])
ON DELETE CASCADE;
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Objective_Metric_Metric]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Objective_Metric]'))
ALTER TABLE [dbo].[cross_Objective_Metric] CHECK CONSTRAINT [FK_cross_Objective_Metric_Metric];
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Objective_Metric_Objective]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Objective_Metric]'))
ALTER TABLE [dbo].[cross_Objective_Metric]  WITH CHECK ADD  CONSTRAINT [FK_cross_Objective_Metric_Objective] FOREIGN KEY([ObjectiveKey])
REFERENCES [Objective] ([ObjectiveKey]);
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cross_Objective_Metric_Objective]') AND parent_object_id = OBJECT_ID(N'[dbo].[cross_Objective_Metric]'))
ALTER TABLE [dbo].[cross_Objective_Metric] CHECK CONSTRAINT [FK_cross_Objective_Metric_Objective];
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dashboard_sys_active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dashboard] ADD  CONSTRAINT [DF_Dashboard_sys_active]  DEFAULT ((1)) FOR [sys_active]
END;
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gant_Initiative]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gant]'))
ALTER TABLE [dbo].[Gant]  WITH CHECK ADD  CONSTRAINT [FK_Gant_Initiative] FOREIGN KEY([InitiativeKey])
REFERENCES [Initiative] ([InitiativeKey]);
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Gant_Initiative]') AND parent_object_id = OBJECT_ID(N'[dbo].[Gant]'))
ALTER TABLE [dbo].[Gant] CHECK CONSTRAINT [FK_Gant_Initiative];
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Gant_sys_active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Gant] ADD  CONSTRAINT [DF_Gant_sys_active]  DEFAULT ((1)) FOR [sys_active]
END;
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Initiative_ProgressValue]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Initiative] ADD  CONSTRAINT [DF_Initiative_ProgressValue]  DEFAULT ((0)) FOR [ProgressValue]
END;
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Initiative_sys_active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Initiative] ADD  CONSTRAINT [DF_Initiative_sys_active]  DEFAULT ((1)) FOR [sys_active]
END;
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Metric_cat_ComparatorMethod]') AND parent_object_id = OBJECT_ID(N'[dbo].[Metric]'))
ALTER TABLE [dbo].[Metric]  WITH CHECK ADD  CONSTRAINT [FK_Metric_cat_ComparatorMethod] FOREIGN KEY([ComparatorMethodKey])
REFERENCES [cat_ComparatorMethod] ([ComparatorMethodKey]);
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Metric_cat_ComparatorMethod]') AND parent_object_id = OBJECT_ID(N'[dbo].[Metric]'))
ALTER TABLE [dbo].[Metric] CHECK CONSTRAINT [FK_Metric_cat_ComparatorMethod];
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Metric_cat_MetricBasis]') AND parent_object_id = OBJECT_ID(N'[dbo].[Metric]'))
ALTER TABLE [dbo].[Metric]  WITH CHECK ADD  CONSTRAINT [FK_Metric_cat_MetricBasis] FOREIGN KEY([BasisKey])
REFERENCES [cat_MetricBasis] ([MetricBasisKey]);
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Metric_cat_MetricBasis]') AND parent_object_id = OBJECT_ID(N'[dbo].[Metric]'))
ALTER TABLE [dbo].[Metric] CHECK CONSTRAINT [FK_Metric_cat_MetricBasis];
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Metric_cat_MetricFormat]') AND parent_object_id = OBJECT_ID(N'[dbo].[Metric]'))
ALTER TABLE [dbo].[Metric]  WITH CHECK ADD  CONSTRAINT [FK_Metric_cat_MetricFormat] FOREIGN KEY([FormatKey])
REFERENCES [cat_MetricFormat] ([MetricFormatKey]);
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Metric_cat_MetricFormat]') AND parent_object_id = OBJECT_ID(N'[dbo].[Metric]'))
ALTER TABLE [dbo].[Metric] CHECK CONSTRAINT [FK_Metric_cat_MetricFormat];
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Metric_sys_active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Metric] ADD  CONSTRAINT [DF_Metric_sys_active]  DEFAULT ((1)) FOR [sys_active]
END;
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Objective_sys_status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Objective] ADD  CONSTRAINT [DF_Objective_sys_status]  DEFAULT ((1)) FOR [sys_status]
END;
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Sort_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Sort]'))
ALTER TABLE [dbo].[Sort]  WITH CHECK ADD  CONSTRAINT [FK_Sort_User] FOREIGN KEY([Sort_User_ID])
REFERENCES [User] ([UserKey])
ON DELETE CASCADE;
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Sort_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Sort]'))
ALTER TABLE [dbo].[Sort] CHECK CONSTRAINT [FK_Sort_User];
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_User] FOREIGN KEY([User_LastEditedBy])
REFERENCES [User] ([UserKey]);
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track] CHECK CONSTRAINT [FK_Track_User];
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_User1] FOREIGN KEY([User_RemovedBy])
REFERENCES [User] ([UserKey]);
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track] CHECK CONSTRAINT [FK_Track_User1];
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_User2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_User2] FOREIGN KEY([User_AssignedTo])
REFERENCES [User] ([UserKey]);
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_User2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track] CHECK CONSTRAINT [FK_Track_User2];
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_User3]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_User3] FOREIGN KEY([User_AssignedBy])
REFERENCES [User] ([UserKey]);
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_User3]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track] CHECK CONSTRAINT [FK_Track_User3];
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Track_Date_CreatedOn]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Track] ADD  CONSTRAINT [DF_Track_Date_CreatedOn]  DEFAULT (getdate()) FOR [Date_CreatedOn]
END;
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_sys_active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_sys_active]  DEFAULT ((1)) FOR [sys_active]
END;
-- VIEWS

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_dashboard]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.vw_dashboard
AS
SELECT     DashboardKey, Name, Description
FROM         dbo.Dashboard
WHERE     (sys_active = 1)
';
 ALTER AUTHORIZATION ON [dbo].[vw_dashboard] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_gant]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.vw_gant
AS
SELECT     GantKey, InitiativeKey, GantData
FROM         dbo.Gant
WHERE     (sys_active = 1)
';
 ALTER AUTHORIZATION ON [dbo].[vw_gant] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_initiative]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.vw_initiative
AS
SELECT     dbo.Initiative.InitiativeKey, dbo.Initiative.Description, dbo.Initiative.ProgressValue, dbo.Initiative.DueDate, dbo.Initiative.ActualDate, 
                      dbo.cross_Objective_Initiative.ObjectiveKey, dbo.Sort.Sort_Entity_Kind, dbo.Sort.Sort_User_ID, dbo.Sort.Sort_Edited_On, dbo.Sort.Sort_Sequence
FROM         dbo.Initiative INNER JOIN
                      dbo.cross_Objective_Initiative ON dbo.Initiative.InitiativeKey = dbo.cross_Objective_Initiative.InitiativeKey INNER JOIN
                      dbo.Sort ON dbo.cross_Objective_Initiative.ID = dbo.Sort.Sort_Entity_ID
WHERE     (dbo.Initiative.sys_active = 1) AND (dbo.Sort.Sort_Entity_Kind = N''Initiative'')
';
 ALTER AUTHORIZATION ON [dbo].[vw_initiative] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_metric]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.vw_metric
AS
SELECT     dbo.Metric.MetricKey, dbo.Metric.Description, dbo.Metric.CurrentValue, dbo.Metric.GoalValue, dbo.Metric.FormatKey, dbo.Metric.BasisKey, 
                      dbo.Metric.ComparatorMethodKey, dbo.cross_Objective_Metric.ObjectiveKey, dbo.Sort.Sort_Entity_Kind, dbo.Sort.Sort_User_ID, dbo.Sort.Sort_Edited_On, 
                      dbo.Sort.Sort_Sequence
FROM         dbo.Metric INNER JOIN
                      dbo.cross_Objective_Metric ON dbo.Metric.MetricKey = dbo.cross_Objective_Metric.MetricKey INNER JOIN
                      dbo.Sort ON dbo.cross_Objective_Metric.ID = dbo.Sort.Sort_Entity_ID
WHERE     (dbo.Metric.sys_active = 1) AND (dbo.Sort.Sort_Entity_Kind = N''Metric'')
';
 ALTER AUTHORIZATION ON [dbo].[vw_metric] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_objective]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW dbo.vw_objective
AS
SELECT     dbo.Objective.ObjectiveKey, dbo.Objective.Title, dbo.cross_Dashboard_Objective.DashboardKey, dbo.Sort.Sort_Entity_Kind, dbo.Sort.Sort_User_ID, 
                      dbo.Sort.Sort_Edited_On, dbo.Sort.Sort_Sequence
FROM         dbo.Objective INNER JOIN
                      dbo.cross_Dashboard_Objective ON dbo.Objective.ObjectiveKey = dbo.cross_Dashboard_Objective.ObjectiveKey INNER JOIN
                      dbo.Sort ON dbo.cross_Dashboard_Objective.ID = dbo.Sort.Sort_Entity_ID
WHERE     (dbo.Objective.sys_status = 1) AND (dbo.Sort.Sort_Entity_Kind = N''Objective'')
';
 ALTER AUTHORIZATION ON [dbo].[vw_objective] TO  SCHEMA OWNER;
-- STORED PROCEDURES

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_cat_ComparatorMethod_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[udp_cat_ComparatorMethod_ups]	@ComparatorMethodKey int,	@Value nvarchar(50)ASSET NOCOUNT ONIF @ComparatorMethodKey = 0 BEGIN	INSERT INTO cat_ComparatorMethod (		[Value]	)	VALUES (		@Value	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE cat_ComparatorMethod SET 		[Value] = @Value	WHERE [ComparatorMethodKey] = @ComparatorMethodKeyEND' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_cat_ComparatorMethod_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_cat_MetricBasis_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[udp_cat_MetricBasis_ups]	@MetricBasisKey int,	@Value nvarchar(50)ASSET NOCOUNT ONIF @MetricBasisKey = 0 BEGIN	INSERT INTO cat_MetricBasis (		[Value]	)	VALUES (		@Value	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE cat_MetricBasis SET 		[Value] = @Value	WHERE [MetricBasisKey] = @MetricBasisKeyEND' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_cat_MetricBasis_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_cat_MetricFormat_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[udp_cat_MetricFormat_ups]	@MetricFormatKey int,	@Value nvarchar(50)ASSET NOCOUNT ONIF @MetricFormatKey = 0 BEGIN	INSERT INTO cat_MetricFormat (		[Value]	)	VALUES (		@Value	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE cat_MetricFormat SET 		[Value] = @Value	WHERE [MetricFormatKey] = @MetricFormatKeyEND' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_cat_MetricFormat_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_cross_Dashboard_Objective_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[udp_cross_Dashboard_Objective_ups]	@ID int,	@DashboardKey int,	@ObjectiveKey intASSET NOCOUNT ONIF @ID = 0 BEGIN	INSERT INTO cross_Dashboard_Objective (		[DashboardKey],		[ObjectiveKey]	)	VALUES (		@DashboardKey,		@ObjectiveKey	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE cross_Dashboard_Objective SET 		[DashboardKey] = @DashboardKey,		[ObjectiveKey] = @ObjectiveKey	WHERE [ID] = @IDEND' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_cross_Dashboard_Objective_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_cross_Objective_Initiative_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[udp_cross_Objective_Initiative_ups]	@ID int,	@ObjectiveKey int,	@InitiativeKey intASSET NOCOUNT ONIF @ID = 0 BEGIN	INSERT INTO cross_Objective_Initiative (		[ObjectiveKey],		[InitiativeKey]	)	VALUES (		@ObjectiveKey,		@InitiativeKey	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE cross_Objective_Initiative SET 		[ObjectiveKey] = @ObjectiveKey,		[InitiativeKey] = @InitiativeKey	WHERE [ID] = @IDEND' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_cross_Objective_Initiative_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_cross_Objective_Metric_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[udp_cross_Objective_Metric_ups]	@ID int,	@ObjectiveKey int,	@MetricKey intASSET NOCOUNT ONIF @ID = 0 BEGIN	INSERT INTO cross_Objective_Metric (		[ObjectiveKey],		[MetricKey]	)	VALUES (		@ObjectiveKey,		@MetricKey	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE cross_Objective_Metric SET 		[ObjectiveKey] = @ObjectiveKey,		[MetricKey] = @MetricKey	WHERE [ID] = @IDEND' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_cross_Objective_Metric_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_Dashboard_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[udp_Dashboard_ups]	@DashboardKey int,	@Name nvarchar(150) = null,	@Description nvarchar(300) = nullASSET NOCOUNT ONIF @DashboardKey = 0 BEGIN	INSERT INTO Dashboard (		[Name],		[Description]	)	VALUES (		@Name,		@Description	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE Dashboard SET 		[Name] = @Name,		[Description] = @Description	WHERE [DashboardKey] = @DashboardKeyEND' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_Dashboard_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_Gant_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[udp_Gant_ups]	@GantKey int,	@InitiativeKey int,	@GantData varchar(MAX)ASSET NOCOUNT ONIF @GantKey = 0 BEGIN	INSERT INTO Gant (		[InitiativeKey],		[GantData]	)	VALUES (		@InitiativeKey,		@GantData	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE Gant SET 		[InitiativeKey] = @InitiativeKey,		[GantData] = @GantData	WHERE [GantKey] = @GantKeyEND' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_Gant_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_Initiative_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[udp_Initiative_ups]	@InitiativeKey int,	@Description nvarchar(250) = null,	@ProgressValue decimal(15,4) = null,	@DueDate datetime = null,	@ActualDate datetime = nullASSET NOCOUNT ONIF @InitiativeKey = 0 BEGIN	INSERT INTO Initiative (		[Description],		[ProgressValue],		[DueDate],		[ActualDate]	)	VALUES (		@Description,		@ProgressValue,		@DueDate,		@ActualDate	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE Initiative SET 		[Description] = @Description,		[ProgressValue] = @ProgressValue,		[DueDate] = @DueDate,		[ActualDate] = @ActualDate	WHERE [InitiativeKey] = @InitiativeKeyENDSET NOCOUNT OFF' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_Initiative_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_Metric_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[udp_Metric_ups]	@MetricKey int,	@Description nvarchar(250) = null,	@CurrentValue decimal(15,4) = null,	@GoalValue decimal(15,4) = null,	@FormatKey int = null,	@BasisKey int = null,	@ComparatorMethodKey int = nullASSET NOCOUNT ONIF @MetricKey = 0 BEGIN	INSERT INTO Metric (		[Description],		[CurrentValue],		[GoalValue],		[FormatKey],		[BasisKey],		[ComparatorMethodKey]	)	VALUES (		@Description,		@CurrentValue,		@GoalValue,		@FormatKey,		@BasisKey,		@ComparatorMethodKey	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE Metric SET 		[Description] = @Description,		[CurrentValue] = @CurrentValue,		[GoalValue] = @GoalValue,		[FormatKey] = @FormatKey,		[BasisKey] = @BasisKey,		[ComparatorMethodKey] = @ComparatorMethodKey	WHERE [MetricKey] = @MetricKeyENDSET NOCOUNT OFF' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_Metric_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_Objective_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[udp_Objective_ups]	@ObjectiveKey int,	@Title nvarchar(250) = nullASSET NOCOUNT ONIF @ObjectiveKey = 0 BEGIN	INSERT INTO Objective (		[Title]	)	VALUES (		@Title	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE Objective SET 		[Title] = @Title	WHERE [ObjectiveKey] = @ObjectiveKeyENDSET NOCOUNT OFF' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_Objective_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_Sort_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROC [dbo].[udp_Sort_ups]

	@Sort_TransactionAction nvarchar(20),
	@Sort_Entity_ID int,
	@Sort_Entity_Kind nvarchar(50),
	@Sort_User_ID int,
	@Sort_Edited_On datetime = null,
	@Sort_Sequence int = null
AS
SET NOCOUNT ON
IF @Sort_TransactionAction = ''INSERT'' BEGIN
	INSERT INTO [Sort] (
		[Sort_Entity_ID],
		[Sort_Entity_Kind],
		[Sort_User_ID],
		[Sort_Edited_On],
		[Sort_Sequence]
	)
	VALUES (
		@Sort_Entity_ID,
		@Sort_Entity_Kind,
		@Sort_User_ID,
		@Sort_Edited_On,
		@Sort_Sequence
	)
	SELECT SCOPE_IDENTITY() As InsertedID
END
ELSE BEGIN
	UPDATE [Sort] SET 
		[Sort_Edited_On] = @Sort_Edited_On,
		[Sort_Sequence] = @Sort_Sequence
		
	WHERE Sort_Entity_ID = @Sort_Entity_ID and Sort_Entity_Kind = @Sort_Entity_Kind

END

SET NOCOUNT OFF



' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_Sort_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_Track_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[udp_Track_ups]

	@TransactionAction nvarchar(20),
	@Entity_ID int,
	@Entity_Kind nvarchar(50),
	@User_CreatedBy int,
	@Date_EditedOn datetime = null,
	@Date_RemovedOn datetime = null,
	@Date_LastTimeUsed datetime = null,
	@User_LastEditedBy int = null,
	@User_RemovedBy int = null,
	@User_AssignedTo int = null,
	@User_AssignedBy int = null
AS
SET NOCOUNT ON
IF @TransactionAction = ''INSERT'' BEGIN
	INSERT INTO Track (
		[Entity_ID],
		[Entity_Kind],
		[User_CreatedBy],
		[Date_LastTimeUsed],
		[User_AssignedTo],
		[User_AssignedBy]
	)
	VALUES (
		@Entity_ID,
		@Entity_Kind,
		@User_CreatedBy,
		@Date_LastTimeUsed,
		@User_AssignedTo,
		@User_AssignedBy
	)
	SELECT SCOPE_IDENTITY() As InsertedID
END
ELSE BEGIN
	UPDATE Track SET 
		[Date_EditedOn] = @Date_EditedOn,
		[Date_RemovedOn] = @Date_RemovedOn,
		[Date_LastTimeUsed] = @Date_LastTimeUsed,
		[User_LastEditedBy] = @User_LastEditedBy,
		[User_RemovedBy] = @User_RemovedBy,
		[User_AssignedTo] = @User_AssignedTo,
		[User_AssignedBy] = @User_AssignedBy
	WHERE Entity_ID = @Entity_ID and Entity_Kind = @Entity_Kind

END

SET NOCOUNT OFF


' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_Track_ups] TO  SCHEMA OWNER;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udp_User_ups]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[udp_User_ups]	@UserKey int,	@Value nvarchar(50),	@UserName nvarchar(20),	@Role nvarchar(50) = null,	@Email nvarchar(256) = null,	@Phone1 nvarchar(50) = null,	@Phone2 nvarchar(50) = null,	@Identicon varbinary = null,	@Identicon64 varchar(MAX) = nullASSET NOCOUNT ONIF @UserKey = 0 BEGIN	INSERT INTO [User] (		[Value],		[UserName],		[Role],		[Email],		[Phone1],		[Phone2],		[Identicon],		[Identicon64]	)	VALUES (		@Value,		@UserName,		@Role,		@Email,		@Phone1,		@Phone2,		@Identicon,		@Identicon64	)	SELECT SCOPE_IDENTITY() As InsertedIDENDELSE BEGIN	UPDATE [User] SET 		[Value] = @Value,		[UserName] = @UserName,		[Role] = @Role,		[Email] = @Email,		[Phone1] = @Phone1,		[Phone2] = @Phone2,		[Identicon] = @Identicon,		[Identicon64] = @Identicon64	WHERE [UserKey] = @UserKeyEND' 
END;
 ALTER AUTHORIZATION ON [dbo].[udp_User_ups] TO  SCHEMA OWNER;
