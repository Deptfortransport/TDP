-- ***********************************************
-- NAME ,,: SC10014_TransportDirect_Content_14_ContactUs.sql
-- DESCRIPTION ,,: Add Contact Us content
-- AUTHOR,,: xxxx
-- DATE,,,: 09 July 2008 13:00:00
-- ************************************************


USE [Content]
GO

EXEC AddtblContent
1, 23, 'BodyText', '/Channels/TransportDirect/ContactUs/Details',
'
<div id="primcontent">
<div id="contentarea">
<p><strong>Address</strong></p>
<p>Programme Support Office&nbsp;</p>
<p>Transport Direct</p>
<p>Department for Transport</p>
<p>Zone 3/14</p>
<p>Great Minster House</p>
<p>76 Marsham Street</p>
<p>LONDON</p>
<p>SW1P 4DR</p><br /><br />
<p><strong>Email</strong></p>
<p>TDPortal.Feedback@dft.gsi.gov.uk</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p></div></div>'
,
'
<div id="primcontent">
<div id="contentarea">
<p><strong>Cyfeiriad</strong></p>
<p>Programme Support Office&nbsp;</p>
<p>Transport Direct</p>
<p>Department for Transport</p>
<p>Zone 3/14</p>
<p>Great Minster House</p>
<p>76 Marsham Street</p>
<p>LONDON</p>
<p>SW1P 4DR</p><br /><br />
<p><strong>Cyfeiriad ebost</strong></p>
<p>TDPortal.Feedback@dft.gsi.gov.uk</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p></div></div>'

GO

