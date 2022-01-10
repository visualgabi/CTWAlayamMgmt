--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [dbo].[User](	
--	[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	[Email] [nvarchar](256) NOT NULL,
--	[FirstName] [nvarchar](256) NOT NULL,
--	[LastName] [nvarchar](256) NOT NULL,	
--	[PasswordHash] [nvarchar](250) NOT NULL,
--	[TemporaryPassword] [bit] NOT NULL,
--	[AccessFailedCount] [int] NOT NULL,
--	[LockoutDateTime] [datetime] NULL,
--	[Active] BIT NOT NULL DEFAULT 1,	
--	[CreateUser] [nvarchar](256) NOT NULL,
--	[CreateDateTime] DATETIME DEFAULT GETDATE(),
--    [LastUpdateUser] [nvarchar](256) NOT NULL,
--    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()	
--)

--GO

----DROP TABLE [dbo].[User]


--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [dbo].[Role](
--	[Id] [INT] NOT NULL PRIMARY KEY,
--	[Name] [nvarchar](256) NOT NULL,
--	[Active] BIT NOT NULL DEFAULT 1,	
--	[CreateUser] [nvarchar](256) NOT NULL,
--	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
--    [LastUpdateUser] [nvarchar](256) NOT NULL,
--    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
--) 

--GO

--DROP TABLE [dbo].[Role]

CREATE TABLE [dbo].[Log] (
    [Id] [int] IDENTITY (1, 1) NOT NULL,
    [Date] [datetime] NOT NULL,
    [Thread] [varchar] (255) NOT NULL,
    [Level] [varchar] (50) NOT NULL,
    [Logger] [varchar] (255) NOT NULL,
    [Message] [varchar] (4000) NOT NULL,
    [Exception] [varchar] (2000) NULL
)



SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MailMessage](
	[Id] [INT] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[From] VARCHAR(1000) NOT NULL,
	[To] VARCHAR(1000) NOT NULL,
	[CC] VARCHAR(1000) NOT NULL,
	[BCC] VARCHAR(1000) NOT NULL,
	[Subject] VARCHAR(200) NOT NULL,
	[Content] VARCHAR(MAX) NOT NULL,
	[NoOfReTries] INT NOT NULL,
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
) 

GO

--DROP TABLE [MailMessage]

