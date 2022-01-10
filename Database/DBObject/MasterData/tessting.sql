USE [MySample]
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


