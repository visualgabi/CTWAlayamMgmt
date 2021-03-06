USE [master]
GO
/****** Object:  Database [CTWAlayamMgmt]    Script Date: 1/22/2022 8:14:18 AM ******/
CREATE DATABASE [CTWAlayamMgmt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CTWAlayamMgmt', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\CTWAlayamMgmt.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CTWAlayamMgmt_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\CTWAlayamMgmt_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CTWAlayamMgmt] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CTWAlayamMgmt].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CTWAlayamMgmt] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET ARITHABORT OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CTWAlayamMgmt] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CTWAlayamMgmt] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CTWAlayamMgmt] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CTWAlayamMgmt] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET RECOVERY FULL 
GO
ALTER DATABASE [CTWAlayamMgmt] SET  MULTI_USER 
GO
ALTER DATABASE [CTWAlayamMgmt] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CTWAlayamMgmt] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CTWAlayamMgmt] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CTWAlayamMgmt] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CTWAlayamMgmt] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CTWAlayamMgmt', N'ON'
GO
ALTER DATABASE [CTWAlayamMgmt] SET QUERY_STORE = OFF
GO
USE [CTWAlayamMgmt]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [CTWAlayamMgmt]
GO
/****** Object:  User [dev]    Script Date: 1/22/2022 8:14:18 AM ******/
CREATE USER [dev] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [dbUser]    Script Date: 1/22/2022 8:14:18 AM ******/
CREATE USER [dbUser] FOR LOGIN [dbUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [dbUser]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 1/22/2022 8:14:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[AccountTypeId] [int] NOT NULL,
	[BankId] [int] NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_Account] UNIQUE NONCLUSTERED 
(
	[BankId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountType]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_BankName] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Secret] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ApplicationType] [int] NOT NULL,
	[RefreshTokenLifeTime] [int] NOT NULL,
	[AllowedOrigin] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Denomination]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Denomination](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Deposit]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deposit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 0) NULL,
	[UserId] [int] NOT NULL,
	[DepositDate] [date] NOT NULL,
	[AccountId] [int] NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[OfferingDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailContent]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailContent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EamilTypeId] [int] NOT NULL,
	[OrganizationId] [int] NULL,
	[Subject] [nvarchar](200) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 1/22/2022 8:14:19 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_EmailTemplate] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailType]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EthnicOrigin]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EthnicOrigin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BranchId] [int] NOT NULL,
	[EventTypeId] [int] NOT NULL,
	[EventDate] [datetime] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsSpecialEvent] [bit] NOT NULL,
	[Description] [nvarchar](250) NULL,
	[SplGuestDetails] [nvarchar](1000) NULL,
	[NoOfAdultAttended] [int] NULL,
	[NoOfMenAttended] [int] NULL,
	[NoOfWomenAttended] [int] NULL,
	[NoOfKidsParticipated] [int] NULL,
	[Offering] [decimal](18, 0) NULL,
	[Expense] [decimal](18, 0) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventType]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[OrganizationId] [int] NULL,
 CONSTRAINT [PK__EventT__3214EC07542C7691] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_EventType] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expense]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expense](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NULL,
	[ExpenseTypeId] [int] NOT NULL,
	[SubExpenseTypeId] [int] NOT NULL,
	[PaymentTypeId] [int] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[TransactionNumber] [nvarchar](100) NULL,
	[Notes] [nvarchar](100) NULL,
	[ExpenseDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[BranchId] [int] NULL,
	[AccountId] [int] NOT NULL,
 CONSTRAINT [PK__Expense__3214EC07627A95E8] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExpenseType]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpenseType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[OrganizationId] [int] NULL,
 CONSTRAINT [PK__ExpenseT__3214EC07542C7691] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_ExpenseType] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Family]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Family](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[FirstVisitedDate] [datetime] NOT NULL,
	[MembershipStartDate] [datetime] NULL,
	[BranchId] [int] NOT NULL,
	[EthnicOriginId] [int] NOT NULL,
	[PrimaryLanguageId] [int] NOT NULL,
	[SecondaryLanguageId] [int] NOT NULL,
	[MembershipStatusId] [int] NOT NULL,
	[Address1] [nvarchar](100) NOT NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[City] [nvarchar](100) NOT NULL,
	[StateId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[ZipCode] [nvarchar](15) NOT NULL,
	[Phone1] [varchar](15) NULL,
	[Phone2] [varchar](15) NULL,
	[EmailId1] [nvarchar](100) NOT NULL,
	[EmailId2] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[MariageDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FamilyMember]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamilyMember](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FamilyId] [int] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[MiddleName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Initial] [nvarchar](5) NULL,
	[Gender] [nvarchar](1) NOT NULL,
	[DOB] [datetime] NOT NULL,
	[Phone1] [varchar](15) NULL,
	[EmailId1] [nvarchar](100) NULL,
	[Notes] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[IsTaxPayer] [bit] NOT NULL,
	[RelationshipId] [int] NOT NULL,
 CONSTRAINT [PK__FamilyMe__3214EC072D7CBDC4] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FamilyPledge]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamilyPledge](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FamilyId] [int] NOT NULL,
	[OrgFiscalYearId] [int] NOT NULL,
	[FundTypeId] [int] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FiscalYear]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FiscalYear](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FiscalYearQuarter]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FiscalYearQuarter](
	[Id] [int] NOT NULL,
	[FiscalYearStartAndEndMonthId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[StartMonth] [int] NOT NULL,
	[EndMonth] [int] NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FiscalYearStartAndEndMonth]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FiscalYearStartAndEndMonth](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FundType]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FundType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[OrganizationId] [int] NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK__FundType__3214EC071FEDB87C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_FundType] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_Group] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupMember]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMember](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[FamilyMemberId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_GroupMember] UNIQUE NONCLUSTERED 
(
	[GroupId] ASC,
	[FamilyMemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IssueStatus]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IssueStatus](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL,
	[AppName] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MailMessage]    Script Date: 1/22/2022 8:14:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[From] [nvarchar](1000) NOT NULL,
	[To] [nvarchar](1000) NOT NULL,
	[CC] [nvarchar](1000) NOT NULL,
	[BCC] [nvarchar](1000) NOT NULL,
	[Subject] [nvarchar](200) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[NoOfReTries] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MembershipStatus]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MembershipStatus](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[MessageTypeId] [int] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageType]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OfferingType]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfferingType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[OrganizationId] [int] NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK__Offering__3214EC07536D5C82] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_OfferingType] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OpeningBalance]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OpeningBalance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrgFiscalYearId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_OpeningBalance] UNIQUE NONCLUSTERED 
