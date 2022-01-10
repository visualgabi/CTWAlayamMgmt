INSERT INTO [dbo].[Roles] (Id,name,CreateUser,LastUpdateUser)  VALUES(1, 'Admin','visualgabi@gmail.com','visualgabi@gmail.com');
INSERT INTO [dbo].[Roles] (Id,name,CreateUser,LastUpdateUser)  VALUES(2, 'Pastor','visualgabi@gmail.com','visualgabi@gmail.com');
--INSERT INTO [dbo].[Roles] VALUES(2, 'Pastor');
--INSERT INTO [dbo].[Roles] VALUES(3, 'President');
--INSERT INTO [dbo].[Roles] VALUES(4, 'Business Administrator');
--INSERT INTO [dbo].[Roles] VALUES(5, 'Finance Officer');
--INSERT INTO [dbo].[Roles] VALUES(6, 'Event Manager');
--INSERT INTO [dbo].[Roles] VALUES(7, 'Children''s Ministry Director');

INSERT INTO [dbo].[Users]
           (		    
			[UserName],    
			[PasswordHash],
			[FirstName],
			[LastName],						
			[CreateUser],
			LastUpdateUser			
         )
     VALUES
           ('visualgabi@gmail.com'           
           ,'AH+cQFRaD2Q0V7IZdYR1JZB0PljGjflWDFcKwrbgvNsga05caLzEHhfxy/nJ3gkhBg=='           
           ,'Gabriel'
           ,'Samuel'
		   , 'visualgabi@gmail.com',
		    'visualgabi@gmail.com'
		   )
GO

SELECT * FROM [Users]

INSERT INTO [dbo].[Users]
           (		    
			[UserName],    
			[PasswordHash],
			[FirstName],
			[LastName],						
			[CreateUser],
			LastUpdateUser			
         )
     VALUES
           ('gabisusila@gmail.com'           
           ,'AH+cQFRaD2Q0V7IZdYR1JZB0PljGjflWDFcKwrbgvNsga05caLzEHhfxy/nJ3gkhBg=='           
           ,'Susila'
           ,'David'
		   , 'visualgabi@gmail.com',
		    'visualgabi@gmail.com'
		   )
GO


INSERT INTO UserRoles(UserId, RoleId, OrganizationId, CreateUser, LastUpdateUser)
VALUES (2,1,null, 'visualgabi@gmail.com','visualgabi@gmail.com')

INSERT INTO UserRoles(UserId, RoleId, OrganizationId, CreateUser, LastUpdateUser)
VALUES (3,2,1, 'visualgabi@gmail.com','visualgabi@gmail.com')