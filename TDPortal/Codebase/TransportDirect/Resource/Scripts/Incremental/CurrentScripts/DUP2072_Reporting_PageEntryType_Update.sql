-- ***********************************************
-- NAME 		: DUP2072_Reporting_PageEntryType_Update.sql
-- DESCRIPTION 	: Update PageEntryType in Reporting database
-- AUTHOR		: Mitesh Modi
-- DATE			: 21 Aug 2013
-- ************************************************

USE [Reporting]
GO

DECLARE @id int 

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileDefault')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileDefault', 'Mobile Default home page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileError')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileError', 'Mobile Error page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileSorry')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileSorry', 'Mobile Sorry page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobilePageNotFound')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobilePageNotFound', 'Mobile Page not found page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileInput')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileInput', 'Mobile Journey input page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileInputPartialUpdate')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileInputPartialUpdate', 'Mobile Journey input page update AJAX')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileSummary')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileSummary', 'Mobile Journey summary page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileSummaryPartialUpdate')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileSummaryPartialUpdate', 'Mobile Journey summary input page update AJAX')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileSummaryWait')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileSummaryWait', 'Mobile Journey summary wait page update AJAX')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileSummaryResult')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileSummaryResult', 'Mobile Journey summary results page update AJAX')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileDetail')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileDetail', 'Mobile Journey detail page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileDirection')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileDirection', 'Mobile Journey direction page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileMap')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileMap', 'Mobile Map page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileMapSummary')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileMapSummary', 'Mobile Map summary page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileMapJourney')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileMapJourney', 'Mobile Map journey page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileTravelNews')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileTravelNews', 'Mobile Travel News page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileTravelNewsPartialUpdate')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileTravelNewsPartialUpdate', 'Mobile Travel News page update AJAX')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileTravelNewsDetail')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileTravelNewsDetail', 'Mobile Travel News detail page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileAccessibilityOptions')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileAccessibilityOptions', 'Mobile Accessibility stops page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileTerms')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileTerms', 'Mobile Terms page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobilePrivacy')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobilePrivacy', 'Mobile Privacy page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'MobileCookie')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'MobileCookie', 'Mobile Cookie page')
END

IF NOT EXISTS (SELECT * FROM [dbo].[PageEntryType] WHERE PETCode = 'RedirectMobile')
BEGIN
	SET @id = (SELECT MAX(PETID) + 1 FROM [dbo].[PageEntryType])
	INSERT INTO [dbo].[PageEntryType] VALUES (@id, 'RedirectMobile', 'Redirect to Mobile')
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2072, 'Update PageEntryType in Reporting database'