(
	[OrgFiscalYearId] ASC,
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Discription] [nvarchar](250) NULL,
	[DenominationId] [int] NULL,
	[EthnicOriginId] [int] NOT NULL,
	[Website] [nvarchar](100) NULL,
	[PrimaryLanguageId] [int] NOT NULL,
	[SecondaryLanguageId] [int] NOT NULL,
	[Address1] [nvarchar](100) NOT NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[City] [nvarchar](100) NOT NULL,
	[StateId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[ZipCode] [nvarchar](15) NOT NULL,
	[Phone1] [varchar](15) NULL,
	[Phone2] [varchar](15) NULL,
	[EmailId1] [nvarchar](100) NOT NULL,
	[EmailId2] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[ParentId] [int] NULL,
	[TaxId] [nvarchar](250) NULL,
	[CurrencyId] [int] NULL,
	[FiscalYearStartAndEndMonthId] [int] NULL,
 CONSTRAINT [PK__Organiza__3214EC075BE2A6F2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uniqueOrgName] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrgContributionMessage]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrgContributionMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_OrgContributionMessageOrganizationId] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrgFiscalYear]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrgFiscalYear](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
	[FiscalYearStart] [datetime] NOT NULL,
	[FiscalYearEnd] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_OrgFiscalYear] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[FiscalYearId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrgFiscalYearBudget]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrgFiscalYearBudget](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrgFiscalYearId] [int] NOT NULL,
	[FundTypeId] [int] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uq_OrgFiscalYearBudget] UNIQUE NONCLUSTERED 
(
	[OrgFiscalYearId] ASC,
	[FundTypeId] ASC,
	[Active] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrgOffering]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrgOffering](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[FamilyMemberId] [int] NULL,
	[OfferingTypeId] [int] NOT NULL,
	[FundTypeId] [int] NOT NULL,
	[PaymentTypeId] [int] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[TransactionNumber] [nvarchar](100) NULL,
	[Notes] [nvarchar](100) NULL,
	[OfferingDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[SponsorId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrgSMTPDetails]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrgSMTPDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[SMTPServer] [nvarchar](256) NOT NULL,
	[SMTPServerPort] [nvarchar](10) NOT NULL,
	[SMTPServerUserName] [nvarchar](256) NOT NULL,
	[SMTPServerPassword] [nvarchar](256) NOT NULL,
	[FromEmailId] [nvarchar](256) NOT NULL,
	[FromEmailLabel] [nvarchar](256) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_OrganizationId] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentType]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TokenId] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[ClientId] [nvarchar](250) NOT NULL,
	[IssuedUtc] [datetime] NOT NULL,
	[ExpiresUtc] [datetime] NOT NULL,
	[ProtectedTicket] [nvarchar](max) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Relationship]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Relationship](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportedIssue]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportedIssue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[IssueStatusId] [int] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Description] [nvarchar](500) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sponsor]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sponsor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Website] [nvarchar](100) NULL,
	[Address1] [nvarchar](100) NOT NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[City] [nvarchar](100) NOT NULL,
	[StateId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[ZipCode] [nvarchar](15) NOT NULL,
	[Phone1] [varchar](15) NULL,
	[EmailId1] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[OrganizationId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubExpenseType]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubExpenseType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseTypeId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK__SubExpen__3214EC075AD97420] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_SubExpenseType] UNIQUE NONCLUSTERED 
(
	[ExpenseTypeId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxFiling]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxFiling](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrgFiscalYearId] [int] NOT NULL,
	[FiledDate] [datetime] NOT NULL,
	[FiledBy] [varchar](100) NOT NULL,
	[TotalRevenue] [decimal](18, 0) NOT NULL,
	[TotalExpense] [decimal](18, 0) NOT NULL,
	[TotalAsset] [decimal](18, 0) NOT NULL,
	[TotalLiability] [decimal](18, 0) NOT NULL,
	[Notes] [nvarchar](250) NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[OrgFiscalYearId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[OrganizationId] [int] NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.UserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[RowTimeStamp] [timestamp] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateUser] [nvarchar](256) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[AccountType] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[AccountType] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[AccountType] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Bank] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Bank] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Bank] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Clients] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Clients] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Currency] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Currency] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Currency] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Denomination] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Denomination] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Denomination] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Deposit] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Deposit] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Deposit] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[EmailContent] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[EmailContent] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[EmailContent] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[EmailTemplate] ADD  CONSTRAINT [DF__EmailTemplate__Active__22CA2527]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[EmailTemplate] ADD  CONSTRAINT [DF__EmailTemplate__Create__23BE4960]  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[EmailTemplate] ADD  CONSTRAINT [DF__EmailTemplate__LastUp__24B26D99]  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[EmailType] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[EmailType] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[EmailType] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[EthnicOrigin] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[EthnicOrigin] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[EthnicOrigin] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Event] ADD  DEFAULT ((0)) FOR [IsSpecialEvent]
