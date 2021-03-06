-- ***********************************************
-- NAME           : SC10017_CyclePlanner_Content_17_RightHandPanel.sql
-- DESCRIPTION    : Script to update Cycle Planner content with new area names
-- AUTHOR         : Rich broddle
-- DATE           : 15 July 2010
-- ***********************************************

USE [Content]
GO


--------------------------------------------------------------------------------------------------------------------------------
-- Cycle page Group 
--------------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM tblGroup WHERE [Name] like 'journeyplanning_findcycleinput') 
	INSERT INTO tblGroup (GroupId, [Name])
	SELECT MAX(GroupId)+1, 'journeyplanning_findcycleinput' FROM tblGroup

DECLARE @GroupId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'journeyplanning_findcycleinput')


--------------------------------------------------------------------------------------------------------------------------------
-- Cycle input page - Right hand information panel
--------------------------------------------------------------------------------------------------------------------------------
EXEC AddtblContent
1, @GroupId, 'TDFindCyclePromoHtmlPlaceholderDefinition', '/Channels/TransportDirect/JourneyPlanning/FindCycleInput',
'<div class="Column3Header">
  <div class="txtsevenbbl">Cycle Planning</div>
  <div class="clearboth"></div>
</div>
<div class="Column3Content">
  <table cellspacing="0" cellpadding="2" width="100%" border="0">
    <tbody>
      <tr>
        <td class="txtseven">We have made some <strong>changes</strong> to the cycle planner in response to user feedback.  For example your cycle journey will now avoid steep hills where possible.  You can still change your journey preferences to ‘quietest’, ‘quickest’ or ‘most recreational’.  Transport Direct continues to work with our partners to ensure that there is good quality information on cycling in the following areas: 
        <ul class="listerdisc">
		  <li>
            <strong>All England</strong>
          </li>
          <li>
            <strong>Cardiff</strong>
          </li>
        </ul>
        <br />We’d appreciate your further feedback on the cycle planner; please click on "Contact us" at the bottom of the page to let us know what you think or report any problems.  We will consider all feedback and will be improving the planner further over the coming months. 
        <br /></td>
      </tr>
    </tbody>
  </table>
</div>'
,
'<div class="Column3Header">
  <div class="txtsevenbbl">Trefnu Taith Feicio</div>
  <div class="clearboth"></div>
</div>
<div class="Column3Content">
  <table cellspacing="0" cellpadding="2" width="100%" border="0">
    <tbody>
      <tr>
        <td class="txtseven">Dyma''r Fersiwn gyntaf o drefnwr
        teithiau beicio newydd Transport Direct. Rydym wedi
        gweithio gyda Cycling England, Arolwg Ordnans a''r
        awdurdodau lleol perthnasol i sicrhau bod gwybodaeth o
        ansawdd ar gael am feicio yn yr ardaloedd canlynol: 
        <ul class="listerdisc">
		  <li>
            <strong>All England</strong>
          </li>
          <li>
            <strong>Cardiff</strong>
          </li>
        </ul>
        <br />Byddem yn wirioneddol yn gwerthfawrogi eich adborth
        am y fersiwn wreiddiol hon o''r trefnwr. Cliciwch ar
        ''Cysylltwch &#212; ni'' ar waelod y dudalen i roi gwybod
        inni beth yw eich barn neu adrodd am unrhyw broblemau. 
        <br />
        <br />Byddwn yn ystyried pob adborth ac yn gwella''r
        trefnwr dros yr wythnosau nesaf.
        <br />
        <br /></td>
      </tr>
    </tbody>
  </table>
</div>'


GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10017
SET @ScriptDesc = 'Script to update Cycle Planner content with new area names'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision$'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc, VersionInfo = @VersionInfo
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary, VersionInfo)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc, @VersionInfo)
  END
GO