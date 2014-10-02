-- ***********************************************
-- AUTHOR      	: David Lane
-- Title        : DUP2082_Batch_Content_Update
-- DESCRIPTION 	: Batch content update
-- SOURCE 		: TDP Apps Support
-- ************************************************

USE Content
GO

DECLARE @ThemeId INT = 1
DECLARE @GroupId INT = 1
DECLARE @ControlName NVARCHAR(60) = 'langStrings'

EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.Welcome', '<br/>Welcome to the batch journey planner. Please use the template provided to create request files to upload requests.<br/>', '<br/>Croeso i''r Cynllunydd Amldeithiau. Defnyddiwch y templed a ddarperir i greu ffeiliau cais i lwytho ceisiadau i fyny.<br/>'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.UploadInstructions', '<br/>To request journey plans or statistics for a new set of journeys, first create your journeys file in CSV, then choose from the options below before uploading it.  See the <a target="_child" href="/Web2/Downloads/BatchJourneyPlannerUserGuide.pdf">User Guide</a> for detailed instructions.<br/>', '<br/>I wneud cais am gynlluniau teithiau neu ystadegau ar gyfer set newydd o deithiau, yn gyntaf dylech greu eich ffeil deithiau ar ffurf CSV, yna dewis o''r opsiynau isod cyn ei llwytho i fyny. <a target="_child" href="/Web2/Downloads/BatchJourneyPlannerUserGuide.pdf">Gweler cyfarwyddiadau manwl yn y Canllaw i Ddefnyddwyr.</a><br/>'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.TemplateDesc', '<br/>1 - Read the User Guide<br/>   2 - Download the CSV template file and populate it with your set of journeys<br/>   3 - Upload the CSV file using the "New request" form<br/>   4 - Collect your results when they appear in the table below<br/>', '<br/>1 - Darllenwch y Canllaw i Ddefnyddwyr<br/>   2 - Lawrlwythwch y ffeil templed CSV a''i llenwi Ô''ch set o deithiau<br/>   3 - Llwythwch y ffeil CSV i fyny gan ddefnyddio''r ffurflen "Cais newydd"<br/>  4 - Casglwch eich canlyniadau pan fyddant yn ymddangos yn y tabl isod<br/>'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.LinkTemplate', '<br/><a target="_child" href="/Web2/BatchJourneyPlanner/Template.csv">CSV template file</a>', '<br/><a target="_child" href="/Web2/BatchJourneyPlanner/Template.csv">Ffeil templed CSV</a>'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.LinkTemplateDesc', '<br/><a target="_child" href="/Web2/Downloads/BatchJourneyPlannerUserGuide.pdf">Batch Journey Planner user guide</a>', '<br/><a target="_child" href="/Web2/Downloads/BatchJourneyPlannerUserGuide.pdf">Cynllunydd Amldeithiau - Canllaw i Ddefnyddwyr</a>'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.NotLoggedIn', '<br/>You must be logged in to use this service. Please log in by clicking Login / Register on the menu tab bar<br/>', '<br/>Rhaid ichi fod wed mewngofnodi i ddefnyddio''r gwasanaeth hwn. Mewngofnodwch drwy glicio ar y tab Logio i Mewn / Cofrestru ar y ddewislen<br/>'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.RegistrationPending', '<br/>You have registered for batch journey planning however your registration has yet to be approved. Please wait to be contacted.<br/>', '<br/>Rydych wedi cofrestru ar gyfer Cynllunio Amldeithiau, fodd bynnag nid yw eich cofrestriad wedi ei gymeradwyo eto. Disgwyliwch os gwelwch yn dda nes byddwn yn cysylltu Ô chi.<br/>'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.RegistrationSuspended', '<br/>You have registered for batch journey planning however your registration has been suspended. You may not use the batch journey planner.<br/>', '<br/>Rydych wedi cofrestru ar gyfer Cynllunio Amldeithiau, fodd bynnag mae eich cofrestriad wedi ei ohirio. Ni allwch ddefnyddio''r Cynllunydd Amldeithiau.<br/>'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.NoFirstName', '<br/>Please enter your first name', '<br/>Mewnbynnwch eich enw cyntaf'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.NoLastName', '<br/>Please enter your last name', '<br/>Mewnbynnwch eich cyfenw'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.NoPhone', '<br/>Please enter a valid phone number - this can include numbers, spaces, "(", ")" and "+"', '<br/>Mewnbynnwch rif ff¶n dilys - gall hwn gynnwys rhifau, bylchau, "(", ")" a "+"'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.NoUsage', '<br/>Please enter a usage reason', '<br/>Nodwch reswm dros ddefnyddio'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.BadRecaptcha', '<br/>Please enter the words displayed', '<br/>Mewnbynnwch y geiriau sy''n cael eu harddangos'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.NoTerms', '<br/>Please accept the terms and conditions', '<br/>Derbyniwch y telerau a''r amodau'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.Confirmation', '<br/>Thank you for applying to use the batch journey planner. We will be in touch soon.<br/><a href="/Web2/Home.aspx">Return to Transport Direct homepage</a>', '<br/>Diolch ichi am wneud cais i ddefnyddio''r Cynllunydd Amldeithiau. Byddwn yn cysylltu Ô chi yn fuan.'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.WrongFileType', '<br/>You may only upload .csv files. Please select a .csv file to upload.', '<br/>Dim ond ffeiliau .csv y gellir eu llwytho i fyny. Dewiswch ffeil .csv i''w llwytho i fyny.'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.NoFileSelected', '<br/>Please browse to your CSV file before clicking "Load file".', '<br/>Porwch i''ch ffeil CSV cyn clicio ar  "Llwytho ffeil".'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.FileUploadFailed', '<br/>Sorry the file you selected could not be uploaded.', '<br/>Yn anffodus nid oedd yn bosibl llwytho''r ffeil a ddewiswyd gennych.'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.EmptyFile', '<br/>Sorry the file you selected was empty and cannot be processed.', '<br/>Yn anffodus roedd y ffeil a ddewiswyd gennych yn wag ac ni ellir ei phrosesu.'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.WrongHeader', '<br/>Sorry the file you selected had incorrect headers, the first line should read "JourneyID,OriginType,Origin,DestinationType,Destination,OutwardDate,OutwardTime,OutwardArrDep,ReturnDate,ReturnTime,ReturnArrDep".', '<br/>Yn anffodus roedd penawdau''r ffeil a ddewiswyd gennych yn anghywir, dylai''r llinell gyntaf ddweud "JourneyID,OriginType,Origin,DestinationType,Destination,OutwardDate,OutwardTime,OutwardArrDep,ReturnDate,ReturnTime,ReturnArrDep".'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.StatsDetails', '<br/>Please choose at least one output (journey statistics and/or journey plans).', '<br/>Dewiswch o leiaf un allbwn (ystadegau taith a/neu gynlluniau taith).'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.PtCarCycle', '<br/>Please choose at least one of public transport, car and cycle.', '<br/>Dewiswch o leiaf un o''r canlynol, cludiant cyhoeddus, car a beicio.'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.XmlRtf', '<br/>Please choose either RTF or XML.', '<br/>Dewiswch un ai RTF neu XML.'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.MaxFileLength', '<br/>The maximum number of request lines has been exceeded. Please try a smaller file.', '<br/>Mae nifer llinellau''r cais yn fwy na''r nifer macsimwm. Rhowch gynnig ar ffeil sy''n llai o ran maint.'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.UploadSuccess', '<br/>Request {0} has been successfully uploaded to the Batch Journey Planner and placed {1} in the queue to be processed.', '<br/>Mae cais {0} wedi cael ei lwytho i fyny yn llwyddiannus i''r Cynllunydd Amldeithiau ac wedi ei osod {1} yn y ciw i''w brosesu.'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.ResultsLabel', '<br/>Please select the results file you wish to download.', '<br/>Dewiswch y ffeil ganlyniadau y dymunwch ei lawrlwytho.'
EXEC AddtblContent @ThemeId, @GroupId, @ControlName, 'BatchJourneyPlanner.NoDetailRows', '<br/>Sorry the file you selected did not contain any requests.', '<br/>Yn anffodus nid oedd y ffeil a ddewiswyd gennych yn cynnwys unrhyw geisiadau.'


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2082
SET @ScriptDesc = 'DUP2082_Batch_Content_Update'

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








