-- ***********************************************
-- NAME 		: SC10011_TransportDirect_Content_11_PrivacyPolicy.sql
-- DESCRIPTION 	: Script to add Privacy Policy content
-- AUTHOR		: xxxx
-- DATE			: 16 May 2008 15:00:00
-- ************************************************


USE [Content]
GO

DECLARE @GroupId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'staticwithoutprint')


EXEC AddtblContent
1, @GroupId, 'TitleText', '/Channels/TransportDirect/About/PrivacyPolicy',
'<div>
    <h1>
        Privacy policy
    </h1>
</div>'
,
'<div>
    <h1>
        Polisi preifatrwydd
    </h1>
</div>'



EXEC AddtblContent
1, @GroupId, 'Body Text', '/Channels/TransportDirect/About/PrivacyPolicy',

'<div id="primcontent">  <div id="contentarea">  <div id="hdtypethree"> 
<h2>Privacy policy</h2></div><br />  <h3>1.&nbsp; General</h3>  <p>&nbsp;</p>  
<p>1.1 &nbsp;Transport Direct is a division of the Department for Transport whose main contact address for the purpose of this Privacy Policy is Portal Service Team, Transport Direct, Department for Transport, Zone 2/17, Great Minster House, 76 Marsham Street, LONDON, SW1P 4DR. ("<strong>Transport Direct</strong>").&nbsp;&nbsp; In the course of your use of the Transport Direct website located at www.TransportDirect.info (the "<strong>Website</strong>") we will only collect your email address.&nbsp;&nbsp; This Privacy Policy applies to the processing by Transport Direct of your email address.&nbsp; By accessing and using the Website you are agreeing or have agreed to be bound by the Website Terms and this Privacy policy and you consent to Transport Direct holding, processing and transferring your email address as set out herein.&nbsp;&nbsp; Transport Direct may at any time modify, recover, alter or update this Privacy Policy. This policy applies equally to the Transport Direct mobile site located at <a href="http://mobile.transportdirect.info">mobile.transportdirect.info</a></p><br />  
<h3>2.&nbsp; Transport Direct Use of Your Email Address </h3>  <p>&nbsp;</p>  
<p>2.1 &nbsp;Transport Direct will use your e-mail address to profile the Website to better suit your needs and to contact you when necessary. </p><br />  
<p>2.2&nbsp; Other than is required by law or as set out in this Privacy Policy, Transport Direct will not disclose, rent or sell your email address to any third party without your permission.</p><br />  
<p>2.3&nbsp; Transport Direct will hold your email address on its systems for as long as you remain a registered user of the Website.&nbsp; Transport Direct will remove it when requested by you. </p><br />  
<h3>3. &nbsp;Cookies </h3>  <p>&nbsp;</p>  
<p>3.1&nbsp; You should be aware that information in data may be automatically collected through the use of "cookies".&nbsp; Cookies are small text files a website can use to recognise you and allow Transport Direct to observe behaviour and compile accurate data in order to improve the website experience for you. </p><br />  
<p>3.2&nbsp; Most internet browsers enable you to delete cookies or receive a warning that cookies are being installed and if you do not want information collected through these cookies there is a simple procedure by which most browsers allow you to block or deny the cookies.&nbsp; Please refer to your browser instructions or help pages to learn more about these functions. <u>However, for you to make best use of the site&#8217;s full functionality we recommend you do not block or deny cookies.</u>&nbsp; </p><br />  
<p>3.3&nbsp; Transport Direct will install a cookie named "TDP" containing the following details:</p>
<ul>
<li class="bullet">Session ID</li>
<li class="bullet">Site theme id and domain</li>
<li class="bullet">Last visited date and time</li>
<li class="bullet">Last page visited</li>
</ul><br />
<p>The cookie will be used to detect new or repeat visitors of the site. To opt out of this process, please follow your browser instructions or help pages to block or deny the cookies.</p><br />
<p>3.4&nbsp; Transport Direct uses pixels, or transparent GIF files, to help manage online advertising. These GIF files are provided by DoubleClick and enable DoubleClick to recognize a unique cookie on your web browser, which in turn helps us to learn which of our advertisements bring most users to our website. The cookie was placed by us, or by another advertiser who works with DoubleClick. With both cookies and Spotlight technology, the information that we collect and share is anonymous and not personally identifiable. It does not reveal your name, address, telephone number, or email address. For more information about DoubleClick, including information about how to opt out of these technologies, go to <a href="http://www.google.com/doubleclick/" target="_blank" >www.google.com/doubleclick <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>.</p><br />
<p>3.5&nbsp; Transport Direct also uses page tag based web analytics. These services are provided by Cognesia and Google Analytics, and help us to learn more about the usage of Transport Direct. The information that we collect and share is anonymous and not personally identifiable. It does not reveal your name, address, telephone number, or email address. For more information about Cognesia, go to <a href="http://www.cognesia.com/index.php/" target="_blank" >www.cognesia.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a> or Google Analytics go to <a href="http://www.google.co.uk/analytics" target="_blank" >www.google.co.uk/analytics <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>.</p><br />
<p>&nbsp;</p>  <div></div></div></div>  <div></div>'
,
'<div id="primcontent">  <div id="contentarea">  <div id="hdtypethree"> 
<h2>Polisi preifatrwydd</h2></div><br />  <h3>1.&nbsp; Cyffredinol</h3>  <p>&nbsp;</p>
<p>1.1&nbsp; Mae Transport Direct yn is-adran o''r Adran Cludiant a''i brif gyfeiriad cyswllt i bwrpas y Polisi Preifatrwydd hwn yw''r Portal Service Team, Transport Direct, Department for Transport, Zone 2/17, Great Minster House, 76 Marsham Street, LONDON, SW1P 4DR ("<strong>Transport Direct</strong>"). Pan fyddwch yn defnyddio gwefan Transport Direct a leolir yn www.TransportDirect.info (y "<strong>Wefan</strong>") byddwn yn casglu eich cyfeiriad ebost yn unig.&nbsp; Mae''r Polisi Preifatrwydd hwn yn berthnasol i brosesu eich cyfeiriad ebost gan Transport Direct.&nbsp; Drwy gyrchu at a defnyddio''r Wefan&nbsp; rydych yn cytuno neu rydych wedi cytuno i gael eich rhwymo gan Delerau''r Wefan &nbsp;a''r Polisi Preifatrwydd hwn ac rydych yn caniat&#225;u i Transport Direct ddal, prosesu a throsglwyddo eich cyfeiriad ebost fel y cyflwynir yma.&nbsp; Gall Transport Direct ddiwygio, adfer, newid neu ddiweddaru''r Polisi Preifatrwydd hwn unrhyw bryd. This policy applies equally to the Transport Direct mobile site located at <a href="http://mobile.transportdirect.info">mobile.transportdirect.info</a></p><br />  
<h3>2.&nbsp; Defnydd Transport Direct o''ch Cyfeiriad Ebost </h3>  <p>&nbsp;</p>  
<p>2.1 &nbsp;Bydd Transport Direct yn defnyddio eich cyfeiriad ebost i ddiwygio''r Wefan i fod yn fwy addas ar gyfer eich gofynion ac i gysylltu &#226; chi pan fo hynny yn angenrheidiol. </p><br />  
<p>2.2&nbsp; Heblaw am yr hyn sy''n ofynnol yn &#244;l y ddeddf neu fel y cyflwynwyd yn y Polisi Preifatrwydd hwn, ni fydd Transport Direct yn datgelu, rhentu na gwerthu eich cyfeiriad ebost i unrhyw drydydd parti heb eich caniat&#226;d.</p><br />  
<p>2.3 &nbsp;Bydd Transport Direct yn cadw eich gwybodaeth bersonol ar ei systemau cyhyd ag y bydd hynny yn gwbl angenrheidiol yn unig a bydd yn tynnu gwybodaeth bersonol o''r fath ymaith cyn gynted ag y bydd hynny''n rhesymol o ymarferol wedi derbyn cais ysgrifenedig oddi wrthych chi. </p><br />  
<h3>3.&nbsp; "Cookies" </h3>  <p>&nbsp;</p>  
<p>3.1 &nbsp;Dylech fod yn ymwybodol y gall gwybodaeth mewn data gael ei chasglu yn awtomatig gan ddefnyddio "cookies".&nbsp; Mae "cookies" yn ffeiliau testun bychain y gall gwefan eu defnyddio i''ch adnabod chi a chaniat&#225;u i Transport Direct sylwi ar ymddygiad a chasglu gwybodaeth gywir er mwyn gwella''r profiad a gewch chi o''r wefan.</p><br />
<p>3.2&nbsp; Mae''r rhan fwyaf o borwyr y rhyngrwyd yn eich galluogi i ddileu "cookies" neu dderbyn rhybudd bod "cookies" yn cael eu gosod ac os nad ydych am i wybodaeth gael ei chasglu drwy''r "cookies" hyn mae trefn syml lle gall y rhan fwyaf o borwyr ganiat&#225;u i chi rwystro neu atal "cookies".&nbsp; Cyfeiriwch at gyfarwyddiadau neu dudalennau cymorth eich porwr i ddysgu mwy am y swyddogaethau hyn.&nbsp; <u>Ond er mwyn i chi wneud y defnydd gorau o swyddogaeth llawn y safle rydym yn argymell nad ydych yn rhwystro nac yn atal y "cookies" hyn.</u>&nbsp; </p><br />
<p>3.3&nbsp; Bydd Transport Direct yn gosod cwci o''r enw "TDP" a fydd yn cynnwys y manylion canlynol:</p>
<ul>
<li class="bullet">Rhif/enw adnabod sesiwn</li>
<li class="bullet">Rhif/enw adnabod a pharth thema''r safle</li>
<li class="bullet">Dyddiad ac amser ymweliad diwethaf</li>
<li class="bullet">Y dudalen olaf yr ymwelwyd &#226; hi</li>
</ul><br />
<p>Bydd y cwci yn cael ei defnyddio i ganfod ymwelwyr newydd neu rai sy''n ailymweld &#226;''r wefan. Er mwyn dewis peidio &#226; derbyn hyn, dilynwch gyfarwyddiadau eich porwr neu''r tudalennau cymorth i flocio neu wrthod y cwcis.</p><br />
<p>3.4&nbsp; Mae Transport Direct yn defnyddio picseli neu ffeiliau GIF tryloyw i helpu i reoli hysbysebu arlein. Darperir y ffeiliau GIF hyn gan DoubleClick ac maent yn galluogi DoubleClick i adnabod &#8216;cookie&#8217; unigryw ar eich porwr gwe, sydd yn ei dro yn ein helpu i ddysgu pa rai o&#8217;n hysbysebion sydd yn dod &#226;&#8217;r nifer mwyaf o ddefnyddwyr i&#8217;n gwefan. Gosodwyd y &#8216;cookie&#8217; gennym ni, neu gan hysbysebwr arall sy&#8217;n gweithio gyda DoubleClick. Gyda &#8216;cookies&#8217; a thechnoleg Spotlight, mae&#8217;r wybodaeth a gasglwn ac a rannwn yn anhysbys ac ni ellir adnabod unrhyw un yn bersonol oddi wrtho. Nid yw&#8217;n datgelu eich enw, cyfeiriad, rhif ff&#244;n na&#8217;ch cyfeiriad ebost. I gael mwy o wybodaeth am DoubleClick, gan gynnwys gwybodaeth am sut i optio allan o&#8217;r technolegau hyn ewch i <a href="http://www.google.com/doubleclick/" target="_blank" >www.google.com/doubleclick <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a>.</p><br />
<p>3.5&nbsp; Mae Transport Direct hefyd yn defnyddio dadansoddeg sy''n seiliedig ar dagio tudalennau ar y we. Darperir y gwasanaethau hyn gan Cognesia a Google, ac maent yn ein helpu i ddysgu mwy am sut y defnyddir Transport Direct. Mae''r wybodaeth yr ydym yn ei chasglu a''i rhannu yn ddi-enw ac nid yw''n wybodaeth bersonol adnabyddadwy. Nid yw''n datgelu eich enw, cyfeiriad, rhif ffôn, neu gyfeiriad e-bost. I gael rhagor o wybodaeth am Cognesia, ewch i <a href="http://www.cognesia.com/index.php/" target="_blank" >www.cognesia.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a> neu Google Analytics drwy fynd i <a href="http://www.google.co.uk/analytics" target="_blank" >www.google.co.uk/analytics <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a>.</p><br />
<p>&nbsp;</p></div></div>  <div></div>  <div></div>  <div></div>'

GO


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10011
SET @ScriptDesc = 'Add Privacy Policy content'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.16  $'

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
