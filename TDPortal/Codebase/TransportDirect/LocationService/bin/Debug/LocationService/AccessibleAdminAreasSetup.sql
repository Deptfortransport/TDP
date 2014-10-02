-- AccessibleAdminAreas test Set up

-- SQL deletes any test data from previous run (in case it failed)

USE [AtosAdditionalData]

BEGIN

	DELETE FROM [dbo].[AccessibleAdminAreas]
	WHERE [AdministrativeAreaCode] like '9999%'
	AND [DistrictCode] like '9999%'
	
	DELETE FROM [dbo].[AccessibleAdminAreas]
	WHERE [AdministrativeAreaCode] like '9999%'
	AND [DistrictCode] like 'ALL'
	
	INSERT INTO [dbo].[AccessibleAdminAreas] VALUES ('99991', '99991', 1, 1)
	INSERT INTO [dbo].[AccessibleAdminAreas] VALUES ('99991', '99992', 1, 0)
	INSERT INTO [dbo].[AccessibleAdminAreas] VALUES ('99991', '99993', 0, 1)
	INSERT INTO [dbo].[AccessibleAdminAreas] VALUES ('99992', 'ALL', 1, 1)

END