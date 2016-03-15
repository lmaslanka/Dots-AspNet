USE [DotsDb]
GO

--DELETE [dbo].[UserRoles] WHERE UserId = 1
--GO

INSERT INTO [dbo].[UserRoles] ([UserId],[RoleId],[ModifiedBy],[ModifiedOn],[CreatedBy],[CreatedOn])
VALUES
(1, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(2, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(3, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(4, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(5, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(6, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(7, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(8, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(9, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(10, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(11, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(12, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE()),
(13, 1, 'lukasz.maslanka', GETDATE(), 'lukasz.maslanka', GETDATE())

GO