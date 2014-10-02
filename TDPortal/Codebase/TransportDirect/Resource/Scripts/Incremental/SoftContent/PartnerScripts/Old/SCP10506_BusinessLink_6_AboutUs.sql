-- *************************************************************************************
-- NAME 		: SCP10506_BusinessLink_6_AboutUs.sql
-- DESCRIPTION  : About Us content - modified for DBisinessLink
-- AUTHOR		: Phil Scott
-- *************************************************************************************

USE [Content]
GO

DECLARE @ThemeId INT
SET @ThemeId = 6  


EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/About/AboutUs',
'<div>
    <h1>
        About Transport Direct
    </h1>
</div>'
,
'<div>
    <h1>
        Amdanom Transport Direct
    </h1>
</div>'


EXEC AddtblContent
@ThemeId, 19, 'Body Text', '/Channels/TransportDirect/About/AboutUs'
,'    <div id="primcontent">
        <div id="contentarea">
            <div class="hdtypethree">
                <h2>
                    <a id="Introduction" name="Introduction"></a>Introduction</h2>
            </div>
            <p>
                Transport Direct is the only website that offers information for door-to-door travel
                for both public transport and car journeys around Britain. Our aim is to provide
                you with comprehensive, easy-to-use travel information to help you plan your journeys
                effectively and efficiently.</p>
            <p>
                &nbsp;</p>
            <p>
                <strong>Transport Direct is unique in that you can:</strong></p>
            <div>
                <ul class="lister">
                    <li>Compare public transport options with a car route to find a way of travelling which
                        best suits your needs.</li>
                </ul>
            </div>
            <div>
                <ul class="lister">
                    <li>Obtain&nbsp;a car route that takes into account predicted&nbsp;traffic levels at
                        different times of the day so that you can make informed decisions about when to
                        travel.</li>
                </ul>
            </div>
            <div>
                <ul class="lister">
                    <li>Get an estimate of the cost of a car journey.</li>
                </ul>
            </div>
            <div>
                <ul class="lister">
                    <li>Buy train and coach tickets from our affiliated retail sites without having to re-enter
                        your details.</li>
                </ul>
            </div>
            <div>
                <ul class="lister">
                    <li>Use PDAs and mobile phones using the latest browser technology (WAP2.0) over a GPRS
                        or a 3G connection to find out departure and arrival times for railway stations
                        throughout Britain and for some bus or coach stops.</li>
                    <li>Calculate CO2 emissions for a car or public transport for a specified journey.</li>
                </ul>
            </div>
            <p>
                &nbsp;</p>
            <p>
                We have an ongoing development programme to increase the number of travel services
                available on Transport Direct, which is available on any browser enabled device.
                We are also working with third parties to make elements of our service available
                through their websites.</p>
            <div>
                <br />
            </div>
            <div align="right">
                <a name="EnablingIntelligentTravel"></a><a class="jpt" href="/Web2/About/AboutUs.aspx#logoTop">
                    Top of page
                    <img alt="Go to top &#13;&#10;of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
                        border="0" /></a></div>
            <div class="hdtypethree">
                <h2>
                    <a id="EnablingIntelligentTravel"></a>Enabling intelligent travel</h2>
            </div>
            <p>
                Transport Direct is a world first. We are the first ever web site to provide national
                coverage for information about all types of transport.</p>
            <p>
                &nbsp;</p>
            <p>
                Our aim is to provide you with all the trusted information you need to find the
                best travel option to suit your particular circumstances. We aspire to be world
                class, providing the best quality information for all types of transport, so that
                our users can make better travel decisions.</p>
            <p>
                &nbsp;</p>
            <p>
                Our strapline is "Connecting People to Places", because we want to make it easier
                for you to find both the place you want to go to and the best means of getting there.</p>
            <div>
                <br />
            </div>
            <div align="right">
                <a name="WhoOperates"></a><a class="jpt" href="/Web2/About/AboutUs.aspx#logoTop">Top of
                    page
                    <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
                        border="0" /></a></div>
            <div class="hdtypethree">
                <h2>
                    <a id="WhoOperates"></a>Who operates Transport Direct?</h2>
            </div>
            <p>
                Transport Direct works together with both public and private travel operators and
                local/national government.&nbsp; Transport Direct is operated by a consortium, led
                by Atos. The non-profit service is funded by the UK Department for Transport,
                the Welsh Assembly Government and the Scottish Government.</p>
            <p>
                &nbsp;&nbsp;
                <img alt="Department for Transport logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/DepartmentForTransportLogo.gif"
                    border="0" />&nbsp;&nbsp;<img alt="Scottish Government" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/Scottish_Gov.gif"
                        border="0" />&nbsp;&nbsp;&nbsp;&nbsp;
                <img alt="Welsh Assembly &#13;&#10;Government logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/WALogo.gif"
                    border="0" /></p>
            <p>
                &nbsp;</p>
            <p>
                The Highways Agency, Traffic Wales,&nbsp;Transport Scotland&nbsp;and the rail, coach
                and bus operators provide information to Transport Direct either directly or through
                our partners, "Traveline" who operate a public transport telephone service.</p>
            <p>
                &nbsp;</p>
            <div align="right">
                <a name="WhoBuilds"></a><a class="jpt" href="/Web2/About/AboutUs.aspx#logoTop">Top of page
                    <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
                        border="0" /></a></div>
            <div class="hdtypethree">
                <h2>
                    <a id="WhoBuilds"></a>Who builds and develops this site?</h2>
            </div>
            <p>
                &nbsp;</p>
            <p>
                <img alt="Atos Logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/AtosOriginLogo1.GIF"
                    border="0" /></p>
            <p>
                Atos is an international information technology (IT) services company that
                leads a consortium appointed by the DfT in January 2003 to design, build and operate
                the Transport Direct portal. Atos&#8217;s understanding of UK transport sector
                information models and data structures provides the insight necessary to identify
                and make connections - both physical and intuitive - between disparate databases
                and siloed systems across the travel industry. This enables Atos, in partnership
                with the Department for Transport and a range of data providers, to deliver travel
                intelligence to passengers, car drivers and pedestrians.</p>
            <p>
                &nbsp;</p>
            <div align="right">
                <a name="WhatsNew"></a><a class="jpt" href="/Web2/About/AboutUs.aspx#logoTop">Top of page
                    <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
                        border="0" /></a></div>
        </div>
    </div>
    <div>
    </div>
