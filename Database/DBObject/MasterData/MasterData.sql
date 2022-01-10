INSERT INTO [dbo].[User] VALUES( 'visualgabi@gmail.com', 'Gabriel', 'Samuel', 'E1540028B92247BB00EC4A45FCDFA4B50C9462985756EBD2BC08619B025EABAA18C0CFB25FAEA436D2CE9FED78B4C39F4B79B8D5E2968FCF13B000BA805AC40D',
 0,0,NULL,1,'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
 
--UPDATE [dbo].[User] SET [CreateUser] = 1, [CreateDateTime] = GETDATE(), [LastUpdateUser] = 1, [LastUpdateDateTime] = GETDATE()


--INSERT INTO [dbo].[AspNetRoles] VALUES(1, 'Admin', 1,'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
----INSERT INTO [dbo].[Role] VALUES(2, 'President', 1,'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[Roles]  (Id, Name, [Active],[CreateUser],[CreateDateTime], [LastUpdateUser],[LastUpdateDateTime])   VALUES(3, 'Finance Officer', 1,'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[Roles]  (Id, Name, [Active],[CreateUser],[CreateDateTime], [LastUpdateUser],[LastUpdateDateTime])   VALUES(4, 'Business Administrator', 1,'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

--INSERT INTO [dbo].[AspNetRoles] VALUES(6, 'Event Manager', 1,'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
--INSERT INTO [dbo].[AspNetRoles] VALUES(7, 'Children''s Ministry Director', 1,'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

INSERT INTO [dbo].[Clients](
	[Name],
	[Secret],	
	[ApplicationType],	
	[RefreshTokenLifeTime],	
	[AllowedOrigin],
	[Active],
	[CreateUser],
	[CreateDateTime],
    [LastUpdateUser],
    [LastUpdateDateTime] 
	)
VALUES ('alayamWebApp', '5YV7M1r981yoGhELyB84aC+KiYksxZf1OY3++C1CtRM=', 1, 7200, 'http://localhost:32150', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE())

INSERT INTO [dbo].[Clients](
	[Name],
	[Secret],	
	[ApplicationType],	
	[RefreshTokenLifeTime],	
	[AllowedOrigin],
	[Active],
	[CreateUser],
	[CreateDateTime],
    [LastUpdateUser],
    [LastUpdateDateTime] 
	)
VALUES ('alayamWebAppDev', '5YV7M1r981yoGhELyB84aC+KiYksxZf1OY3++C1CtRM=', 1, 7200, 'http://localhost:8030', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE())

INSERT INTO [dbo].[AspNetRoles] VALUES(1, 'Admin');
INSERT INTO [dbo].[AspNetRoles] VALUES(2, 'Pastor');
INSERT INTO [dbo].[AspNetRoles] VALUES(3, 'President');
INSERT INTO [dbo].[AspNetRoles] VALUES(4, 'Business Administrator');
INSERT INTO [dbo].[AspNetRoles] VALUES(5, 'Finance Officer');
INSERT INTO [dbo].[AspNetRoles] VALUES(6, 'Event Manager');
INSERT INTO [dbo].[AspNetRoles] VALUES(7, 'Children''s Ministry Director');

INSERT INTO AspNetUserRoles VALUES(1,1,null);
INSERT INTO AspNetUserRoles VALUES(1,3,1);

INSERT INTO [dbo].[Country] VALUES('India', 'India', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[Country] VALUES('USA', 'USA', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[Country] VALUES('Canada', 'Canada', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

--SELECT * FROM [Country]

INSERT INTO [dbo].[State] VALUES(1,'Tamil Nadu', 'India''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[State] VALUES(1,'Andhra Pradesh', 'India''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[State] VALUES(1, 'Kerala', 'India''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

INSERT INTO [dbo].[State] VALUES(2,'TX', 'USA''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[State] VALUES(2,'NY', 'USA''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[State] VALUES(2, 'IL', 'USA''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[State] VALUES(2, 'OH', 'USA''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());


INSERT INTO [dbo].[State] VALUES(3, 'ON', 'Canada''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());


INSERT INTO [dbo].[Language] VALUES('English', 'Available Languages', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[Language] VALUES('Tamil', 'Available Languages', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[Language] VALUES('Hindi', 'Available Languages', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[Language] VALUES('Bengali', 'Available Languages', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[Language] VALUES('Bengali', 'Available Languages', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[Language] VALUES('Malayalam', 'Available Languages', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());


INSERT INTO [dbo].[EthnicOrigin] VALUES('N/A', 'Not Applicable ', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[EthnicOrigin] VALUES('Indian', 'List the EthnicOrigin in the selected EthnicOrigin', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[EthnicOrigin] VALUES('American', 'List the EthnicOrigin in the selected EthnicOrigin', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[EthnicOrigin] VALUES('Asian', 'List the EthnicOrigin in the selected EthnicOrigin', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[EthnicOrigin] VALUES('Canadian', 'List the EthnicOrigin in the selected EthnicOrigin', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

INSERT INTO [dbo].[Denomination] VALUES('Non-Denomination', 'List the EthnicOrigin in the selected EthnicOrigin', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[Denomination] VALUES('Baptists', 'List the EthnicOrigin in the selected EthnicOrigin', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[Denomination] VALUES('Methodist', 'List the EthnicOrigin in the selected EthnicOrigin', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());


INSERT INTO [dbo].[EmailType] VALUES(1,'User Creation', 'Email Type', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[EmailType] VALUES(3,'Reset Password by Admin', 'Email Type', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[EmailType] VALUES(2,'Forgot Password', 'Email Type', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

select * from [dbo].[EmailType]

SELECT * FROM [dbo].[EmailContent]

INSERT INTO [dbo].[EmailContent] VALUES
(1, NULL, 'Wellcome to ICMSS application',
'<p>Wellcome to ICMSS application.</p>
<p></p>
<p></p>
<p>your Organization[@OrgName] created a new account in ICMSS application for you. Now you able to access ICMSS with below information. When you login first, you will asked to reset 

the password</p>
<p></p>
<p><B>Account information</B></p>
<p></p>
<table>
	</tr><th align=''right''>First Name :</th><td>@FirstName</td></tr>
	</tr><th align=''right''>Last Name :</th><td>@LastName</td></tr>
	</tr><th align=''right''>UserId :</th><td>@UserId</td></tr>
	</tr><th align=''right''>Password :</th><td>@Password</td></tr>
	</tr><th align=''right''>Web Site :</th><td>@WebSite</td></tr>
</table>
<p></p>
<p>Please contact DYIl - ICMSS support center, If you need help.</p>
<p></p>
<p>Thanks,</p>
<p>DYIL - ICMSS support Center.</p>', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE() );


INSERT INTO [dbo].[EmailContent] VALUES
(3, NULL, 'ICMSS application Account Notification', '<p>ICMSS application Account Notification.</p>
<p></p>
<p></p>
<p>your Oraganization[@OrgName] reseted your account password in ICMSS application.</p>
<p></p>
<p><B>Account information</B></p>
<p></p>
<table>
	</tr><th align=''right''>First Name :</th><td>@FirstName</td></tr>
	</tr><th align=''right''>Last Name :</th><td>@LastName</td></tr>
	</tr><th align=''right''>UserId :</th><td>@UserId</td></tr>
	</tr><th align=''right''>Password :</th><td>@Password</td></tr>
	</tr><th align=''right''>Web Site :</th><td>@WebSite</td></tr>
</table>
<p></p>
<p>When you login next time in the ICMSS system, use the above information and will be asked to reset your password, Since your password got reseted by Admin</p>
<p></p>
<p>Please contact DYIl - ICMSS support center, If you need help.</p>
<p></p>
<p>Thanks,</p>
<p>DYIL - ICMSS support Center.</p>', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE() );


INSERT INTO [EmailContent]  VALUES
(2, NULL, 'ICMSS System Forgot Password','<p>Wellcome to ICMSS application.</p>
<p></p>
<p></p>
<p>ICMSS System reseted your account password in ICMSS application.</p>
<p></p>
<p><B>Account information</B></p>
<p></p>
<table>	
	</tr><th align=''right''>UserId :</th><td>@UserId</td></tr>
	</tr><th align=''right''>Password :</th><td>@Password</td></tr>
	</tr><th align=''right''>Web Site :</th><td>@WebSite</td></tr>
</table>
<p></p>
<p>When you login next time in the ICMSS system, use the above information and will be asked to reset your password, Since your password got reseted by ICMSS system</p>
<p></p>
<p>Please contact DYIl - ICMSS support center, If you need help.</p>
<p></p>
<p>Thanks,</p>
<p>DYIL - ICMSS support Center.</p>', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE() );

SELECT * FROM [Organization]

INSERT INTO [Organization] (Name, Discription, DenominationId,EthnicOriginId,Website,PrimaryLanguageId,SecondaryLanguageId, Address1,Address2,Address3,City, CountryId, [StateId],ZipCode,Phone1,Phone2,EmailId1,EmailId2,
Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)  VALUES( 'Tamil Fatih Fellowship', NULL, 1, 1, 'WWW.TamilFatihFellowship.com', 1, 2, 'First United Methodist Church',  NULL, NULL, 'Richardson', 
2,1, '75080', '214-529-7886', NULL, 'kasthuri_raju@yahoo.com', NULL,1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE() )

INSERT INTO [Organization] (Name, Discription, DenominationId,EthnicOriginId,Website,PrimaryLanguageId,SecondaryLanguageId, Address1,Address2,Address3,City, CountryId, [StateId],ZipCode,Phone1,Phone2,EmailId1,EmailId2,
Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)  VALUES( 'Tamil Fatih Fellowship', NULL, 1, 1, 'WWW.DallasTamilChurch.com', 1, 2, '12345 Irving Bible Chruch',  NULL, NULL, 'Irving', 
2,1, '75080', '214-529-7886', NULL, 'Henry1@gmail.com', NULL,1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE() )

INSERT INTO [dbo].[AspNetUsers]
           ([Email]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEndDateUtc]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[UserName]
           ,[FirstName]
           ,[LastName])
     VALUES
           ('visualgabi@gmail.com'
           ,0
           ,'AH+cQFRaD2Q0V7IZdYR1JZB0PljGjflWDFcKwrbgvNsga05caLzEHhfxy/nJ3gkhBg=='
           ,'50f4907d-382c-4106-86bc-e24b1fb91b84'
           ,'7604053565'
           ,0
           ,0
           ,NULL
           ,0
           ,0
           ,'visualgabi@gmail.com'
           ,'Gabriel'
           ,'Samuel')
GO

INSERT INTO [dbo].[AspNetUsers]
           ([Email]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEndDateUtc]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[UserName]
           ,[FirstName]
           ,[LastName])
     VALUES
           ('gabisusila@gmail.com'
           ,0
           ,'AH+cQFRaD2Q0V7IZdYR1JZB0PljGjflWDFcKwrbgvNsga05caLzEHhfxy/nJ3gkhBg=='
           ,'50f4907d-382c-4106-86bc-e24b1fb91b84'
           ,'7604053565'
           ,0
           ,0
           ,NULL
           ,0
           ,0
           ,'gabisusila@gmail.com'
           ,'Susila'
           ,'David')
GO


INSERT INTO [UserRole] VALUES(1,1,null,1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE())
INSERT INTO [UserRole] VALUES(2,2,1,1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE())

INSERT INTO [dbo].[MembershipStatus] VALUES(1,'Regular', 'USA''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[MembershipStatus] VALUES(2,'Irregular', 'USA''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[MembershipStatus] VALUES(3, 'Died', 'USA''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[MembershipStatus] VALUES(4, 'Discontinued', 'USA''s state', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

INSERT INTO [Family] (Name, FirstVisitedDate, MembershipStartDate, BranchId, EthnicOriginId,PrimaryLanguageId,SecondaryLanguageId, MembershipStatusId, Address1,Address2,Address3,City, CountryId, [StateId],ZipCode,Phone1,Phone2,EmailId1,EmailId2,
Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)  VALUES( 'Samuel Family', getdate(), getdate(), 1, 1, 1, 2,1, '12345 Irving Bible Chruch',  NULL, NULL, 'Irving', 
2,1, '75080', '214-529-7886', NULL, 'Henry1@gmail.com', NULL,1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE() )

INSERT INTO [dbo].[FamilyMember] ([FamilyId], FirstName,MiddleName,LastName,Initial,Gender,
DOB, Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)  
VALUES( 1, 'Gabriel',null, 'Samuel', null,'M','05/19/1979', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE() )

INSERT INTO [dbo].[FamilyMember] ([FamilyId], FirstName,MiddleName,LastName,Initial,Gender,
DOB, Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)  
VALUES( 1, 'Susila',null, 'David', null,'F','08/01/1987', 1,'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE() )

INSERT INTO [dbo].[FamilyMember] ([FamilyId], FirstName,MiddleName,LastName,Initial,Gender,
DOB, Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)  
VALUES( 1, 'Ian',null, 'Samuel', null,'M','11/14/2011', 1,'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE() )

INSERT INTO [dbo].[FamilyMember] ([FamilyId], FirstName,MiddleName,LastName,Initial,Gender,
DOB, Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)  
VALUES( 1, 'Nathan',null, 'Samuel', null,'M','04/14/2015', 1,'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE() )

INSERT INTO [dbo].[Currency] (Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES(1,'$', 'US Dollor', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

 select * from Organization

 SELECT * FROM [OrgFiscalYear] (

 INSERT INTO [OrgFiscalYear] (OrganizationId,FiscalYear,FiscalYearStart,FiscalYearEnd,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1, 2015, '01/01/2015', '12/31/2015', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

 INSERT INTO OfferingType (Id, Name, [Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1, 'Tithes offering', 'Tithes offering', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

 INSERT INTO OfferingType (Id, Name, [Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (2, 'Freewill offering', 'Freewill offering', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

 INSERT INTO FundType (Id, Name, [Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1, 'Building Fund', 'Building Fund', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
 
  INSERT INTO FundType (Id, Name, [Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (2, 'Mission Fund', 'Mission Fund', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

 INSERT INTO FundType (Id, Name, [Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (3, 'Internal Management fund', 'Internal Management fund', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

 INSERT INTO FundType (Id, Name, [Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (4, 'Homeless Mission Fund', 'Homeless Mission Fund', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE())

 INSERT INTO FundType (Id, Name, [Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (5, 'Childcare Management Fund', 'Homeless Mission Fund', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

 SELECT * FROM FundType

 INSERT INTO OrgFiscalYearBudget (OrgFiscalYearId,FundTypeId,Amound,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1,1,50000.00, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

 INSERT INTO OrgFiscalYearBudget (OrgFiscalYearId,FundTypeId,Amound,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1,2,40000.00, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

  INSERT INTO OrgFiscalYearBudget (OrgFiscalYearId,FundTypeId,Amound,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1,3,30000.00, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

 INSERT INTO FamilyPledge (FamilyId,OrgFiscalYearId,FundTypeId,Amount,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1,1,1,3000.00, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

  INSERT INTO FamilyPledge (FamilyId,OrgFiscalYearId,FundTypeId,Amount,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1,1,2,3000.00, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

INSERT INTO FamilyPledge (FamilyId,OrgFiscalYearId,FundTypeId,Amount,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1,1,3,100.00, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

SELECT * FROM [dbo].[Family]

select * from [dbo].[Currency]

select * from OrgFiscalYearBudget

select * from OrgFiscalYear
select * from Organization


INSERT INTO [dbo].[FiscalYear] VALUES('2015', '2015', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[FiscalYear] VALUES('2016', '2016', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[FiscalYear] VALUES('2017', '2017', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[FiscalYear] VALUES('2018', '2018', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[FiscalYear] VALUES('2019', '2019', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[FiscalYear] VALUES('2020', '2020', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[FiscalYear] VALUES('2021', '2021', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[FiscalYear] VALUES('2022', '2022', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[FiscalYear] VALUES('2023', '2023', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[FiscalYear] VALUES('2024', '2024', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[FiscalYear] VALUES('2025', '2025', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());


UPDATE [FiscalYear] SET Name = 'Fiscal year 2015' WHERE NAME = '2015'
UPDATE [FiscalYear] SET Name = 'Fiscal year 2016' WHERE NAME = '2016'
UPDATE [FiscalYear] SET Name = 'Fiscal year 2017' WHERE NAME = '2017'
UPDATE [FiscalYear] SET Name = 'Fiscal year 2018' WHERE NAME = '2018'
UPDATE [FiscalYear] SET Name = 'Fiscal year 2019' WHERE NAME = '2019'
UPDATE [FiscalYear] SET Name = 'Fiscal year 2020' WHERE NAME = '2020'
UPDATE [FiscalYear] SET Name = 'Fiscal year 2021' WHERE NAME = '2021'
UPDATE [FiscalYear] SET Name = 'Fiscal year 2022' WHERE NAME = '2022'
UPDATE [FiscalYear] SET Name = 'Fiscal year 2023' WHERE NAME = '2023'
UPDATE [FiscalYear] SET Name = 'Fiscal year 2024' WHERE NAME = '2024'
UPDATE [FiscalYear] SET Name = 'Fiscal year 2025' WHERE NAME = '2025'


INSERT INTO FamilyPledge(FamilyId,OrgFiscalYearId,FundTypeId,Amount,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES(1,1,1,3000, 1, 'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE());

 INSERT INTO FamilyPledge(FamilyId,OrgFiscalYearId,FundTypeId,Amount,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES(1,1,1,3000, 1, 'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE());

 INSERT INTO OrgOffering(OrganizationId,FamilyMemberId,OfferingTypeId,FundTypeId,PaymentTypeId,Amount,OfferingDate,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1,1,1,1,2,500, GETDATE(),1, 'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE());

 INSERT INTO OrgOffering(OrganizationId,FamilyMemberId,OfferingTypeId,FundTypeId,PaymentTypeId,Amount,OfferingDate,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1,1,2,2,2,500, GETDATE(),1, 'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE());

 INSERT INTO OrgOffering(OrganizationId,FamilyMemberId,OfferingTypeId,FundTypeId,PaymentTypeId, Amount,OfferingDate,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES (1,2,1,1,2,600, GETDATE(),1, 'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE());

INSERT INTO [dbo].[PaymentType](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(1,'Cash', 'Payment Type is cash', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[PaymentType](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(2,'Cheque', 'Payment Type is cheque', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
INSERT INTO [dbo].[PaymentType](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(3, 'Online', 'Payment Type is cheque', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());


 SELECT * FROM OrgOffering

 SELECT * FROM [FamilyMember]


INSERT INTO [dbo].[ExpenseType](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(1,'Salaries and Allowances', 'Salaries and Allowances', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[ExpenseType](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(2,'Supplies', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[ExpenseType](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(3,'Other Programs', 'Other Programs', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[ExpenseType](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(4,'Building and Utilities Expense', 'Building and Utilities Expense', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[ExpenseType](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(5,'Mission Work', 'Mission Work', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  


INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(1,1,'Salary', 'Salaries and Allowances', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(2,1,'FICA and Medicare Taxes', 'Salaries and Allowances', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(3,1,'Employee benefits—Concordia Plans', 'Employee benefits—Concordia Plans', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(4,1,'FICA and Medicare Taxes', 'Employee benefits—Concordia Plans', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(5,1,'Housing Allowance', 'Employee benefits—Concordia Plans', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(6,1,'Automobile', 'Employee benefits—Concordia Plans', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(7,1,'Continuing Education Allowance', 'Employee benefits—Concordia Plans', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(8,1,'Utilities allowance', 'Employee benefits—Concordia Plans', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(9,1,'Conferences and workshops', 'Employee benefits—Concordia Plans', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(10,1,'Guest pastors, speakers', 'Employee benefits—Concordia Plans', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  

INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(11,2,'Office supplies', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(12,2,'Altar supplies', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(13,2,'Service bulletins', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(14,2,'Hymnals', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(15,2,'Choir music', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(16,2,'Publicity and signs', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(17,2,'Radio-TV', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(18,2,'Offering envelopes', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(19,2,'School supplies', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(20,2,'Postage', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(21,2,'Sunday school', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(22,2,'Vacation Bible school', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(23,2,'Adult Bible study', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(24,2,'Church library', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(25,2,'Portals of Prayer', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(26,2,'A/V materials', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(27,2,'School curriculum', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(28,2,'Rental', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(29,2,'Maintenance', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(30,2,'Major repairs', 'Supplies', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  

INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(31,3,'Leadership training', 'Other Programs', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(32,3,'Social ministry', 'Other Programs', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  

INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(33,3,'Telephone', 'Building and Utilities Expense', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(34,3,'Rental', 'Building and Utilities Expense', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(35,3,'Utilities—church', 'Building and Utilities Expense', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(36,3,'Insurance', 'Building and Utilities Expense', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(37,3,'Maintenance and repairs', 'Building and Utilities Expense', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(38,3,'Repairs', 'Building and Utilities Expense', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  

INSERT INTO [dbo].[SubExpenseType](Id,ExpenseTypeId,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(39,4,'Mission work—local', 'Mission Work', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  

 INSERT INTO [dbo].[Expense](OrganizationId,ExpenseTypeId,SubExpenseTypeId,PaymentTypeId,Amount,TransactionNumber,Notes,ExpenseDate,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES(1,1,1,1,450, '01989787', null, GETDATE(),1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE()); 

 INSERT INTO [dbo].[Expense](OrganizationId,ExpenseTypeId,SubExpenseTypeId,PaymentTypeId,Amount,TransactionNumber,Notes,ExpenseDate,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES(1,1,1,2,500, '01989784', null, GETDATE(),1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE()); 

 INSERT INTO [dbo].[Expense](OrganizationId,ExpenseTypeId,SubExpenseTypeId,PaymentTypeId,Amount,TransactionNumber,Notes,ExpenseDate,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime)
 VALUES(1,1,2,1,320.40, '01989111', null, GETDATE(),1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE()); 


 SELECT * FROM [Expense]

 INSERT INTO [dbo].[Currency] (Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(2,'₹', 'Indian Rupee', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());
 INSERT INTO [dbo].[Currency] (Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(3,'C$', 'Indian Rupee', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());

 select * from Organization

 select * from [Currency]

 select * from [users] where username = 'visualgabi@gmail.com11'

 select * from UserRoles where userid = 24

 SELECT * FROM FamilyMember


 INSERT INTO [dbo].[Relationship](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(1,'Husband', 'head of the family', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[Relationship](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(2,'Wife', 'head of the family', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[Relationship](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(3,'Baby', 'head of the family', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[Relationship](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(4,'Kid', 'head of the family', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[Relationship](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(5,'Youth', 'head of the family', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[Relationship](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(6,'Adult', 'head of the family', 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  


 

 INSERT INTO [dbo].[FiscalYearStartAndEndMonth](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(1,'Jan-Dec', NULL, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[FiscalYearStartAndEndMonth](Id,Name,[Description],Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(2,'Apr-Mar', NULL, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  

 INSERT INTO [dbo].[FiscalYearQuarter](Id,Name,FiscalYearStartAndEndMonthId,StartMonth,EndMonth,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(1,'Q1', 1, 1,3, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[FiscalYearQuarter](Id,Name,FiscalYearStartAndEndMonthId,StartMonth,EndMonth,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(2,'Q2', 1, 4, 6, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[FiscalYearQuarter](Id,Name,FiscalYearStartAndEndMonthId,StartMonth,EndMonth,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(3,'Q3', 1, 7,9, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[FiscalYearQuarter](Id,Name,FiscalYearStartAndEndMonthId,StartMonth,EndMonth,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(4,'Q4', 1, 10, 12,1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  

 INSERT INTO [dbo].[FiscalYearQuarter](Id,Name,FiscalYearStartAndEndMonthId,StartMonth,EndMonth,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(5,'Q1', 2, 4,6, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[FiscalYearQuarter](Id,Name,FiscalYearStartAndEndMonthId,StartMonth,EndMonth,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(6,'Q2', 2, 7, 9, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[FiscalYearQuarter](Id,Name,FiscalYearStartAndEndMonthId,StartMonth,EndMonth,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(7,'Q3', 2, 10,12, 1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());  
 INSERT INTO [dbo].[FiscalYearQuarter](Id,Name,FiscalYearStartAndEndMonthId,StartMonth,EndMonth,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES(8,'Q4', 2, 1, 3,1, 'visualgabi@gmail.com',GETDATE(),'visualgabi@gmail.com',GETDATE());