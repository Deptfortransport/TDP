-- ***********************************************
-- NAME           : SC10018_TransportDriect_Content_18_Help_JourneyDetails.sql
-- DESCRIPTION    : Updates to Help text for Journey Details page
-- AUTHOR         : Mitesh Modi
-- DATE           : 19 Sep 2011
-- ***********************************************

USE [Content] 

DECLARE @GroupId int,
	@ThemeId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'General')
SET @ThemeId = 1

-- Journey Details
EXEC AddtblContent
@ThemeId, @GroupId, 'langStrings', 'helpLabelJourneyDetails'
,'<p>
<strong>Journey details 
<br />
<br />Public transport journey</strong>
<br />The journey details are initially shown on this page in a
diagram format. If you prefer, you can view the details in a table
format by clicking "Show details in table".
<br />
<br />
<strong>Car journey 
<br /></strong>The directions for your car journey are initially
shown in a list. If you prefer, you can view them on a map by
clicking "Show on map". The map will appear with the directions
listed below it.
<br />Journey directions:</p>
<br />
<ul>
  <li>The directions for your journey are summarised at the top of
  the page</li>
  <li>If the journey involves ferries, tolls, etc. you can click on
  the name in the instruction to be taken to their websites.</li>
  <li>If the journey is impacted by high traffic volumes, roadworks, 
  a road incident or a closed road then a suitable icon will be displayed 
  next to the journey leg. In the case of the closed road it will be 
  possible to re-plan the journey avoiding all closed roads in the journey 
  results.</li>
</ul>
<br />
<p>
  <strong>Amend date and time</strong>
</p>
<br />
<p>To amend the dates and times of your journey:
<br />1. Select the new date(s) and time(s) in the drop-down lists
<br />2. Click ''Search with new dates/times''
<br />
<br />To amend the entire journey, you can click ''Amend journey'' at
the top of the page</p>
<br />
<p>
  <strong>Save as a favourite journey</strong>
</p>
<br />
<p>To save the journey:
<br />1. Make sure you are logged in
<br />2. Enter a meaningful name for the journey (e.g.
"Journey to work") 
<br />3. Click ''OK''
<br />
<br />You can save up to five journeys and you can overwrite
existing journeys.</p>
<br />
<p>
  <strong>Send to a friend</strong>
</p>
<p>To send this page to a friend:
<br />1. Make sure you are logged in.
<br />2. If you are not logged in, you will need to enter your
username and password.
<br />3. Type in the email address of the person you would like to
send the page to in the box
<br />4. Click ''Send''
<br />
<br />A text-based email with a summary of the journey and the
details/directions will be sent to that email address. Maps (if
applicable) will be attached as an image file (Avg. email size
150k). Your email address will be shown in the email.</p>
<br />
'
,
'<p>
<strong>Manylion siwrnai 
<br />
<br />Siwrnai cludiant cyhoeddus</strong>
<br />Dangosir manylion y siwrnai i ddechrau ar y dudalen hon ar
ffurf diagram. Os yw''n well gennych, gallwch weld y manylion ar
ffurf tabl drwy glicio ar ''Dangos manylion mewn tabl''.
<br />
<br />
<strong>Siwrnai car 
<br /></strong>Dangosir y cyfeiriadau ar gyfer eich siwrnai car i
ddechrau mewn rhestr. Os byddai''n well gennych, gallwch eu gweld
ar fap drwy glicio ar ''Dangoswch ar y map''. Bydd y map yn
ymddangos gyda''r cyfarwyddiadau wedi eu rhestru oddi tano. 
<br />Cyfeiriadau''r siwrnai:</p>
<br />
<ul>
  <li>Mae''r cyfarwyddiadau ar gyfer eich taith wedi eu crynhoi ar ben y dudalen. </li>
  <li>Os yw''r daith yn cynnwys fferïau, tollffyrdd ayyb gallwch glicio ar yr enw yn y cyfarwyddyd er mwyn cael eich trosglwyddo i''w gwefan.</li>
  <li>Os oes traffig trwm, gwaith ffordd, digwyddiad ar y ffordd neu ffordd wedi cau yn effeithio ar y daith yna dangosir eicon addas gyferbyn â''r darn hwnnw o''r daith. Yn achos y ffordd sydd wedi cau bydd yn bosibl ailgynllunio''r daith gan osgoi''r holl ffyrdd sydd wedi cau yng nghanlyniadau''r daith.</li>