GO
ALTER TABLE [dbo].[Event] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Event] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Event] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[EventType] ADD  CONSTRAINT [DF__EventTy__Activ__5614BF03]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[EventType] ADD  CONSTRAINT [DF__EventTy__Creat__5708E33C]  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[EventType] ADD  CONSTRAINT [DF__EventTy__LastU__57FD0775]  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Expense] ADD  CONSTRAINT [DF__Expense__Expense__68336F3E]  DEFAULT (getdate()) FOR [ExpenseDate]
GO
ALTER TABLE [dbo].[Expense] ADD  CONSTRAINT [DF__Expense__Active__69279377]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Expense] ADD  CONSTRAINT [DF__Expense__CreateD__6A1BB7B0]  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Expense] ADD  CONSTRAINT [DF__Expense__LastUpd__6B0FDBE9]  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[ExpenseType] ADD  CONSTRAINT [DF__ExpenseTy__Activ__5614BF03]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[ExpenseType] ADD  CONSTRAINT [DF__ExpenseTy__Creat__5708E33C]  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[ExpenseType] ADD  CONSTRAINT [DF__ExpenseTy__LastU__57FD0775]  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Family] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Family] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Family] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[FamilyMember] ADD  CONSTRAINT [DF__FamilyMem__Activ__30592A6F]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[FamilyMember] ADD  CONSTRAINT [DF__FamilyMem__Creat__314D4EA8]  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[FamilyMember] ADD  CONSTRAINT [DF__FamilyMem__LastU__324172E1]  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[FamilyMember] ADD  CONSTRAINT [DF__FamilyMem__IsTax__4A03EDD9]  DEFAULT ((0)) FOR [IsTaxPayer]
GO
ALTER TABLE [dbo].[FamilyPledge] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[FamilyPledge] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[FamilyPledge] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[FiscalYear] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[FiscalYear] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[FiscalYear] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[FiscalYearQuarter] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[FiscalYearQuarter] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[FiscalYearQuarter] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[FiscalYearStartAndEndMonth] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[FiscalYearStartAndEndMonth] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[FiscalYearStartAndEndMonth] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[FundType] ADD  CONSTRAINT [DF__FundType__Active__22CA2527]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[FundType] ADD  CONSTRAINT [DF__FundType__Create__23BE4960]  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[FundType] ADD  CONSTRAINT [DF__FundType__LastUp__24B26D99]  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[GroupMember] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[GroupMember] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[GroupMember] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[IssueStatus] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[IssueStatus] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Language] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Language] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Language] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[MailMessage] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[MailMessage] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[MailMessage] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[MembershipStatus] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[MembershipStatus] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[MembershipStatus] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Message] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Message] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Message] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[MessageType] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[MessageType] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[MessageType] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[OfferingType] ADD  CONSTRAINT [DF__OfferingT__Activ__5649C92D]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[OfferingType] ADD  CONSTRAINT [DF__OfferingT__Creat__573DED66]  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[OfferingType] ADD  CONSTRAINT [DF__OfferingT__LastU__5832119F]  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[OpeningBalance] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[OpeningBalance] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[OpeningBalance] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Organization] ADD  CONSTRAINT [DF__Organizat__Activ__6383C8BA]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Organization] ADD  CONSTRAINT [DF__Organizat__Creat__6477ECF3]  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Organization] ADD  CONSTRAINT [DF__Organizat__LastU__656C112C]  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Organization] ADD  CONSTRAINT [DF__Organizat__Paren__6FE99F9F]  DEFAULT (NULL) FOR [ParentId]
GO
ALTER TABLE [dbo].[OrgContributionMessage] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[OrgContributionMessage] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[OrgContributionMessage] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[OrgFiscalYear] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[OrgFiscalYear] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[OrgFiscalYear] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[OrgFiscalYearBudget] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[OrgFiscalYearBudget] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[OrgFiscalYearBudget] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[OrgOffering] ADD  DEFAULT (getdate()) FOR [OfferingDate]
GO
ALTER TABLE [dbo].[OrgOffering] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[OrgOffering] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[OrgOffering] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[OrgSMTPDetails] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[OrgSMTPDetails] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[OrgSMTPDetails] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[PaymentType] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[PaymentType] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[PaymentType] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[RefreshTokens] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[RefreshTokens] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[RefreshTokens] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Relationship] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Relationship] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Relationship] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[ReportedIssue] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[ReportedIssue] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Sponsor] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Sponsor] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Sponsor] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[State] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[State] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[State] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[SubExpenseType] ADD  CONSTRAINT [DF__SubExpens__Activ__5DB5E0CB]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SubExpenseType] ADD  CONSTRAINT [DF__SubExpens__Creat__5EAA0504]  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[SubExpenseType] ADD  CONSTRAINT [DF__SubExpens__LastU__5F9E293D]  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[TaxFiling] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[TaxFiling] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[UserClaims] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[UserClaims] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[UserClaims] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreateDateTime]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [LastUpdateDateTime]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([AccountTypeId])
REFERENCES [dbo].[AccountType] ([Id])
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([BankId])
REFERENCES [dbo].[Bank] ([Id])
GO
ALTER TABLE [dbo].[Bank]  WITH CHECK ADD  CONSTRAINT [FK__Bank__Organizati__200DB40D] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[Bank] CHECK CONSTRAINT [FK__Bank__Organizati__200DB40D]
GO
ALTER TABLE [dbo].[Deposit]  WITH CHECK ADD FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Deposit]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[EmailContent]  WITH CHECK ADD FOREIGN KEY([EamilTypeId])
REFERENCES [dbo].[EmailType] ([Id])
GO
ALTER TABLE [dbo].[EmailContent]  WITH CHECK ADD  CONSTRAINT [FK__EmailCont__Organ__6B24EA82] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[EmailContent] CHECK CONSTRAINT [FK__EmailCont__Organ__6B24EA82]
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK__EmailTemplate__Organi__21D600EE] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK__EmailTemplate__Organi__21D600EE]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK__Event__BranchId__01892CED] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK__Event__BranchId__01892CED]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD FOREIGN KEY([EventTypeId])
REFERENCES [dbo].[EventType] ([Id])
GO
ALTER TABLE [dbo].[EventType]  WITH CHECK ADD  CONSTRAINT [FK__EventTy__Organ__4D1564AE] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[EventType] CHECK CONSTRAINT [FK__EventTy__Organ__4D1564AE]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK__Expense__Account__63C3BFDC] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK__Expense__Account__63C3BFDC]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK__Expense__BranchI__4AF81212] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK__Expense__BranchI__4AF81212]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK__Expense__Expense__65570293] FOREIGN KEY([ExpenseTypeId])
REFERENCES [dbo].[ExpenseType] ([Id])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK__Expense__Expense__65570293]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK__Expense__Organiz__6462DE5A] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK__Expense__Organiz__6462DE5A]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK__Expense__Payment__673F4B05] FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentType] ([Id])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK__Expense__Payment__673F4B05]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK__Expense__SubExpe__664B26CC] FOREIGN KEY([SubExpenseTypeId])
REFERENCES [dbo].[SubExpenseType] ([Id])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK__Expense__SubExpe__664B26CC]
GO
ALTER TABLE [dbo].[ExpenseType]  WITH CHECK ADD  CONSTRAINT [FK__ExpenseTy__Organ__4D1564AE] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[ExpenseType] CHECK CONSTRAINT [FK__ExpenseTy__Organ__4D1564AE]
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD  CONSTRAINT [FK__Family__BranchId__7167D3BD] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[Family] CHECK CONSTRAINT [FK__Family__BranchId__7167D3BD]
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD FOREIGN KEY([EthnicOriginId])
REFERENCES [dbo].[EthnicOrigin] ([Id])
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD FOREIGN KEY([MembershipStatusId])
REFERENCES [dbo].[MembershipStatus] ([Id])
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD FOREIGN KEY([PrimaryLanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD FOREIGN KEY([SecondaryLanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[FamilyMember]  WITH CHECK ADD  CONSTRAINT [FK__FamilyMem__Famil__2F650636] FOREIGN KEY([FamilyId])
REFERENCES [dbo].[Family] ([Id])
GO
ALTER TABLE [dbo].[FamilyMember] CHECK CONSTRAINT [FK__FamilyMem__Famil__2F650636]
GO
ALTER TABLE [dbo].[FamilyMember]  WITH CHECK ADD  CONSTRAINT [FK__FamilyMem__Relat__118A8A8C] FOREIGN KEY([RelationshipId])
REFERENCES [dbo].[Relationship] ([Id])
GO
ALTER TABLE [dbo].[FamilyMember] CHECK CONSTRAINT [FK__FamilyMem__Relat__118A8A8C]
GO
ALTER TABLE [dbo].[FamilyPledge]  WITH CHECK ADD FOREIGN KEY([FamilyId])
REFERENCES [dbo].[Family] ([Id])
GO
ALTER TABLE [dbo].[FamilyPledge]  WITH CHECK ADD  CONSTRAINT [FK__FamilyPle__FundT__5FD33367] FOREIGN KEY([FundTypeId])
REFERENCES [dbo].[FundType] ([Id])
GO
ALTER TABLE [dbo].[FamilyPledge] CHECK CONSTRAINT [FK__FamilyPle__FundT__5FD33367]
GO
ALTER TABLE [dbo].[FamilyPledge]  WITH CHECK ADD FOREIGN KEY([OrgFiscalYearId])
REFERENCES [dbo].[OrgFiscalYear] ([Id])
GO
ALTER TABLE [dbo].[FiscalYearQuarter]  WITH CHECK ADD FOREIGN KEY([FiscalYearStartAndEndMonthId])
REFERENCES [dbo].[FiscalYearStartAndEndMonth] ([Id])
GO
ALTER TABLE [dbo].[FiscalYearQuarter]  WITH CHECK ADD FOREIGN KEY([FiscalYearStartAndEndMonthId])
REFERENCES [dbo].[FiscalYearStartAndEndMonth] ([Id])
GO
ALTER TABLE [dbo].[FundType]  WITH CHECK ADD  CONSTRAINT [FK__FundType__Organi__21D600EE] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[FundType] CHECK CONSTRAINT [FK__FundType__Organi__21D600EE]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[GroupMember]  WITH CHECK ADD FOREIGN KEY([FamilyMemberId])
REFERENCES [dbo].[FamilyMember] ([Id])
GO
ALTER TABLE [dbo].[GroupMember]  WITH CHECK ADD FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD FOREIGN KEY([MessageTypeId])
REFERENCES [dbo].[MessageType] ([Id])
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[OfferingType]  WITH CHECK ADD  CONSTRAINT [FK__OfferingT__Organ__5555A4F4] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[OfferingType] CHECK CONSTRAINT [FK__OfferingT__Organ__5555A4F4]
GO
ALTER TABLE [dbo].[OpeningBalance]  WITH CHECK ADD FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[OpeningBalance]  WITH CHECK ADD FOREIGN KEY([OrgFiscalYearId])
REFERENCES [dbo].[OrgFiscalYear] ([Id])
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK__Organizat__Count__628FA481] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK__Organizat__Count__628FA481]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK__Organizat__Curre__7B113988] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK__Organizat__Curre__7B113988]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK__Organizat__Denom__5DCAEF64] FOREIGN KEY([DenominationId])
REFERENCES [dbo].[Denomination] ([Id])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK__Organizat__Denom__5DCAEF64]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK__Organizat__Ethni__5EBF139D] FOREIGN KEY([EthnicOriginId])
REFERENCES [dbo].[EthnicOrigin] ([Id])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK__Organizat__Ethni__5EBF139D]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD FOREIGN KEY([FiscalYearStartAndEndMonthId])
REFERENCES [dbo].[FiscalYearStartAndEndMonth] ([Id])
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK__Organizat__Prima__5FB337D6] FOREIGN KEY([PrimaryLanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK__Organizat__Prima__5FB337D6]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK__Organizat__Secon__60A75C0F] FOREIGN KEY([SecondaryLanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK__Organizat__Secon__60A75C0F]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK__Organizat__State__619B8048] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK__Organizat__State__619B8048]
GO
ALTER TABLE [dbo].[OrgContributionMessage]  WITH CHECK ADD FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[OrgFiscalYear]  WITH CHECK ADD FOREIGN KEY([FiscalYearId])
REFERENCES [dbo].[FiscalYear] ([Id])
GO
ALTER TABLE [dbo].[OrgFiscalYear]  WITH CHECK ADD  CONSTRAINT [FK__OrgFiscal__Organ__4CC05EF3] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[OrgFiscalYear] CHECK CONSTRAINT [FK__OrgFiscal__Organ__4CC05EF3]
GO
ALTER TABLE [dbo].[OrgFiscalYearBudget]  WITH CHECK ADD  CONSTRAINT [FK__OrgFiscal__FundT__68687968] FOREIGN KEY([FundTypeId])
REFERENCES [dbo].[FundType] ([Id])
GO
ALTER TABLE [dbo].[OrgFiscalYearBudget] CHECK CONSTRAINT [FK__OrgFiscal__FundT__68687968]
GO
ALTER TABLE [dbo].[OrgFiscalYearBudget]  WITH CHECK ADD FOREIGN KEY([OrgFiscalYearId])
REFERENCES [dbo].[OrgFiscalYear] ([Id])
GO
ALTER TABLE [dbo].[OrgOffering]  WITH CHECK ADD  CONSTRAINT [FK__OrgOfferi__Famil__4AA30C57] FOREIGN KEY([FamilyMemberId])
REFERENCES [dbo].[FamilyMember] ([Id])
GO
ALTER TABLE [dbo].[OrgOffering] CHECK CONSTRAINT [FK__OrgOfferi__Famil__4AA30C57]
GO
ALTER TABLE [dbo].[OrgOffering]  WITH CHECK ADD  CONSTRAINT [FK__OrgOfferi__FundT__4C8B54C9] FOREIGN KEY([FundTypeId])
REFERENCES [dbo].[FundType] ([Id])
GO
ALTER TABLE [dbo].[OrgOffering] CHECK CONSTRAINT [FK__OrgOfferi__FundT__4C8B54C9]
GO
ALTER TABLE [dbo].[OrgOffering]  WITH CHECK ADD  CONSTRAINT [FK__OrgOfferi__Offer__4B973090] FOREIGN KEY([OfferingTypeId])
REFERENCES [dbo].[FundType] ([Id])
GO
ALTER TABLE [dbo].[OrgOffering] CHECK CONSTRAINT [FK__OrgOfferi__Offer__4B973090]
GO
ALTER TABLE [dbo].[OrgOffering]  WITH CHECK ADD  CONSTRAINT [FK__OrgOfferi__Organ__49AEE81E] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[OrgOffering] CHECK CONSTRAINT [FK__OrgOfferi__Organ__49AEE81E]
GO
ALTER TABLE [dbo].[OrgOffering]  WITH CHECK ADD FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentType] ([Id])
GO
ALTER TABLE [dbo].[OrgOffering]  WITH CHECK ADD FOREIGN KEY([SponsorId])
REFERENCES [dbo].[Sponsor] ([Id])
GO
ALTER TABLE [dbo].[OrgSMTPDetails]  WITH CHECK ADD FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[ReportedIssue]  WITH CHECK ADD FOREIGN KEY([IssueStatusId])
REFERENCES [dbo].[IssueStatus] ([Id])
GO
ALTER TABLE [dbo].[ReportedIssue]  WITH CHECK ADD FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[Sponsor]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Sponsor]  WITH CHECK ADD  CONSTRAINT [FK__Sponsor__Organiz__758D6A5C] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[Sponsor] CHECK CONSTRAINT [FK__Sponsor__Organiz__758D6A5C]
GO
ALTER TABLE [dbo].[Sponsor]  WITH CHECK ADD FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[State]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[SubExpenseType]  WITH CHECK ADD  CONSTRAINT [FK__SubExpens__Expen__5CC1BC92] FOREIGN KEY([ExpenseTypeId])
REFERENCES [dbo].[ExpenseType] ([Id])
GO
ALTER TABLE [dbo].[SubExpenseType] CHECK CONSTRAINT [FK__SubExpens__Expen__5CC1BC92]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK__UserRoles__Organ__5F691F13] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK__UserRoles__Organ__5F691F13]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[uspBalanceSheet]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspBalanceSheet] ( @OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId

DECLARE @totalOffering DECIMAL(18,2);
DECLARE @totalExpene DECIMAL(18,2);
DECLARE @OpenningBalance DECIMAL(18,2);



	--SELECT @totalOffering = ISNULL(SUM(ISNULL(D.Amount,0)),0) FROM Deposit D 
	--INNER JOIN [Account] A ON D.AccountId = A.Id
	--INNER JOIN [Bank] B ON A.BankId = B.Id	
	--WHERE D.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
	--AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),@EndDate, 120)	
	
	--SELECT @totalExpene = ISNULL(SUM(ISNULL(E.Amount,0)),0)  FROM Expense E
	--INNER JOIN [Account] A ON E.AccountId = A.Id
	--INNER JOIN [Bank] B ON A.BankId = B.Id	
	--WHERE E.Active = 1 AND B.OrganizationId =  @OrganizationId AND A.Active = 1
	--AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),@EndDate, 120)	
	
	--SELECT @OpenningBalance = ISNULL(SUM(ISNULL(Amount,0)),0) FROM [OpeningBalance] OP  	
	--WHERE [OrgFiscalYearId] = @OrgFiscalYearId AND Active = 1 