'
,
'    <div id="primcontent">
        <div id="contentarea">
            <div class="hdtypethree">
                <h2>
                    <a id="Introduction" name="Introduction"></a>Rhagarweiniad</h2>
            </div>
            <p>
                Transport Direct yw"r unig wefan sy"n cynnig gwybodaeth am deithio o ddrws i ddrws
                ar gyfer cludiant cyhoeddus ar siwrneion mewn car o amgylch Prydain.&nbsp; Ein hamcan
                yw rhoi gwybodaeth gynhwysfawr a hawdd ei defnyddio i chi am deithio i"ch helpu
                i gynllunio eich teithiau yn effeithiol ac yn effeithlon.</p>
            <p>
                &nbsp;</p>
            <p>
                <strong>Mae Transport Direct yn unigryw oherwydd gallwch:</strong></p>
            <div>
                <ul class="lister">
                    <li>Cymharwch opsiynau cludiant cyhoeddus gyda llwybr car i ddarganfod y ffordd o deithio
                        sy&#8217;n gweddu orau i&#8217;ch anghenion</li>
                </ul>
            </div>
            <div>
                <ul class="lister">
                    <li>Gofalwch eich bod yn cael llwybr car sy&#8217;n rhoi ystyriaeth i&#8217;r lefelau
                        traffig a ragfynegir ar wahanol amserau o&#8217;r dydd fel y gallwch w?eud penderfyniadau
                        doeth am pryd i deithio</li>
                </ul>
            </div>
            <div>
                <ul class="lister">
                    <li>Gofalwch eich bod yn cael amcangyfrif o gost siwrnai car</li>
                </ul>
            </div>
            <div>
                <ul class="lister">
                    <li>Prynu tocynnau tr&#234;n a bysiau moethus o"n safleoedd adwerthu cysylltiedig heb
                        orfod ail gofnodi eich manylion</li>
                </ul>
            </div>
            <div>
                <ul class="lister">
                    <li>Defnyddiwch PDAs a ffonau symudol gan ddefnyddio&#8217;r dechnoleg pori ddiweddaraf
                        (WAP2.0) dros GPRS neu gysylltiad 3G i ddarganfod amserau ymadael a chyrraedd ar
                        gyfer gorsafoedd rheilffordd ledled Prydain ac ar gyfer rhai arosfannau bysiau.</li>
                    <li>Cyfrifwch allyriadau CO2 ar gyfer car neu drafnidiaeth gyhoeddus ar gyfer siwrnai
                            benodol.</li>
                </ul>
            </div>
            <p>
                &nbsp;</p>
            <p>
                Mae gennym raglen ddatblygu ar y gweill i gynyddu nifer y gwasanaethau teithio sydd
                ar gael ar y safle sydd ar gael ar unrhyw ddyfais a alluogir gan borwr. Mae rhai
                elfennau o&#8217;r gwasanaeth ar gael hefyd ar ffonau smart (WAP 2 a/neu 3G) a PDAs.
                Rydym hefyd yn gweithio gyda grwpiau trydydd parti i sicrhau bod elfennau o wasanaeth
                Transport Direct ar gael drwy eu gwefannau.</p>
            <div>
                <br />
            </div>
            <div align="right">
                <a name="EnablingIntelligentTravel"></a><a class="jpt" href="/Web2/About/AboutUs.aspx#logoTop">
                    Yn &#244;l i"r brig
                    <img alt="Yn cl i&rsquo;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
                        border="0" /></a></div>
            <div class="hdtypethree">
                <div align="right">
                    <a name="EnablingIntelligentTravel"></a>
                </div>
                <h2>
                    <a id="EnablingIntelligentTravel"></a>Galluogi teithio deallus</h2>
            </div>
            <p>
                Transport Direct yw&#8217;r cynllun cyntaf o&#8217;i fath yn y byd. Ni yw&#8217;r
                wefan gyntaf erioed i roi sylw cenedlaethol i wybodaeth am bob math o gludiant.</p>
            <p>
                &nbsp;</p>
            <p>
                Ein hamcan yw rhoi&#8217;r wybodaeth yr ydych ei hangen i chi i ddod o hyd i&#8217;r
                dewis gorau o ran teithio i weddu i&#8217;ch amgylchiadau arbennig. Ceisiwn ddarparu&#8217;r
                gwasanaeth gorau yn y byd, gan ddarparu&#8217;r wybodaeth o&#8217;r ansawdd gorau
                ar gyfer pob math o gludiant, fel y gall ein holl ddefnyddwyr wneud dewisiadau doeth
                ynglyn &#226; theithio.</p>
            <p>
                &nbsp;</p>
            <p>
                Ein arwyddair yw &#8220;Cysylltu Pobl &#226; Lleoedd&#8221;, oherwydd rydym am ei
                gwneud yn haws i chi ddod o hyd i&#8217;r lle y dymunwch fynd iddo a dod o hyd i&#8217;r
                dull gorau o fynd yno.</p>
            <div>
                <br />
            </div>
            <div align="right">
                <a name="WhoOperates"></a><a class="jpt" href="/Web2/About/AboutUs.aspx#logoTop">Yn
                    &#244;l i"r brig
                    <img alt="Yn cl i&rsquo;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
                        border="0" /></a></div>
            <div class="hdtypethree">
                <h2>
                    <a id="WhoOperates"></a>Pwy sy"n gweithredu Transport Direct?</h2>
            </div>
            <p>
                Mae Transport Direct yn gweithio gyda gweithredwyr teithio cyhoeddus a phreifat
                a llywodraeth leol/genedlaethol.&nbsp; Gweithredir Transport Direct gan gonsortiwm
                o dan arweiniad Atos. Cyllidir y gwasanaeth heb fod ar gyfer gwneud elw gan
                Adran Cludiant y DG, Llywodraeth Cynulliad Cymru a Gweithrediaeth yr Alban.</p>
            <p>
                &nbsp;&nbsp;
                <img alt="Logo yr Adran Drafnidiaeth" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/DepartmentForTransportLogo.gif"
                    border="0" />&nbsp;&nbsp;<img alt="Scottish Government" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/Scottish_Gov.gif"
                        border="0" />&nbsp;&nbsp;&nbsp;&nbsp;
                <img alt="Logo Llywodraeth &#13;&#10;Cynulliad Cymru" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/WALogo.gif"
                    border="0" /></p>
            <p>
                &nbsp;</p>
            <p>
                Mae"r Asiantaeth Priffyrdd, Trafnidiaeth Cymru, Transport Scotland a"r gweithredwyr
                rheilffyrdd, bysiau moethus a bysiau yn darparu gwybodaeth i Transport Direct naill
                ai"n uniongyrchol neu drwy ein partneriaid "Traveline" sy"n gweithredu gwasanaeth
                ff&#244;n am gludiant cyhoeddus.
            </p>
            <div>
                <br />
            </div>
            <div align="right">
                <a name="WhoBuilds"></a><a class="jpt" href="/Web2/About/AboutUs.aspx#logoTop">Yn &#244;l
                    i"r brig
                    <img alt="Yn cl i&rsquo;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
                        border="0" /></a></div>
            <div class="hdtypethree">
                <h2>
                    <a id="WhoBuilds"></a>Pwy sy"n adeiladu a datblygu"r safle hwn?</h2>
            </div>
            <p>
                &nbsp;</p>
            <p>
                <img alt="Atos logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/AtosOriginLogo.GIF"
                    border="0" /></p>
            <p>
                Mae Atos yn gwmni gwasanaethau technoleg gwybodaeth (TG) rhyngwladol sy"n
                arwain consortiwm a benodwyd gan DfT ym mis Ionawr 2003 i gynllunio, adeiladu a
                gweithredu porth Transport Direct. Mae dealltwriaeth Atos o fodelau gwybodaeth
                a strwythurau data sector drafnidiaeth y DU yn darparu"r mewnwelediad sy"n angenrheidiol
                i adnabod a gwneud cysylltiadau - yn ffisegol ac yn sythweledol - rhwng cronfeydd
                data a systemau siloed gwahanol ar draws y diwydiant drafnidiaeth. Mae hyn yn galluogi
                Atos, mewn partneriaeth &#226;"r Adran Drafnidiaeth a ystod o ddarparwyr
                data, i gyflwyno gwybodaeth deithio i deithwyr a gyrwyr ceir.</p>
            <div>
                <br />
            </div>
            <div align="right">
                <a name="WhatsNew"></a><a class="jpt" href="/Web2/About/AboutUs.aspx#logoTop">Yn &#244;l
                    i"r brig
                    <img alt="Yn cl i&rsquo;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
                        border="0" /></a></div>
        </div>
    </div>'

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10506
SET @ScriptDesc = 'Add About Us content'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.3  $'

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
