USE [DotsDb]
GO

DELETE [dbo].[UserRoles] WHERE UserId = 1
GO

INSERT INTO [dbo].[UserRoles] ([UserId],[RoleId],[ModifiedBy],[ModifiedOn],[CreatedBy],[CreatedOn])
VALUES
(1, 2, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE())
GO


