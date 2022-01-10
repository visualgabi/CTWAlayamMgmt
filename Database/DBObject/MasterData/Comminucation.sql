
INSERT INTO [dbo].[OrgContributionMessage] 
(
[OrganizationId],
[Message],
[Active],	
[CreateUser],
[CreateDateTime],
[LastUpdateUser],
[LastUpdateDateTime]
) VALUES (1,'The IRS requires that individual contributions of $250.00 or more be listed separately to ensure tax deductibility. Contributions to Tamil Fatih Fellowship are tax deductible. Statements that are not questioned within 90 days will be considered to be accurate. No goods or services were provided by in connection with any contribution, or their value were insignificant or consisted entirely of intangible religious benefits.',1,'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE())

INSERT INTO [dbo].[MessageType] ([Id],[Name],[Description],[Active],[CreateUser],[CreateDateTime],[LastUpdateUser],[LastUpdateDateTime]) VALUES (1,'ContributionMessage', NULL,1,'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE())
INSERT INTO [dbo].[MessageType] ([Id],[Name],[Description],[Active],[CreateUser],[CreateDateTime],[LastUpdateUser],[LastUpdateDateTime]) VALUES (2,'ContributionEmailMessage', NULL,1,'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE())

INSERT INTO [dbo].[Message] 
(
[OrganizationId],
[MessageTypeId],
[Message],
[Active],	
[CreateUser],
[CreateDateTime],
[LastUpdateUser],
[LastUpdateDateTime]
) VALUES (1,1,'The IRS requires that individual contributions of $250.00 or more be listed separately to ensure tax deductibility. Contributions to Tamil Fatih Fellowship are tax deductible. Statements that are not questioned within 90 days will be considered to be accurate. No goods or services were provided by in connection with any contribution, or their value were insignificant or consisted entirely of intangible religious benefits.',1,'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE())

INSERT INTO [dbo].[Message] 
(
[OrganizationId],
[MessageTypeId],
[Message],
[Active],	
[CreateUser],
[CreateDateTime],
[LastUpdateUser],
[LastUpdateDateTime]
) VALUES (1,2,'Thank you for your contribution towards Dallas Tamil Church in @TaxYear. Last year''s donation receipt is attached. May the Lord increase you and bless you in @NextYear',1,'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE())


UPDATE [dbo].[Roles] SET Name = 'Pastor' WHERE Id = 2;

INSERT INTO [dbo].[Roles] (Id,[Name],[Active],[CreateUser],[CreateDateTime], [LastUpdateUser],[LastUpdateDateTime] ) VALUES (5,'Lead Pastor', 1, 'visualgabi@gmail.com', GETDATE(), 'visualgabi@gmail.com', GETDATE())

INSERT INTO [dbo].[MessageType] (iD,Name,Description,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES (3,'AccountCreation', NULL, 1, 'visualgabi@gmail.com', GETDATE(), 'visualgabi@gmail.com', GETDATE())
INSERT INTO [dbo].[MessageType] (iD,Name,Description,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES (4,'PasswordReset', NULL, 1, 'visualgabi@gmail.com', GETDATE(), 'visualgabi@gmail.com', GETDATE())
INSERT INTO [dbo].[MessageType] (iD,Name,Description,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES (5,'ChangePassword', NULL, 1, 'visualgabi@gmail.com', GETDATE(), 'visualgabi@gmail.com', GETDATE())
INSERT INTO [dbo].[MessageType] (iD,Name,Description,Active,CreateUser,CreateDateTime,LastUpdateUser,LastUpdateDateTime) VALUES (6,'AccountModification', NULL, 1, 'visualgabi@gmail.com', GETDATE(), 'visualgabi@gmail.com', GETDATE())

INSERT INTO [dbo].[Message] 
(
[OrganizationId],
[MessageTypeId],
[Message],
[Active],	
[CreateUser],
[CreateDateTime],
[LastUpdateUser],
[LastUpdateDateTime]
) VALUES (1,3,'<p>Norahsoft ChurchMgnt application account creation notification.</p>
<p></p>
<p></p>
<p>your Oraganization[@OrgName] created the user account in Norahsoft ChurchMgnt application.</p>
<p></p>
<p><B>Account information</B></p>
<p></p>
<table>
	<tr><th align=''right''>First Name :</th><td>@FirstName</td></tr>
	<tr><th align=''right''>Last Name :</th><td>@LastName</td></tr>
	<tr><th align=''right''>User Name :</th><td>@UserId</td></tr>
	<tr><th align=''right''>Password :</th><td>@Password</td></tr>
	<tr><th align=''right''>Role :</th><td>@Role</td></tr>
	<tr><th align=''right''>Web Site :</th><td>@WebSite</td></tr>
</table>
<p></p>
<p>After login into the system, you can change the password</p>
<p></p>
<p>Please contact Norahsoft - ChurchMgnt support center, If you need any help.</p>
<p></p>
<p>Thanks,</p>
<p>Norahsoft ChurchMgnt support Center.</p>',1,'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE())


INSERT INTO [dbo].[Message] 
(
[OrganizationId],
[MessageTypeId],
[Message],
[Active],	
[CreateUser],
[CreateDateTime],
[LastUpdateUser],
[LastUpdateDateTime]
) VALUES (1,4,'<p>Norahsoft ChurchMgnt password reset notification.</p>
<p></p>
<p></p>
<p>your Oraganization[@OrgName] reseted your account password in Norahsoft ChurchMgnt application.</p>
<p></p>
<p><B>Account information</B></p>
<p></p>
<table>	
	<tr><th align=''right''>User Name :</th><td>@UserId</td></tr>
	<tr><th align=''right''>New password :</th><td>@Password</td></tr>
	<tr><th align=''right''>Web Site :</th><td>@WebSite</td></tr>
</table>
<p></p>
<p>After login into the system, you can change the password</p>
<p></p>
<p>Please contact Norahsoft - ChurchMgnt support center, If you need any help.</p>
<p></p>
<p>Thanks,</p>
<p>Norahsoft ChurchMgnt support Center.</p>',1,'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE())

INSERT INTO [dbo].[Message] 
(
[OrganizationId],
[MessageTypeId],
[Message],
[Active],	
[CreateUser],
[CreateDateTime],
[LastUpdateUser],
[LastUpdateDateTime]
) VALUES (1,5,'<p>Norahsoft ChurchMgnt change password notification.</p>
<p></p>
<p></p>
<p>your login password got changed in Norahsoft ChurchMgnt application.</p>
<p></p>
<p><B>Account information</B></p>
<p></p>
<table>	
	<tr><th align=''right''>User Name :</th><td>@UserId</td></tr>
	<tr><th align=''right''>New password :</th><td>@Password</td></tr>
	<tr><th align=''right''>Web Site :</th><td>@WebSite</td></tr>
</table>
<p></p>
<p></p>
<p>Please contact Norahsoft - ChurchMgnt support center, If you need help.</p>
<p></p>
<p>Thanks,</p>
<p>Norahsoft ChurchMgnt support Center.</p>',1,'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE())

INSERT INTO [dbo].[Message] 
(
[OrganizationId],
[MessageTypeId],
[Message],
[Active],	
[CreateUser],
[CreateDateTime],
[LastUpdateUser],
[LastUpdateDateTime]
) VALUES (1,6,'<p>Norahsoft ChurchMgnt application account got modified.</p>
<p></p>
<p></p>
<p>your Oraganization[@OrgName] modified your user account in Norahsoft ChurchMgnt application.</p>
<p></p>
<p><B>Account information</B></p>
<p></p>
<table>
	<tr><th align=''right''>First Name :</th><td>@FirstName</td></tr>
	<tr><th align=''right''>Last Name :</th><td>@LastName</td></tr>
	<tr><th align=''right''>User Name :</th><td>@UserId</td></tr>	
	<tr><th align=''right''>Role :</th><td>@Role</td></tr>
	<tr><th align=''right''>Web Site :</th><td>@WebSite</td></tr>
</table>
<p></p>
<p>After login into the system, you can change the password</p>
<p></p>
<p>Please contact Norahsoft - ChurchMgnt support center, If you need any help.</p>
<p></p>
<p>Thanks,</p>
<p>Norahsoft ChurchMgnt support Center.</p>',1,'visualgabi@gmail.com', GETDATE(),'visualgabi@gmail.com',GETDATE())