EXEC AddtblContent 
1,1,'langStrings','FeedbackInitialPage.FeedbackLinkButton.AlternateText','Feedback form','Ffurflen adborth'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.FeedbackLinkButton.ImageUrl','/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/jp_Feedback_gry.gif','/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/cy/jp_Feedback_gry.gif'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.FillDetailsLabel','Please enter your feedback.','Nodwch eich adborth.'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.labelClickToAdd','Please enter your feedback (mandatory). Try to include information about times, dates and places, the internet browser and any assistive technology you are using (if applicable). Click button to automatically add journey details to the feedback.','Nodwch eich adborth (gorfodol). Ceisiwch gynnwys gwybodaeth am amseroedd, dyddiadau a phrisiau, y porwr rhyngrwyd ac unrhyw dechnoleg gynorthwyol yr ydych yn ei defnyddio (os yw''n berthnasol). Cliciwch fotwm i ychwanegu manylion teithio yn awtomatig at yr adborth.'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.labelDetails.Text','<A href="/Web2/ContactUs/Details.aspx"/>Contact details</A/>','<A href="/Web2/ContactUs/Details.aspx"/>Manylion cyswllt</A/>'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.labelFeedback.Text','<A href="/Web2/ContactUs/FeedbackInitialPage.aspx"/>Feedback</A/>','<A href="/Web2/ContactUs/FeedbackInitialPage.aspx"/>Adborth</A/>'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.labelIntroduction.Text','<p style="font-size: 12pt; line-heigt: 1.5;">We welcome comments that will help us to improve the service that we offer.  Please fill in the form below to tell us about:</p> <br/> <ul class="listround">  <li>Errors that you find with the information that we provide</li><li>Technical problems that you experience with the site.</li><li>Your suggestions for future developments</li></ul>  <br/>  <p>Please give us plenty of detail to support your comments.  For journey planning problems tell us the date and time of the journey; the exact start and finish points; the nature of the error, etc.  For technical problems please tell us what Operating System and browser you are using and so forth.</p><p> </p> <br/> <p>All comments are taken forward but we regret that we are unable to offer individual replies.</p><p> </p> <br/> <p><b>NOTE - If you want to know how to do something on this site, try clicking FAQ, or see the overview information on the first page under each part of the site.  Please do not use this form to ask us to search the site</b> </p>','<p style="font-size: 12pt; line-heigt: 1.5;">Rydym yn croesawu sylwadau a fydd yn gymorth i ni wella''r gwasanaeth a gynigiwn. Llenwch y ffurflen isod i roi gwybod inni am:</p> <br/> <ul class="listround"><li>Wallau a welsoch yn yr wybodaeth a ddarparwn</li><li>Problemau technegol a gawsoch gyda''r safle</li><li>Eich awgrymiadau yngl&#375;n &#226; datblygiadau yn y dyfodol</li></ul> <br/> <p>Rhowch ddigon o fanylion i ni er mwyn cefnogi eich sylwadau. Yn achos problemau sy''n codi wrth ichi gynllunio siwrneion dywedwch wrthym beth yw dyddiad ac amser y siwrnai; yr union fannau cychwyn a gorffen; natur y gwall ayyb. Yn achos problemau technegol, dywedwch wrthym pa System Weithredu a phorwr rydych chi''n eu defnyddio ac ati.</p><p> </p> <br/> <p>Mae pob sylw yn cael ystyriaeth ond ni allwn gynnig atebion unigol yn anffodus. </p><p> </p> <br/> <p><b>SYLWER - Os hoffech wybod sut i wneud rhywbeth ar y safle hwn, beth am glicio Cwestiynau a Ofynnir yn Aml (COA), neu ewch i''r tudalennau "Gorolwg" o dan bob rhan o''r safle. Peidiwch &#226; defnyddio''r ffurflen hon i ofyn i ni chwilio''r safle.</b> </p>'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.labelPageTitle.Text','Contact us','Cysylltwch &#226; ni'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.labelSRFeedback','Type in your feedback','Teipiwch eich adborth'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.labelTitle.Text','Send us your feedback','Anfonwch eich adborth atom ni'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.lblFeedbackInstruction','If you experience problems with any information, please let us know by completing the feedback form below. If you would like to contact the Transport Direct team at the Department of Transport for any other reason please click on the "Contact details'' button on this page.','Os ydych chi''n profi problemau gydag unrhyw wybodaeth, rhowch wybod i ni os gwelwch yn dda, drwy lenwi''r ffurflen atborth isod. Os hoffech chi gysylltu &#226;''r t&#238;m Transport Direct yn yr Adran Drafnidiaeth am unrhyw reswm arall, cliciwch ar y botwm ''Manylion cyswllt'' ar y dudalen hon.'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.MakeClaimLinkButton.AlternateText','View the Make a claim page','Edrychwch ar y dudalen Gwneud hawliad'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.MakeClaimLinkButton.ImageUrl','/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/jp_MakeClaim.gif','/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/cy/jp_MakeClaim.gif'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.MandatoryLabel','Select the type of feedback you are giving and then select the specific issue from the drop-down list.','Dewiswch y math o adborth rydych yn ei roi ac yna dewis y pwnc penodol o''r rhestr a ollyngir i lawr'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.SubjectLine','Thank you for contacting Transport Direct','Diolch i chi am gysylltu &#226; Transport Direct'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.SubmitButton.Text','Submit','Cyflwynwch'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.SubmitImageButton.AlternateText','Submit Feedback to Transport Direct','Cyflwynwch adborth i Transport Direct'
GO


EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.SubmitImageButton.ImageUrl','/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/jp_Submit_blue.gif','/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/cy/jp_Submit.gif'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.SurveyLinkButton.AlternateText','User survey','Arolwg defnyddwyr'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.SurveyLinkButton.ImageUrl','/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/jp_UserSurvey_gry.gif','/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/cy/jp_UserSurvey_gry.gif'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.TextboxValidator','Please enter your feedback details',''
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.UnableToReplyLabel','<b>We are unable to reply to individual requests for transport information (e.g. "when is the train to...?").  If you want to know how to do something on this site try clicking on <A href="/Web2/Help/NewHelp.aspx">FAQ</A>.</b>','<b>Nid ydym yn gallu ateb ceisiadau unigol am wybodaeth am gludiant (e.e. “pryd mae''r tr&#234;n i ....?”) .  Os hoffech wybod sut i wneud rhywbeth ar y safle hwn cliciwch ar <A href="/Web2/Help/NewHelp.aspx">COA</A>.</b>'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.AcknowledgementComment',
'Dear Sir/Madam, 

Thank you for providing feedback to Transport Direct. Please be assured that every comment and suggestion made is taken forward for investigation and action as appropriate. We trust you will understand that we are unable to enter into detailed correspondence in response to your message.  Please see below for advice on some frequently received feedback topics.

If you are writing regarding the planned closure of Transport Direct, your comments will be read and noted, as is the case with all feedbacks received.  For further details about the planned closure, please see the PDF via the link on the right hand side of the homepage.

Once again thank you for taking the trouble to contact us. We regret that we cannot enter into correspondence through this e-mail address. 

Yours faithfully,
Transport Direct Team

--------------------------------------------------------------------------

JOURNEY PLANNING ERRORS AND OMISSIONS
Please be aware that it can take some time to effect necessary changes to correct reported errors, especially where third party data is involved (much of our data is supplied by third parties). Nevertheless all data errors are passed on to our suppliers and together we aim to resolve all of the issues you report.

OPERATION OF PUBLIC TRANSPORT SERVICES
Transport Direct is not responsible for the operation of public transport; therefore we are unfortunately unable to help with issues relating to the operation of bus or rail services. For matters such as this, including enquiries about lost property, or comments about the conduct of members of staff of a transport operator, you should contact the relevant transport operator directly.

RAIL AND COACH TICKET PURCHASES
If you require information about rail or coach tickets purchased using our retail hand-off facility, you should contact the relevant retail partner directly.
',

'Dear Sir/Madam, 

Thank you for providing feedback to Transport Direct. Please be assured that every comment and suggestion made is taken forward for investigation and action as appropriate. We trust you will understand that we are unable to enter into detailed correspondence in response to your message.  Please see below for advice on some frequently received feedback topics.

If you are writing regarding the planned closure of Transport Direct, your comments will be read and noted, as is the case with all feedbacks received.  For further details about the planned closure, please see the PDF via the link on the right hand side of the homepage.

Once again thank you for taking the trouble to contact us. We regret that we cannot enter into correspondence through this e-mail address. 

Yours faithfully,
Transport Direct Team

--------------------------------------------------------------------------

JOURNEY PLANNING ERRORS AND OMISSIONS
Please be aware that it can take some time to effect necessary changes to correct reported errors, especially where third party data is involved (much of our data is supplied by third parties). Nevertheless all data errors are passed on to our suppliers and together we aim to resolve all of the issues you report.

OPERATION OF PUBLIC TRANSPORT SERVICES
Transport Direct is not responsible for the operation of public transport; therefore we are unfortunately unable to help with issues relating to the operation of bus or rail services. For matters such as this, including enquiries about lost property, or comments about the conduct of members of staff of a transport operator, you should contact the relevant transport operator directly.

RAIL AND COACH TICKET PURCHASES
If you require information about rail or coach tickets purchased using our retail hand-off facility, you should contact the relevant retail partner directly.
'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.AddLastDetailsButton.Text','Insert details of last journey','Nodi manylion siwrnai ddiwethaf'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.AddLastDetailsImageButton.AlternateText','Insert details of your last journey','Nodi manylion siwrnai ddiwethaf'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.AddLastDetailsImageButton.ImageUrl','/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/AddLastJourneyDetails.gif','/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/cy/AddLastJourneyDetails.gif'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.ContactLinkButton.AlternateText','Transport Direct contact details','Manylion cyswllt Transport Direct'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.ContactLinkButton.ImageUrl','/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/jp_ContactDetails.gif','/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/cy/jp_ContactDetails.gif'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.ContactUsLabel','Contact Us','Cysylltwch &#226; ni'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.DetailsLabel','Details:','Manylion:'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.DropDownValidator','Please make a selection','Dewiswch un os gwelwch yn dda'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.EmailAddressLabel','Type your email in the box if you would like us to acknowledge receipt of your feedback.','Teipiwch eich cyfeiriad ebost yn y blwch os hoffech chi i ni gydnabod ein bod wedi derbyn eich adborth.'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.EmailValidator','The email address typed in was not recognised as a valid email address.  Please type in a valid email address.','Ni chafodd y cyfeiriad ebost a deipiwyd ei adnabod fel cyfeiriad ebost dilys. Teipiwch gyfeiriad ebost dilys.'
GO

EXEC AddtblContent 
1,1,'langstrings','FeedbackInitialPage.FeedbackLabel','Feedback','Adborth'
GO


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10014
SET @ScriptDesc = 'Add Contact Us content'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.4  $'

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