SELECT [OrderId], [Amount], [Type] FROM
(

	SELECT 1 [OrderId], ISNULL(SUM(ISNULL(Amount,0)),0) [Amount], 'Account(s) Openning Balance' [Type] FROM [OpeningBalance] OP  	
	WHERE [OrgFiscalYearId] = @OrgFiscalYearId AND Active = 1 
	
	 UNION

	SELECT 2 [OrderId], ISNULL(SUM(ISNULL(D.Amount,0)),0) [Amount], 'Amount Received' [Type] FROM Deposit D 
	INNER JOIN [Account] A ON D.AccountId = A.Id
	INNER JOIN [Bank] B ON A.BankId = B.Id	
	WHERE D.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
	AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),@EndDate, 120)	

	 UNION
	
	SELECT 3 [OrderId], ISNULL(SUM(ISNULL(E.Amount,0)),0) [Amount], 'Expense Amount' [Type]  FROM Expense E
	INNER JOIN [Account] A ON E.AccountId = A.Id
	INNER JOIN [Bank] B ON A.BankId = B.Id	
	WHERE E.Active = 1 AND B.OrganizationId =  @OrganizationId AND A.Active = 1
	AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),@EndDate, 120)

	-- UNION
	
	--SELECT 4 [Id], ( @totalOffering - @totalExpene ) + @OpenningBalance, 'Available Balance' [Type]  FROM Expense E
	--INNER JOIN [Account] A ON E.AccountId = A.Id
	--INNER JOIN [Bank] B ON A.BankId = B.Id	
	--WHERE E.Active = 1  AND B.OrganizationId = @OrganizationId AND A.Active = 1
	--AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),@EndDate, 120)
	
 ) V3
 ORDER BY [OrderId]

 END
