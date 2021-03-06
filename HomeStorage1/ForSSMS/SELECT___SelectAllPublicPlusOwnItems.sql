USE [HomeStorage]
GO
/****** Object:  StoredProcedure [dbo].[SelectAllPublicPlusOwnItems]    Script Date: 2/14/2017 11:03:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[SelectAllPublicPlusOwnItems]
(
	@ItemId nvarchar(50),
	@ItemName nvarchar(80),
	@ItemTypeName nvarchar(50),
	@Tags nvarchar(500),
	@Description nvarchar(500),
	@ContainedBy nvarchar(50),
	@Private nvarchar(50),
	@UserId uniqueidentifier
)
AS
--SELECT UserName AS "Owner", UserItem.ItemId AS "Item Id", ItemName, Tags, Description, ContainedBy AS "Container Id", Private, ItemTypeName AS "Item Type"
--	FROM aspnet_Users INNER JOIN UserItem  ON aspnet_Users.UserId = UserItem.UserId
--	INNER JOIN Item ON UserItem.ItemId = Item.ItemId
--	INNER JOIN ItemType ON Item.TypeId = ItemType.ItemTypeId
--		WHERE UserItem.ItemId LIKE @ItemId  
--		AND ItemName LIKE @ItemName  
--		AND ItemTypeName LIKE @ItemTypeName  
--		AND Tags LIKE @Tags
--		AND Description LIKE @Description
--		AND ContainedBy LIKE @ContainedBy
--		AND Private LIKE 0  
--		AND aspnet_Users.UserId != @UserId

SELECT UserName AS "Owner", ItemId AS "Item Id", ItemName AS "Item Name", MyTags AS "Tags", MyDescription AS "Description", MyContainedBy AS "Container Id", Private, ItemTypeName AS "Item Type"
	FROM 
	(
		SELECT aspnet_Users.UserId, UserName, UserItem.ItemId, ItemName, ISNULL(Tags, '') AS MyTags, ISNULL(Description, '') AS MyDescription, ISNULL(ContainedBy, '') AS MyContainedBy, Private, ItemTypeName
			FROM aspnet_Users INNER JOIN UserItem  ON aspnet_Users.UserId = UserItem.UserId
			INNER JOIN Item ON UserItem.ItemId = Item.ItemId
			INNER JOIN ItemType ON Item.TypeId = ItemType.ItemTypeId
	) MyTable
		WHERE ItemId LIKE @ItemId  
		AND ItemName LIKE @ItemName  
		AND ItemTypeName LIKE @ItemTypeName  
		AND MyTags LIKE @Tags
		AND MyDescription LIKE @Description
		AND MyContainedBy LIKE @ContainedBy
		AND Private LIKE 0  
		AND MyTable.UserId != @UserId




UNION

SELECT UserName AS "Owner", ItemId AS "Item Id", ItemName AS "Item Name", MyTags AS "Tags", MyDescription AS "Description", MyContainedBy AS "Container Id", Private, ItemTypeName AS "Item Type"
	FROM 
	(
		SELECT aspnet_Users.UserId, UserName, UserItem.ItemId, ItemName, ISNULL(Tags, '') AS MyTags, ISNULL(Description, '') AS MyDescription, ISNULL(ContainedBy, '') AS MyContainedBy, Private, ItemTypeName
			FROM aspnet_Users INNER JOIN UserItem  ON aspnet_Users.UserId = UserItem.UserId
			INNER JOIN Item ON UserItem.ItemId = Item.ItemId
			INNER JOIN ItemType ON Item.TypeId = ItemType.ItemTypeId
	) MyTable
		WHERE ItemId LIKE @ItemId  
		AND ItemName LIKE @ItemName  
		AND ItemTypeName LIKE @ItemTypeName  
		AND MyTags LIKE @Tags
		AND MyDescription LIKE @Description
		AND MyContainedBy LIKE @ContainedBy
		AND MyTable.UserId = @UserId
GO
