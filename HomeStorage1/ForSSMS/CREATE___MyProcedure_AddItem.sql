USE [HomeStorage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[MyProcedure_AddItem]
(
	@ItemName nvarchar(80),
	@TypeId int,
	@Tags nvarchar(500),
	@Description nvarchar(500),
	@ContainedBy int,
	@Private bit,
	@DateAdded date,
	@AddedItemId int OUT
)
AS
INSERT INTO Item
VALUES
(
	@ItemName,
	@TypeId,
	@Tags,
	@Description,
	@ContainedBy,
	@Private,
	@DateAdded
)

SET @AddedItemId = scope_identity()



GO


