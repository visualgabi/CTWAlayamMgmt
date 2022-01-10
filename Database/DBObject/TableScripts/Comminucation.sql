
GO

/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 12/30/2016 14:23:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmailTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK__EmailTemplate__3214EC071FEDB87C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [uc_EmailTemplate] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK__EmailTemplate__Organi__21D600EE] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO

ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK__EmailTemplate__Organi__21D600EE]
GO

ALTER TABLE [dbo].[EmailTemplate] ADD  CONSTRAINT [DF__EmailTemplate__Active__22CA2527]  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [dbo].[EmailTemplate] ADD  CONSTRAINT [DF__EmailTemplate__Create__23BE4960]  DEFAULT (getdate()) FOR [CreateDateTime]
GO

ALTER TABLE [dbo].[EmailTemplate] ADD  CONSTRAINT [DF__EmailTemplate__LastUp__24B26D99]  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO

--drop table [OrgSMTPDetails]
CREATE TABLE [dbo].[OrgSMTPDetails](
	[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
	[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
	[SMTPServer] [nvarchar](256) NOT NULL,
	[SMTPServerPort] [nvarchar](10) NOT NULL,
	[SMTPServerUserName] [nvarchar](256) NOT NULL,	
	[SMTPServerPassword] [nvarchar](256) NOT NULL,	
	[FromEmailId] [nvarchar](256) NOT NULL,		
	[FromEmailLabel] [nvarchar](256) NOT NULL,	
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL
)

alter table [dbo].[OrgSMTPDetails] ADD CONSTRAINT [uc_OrganizationId]  UNIQUE NONCLUSTERED (OrganizationId)

-- DROP TABLE [dbo].[Group]
CREATE TABLE [dbo].[Group](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
[Name] NVARCHAR(100) NOT NULL,
[Description] NVARCHAR(250) NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

alter table [dbo].[Group] ADD CONSTRAINT [uc_Group]  UNIQUE NONCLUSTERED (OrganizationId ASC, Name)

CREATE TABLE [dbo].[GroupMember](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[GroupId] INT FOREIGN  KEY REFERENCES [dbo].[Group] (Id) NOT NULL,	
[FamilyMemberId] INT FOREIGN  KEY REFERENCES [dbo].[FamilyMember] (Id) NOT NULL,	
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

alter table [dbo].[GroupMember] ADD CONSTRAINT [uc_GroupMember]  UNIQUE NONCLUSTERED (GroupId ASC, FamilyMemberId)


--drop table [OrgContributionMessage]
CREATE TABLE [dbo].[OrgContributionMessage](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
[Message] NVARCHAR(MAX) NOT NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

alter table [dbo].[OrgContributionMessage] ADD CONSTRAINT [uc_OrgContributionMessageOrganizationId]  UNIQUE NONCLUSTERED (OrganizationId)

CREATE TABLE [dbo].[MessageType](
	[Id] [INT] NOT NULL PRIMARY KEY,	
	Name NVARCHAR(100) NOT NULL,	
	[Description] NVARCHAR(250) NULL,
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL
)

CREATE TABLE [dbo].[Message](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
[MessageTypeId] INT FOREIGN  KEY REFERENCES [dbo].[MessageType] (Id) NOT NULL,	
[Message] NVARCHAR(MAX) NOT NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL)

DROP TABLE [BirthdayRemainder]

CREATE TABLE [dbo].[BirthdayRemainder](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
[MemberId] INT FOREIGN  KEY REFERENCES [dbo].[FamilyMember] (Id) NOT NULL,	
[BirthDate] DATETIME DEFAULT GETDATE()  NOT NULL,	
[WishedStatus] BIT NOT NULL DEFAULT 0,	
[WishedBy] INT FOREIGN  KEY REFERENCES [dbo].[Users] (Id) NULL,	
[WishedDate] DATETIME NULL,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)


ALTER TABLE OrgOffering DROP CONSTRAINT FK__OrgOfferi__Offer__4B973090

ALTER TABLE [dbo].[OrgOffering]  WITH CHECK ADD  CONSTRAINT [FK__OrgOfferi__Offer__4B973090] FOREIGN KEY([OfferingTypeId])
REFERENCES [dbo].[OfferingType] ([Id])
GO

DROP TABLE [MarriageAnniversaryRemainder]

CREATE TABLE [dbo].[MarriageAnniversaryRemainder](
	[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
	[FamilyId] INT FOREIGN  KEY REFERENCES [dbo].[FamilyMember] (Id) NOT NULL,	
	[MarriageDate] DATETIME DEFAULT GETDATE()  NOT NULL,	
	[WishedStatus] BIT NOT NULL DEFAULT 0,	
	[WishedBy] INT FOREIGN  KEY REFERENCES [dbo].[Users] (Id) NULL,	
	[WishedDate] DATETIME NULL,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL
)
