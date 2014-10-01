﻿CREATE PROCEDURE [dbo].[AddContent]
(
	@Group varchar(100),
	@Language char(2),
	@ControlName varchar(60),
	@PropertyName varchar(100),
	@ContentValue text
)
AS
BEGIN

	DECLARE @GroupId int
   
    --Look up the GroupId:
    SELECT @GroupId = GroupId 
      FROM [dbo].ContentGroup
     WHERE [Name] = @Group

    --Check the group:
    IF @GroupId IS NULL
    	BEGIN
    		RAISERROR ('%s is an invalid group', 16, 1, @Group)
			RETURN 0
    	END

	IF EXISTS (SELECT * FROM [dbo].Content 
					   WHERE Content.PropertyName = @PropertyName 
						 AND Content.ControlName = @ControlName 
						 AND Content.CultureCode = @Language 
						 AND Content.GroupId = @GroupId )
		BEGIN
			UPDATE [dbo].Content
			   SET [ContentValue] = @ContentValue
			 WHERE Content.PropertyName = @PropertyName 
			   AND Content.ControlName = @ControlName 
			   AND Content.CultureCode = @Language 
			   AND Content.GroupId = @GroupId 
		END
	ELSE
	   BEGIN
			INSERT INTO [dbo].Content (GroupId, CultureCode, ControlName, PropertyName, ContentValue)
        	VALUES ( @GroupId, @Language, @ControlName, @PropertyName, @ContentValue)
	   END

END
