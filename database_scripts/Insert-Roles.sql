USE [DotsDb]
GO

INSERT INTO [dbo].[Roles]([Name],[ModifiedBy],[ModifiedOn],[CreatedBy],[CreatedOn])
VALUES
('Administrator','lukasz.maslanka',GETDATE(),'lukasz.maslanka',GETDATE()),
('Editor','lukasz.maslanka',GETDATE(),'lukasz.maslanka',GETDATE()),
('Updater','lukasz.maslanka',GETDATE(),'lukasz.maslanka',GETDATE())
GO


