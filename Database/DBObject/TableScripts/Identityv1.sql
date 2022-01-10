--DROP TABLE [Roles]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/24/2015 11:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
    [Id] INT  NOT NULL,
    [Name] [nvarchar](256) NOT NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL,  
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 


-- DROP TABLE [Users]
 
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/24/2015 11:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
    [Id] INT NOT NULL IDENTITY,
    [UserName] [nvarchar](256) NULL,    
    [PasswordHash] [nvarchar](max) NULL,
	[FirstName] [NVARCHAR](256) NOT NULL,
	[LastName] [NVARCHAR](256) NOT NULL,
    [RowTimeStamp] TIMESTAMP NOT NULL,    
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL    
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
 
 --DROP TABLE [UserClaims]
 GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 6/24/2015 11:07:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UserId] [INT] NOT NULL REFERENCES Users(Id),
    [ClaimType] [nvarchar](max) NULL,
    [ClaimValue] [nvarchar](max) NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL,  
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL
 CONSTRAINT [PK_dbo.UserClaims] PRIMARY KEY CLUSTERED 

(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

 GO
 /****** Object:  Table [dbo].[UserRoles]    Script Date: 6/24/2015 11:07:20 AM ******/
--DROP TABLE [UserRoles]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [INT] NOT NULL IDENTITY,
	[UserId] [INT] NOT NULL REFERENCES Users(Id),
    [RoleId] [INT] NOT NULL REFERENCES Roles(Id),
	[OrganizationId] [INT] NULL REFERENCES Organization(Id),
	[Active] BIT NOT NULL DEFAULT 1,	
	[CreateUser] [nvarchar](256) NOT NULL,
	[CreateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
    [LastUpdateUser] [nvarchar](256) NOT NULL,
    [LastUpdateDateTime] DATETIME DEFAULT GETDATE()  NOT NULL,
	[RowTimeStamp] TIMESTAMP NOT NULL
 CONSTRAINT [PK_dbo.UserRoles] PRIMARY KEY   CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 