</ul>
<br />
<p>
  <strong>Newidiwch y dyddiad a''r amser</strong>
</p>
<br />
<p>I ddiwygio dyddiadau ac amserau eich siwrnai:
<br />1. Dewiswch y dyddiad(au) a''r amser(au) newydd yn y rhestrau
a ollyngir i lawr
<br />2. Cliciwch ar ''Chwiliwch gyda dyddiadau/amserau newydd''
<br />
<br />I ddiwygio''r siwrnai gyfan, gallwch glicio ''Diwygiwch y
siwrnai'' ar frig y dudalen.</p>
<br />
<p>
  <strong>Cadwch fel hoff siwrnai</strong>
</p>
<br />
<p>I gadw''r siwrnai:
<br />1. Gofalwch eich bod wedi logio i mewn
<br />2. Rhowch enw ystyrlon i''r siwrnai (e.e. ''Siwrnai i''r
gwaith'')
<br />3. Cliciwch ar ''Iawn''.
<br />
<br />Gallwch gadw hyd at bump siwrnai a gallwch ysgrifennu dros
siwrneion presennol.</p>
<br />
<p>
  <strong>Anfonwch y dudalen hon at ffrind drwy ebost</strong>
</p>
<br />
<p>I anfon y dudalen hon at ffrind:
<br />1. Gofalwch eich bod wedi logio i mewn.
<br />2. Os nad ydych wedi logio i mewn, bydd yn rhaid i chi roi
eich enw defnyddiwr a''ch cyfrinair.
<br />3. Teipiwch gyfeiriad ebost y sawl yr hoffech anfon y dudalen
atynt yn y blwch
<br />4. Cliciwch ''Anfon''
<br />
<br />Anfonir ebost testun gyda chrynodeb o''r siwrnai a''r
manylion/cyfarwyddiadau at y cyfeiriad ebost hwnnw. Atodir mapiau
(os yn berthnasol) fel ffeil ddelwedd (maint ebost cyfartalog
150k). Dangosir eich cyfeiriad ebost yn yr e-bost.</p>
<br />
'


-- Journey Details Map
EXEC AddtblContent
@ThemeId, @GroupId, 'langStrings', 'helpLabelJourneyDetailsMap'
,'<p>
<strong>Journey details 
<br />
<br />Public transport journey</strong>
<br />The journey details are initially shown on this page in a
diagram format. If you prefer, you can view the details in a table
format by clicking "Show details in table". 
<br />
<br />
<strong>Car journey 
<br /></strong>The directions for your car journey are initially
shown in a list. If you prefer, you can view them on a map by
clicking "Show on map". The map will appear with the directions
listed below it. 
<br />Journey directions:</p>
<br />
<ul>
  <li>The directions for your journey are summarised at the top of
  the page</li>
  <li>If the journey involves ferries, tolls, etc. you can click on
  the name in the instruction to be taken to their websites.</li>
  <li>If the journey is impacted by high traffic volumes, roadworks, 
  a road incident or a closed road then a suitable icon will be displayed 
  next to the journey leg. In the case of the closed road it will be 
  possible to re-plan the journey avoiding all closed roads in the journey 
  results.</li>
</ul>
<br />
<p>You can view specific stages of the journey in more detail: 
<br />1. Select the journey stage from the drop-down list 
<br />2. Click ''Show route'' 
<br />
<br />Click ''Printer friendly'' to open a printer-friendly page that
you can print as usual. 
<br />
<br />You can view symbols on the map when it is highly magnified
(within the top five zoom levels, outlined in yellow). They include
transport symbols (shown automatically) and a range of attraction
and facility symbols. 
<br />
<br />To show or hide any of these symbols, you must: 
<br />1. Click on a category radio button e.g. ''Accommodation'' 
<br />2. Tick or untick the boxes next to the symbols 
<br />3. Click ''Show selected symbols''</p>
<br />
<br />
<p>The colour coding used on the map may show traffic levels as: 
<ul>
  <li>Low traffic</li>
  <li>Medium traffic</li>
  <li>High traffic</li>
  <li>Traffic unknown</li>
