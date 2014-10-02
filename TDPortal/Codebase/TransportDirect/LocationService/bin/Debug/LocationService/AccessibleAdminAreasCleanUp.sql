-- AccessibleAdminAreas test Clean up

-- SQL deletes any test data

USE [AtosAdditionalData]

BEGIN

	DELETE FROM [dbo].[AccessibleAdminAreas]
	WHERE [AdministrativeAreaCode] like '9999%'
	AND [DistrictCode] like '9999%'
	
	DELETE FROM [dbo].[AccessibleAdminAreas]
	WHERE [AdministrativeAreaCode] like '9999%'
	AND [DistrictCode] like 'ALL'

END