GO
/****** Object:  StoredProcedure [dbo].[uspBalanceSheetByAccount]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspBalanceSheetByAccount] ( @OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId


	
	SELECT 1 [OrderId], A.Name [Account], ISNULL(Amount,0) Amount, 'Openning Balance' [Type] FROM Account A
	LEFT OUTER JOIN
	(
		SELECT A.[Id], NAME [Account Name], Amount, 'Openning Balance' [Type] FROM Account A
			INNER JOIN [OpeningBalance] OP ON A.Id= OP.AccountId
			WHERE OP.OrgFiscalYearId = @OrgFiscalYearId AND A.Active = 1 AND OP.Active = 1
		) V ON A.ID = V.Id 
	WHERE A.Active = 1
	
	 UNION
	
	SELECT 2 [OrderId], A.Name [Account], ISNULL(Amount,0) Amount, 'Amount Received' [Type] FROM Account A
	LEFT OUTER JOIN
	(
		SELECT A.[Id], SUM(ISNULL(D.Amount,0)) [Amount], 'Amount Received' [Type] FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE D.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),@EndDate, 120)		
		GROUP BY A.Id
	)V ON A.ID = V.Id 
	WHERE A.Active = 1

	 UNION
	
	SELECT 3 [OrderId],A.Name [Account], ISNULL(Amount,0) Amount, 'Expense Amount' [Type] FROM Account A
	LEFT OUTER JOIN
	(
	SELECT A.[Id], SUM(ISNULL(E.Amount,0)) [Amount], 'Expense Amount' [Type]  FROM Expense E
	INNER JOIN [Account] A ON E.AccountId = A.Id
	INNER JOIN [Bank] B ON A.BankId = B.Id	
	WHERE E.Active = 1 AND B.OrganizationId =  @OrganizationId AND A.Active = 1
	AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),@EndDate, 120)
	GROUP BY A.Id
	)V ON A.ID = V.Id 
	WHERE A.Active = 1


 END
GO
/****** Object:  StoredProcedure [dbo].[uspBalanceSheetByQuarter]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspBalanceSheetByQuarter] ( @OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId

DECLARE @V1 TABLE ( [Quarter] VARCHAR(10) )

INSERT INTO @V1
SELECT CAST(year(@StartDate) AS char(4)) + '-Q1'
INSERT INTO @V1
SELECT CAST(year(@StartDate) AS char(4)) + '-Q2'
INSERT INTO @V1
SELECT CAST(year(@StartDate) AS char(4)) + '-Q3'
INSERT INTO @V1
SELECT CAST(year(@StartDate) AS char(4)) + '-Q4'


SELECT [OrderId], [Quarter], [Amount], [Type] FROM
(
	SELECT 1 [OrderId], V1.[Quarter], ISNULL(V2.[Amount],0) [Amount], 'Deposit' [Type] FROM
	(
		SELECT CAST(year(D.OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,D.OfferingDate) AS char(1)) [Quarter], SUM(D.Amount) [Amount], 'Deposit' [Type] FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE D.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),@EndDate, 120)	
		GROUP BY CAST(year(D.OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,D.OfferingDate) AS char(1))
	) V2 RIGHT OUTER JOIN @V1 AS V1 ON V1.[Quarter] = V2.[Quarter]

	

	 UNION

	 SELECT 2 [OrderId], V1.[Quarter], ISNULL(V2.[Amount],0) [Amount], 'Expense' [Type] FROM
	(
	 SELECT CAST(year(E.ExpenseDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,E.ExpenseDate) AS char(1)) [Quarter], SUM(E.Amount) [Amount], 'Expense' [Type]  FROM Expense E
		INNER JOIN [Account] A ON E.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE E.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),@EndDate, 120)
		GROUP BY CAST(year(E.ExpenseDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,E.ExpenseDate) AS char(1))
		) V2 RIGHT OUTER JOIN @V1 AS V1 ON V1.[Quarter] = V2.[Quarter]
	
 ) V3
 ORDER BY [Quarter], [Type]

 END
GO
/****** Object:  StoredProcedure [dbo].[uspGetCurrentYesarMonthlyMemberCount]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCurrentYesarMonthlyMemberCount]
as
BEGIN
DECLARE @V1 TABLE ( [Month] int )

INSERT INTO @V1
SELECT 1
INSERT INTO @V1
SELECT 2
INSERT INTO @V1
SELECT 3
INSERT INTO @V1
SELECT 4

INSERT INTO @V1
SELECT 5
INSERT INTO @V1
SELECT 6
INSERT INTO @V1
SELECT 7
INSERT INTO @V1
SELECT 8

INSERT INTO @V1
SELECT 9
INSERT INTO @V1
SELECT 10
INSERT INTO @V1
SELECT 11
INSERT INTO @V1
SELECT 12

SELECT V1.[Month], ISNULL( [MemberCount], 0) [Count] FROM
(
	SELECT ( DATEPART(MONTH, F.[MembershipStartDate]) ) [Month], COUNT(*) [MemberCount] FROM [dbo].[Family] F
	WHERE [MembershipStartDate] >= '01/01/'+ CAST(YEAR(getdate()) AS VARCHAR(5)) AND [MembershipStartDate] <= '12/31/'+ CAST(YEAR(getdate()) AS VARCHAR(5))
	GROUP BY DATEPART(MONTH, F.[MembershipStartDate])	
) V2

RIGHT OUTER JOIN @V1 AS V1 ON V1.[Month] = V2.[Month]
ORDER BY CAST(V1.[Month] AS INT) 


END
GO
/****** Object:  StoredProcedure [dbo].[uspGetCurrentYesarMonthlyVisitorCount]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetCurrentYesarMonthlyVisitorCount] 
as
BEGIN

DECLARE @V1 TABLE ( [Month] int )

INSERT INTO @V1
SELECT 1
INSERT INTO @V1
SELECT 2
INSERT INTO @V1
SELECT 3
INSERT INTO @V1
SELECT 4

INSERT INTO @V1
SELECT 5
INSERT INTO @V1
SELECT 6
INSERT INTO @V1
SELECT 7
INSERT INTO @V1
SELECT 8

INSERT INTO @V1
SELECT 9
INSERT INTO @V1
SELECT 10
INSERT INTO @V1
SELECT 11
INSERT INTO @V1
SELECT 12

SELECT V1.[Month], ISNULL( [VisitoCount], 0) [Count] FROM
(
	SELECT ( DATEPART(MONTH, F.[FirstVisitedDate] ) ) [Month], COUNT(*) [VisitoCount] FROM [dbo].[Family] F
	WHERE [FirstVisitedDate] >= '01/01/'+ CAST(YEAR(getdate()) AS VARCHAR(5)) AND [FirstVisitedDate] <= '12/31/'+ CAST(YEAR(getdate()) AS VARCHAR(5))
	AND  F.[MembershipStartDate] IS NULL
	GROUP BY DATEPART(MONTH, F.[FirstVisitedDate])	
) V2

RIGHT OUTER JOIN @V1 AS V1 ON V1.[Month] = V2.[Month]
ORDER BY CAST(V1.[Month] AS INT) 

END
GO
/****** Object:  StoredProcedure [dbo].[uspTransactionDetails]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspTransactionDetails] (@OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId

SELECT * FROM
(
	 SELECT A.Name [Account], SUM(D.Amount) [Amount], D.DepositDate [Date], 'Deposit' [Type] FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE D.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),@EndDate, 120)	
	 GROUP BY A.Name,  D.DepositDate
	 

	 UNION	 
	 
	 SELECT A.Name [Account], SUM(E.Amount ) [Amount], E.ExpenseDate [Date], 'Expense' [Type]  FROM Expense E
		INNER JOIN [Account] A ON E.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE E.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),@EndDate, 120)
		GROUP BY A.Name,  E.ExpenseDate

	 UNION

	 SELECT '' [Account], SUM(Amount) [Amount], OfferingDate [Date], 'Offering' FROM OrgOffering	 
	  WHERE OrganizationId = @OrganizationId  AND Active = 1
	 AND convert(varchar(10),OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),OfferingDate, 120) <= convert(varchar(10),@EndDate, 120) 
	 GROUP BY OfferingDate 
 ) V
 ORDER BY [DATE]

 END 
GO
/****** Object:  StoredProcedure [dbo].[uspTransactionDetailsByDates]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspTransactionDetailsByDates] (@OrganizationId INT,  @StartDate DATETIME, @EndDate DATETIME)
as
BEGIN

SELECT * FROM
(
	 SELECT A.Name [Account], SUM(D.Amount) [Amount], D.DepositDate [Date], 'Deposit' [Type] FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE D.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),@EndDate, 120)	
	 GROUP BY A.Name,  D.DepositDate
	 

	 UNION	 
	 
	 SELECT A.Name [Account], SUM(E.Amount ) [Amount], E.ExpenseDate [Date], 'Expense' [Type]  FROM Expense E
		INNER JOIN [Account] A ON E.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE E.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),@EndDate, 120)
		GROUP BY A.Name,  E.ExpenseDate

	 UNION

	 SELECT '' [Account], SUM(Amount) [Amount], OfferingDate [Date], 'Offering' FROM OrgOffering	 
	  WHERE OrganizationId = @OrganizationId  AND Active = 1
	 AND convert(varchar(10),OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),OfferingDate, 120) <= convert(varchar(10),@EndDate, 120) 
	 GROUP BY OfferingDate 
 ) V
 ORDER BY [DATE]

 END 
GO
/****** Object:  StoredProcedure [dbo].[uspTransactionMonthlySummaryByDates]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspTransactionMonthlySummaryByDates] (@OrganizationId INT,  @StartDate DATETIME, @EndDate DATETIME )
as
BEGIN

SELECT [Account], DATENAME(m, str([Mon]) + '/1/2011') [Month], [Amount], [Type] FROM
(
	SELECT A.Name [Account], DATEPART(month, D.DepositDate) [Mon], SUM(D.Amount) [Amount], 'Deposit' [Type] FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE D.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),@EndDate, 120)	
		GROUP BY DATEPART(month, D.DepositDate), A.Name 

	 UNION

	 SELECT A.Name [Account], DATEPART(month, E.ExpenseDate) [Mon], SUM(E.Amount) [Amount], 'Expense' [Type]  FROM Expense E
		INNER JOIN [Account] A ON E.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE E.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),@EndDate, 120)
		GROUP BY DATEPART(month, E.ExpenseDate), A.Name 

	 UNION

	 SELECT '' [Account], DATEPART(month, OfferingDate) [Mon], SUM(Amount) [Amount], 'Offering' [Type]  FROM OrgOffering WHERE OrganizationId = @OrganizationId  AND Active = 1
	 AND convert(varchar(10),OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),OfferingDate, 120) <= convert(varchar(10),@EndDate, 120) 
	  GROUP BY DATEPART(month, OfferingDate)
 ) v
 ORDER BY [Mon]

 END
GO
/****** Object:  StoredProcedure [dbo].[uspTransactionSummaryByMonthly]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspTransactionSummaryByMonthly] ( @OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId

SELECT [Account], DATENAME(m, str([Mon]) + '/1/2011') [Month], [Amount], [Type] FROM
(
	SELECT A.Name [Account], DATEPART(month, D.DepositDate) [Mon], SUM(D.Amount) [Amount], 'Deposit' [Type] FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE D.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),@EndDate, 120)	
		GROUP BY DATEPART(month, D.DepositDate), A.Name 

	 UNION

	 SELECT A.Name [Account], DATEPART(month, E.ExpenseDate) [Mon], SUM(E.Amount) [Amount], 'Expense' [Type]  FROM Expense E
		INNER JOIN [Account] A ON E.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE E.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),@EndDate, 120)
		GROUP BY DATEPART(month, E.ExpenseDate), A.Name 

	 UNION

	 SELECT '' [Account], DATEPART(month, OfferingDate) [Mon], SUM(Amount) [Amount], 'Offering' [Type]  FROM OrgOffering WHERE OrganizationId = @OrganizationId  AND Active = 1
	 AND convert(varchar(10),OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),OfferingDate, 120) <= convert(varchar(10),@EndDate, 120) 
	  GROUP BY DATEPART(month, OfferingDate)
 ) v
 ORDER BY [Mon]

 END
GO
/****** Object:  StoredProcedure [dbo].[uspTransactionSummaryByQuarter]    Script Date: 1/22/2022 8:14:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspTransactionSummaryByQuarter] ( @OrgFiscalYearId INT )
as
BEGIN

DECLARE @StartDate DATETIME;
DECLARE @EndDate DATETIME;
DECLARE @OrganizationId INT

SELECT @OrganizationId =OrganizationId, @StartDate= FiscalYearStart, @EndDate = FiscalYearEnd FROM OrgFiscalYear WHERE Id = @OrgFiscalYearId

SELECT [Account], [Quarter], [Amount], [Type] FROM
(
	
	SELECT A.Name [Account], CAST(year(D.OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,D.OfferingDate) AS char(1)) [Quarter], SUM(D.Amount) [Amount], 'Deposit' [Type] FROM Deposit D 
		INNER JOIN [Account] A ON D.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE D.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), D.OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),D.OfferingDate, 120) <= convert(varchar(10),@EndDate, 120)	
		GROUP BY CAST(year(D.OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,D.OfferingDate) AS char(1)), A.Name 
	

	 UNION

	 SELECT A.Name [Account], CAST(year(E.ExpenseDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,E.ExpenseDate) AS char(1)) [Quarter], SUM(E.Amount) [Amount], 'Expense' [Type]  FROM Expense E
		INNER JOIN [Account] A ON E.AccountId = A.Id
		INNER JOIN [Bank] B ON A.BankId = B.Id		
		WHERE E.Active = 1 AND B.OrganizationId = @OrganizationId AND A.Active = 1
		AND convert(varchar(10), E.ExpenseDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),E.ExpenseDate, 120) <= convert(varchar(10),@EndDate, 120)
		GROUP BY CAST(year(E.ExpenseDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,E.ExpenseDate) AS char(1)), A.Name 

	 UNION

	 SELECT '' [Account], CAST(year(OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,OfferingDate) AS char(1)) [Quarter], SUM(Amount) [Amount], 'Offering' [Type]  FROM OrgOffering WHERE OrganizationId = @OrganizationId  AND Active = 1
	 AND convert(varchar(10),OfferingDate, 120) >= convert(varchar(10),@StartDate, 120) AND  convert(varchar(10),OfferingDate, 120) <= convert(varchar(10),@EndDate, 120) 
	  GROUP BY CAST(year(OfferingDate) AS char(4)) + '-Q' + CAST(DATEPART(Quarter,OfferingDate) AS char(1))
 ) v
 ORDER BY [Quarter], [Type]

 END
GO
USE [master]
GO
ALTER DATABASE [CTWAlayamMgmt] SET  READ_WRITE 
GO