</ul></p>
<p>Low, medium and high traffic levels are based upon past traffic
measurements provided by the Highways Agency, the Welsh Assembly
and the Scottish Executive. These take into consideration the road,
the direction, and the day and time of travel. Where "Traffic
unknown" is shown, no traffic measurements are available for the
specific roads, so estimated traffic speeds for the time, day and
type of road are used in planning the journey (based on the
National Traffic Model).</p>
<br />
<br />
<p>
<strong>Amend date and time</strong>
<br />To amend the dates and times of your journey: 
<br />1. Select the new date(s) and time(s) in the drop-down lists 
<br />2. Click ''Search with new dates/times'' 
<br />
<br />To amend the entire journey, you can click ''Amend journey'' at
the top of the page</p>
<br />
<p>
<strong>Save as a favourite journey</strong>
<br />To save the journey: 
<br />1. Make sure you are logged in 
<br />2. Enter a meaningful name for the journey (e.g.
&#244;Journey to work&#246;) 
<br />3. Click ''OK'' 
<br />
<br />You can save up to five journeys and you can overwrite
existing journeys.</p>
<br />
<p>
<strong>Send to a friend</strong>
<br />To send this page to a friend: 
<br />1. Make sure you are logged in. 
<br />2. If you are not logged in, you will need to enter your
username and password. 
<br />3. Type in the email address of the person you would like to
send the page to in the box 
<br />4. Click ''Send'' 
<br />
<br />A text-based email with a summary of the journey and the
details/directions will be sent to that email address. Maps (if
applicable) will be attached as an image file (Avg. email size
150k). Your email address will be shown in the email.</p>
<br />
'
,
'<p>
<strong>Manylion siwrnai 
<br />
<br />Siwrnai cludiant cyhoeddus</strong>
<br />Dangosir manylion y siwrnai i ddechrau ar y dudalen hon ar
ffurf diagram. Os yw''n well gennych, gallwch weld y manylion ar
ffurf tabl drwy glicio ar ''Dangos manylion mewn tabl''. 
<br />
<br />
<strong>Siwrnai car 
<br /></strong>Dangosir y cyfeiriadau ar gyfer eich siwrnai car i
ddechrau mewn rhestr. Os byddai''n well gennych, gallwch eu gweld ar
fap drwy glicio ar ''Dangoswch ar y map''. Bydd y map yn ymddangos
gyda''r cyfarwyddiadau wedi eu rhestru oddi tano. 
<br />Cyfeiriadau''r siwrnai:</p>
<br />
<ul>
  <li>Mae''r cyfarwyddiadau ar gyfer eich taith wedi eu crynhoi ar ben y dudalen. </li>
  <li>Os yw''r daith yn cynnwys fferïau, tollffyrdd ayyb gallwch glicio ar yr enw yn y cyfarwyddyd er mwyn cael eich trosglwyddo i''w gwefan.</li>
  <li>Os oes traffig trwm, gwaith ffordd, digwyddiad ar y ffordd neu ffordd wedi cau yn effeithio ar y daith yna dangosir eicon addas gyferbyn â''r darn hwnnw o''r daith. Yn achos y ffordd sydd wedi cau bydd yn bosibl ailgynllunio''r daith gan osgoi''r holl ffyrdd sydd wedi cau yng nghanlyniadau''r daith.</li>
</ul>
<br />
<p>Gallwch weld camau penodol o''r siwrnai yn fanylach: 
<br />1. Dewiswch gam y siwrnai fel rhestr a ollyngir i lawr 
<br />2. Cliciwch ar ''Dangoswch y Llwybr'' 
<br />
<br />Cliciwch ar ''Hawdd ei argraffu'' i agor tudalen briodol y
gallwch ei hargraffu fel arfer 
<br />
<br />Gallwch weld symbolau ar y map pan fo wedi ei chwyddo''n fawr
(o fewn y pump lefel chwyddo mwyaf a amlinellir mewn melyn). Maent
yn cynnwys symbolau cludiant (a ddangosir yn awtomatig) ac
amrywiaeth o symbolau atyniadau a chyfleusterau 
<br />
<br />I ddangos neu guddio unrhyw rai o''r symbolau hyn, rhaid i
chi: 
<br />1. Glicio ar fotwm radio a dewis categori e.e. ''Llety'' 
<br />2. Dicio neu ddad-dicio''r blychau ger y symbolau 
<br />3. Glicio ar ''Dewiswch symbolau detholedig''</p>
<br />
<br />
<p>Mae''n bosibl y bydd y c&#182;d lliw ar y map yn dangos lefelau
trafnidiaeth fel: 
<ul>
  <li>Traffig isel</li>
  <li>Traffig canolig</li>
  <li>Traffig uchel</li>
  <li>Traffig anhysbys</li>
</ul></p>
<p>Mae lefelau trafnidiaeth isel, canolig ac uchel yn seiliedig ar
fesurau traffig y gorffennol a ddarparwyd gan Awdurdod y Priffyrdd,
Cynulliad Cenedlaethol Cymru a Gweithrediaeth yr Alban. Mae''r rhain
yn cymryd y ffordd, y cyfeiriad a diwrnod ac amser teithio i
ystyriaeth. Lle dangosir "Traffig anhysbys", nid oes mesuriadau
traffig ar gael ar gyfer y ffyrdd penodol, felly defnyddir
amcangyfrif o gyflymderau traffig ar gyfer yr amser, y diwrnod a''r
math o ffordd wrth gynllunio''r siwrnai (yn seiliedig ar Model
Traffig Cenedlaethol).</p>
<br />
<br />
<p>
<strong>Newidiwch y dyddiad a''r amser</strong>
<br />I ddiwygio dyddiadau ac amserau eich siwrnai: 
<br />1. Dewiswch y dyddiad(au) a''r amser(au) newydd yn y rhestrau
a ollyngir i lawr 
<br />2. Cliciwch ar ''Chwiliwch gyda dyddiadau/amserau newydd'' 
<br />
<br />I ddiwygio''r siwrnai gyfan, gallwch glicio ''Diwygiwch y
siwrnai'' ar frig y dudalen.</p>
<br />
<p>
<strong>Cadwch fel hoff siwrnai</strong>
<br />I gadw''r siwrnai: 
<br />1. Gofalwch eich bod wedi logio i mewn 
<br />2. Rhowch enw ystyrlon i''r siwrnai (e.e. ''Siwrnai i''r
gwaith'') 
<br />3. Cliciwch ar ''Iawn''. 
<br />
<br />Gallwch gadw hyd at bump siwrnai a gallwch ysgrifennu dros
siwrneion presennol.</p>
<br />
<p>
<strong>Anfonwch y dudalen hon at ffrind drwy ebost</strong>
<br />I anfon y dudalen hon at ffrind: 
<br />1. Gofalwch eich bod wedi logio i mewn. 
<br />2. Os nad ydych wedi logio i mewn, bydd yn rhaid i chi roi
eich enw defnyddiwr a''ch cyfrinair. 
<br />3. Teipiwch gyfeiriad ebost y sawl yr hoffech anfon y dudalen
atynt yn y blwch 
<br />4. Cliciwch ''Anfon'' 
<br />
<br />Anfonir ebost testun gyda chrynodeb o''r siwrnai a''r
manylion/cyfarwyddiadau at y cyfeiriad ebost hwnnw. Atodir mapiau
(os yn berthnasol) fel ffeil ddelwedd (maint ebost cyfartalog
150k). Dangosir eich cyfeiriad ebost yn yr e-bost.</p>
<br />
'


GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10018
SET @ScriptDesc = 'Updates to Help text for Journey Details page'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.2  $'

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