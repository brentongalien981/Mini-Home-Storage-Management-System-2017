USE [HomeStorage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[MyProcedure_AddUserItem]
(
	@UserId uniqueidentifier,
	@ItemId int
)
AS
INSERT INTO UserItem
VALUES
(
	@UserId,
	@ItemId
)



GO