/****** Object:  Table [dbo].[LookupCode]    Script Date: 10/23/2014 11:36:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Country](
	[Id] [INT] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,	
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
) 
GO

-- DROP TABLE [Country]

/****** Object:  Table [dbo].[LookupCode]    Script Date: 10/23/2014 11:36:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Language](
	[Id] [INT] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,	
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
) 
GO

-- DROP TABLE [Language]


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EthnicOrigin](
	[Id] [INT] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,	
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
) 
GO

-- DROP TABLE [EthnicOrigin]

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Denomination](
	[Id] [INT] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,	
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
) 
GO
-- DROP TABLE [Denomination]


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[State](
	[Id] [INT] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[CountryId] INT FOREIGN  KEY REFERENCES [dbo].[Country] (Id) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,	
	[Description] [nvarchar](250) NULL,	
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
) 
GO

--DROP TABLE [State]

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmailType](
	[Id] [INT] NOT NULL PRIMARY KEY,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,	
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
) 
GO

--DROP TABLE [EmailType]


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Organization](
	[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
	[Name] [nvarchar](100) NOT NULL,
	[Discription] [nvarchar](250) NULL,
	[DenominationId] INT FOREIGN  KEY REFERENCES [dbo].[Denomination] (Id) NOT NULL,
	[EthnicOriginId] INT FOREIGN  KEY REFERENCES [dbo].[EthnicOrigin] (Id) NOT NULL,
	[Website] [nvarchar](100) NULL,
	[PrimaryLanguageId] INT FOREIGN  KEY REFERENCES [dbo].[Language] (Id) NOT NULL,
	[SecondaryLanguageId] INT FOREIGN  KEY REFERENCES [dbo].[Language] (Id) NOT NULL,
	[Address1] [nvarchar](100) NOT NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[City] [nvarchar](100) NOT NULL,
	[StateId] INT FOREIGN  KEY REFERENCES [dbo].[State] (Id) NOT NULL,
	[CountryId] INT FOREIGN  KEY REFERENCES [dbo].[Country] (Id) NOT NULL,
	[ZipCode] [nvarchar](15) NOT NULL,
	[Phone1] [varchar](15) NULL,
	[Phone2] [varchar](15) NULL,
	[EmailId1] [nvarchar](100) NOT NULL,
	[EmailId2] [nvarchar](100) NULL,
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
)

--DROP TABLE [Organization]


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmailContent](
	[Id] [INT] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[EamilTypeId] INT NOT NULL FOREIGN  KEY REFERENCES [dbo].[EmailType] (Id),	
	[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id),
	[Subject] NVARCHAR(200) NOT NULL,
	[Content] NVARCHAR(MAX) NOT NULL,
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
) 

GO

ALTER TABLE [dbo].[Organization] ADD [RowTimeStamp] TIMESTAMP NOT NULL;

ALTER TABLE [dbo].[Organization] ADD CONSTRAINT  [uniqueOrgName] UNIQUE ([Name])


-- DROP TABLE [dbo].[EmailContent]

--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [dbo].[UserRole](
--	[Id] [INT] NOT NULL IDENTITY(1,1) PRIMARY KEY,
--	[UserId] INT NOT NULL FOREIGN  KEY REFERENCES [dbo].[User] (Id),
--	[RoleId] INT NOT NULL FOREIGN  KEY REFERENCES [dbo].[Role] (Id),
--	[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id),
--	[Active] BIT NOT NULL DEFAULT 1,	
--	[CreateUser] [nvarchar](256) NOT NULL,
--	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
--    [LastUpdateUser] [nvarchar](256) NOT NULL,
--    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
--) 

--GO

--DROP TABLE [dbo].[UserRole]

ALTER TABLE [Organization] ADD ParentId INT DEFAULT NULL;

--SELECT * FROM [Organization]

CREATE TABLE [dbo].[MembershipStatus](
[Id] [INT] NOT NULL PRIMARY KEY,	
Name NVARCHAR(100) NOT NULL,
[Description] NVARCHAR(250) NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
)

--DROP TABLE [MembershipStatus]

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Family](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
Name NVARCHAR(100) NOT NULL,
FirstVisitedDate DATETIME NOT NULL,
MembershipStartDate  DATETIME NULL,
--[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
[BranchId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
[EthnicOriginId] INT FOREIGN  KEY REFERENCES [dbo].[EthnicOrigin] (Id) NOT NULL,	
[PrimaryLanguageId] INT FOREIGN  KEY REFERENCES [dbo].[Language] (Id) NOT NULL,
[SecondaryLanguageId] INT FOREIGN  KEY REFERENCES [dbo].[Language] (Id) NOT NULL,
[MembershipStatusId] INT FOREIGN  KEY REFERENCES [dbo].[MembershipStatus] (Id) NOT NULL,
[Address1] [nvarchar](100) NOT NULL,
[Address2] [nvarchar](100) NULL,
[Address3] [nvarchar](100) NULL,
[City] [nvarchar](100) NOT NULL,
[StateId] INT FOREIGN  KEY REFERENCES [dbo].[State] (Id) NOT NULL,
[CountryId] INT FOREIGN  KEY REFERENCES [dbo].[Country] (Id) NOT NULL,
[ZipCode] [nvarchar](15) NOT NULL,
[Phone1] [varchar](15) NULL,
[Phone2] [varchar](15) NULL,
[EmailId1] [nvarchar](100) NOT NULL,
[EmailId2] [nvarchar](100) NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
)

--DROP TABLE Family

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
-- DROP TABLE [dbo].[FamilyMember]
CREATE TABLE [dbo].[FamilyMember](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
[FamilyId] INT FOREIGN  KEY REFERENCES [dbo].[Family] (Id) NOT NULL,	
FirstName NVARCHAR(100) NOT NULL,
MiddleName NVARCHAR(100) NULL,
LastName NVARCHAR(100) NOT NULL,
Initial NVARCHAR(5) NULL,
Gender NVARCHAR(1) NOT NULL,
DOB DATETIME NOT NULL,
[Phone1] [varchar](15) NULL,
[EmailId1] [nvarchar](100) NULL,
[Notes] NVARCHAR(MAX) NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

--DROP TABLE [FamilyMember]

ALTER TABLE [dbo].[MembershipStatus] ADD [RowTimeStamp] TIMESTAMP NOT NULL;
ALTER TABLE [dbo].[Family] ADD [RowTimeStamp] TIMESTAMP NOT NULL;

ALTER TABLE [dbo].[MailMessage] ADD [RowTimeStamp] TIMESTAMP NOT NULL;
ALTER TABLE [dbo].[Country]  ADD [RowTimeStamp] TIMESTAMP NOT NULL;
ALTER TABLE [dbo].[Language]  ADD [RowTimeStamp] TIMESTAMP NOT NULL;
ALTER TABLE [dbo].[EthnicOrigin] ADD [RowTimeStamp] TIMESTAMP NOT NULL;
ALTER TABLE [dbo].[Denomination] ADD [RowTimeStamp] TIMESTAMP NOT NULL;
ALTER TABLE [dbo].[State] ADD [RowTimeStamp] TIMESTAMP NOT NULL;
ALTER TABLE [dbo].[EmailType] ADD [RowTimeStamp] TIMESTAMP NOT NULL;
ALTER TABLE [dbo].[EmailContent] ADD [RowTimeStamp] TIMESTAMP NOT NULL;



ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.Organization_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
ON DELETE CASCADE
GO

--DROP TABLE [dbo].FamilyMember

--ALTER TABLE [dbo].[FamilyMember] ADD [PreviuousDenominationId] INT FOREIGN KEY REFERENCES [dbo].[Denomination] (Id) NOT NULL;

--ALTER TABLE [dbo].[FamilyMember] ADD [Notes] NVARCHAR(MAX) NULL;

ALTER TABLE [dbo].[Family] ADD [Notes] NVARCHAR(MAX) NULL;

--ALTER TABLE [dbo].[FamilyMember] DROP COLUMN [PreviuousDenominationId];

-- DROP TABLE [Currency]
CREATE TABLE [dbo].[Currency](
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


-- DROP TABLE [OrgProfile]
CREATE TABLE [dbo].[OrgProfile](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
[CurrencyId] INT FOREIGN  KEY REFERENCES [dbo].[Currency] (Id) NOT NULL,	
[TaxId] VARCHAR(100),
[Logo] BLOB(256),
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FiscalYear](
	[Id] [INT] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,	
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
) 
GO

ALTER TABLE [dbo].[FiscalYear] ADD [RowTimeStamp] TIMESTAMP NOT NULL;

-- DROP TABLE [OrgFiscalYear]
CREATE TABLE [dbo].[OrgFiscalYear](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
FiscalYearId INT FOREIGN  KEY REFERENCES [dbo].[FiscalYear] (Id) NOT NULL,
FiscalYearStart DATETIME NOT NULL,
FiscalYearEnd DATETIME NOT NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

ALTER TABLE dbo.[OrgFiscalYear]
  ADD CONSTRAINT uq_OrgFiscalYear UNIQUE(OrganizationId, FiscalYearId, Active);


-- DROP TABLE [OfferingType]
CREATE TABLE [dbo].[OfferingType](
	[Id] [INT] NOT NULL PRIMARY KEY,	
	Name NVARCHAR(100) NOT NULL,
	[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NULL,	
	[Description] NVARCHAR(250) NULL,
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL
)

CREATE TABLE [dbo].[FundType](
	[Id] [INT] NOT NULL PRIMARY KEY,	
	Name NVARCHAR(100) NOT NULL,
	[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NULL,	
	[Description] NVARCHAR(250) NULL,
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL
	)


-- DROP TABLE [FamilyPledge]
CREATE TABLE [dbo].[FamilyPledge](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
[FamilyId] INT FOREIGN  KEY REFERENCES [dbo].[Family] (Id) NOT NULL,	
[OrgFiscalYearId] INT FOREIGN  KEY REFERENCES [dbo].[OrgFiscalYear] (Id) NOT NULL,	 
[FundTypeId] INT FOREIGN  KEY REFERENCES [dbo].[FundType] (Id) NOT NULL,	 
Amount DECIMAL NOT NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

-- DROP TABLE [OrgFiscalYearBudget]
CREATE TABLE [dbo].[OrgFiscalYearBudget](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
--[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
[OrgFiscalYearId] INT FOREIGN  KEY REFERENCES [dbo].[OrgFiscalYear] (Id) NOT NULL,	 
[FundTypeId] INT FOREIGN  KEY REFERENCES [dbo].[FundType] (Id) NOT NULL,
[Amound] DECIMAL NOT NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

ALTER TABLE dbo.[OrgFiscalYearBudget]
  ADD CONSTRAINT uq_OrgFiscalYearBudget UNIQUE(OrgFiscalYearId, FundTypeId, Active);

-- DROP TABLE [[PaymentType]]
CREATE TABLE [dbo].[PaymentType](
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

--DROP TABLE [OrgOffering]
CREATE TABLE [dbo].[OrgOffering](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
[FamilyMemberId] INT FOREIGN  KEY REFERENCES [dbo].[FamilyMember] (Id) NOT NULL,
[OfferingTypeId] INT FOREIGN  KEY REFERENCES [dbo].[OfferingType] (Id) NOT NULL,
[FundTypeId] INT FOREIGN  KEY REFERENCES [dbo].[FundType] (Id) NOT NULL,
[PaymentTypeId] INT FOREIGN  KEY REFERENCES [dbo].[PaymentType] (Id) NOT NULL,
[Amount] DECIMAL NOT NULL,
[TransactionNumber] [nvarchar](100) NULL,
[Notes] [nvarchar](100) NULL,
[OfferingDate] DATETIME DEFAULT GETDATE()  NOT NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

ALTER TABLE [OrgOffering] ALTER COLUMN [FamilyMemberId] INT NULL;

--DROP TABLE [[ExpenseType]]
CREATE TABLE [dbo].[ExpenseType](
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

--DROP TABLE [[SubExpenseType]]
CREATE TABLE [dbo].[SubExpenseType](
	[Id] [INT] NOT NULL PRIMARY KEY,
	[ExpenseTypeId] INT FOREIGN  KEY REFERENCES [dbo].[ExpenseType] (Id) NOT NULL,		
	Name NVARCHAR(100) NOT NULL,	
	[Description] NVARCHAR(250) NULL,
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL
)

--DROP TABLE [Expense]
CREATE TABLE [dbo].[Expense](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
[ExpenseTypeId] INT FOREIGN  KEY REFERENCES [dbo].[ExpenseType] (Id) NOT NULL,
[SubExpenseTypeId] INT FOREIGN  KEY REFERENCES [dbo].[SubExpenseType] (Id) NOT NULL,
[PaymentTypeId] INT FOREIGN  KEY REFERENCES [dbo].[PaymentType] (Id) NOT NULL,
[Amount] DECIMAL NOT NULL,
[TransactionNumber] [nvarchar](100) NULL,
[Notes] [nvarchar](100) NULL,
[ExpenseDate] DATETIME DEFAULT GETDATE()  NOT NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO


CREATE TABLE [dbo].[Sponsor](
	[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
	[Name] [nvarchar](100) NOT NULL,	
	[Website] [nvarchar](100) NULL,	
	[Address1] [nvarchar](100) NOT NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[City] [nvarchar](100) NOT NULL,
	[OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id),
	[StateId] INT FOREIGN  KEY REFERENCES [dbo].[State] (Id) NOT NULL,
	[CountryId] INT FOREIGN  KEY REFERENCES [dbo].[Country] (Id) NOT NULL,
	[ZipCode] [nvarchar](15) NOT NULL,
	[Phone1] [varchar](15) NULL,	
	[EmailId1] [nvarchar](100) NULL,	
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL
)

ALTER TABLE [OrgOffering] ADD [SponsorId] INT FOREIGN  KEY REFERENCES [dbo].[Sponsor] (Id) NULL;

ALTER TABLE [Sponsor] ADD [OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL;

ALTER TABLE [Organization] ADD [TaxId] NVARCHAR(250);
ALTER TABLE [Organization] ADD [CurrencySymbol] NVARCHAR(250);

ALTER TABLE [Organization] ADD [CurrencyId] INT FOREIGN  KEY REFERENCES [dbo].[Currency] (Id) NULL;

ALTER TABLE [Organization] ALTER COLUMN [CurrencySymbol] NVARCHAR(5);

ALTER TABLE [Organization] drop COLUMN [CurrencySymbol]

--DROP TABLE [RefreshTokens]
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RefreshTokens](
	[Id] [INT] IDENTITY(1,1) NOT NULL,	
	[TokenId] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[ClientId] [nvarchar](250) NOT NULL,
	[IssuedUtc] [datetime] NOT NULL,
	[ExpiresUtc] [datetime] NOT NULL,
	[ProtectedTicket] [nvarchar](max) NOT NULL,
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL
 CONSTRAINT [PK_dbo.RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
-- DROP TABLE [dbo].[EventType]
CREATE TABLE [dbo].[EventType](
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

SET ANSI_NULLS ON
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
-- DROP TABLE [dbo].[Event]
CREATE TABLE [dbo].[Event](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
[BranchId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NOT NULL,	
[EventTypeId] INT FOREIGN  KEY REFERENCES [dbo].[EventType] (Id) NOT NULL,	
[EventDate] DateTime NOT NULL,	
[Name] NVARCHAR(100) NOT NULL,
[IsSpecialEvent] BIT NOT NULL DEFAULT 0,	
[Description] NVARCHAR(250) NULL,
[SplGuestDetails] NVARCHAR(1000) NULL,
NoOfAdultAttended INT NULL,
NoOfMenAttended INT NULL,
NoOfWomenAttended INT NULL,
NoOfKidsParticipated INT NULL,
[Offering] [decimal](18, 0) NULL,
[Expense] [decimal](18, 0) NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

ALTER TABLE ExpenseType ADD [OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NULL


ALTER TABLE OfferingType ADD [OrganizationId] INT FOREIGN  KEY REFERENCES [dbo].[Organization] (Id) NULL

ALTER TABLE OfferingType ADD NewColumn INT IDENTITY(1,1)

ALTER TABLE OfferingType DROP COLUMN ID

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
-- DROP TABLE [dbo].[AccountType]
CREATE TABLE [dbo].[AccountType](
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


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
-- DROP TABLE [dbo].[Bank]
CREATE TABLE [dbo].[Bank](
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

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
-- DROP TABLE [dbo].[Account]
CREATE TABLE [dbo].[Account](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[Number] NVARCHAR(100) NOT NULL,
[Name] NVARCHAR(100) NOT NULL,
[AccountTypeId] INT FOREIGN  KEY REFERENCES [dbo].[AccountType] (Id) NOT NULL,	
[BankId]  INT FOREIGN  KEY REFERENCES [dbo].[Bank] (Id) NOT NULL,	
[Description] NVARCHAR(250) NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
-- DROP TABLE [dbo].[Deposit]
CREATE TABLE [dbo].[Deposit](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
[Amount] [decimal](18, 0) NULL,
[UserId] INT FOREIGN  KEY REFERENCES [dbo].[Users] (Id) NOT NULL,
[DepositDate] Date NOT NULL,
[AccountId] INT FOREIGN  KEY REFERENCES [dbo].[Account] (Id) NOT NULL,	
[Location] NVARCHAR(250) NULL,
[Description] NVARCHAR(250) NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)

ALTER TABLE FamilyMember ADD IsTaxPayer BIT NOT NULL DEFAULT 0

ALTER TABLE Expense ADD BranchId INT FOREIGN  KEY REFERENCES [dbo].[ORGANIZATION] (Id) NULL


--drop table [OpeningBalance]

CREATE TABLE [dbo].[OpeningBalance](
[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,	
[OrgFiscalYearId] INT FOREIGN  KEY REFERENCES [dbo].[OrgFiscalYear] (Id) NOT NULL,	 
[AccountId] INT FOREIGN  KEY REFERENCES [dbo].[Account] (Id) NOT NULL,	 
[Amount] DECIMAL NOT NULL,
[Active] BIT NOT NULL DEFAULT 1,	
[CreateUser] [nvarchar](256) NOT NULL,
[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[LastUpdateUser] [nvarchar](256) NOT NULL,
[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
[RowTimeStamp] TIMESTAMP NOT NULL
)


alter table [dbo].[OpeningBalance] ADD CONSTRAINT [uc_OpeningBalance]  UNIQUE NONCLUSTERED (OrgFiscalYearId ASC, AccountId)

--drop table [Relationship]
CREATE TABLE [dbo].[Relationship](
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


ALTER TABLE FamilyMember ADD RelationshipId INT FOREIGN  KEY REFERENCES [dbo].[Relationship] (Id) NULL


CREATE TABLE [dbo].[FiscalYearStartAndEndMonth](
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

ALTER TABLE ORGANIZATION ADD FiscalYearStartAndEndMonthId INT FOREIGN  KEY REFERENCES [dbo].[FiscalYearStartAndEndMonth] (Id) NULL


--DROP TABLE [FiscalYearQuarter]
CREATE TABLE [dbo].[FiscalYearQuarter](
	[Id] [INT] NOT NULL PRIMARY KEY,	
	FiscalYearStartAndEndMonthId INT FOREIGN  KEY REFERENCES [dbo].[FiscalYearStartAndEndMonth] (Id) NOT NULL,
	Name NVARCHAR(100) NOT NULL,	
	StartMonth INT NOT NULL,	
	EndMonth INT NOT NULL,	
	[Description] NVARCHAR(250) NULL,
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL
)