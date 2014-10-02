-- ***********************************************
-- NAME 		: DUP2003_BatchJourneyPlanner_FAQ_Menu_Link.sql
-- DESCRIPTION 	: Batch journey planning FAQ left hand menu link
-- AUTHOR		: David Lane
-- DATE			: 14 Mar 2013
-- ************************************************

USE TransientPortal

EXEC [AddInternalSuggestionLink]    
'Help/HelpBatchJourneyPlanner.aspx',	-- Relative internal link URL
'Batch Journey Planner Help',		-- Description of internal link. Ensure this is a unique internal link description
'BatchJourneyPlannerHelp',  			-- Used to bind the Display text to the URL. Ensure value is unique per Link, or use existing ResourceName with caution
'Batch journey planner',		-- English display text. Populate only if adding new ResourceName or updating existing display text
'Cynllunydd amldeithiau',		-- Welsh display text. Populate only if adding new ResourceName or updating existing display text
'FAQ',  			-- Use 'General' if not a left hand navigation link
8179,				-- Priority must be unique for the selected CategoryName this link is for
0,					-- Set to 0 if to be used as a Suggestion/Related Link
0,					-- Set to 1 if it is a second level Root link
'FAQMenu',			-- Populate only if adding link to a Context. Used for the grouping of Suggestion/Related links for a page, e.g 'FindTrainInput'
'',					-- Populate only if adding a new ContextName, or updating description
1					-- Theme this link is added for, use 1 as default

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2003
SET @ScriptDesc = 'Batch journey planning FAQ menu link'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc)
  END
GO