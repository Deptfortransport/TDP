-- ***********************************************
-- AUTHOR       : Mark Turner
-- NAME  	: MDS005_Special_Notice_Board_Update.sql
-- DESCRIPTION  : Changes the text of the Special Notice Board page. 
-- SOURCE  	: TDP Apps Support
-- Version 	: $Revision:   1.9  $
--
-- ************************************************
--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS005_Special_Notice_Board_Update.sql-arc  $  
--
--   Rev 1.9   Aug 23 2012 15:12:56   PScott
-- cookies policy schange 
--Resolution for 5829: HomePage/SpecialNoticeBoard Cookies Policy
--
--   Rev 1.8   Jul 01 2010 14:51:00   RBroddle
--Minor grammatical correction in English Concessionary bus scheme section
--
--   Rev 1.7   Jun 17 2010 14:28:20   RBroddle
--Corrected spelling error "Italso" to "It also"
--Resolution for 5555: Small Change Fund change - Updated text for English Concessionary bus scheme.
--
--   Rev 1.6   Jun 17 2010 14:01:04   RBroddle
--Small Change Fund change - Updated text for English Concessionary bus scheme. 
--Resolution for 5555: Small Change Fund change - Updated text for English Concessionary bus scheme.
--
--   Rev 1.5   Oct 13 2009 16:36:22   nrankin
--Open in new window - WAI Compliance (XHTML) fix
--Resolution for 5320: Accessibility - Opens in new window
--
--   Rev 1.4   Sep 16 2009 10:18:36   nrankin
--Accessibility - Opens in new window
--
--Replace "opens in new window" with an icon.  
--Resolution for 5320: Accessibility - Opens in new window
--
--   Rev 1.3   Aug 10 2009 14:32:12   nrankin
--Amends to concessionary fares URL
--
--   Rev 1.2   Nov 04 2008 13:53:04   mturner
--Final updates for XHTML compliance
--Resolution for 5146: WAI AAA copmpliance work (CCN 474)
--
--   Rev 1.1   Nov 04 2008 13:24:54   mturner
--Updated to resolve IR5156 - Broken Links on special notice board.  Also took the opportunity to make compliant with post Del10 portal and make some XHTML compliance fixes.
--Resolution for 5156: Special Noticeboard Contains broken link
--
--   Rev 1.0   Nov 08 2007 12:42:26   mturner
--Initial revision.
--
--   Rev 1.4   Sep 25 2007 16:09:48   nrankin
--Change USD C447790
--
--Update "Special Notice Board" on homepage for DfT.
--
--   Rev 1.3   Dec 21 2006 13:41:38   sangle
--Updated Car park listing...
--
--   Rev 1.3   DEC 19 2006 11:26:02   SAngle 
--Amended Carpark city listings
--
--   Rev 1.2   Oct 27 2006 13:40:02   jfrank
--Updated for Del 9.0
--
--   Rev 1.1   Oct 10 2006 14:09:36   jfrank
--Update to special notice board page for Del 9.0 car parks.
--Resolution for 4219: Find a Car Park - Soft content changes for the homepage
--
--   Rev 1.0   Apr 13 2006 14:29:20   CRees
--Initial revision.
--


USE TransientPortal
GO

-- Special Notice Board page text
-- This should be fully formatted, remembering to escape any apostrophes ( use '' instead of ') 

IF EXISTS (SELECT * FROM dbo.SpecialContent WHERE Posting = 'SpecialNoticeBoard' and Placeholder = 'SpecialNoticeBoardHtmlPlaceHolder' and Culture = 'cy-GB')
BEGIN
	UPDATE dbo.SpecialContent
	SET
		[Text] = 

'<TABLE id="Table2" cellspacing="0" cellpadding="5" width="100%" border="0">
        <TBODY>
        <TR>
          <TD>
            <P><H1>Transport Direct Cookie Policy</H1></P>
			<P>You should be aware that information about your browser may be automatically collected through the use of
			"cookies".  Cookies are small text files a website can use to recognise a browser and allow Transport Direct to 
			compile accurate data in order to improve the website experience for our users. 
			</P>
            <P>&nbsp;</P>
            <P><H2>Cookies used on this site</H2></P><P>
			
			"ASP.NET_SessionId" - This cookie is used to track your Session ID and is required for the majority of the functionality of the site.
			</P>
	        <P>"TDPTestCookie" - This cookie is added and removed straight away to test if a browser supports cookies.</P>		
			<P>"TDP" - This cookie is used to detect new or repeat visitors of the site and contains the following details:<BR>
			<UL>
                    <LI class="bullet">Session ID=20
                    <LI class="bullet">Appropriate branding=20
					<LI class="bullet">Language indicator=20
                    <LI class="bullet">Last visited date and time=20
                    <LI class="bullet">Last page visited </LI></UL>
					</P>
            <P>"vidi" - This cookie is used to track site usage by Cognesia - using tags.transportdirect.info. These services help us to learn more 
			about the usage of Transport Direct. The information that we collect and share is anonymous and not personally identifiable.
			It does not reveal your name, address, telephone number, or email address. For more information about Cognesia, go to 
<A href="http://www.cognesia.com/index.php/" target="_blank">www.cognesia.com <IMG alt="(opens in new window)" 
			 src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></A>.
			</P>
			<P>Google Analytics Cookies - Also used to help us to learn more about the usage of Transport 
			Direct. The information that we collect and share is anonymous and not personally identifiable.
			It does not reveal your name, address, telephone number, or email address. For more information about Google Analytics go to 
			<A href="http://www.google.co.uk/policies/privacy/" target="_blank">www.google.co.uk/policies/privacy 
			<IMG alt="(opens in new window)" src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif">
			</A>.
			<BR>
			<UL>
                    <LI class="bullet">"__utma" - Keeps track of how many times a browser has visited TDP
                    <LI class="bullet">"__utmb" - Works out how long a browser has stayed on TDP for a session
                    <LI class="bullet">"__utmc" - Works out how long a browser has stayed on TDP for a session
					<LI class="bullet">"__utmv" - Any browser defined reporting data in Google Analytics
					<LI class="bullet">"__utmz" - Tracks where a browser came from to the TDP	site				
					</LI>
					</UL></P>
			<BR>				  
			<P><H2>Opting out</H2></P><P>
			Most internet browsers enable you to delete cookies or receive a warning that cookies are being installed. If you do not
			want information to be collected through these cookies there is a simple procedure by which most browsers allow you to block or deny
			the cookies.  Please refer to your browser instructions or help pages to learn more about these functions. Or visit one 
				  of the sites below, which have detailed information on how to manage, control or delete cookies.
				  <UL>
					<li class="bullet"><A href="http://www.aboutcookies.org/" target="_blank">About Cookies 
					<IMG alt="(opens in new window)" src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></a></li> 
					<li class="bullet"><a href="http://www.allaboutcookies.org/">All about Cookies <IMG alt="(opens in new window)" 
					src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></a></li> 
 
			</ul>
			</P>
			<BR>
              <P><H2>DoubleClick</H2></P><P>Transport Direct uses pixels, or transparent GIF
              files, to help manage online advertising. These GIF files are
              provided by DoubleClick and enable DoubleClick to recognize a
              unique cookie on your web browser, which in turn helps us to
              learn which of our advertisements bring most users to our
              website. The cookie was placed by us, or by another advertiser
              who works with DoubleClick. With both cookies and Spotlight
              technology, the information that we collect and share is
              anonymous and not personally identifiable. It does not reveal
              your name, address, telephone number, or email address. For
              more information about DoubleClick, including information
              about how to opt out of these technologies, go to <A href="http://www.google.co.uk/policies/privacy/ads/" target="_blank">www.google.co.uk/policies/privacy/ads/ <IMG alt="(opens in new window)" src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></A>.</P>
			</TD>
			</TR>
		</TBODY>
	</TABLE>'
	WHERE Posting = 'SpecialNoticeBoard' and Placeholder = 'SpecialNoticeBoardHtmlPlaceHolder' and Culture = 'cy-GB'
END
ELSE
BEGIN
	INSERT INTO dbo.SpecialContent
	(
		Posting,
		Placeholder,
		Culture,
		[Text]
	)
	VALUES
	(
		'SpecialNoticeBoard',
		'SpecialNoticeBoardHtmlPlaceHolder',
		'cy-GB',
		'<TABLE id="Table2" cellspacing="0" cellpadding="5" width="100%" border="0">
        <TBODY>
        <TR>
          <TD>
            <P><H1>Transport Direct Cookie Policy</H1></P>
			<P>You should be aware that information about your browser may be automatically collected through the use of
			"cookies".  Cookies are small text files a website can use to recognise a browser and allow Transport Direct to 
			compile accurate data in order to improve the website experience for our users. 
			</P>
            <P>&nbsp;</P>
            <P><H2>Cookies used on this site</H2></P><P>
			
			"ASP.NET_SessionId" - This cookie is used to track your Session ID and is required for the majority of the functionality of the site.
			</P>
	        <P>"TDPTestCookie" - This cookie is added and removed straight away to test if a browser supports cookies.</P>		
			<P>"TDP" - This cookie is used to detect new or repeat visitors of the site and contains the following details:<BR>
			<UL>
                    <LI class="bullet">Session ID=20
                    <LI class="bullet">Appropriate branding=20
					<LI class="bullet">Language indicator=20
                    <LI class="bullet">Last visited date and time=20
                    <LI class="bullet">Last page visited </LI></UL>
					</P>
            <P>"vidi" - This cookie is used to track site usage by Cognesia - using tags.transportdirect.info. These services help us to learn more 
			about the usage of Transport Direct. The information that we collect and share is anonymous and not personally identifiable.
			It does not reveal your name, address, telephone number, or email address. For more information about Cognesia, go to <A href="http://www.cognesia.com/index.php/" target="_blank">www.cognesia.com <IMG alt="(opens in new window)" 
			 src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></A>.
			</P>
			<P>Google Analytics Cookies - Also used to help us to learn more about the usage of Transport 
			Direct. The information that we collect and share is anonymous and not personally identifiable.
			It does not reveal your name, address, telephone number, or email address. For more information about Google Analytics go to 
			<A href="http://www.google.co.uk/policies/privacy/" target="_blank">www.google.co.uk/policies/privacy 
			<IMG alt="(opens in new window)" src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif">
			</A>.
			<BR>
			<UL>
                    <LI class="bullet">"__utma" - Keeps track of how many times a browser has visited TDP
                    <LI class="bullet">"__utmb" - Works out how long a browser has stayed on TDP for a session
                    <LI class="bullet">"__utmc" - Works out how long a browser has stayed on TDP for a session
					<LI class="bullet">"__utmv" - Any browser defined reporting data in Google Analytics
					<LI class="bullet">"__utmz" - Tracks where a browser came from to the TDP	site				
					</LI>
					</UL></P>
			<BR>				  
			<P><H2>Opting out</H2></P><P>
			Most internet browsers enable you to delete cookies or receive a warning that cookies are being installed. If you do not
			want information to be collected through these cookies there is a simple procedure by which most browsers allow you to block or deny
			the cookies.  Please refer to your browser instructions or help pages to learn more about these functions. Or visit one 
				  of the sites below, which have detailed information on how to manage, control or delete cookies.
				  <UL>
					<li class="bullet"><A href="http://www.aboutcookies.org/" target="_blank">About Cookies 
					<IMG alt="(opens in new window)" src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></a></li> 
					<li class="bullet"><a href="http://www.allaboutcookies.org/">All about Cookies <IMG alt="(opens in new window)" 
					src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></a></li> 
 
			</ul>
			</P>
			<BR>
              <P><H2>DoubleClick</H2></P><P>Transport Direct uses pixels, or transparent GIF
              files, to help manage online advertising. These GIF files are
              provided by DoubleClick and enable DoubleClick to recognize a
              unique cookie on your web browser, which in turn helps us to
              learn which of our advertisements bring most users to our
              website. The cookie was placed by us, or by another advertiser
              who works with DoubleClick. With both cookies and Spotlight
              technology, the information that we collect and share is
              anonymous and not personally identifiable. It does not reveal
              your name, address, telephone number, or email address. For
              more information about DoubleClick, including information
              about how to opt out of these technologies, go to <A href="http://www.google.co.uk/policies/privacy/ads/" target="_blank">www.google.co.uk/policies/privacy/ads/ <IMG alt="(opens in new window)" src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></A>.</P>
			</TD>
			</TR>
		</TBODY>
	</TABLE>'
	)
END

IF EXISTS (SELECT * FROM dbo.SpecialContent WHERE Posting = 'SpecialNoticeBoard' and Placeholder = 'SpecialNoticeBoardHtmlPlaceHolder' and Culture = 'en-GB')
BEGIN
	UPDATE dbo.SpecialContent
	SET
		[Text] = '<TABLE id="Table2" cellspacing="0" cellpadding="5" width="100%" border="0">
        <TBODY>
        <TR>
          <TD>
            <P><H1>Transport Direct Cookie Policy</H1></P>
			<P>You should be aware that information about your browser may be automatically collected through the use of
			"cookies".  Cookies are small text files a website can use to recognise a browser and allow Transport Direct to 
			compile accurate data in order to improve the website experience for our users. 
			</P>
            <P>&nbsp;</P>
            <P><H2>Cookies used on this site</H2></P><P>
			
			"ASP.NET_SessionId" - This cookie is used to track your Session ID and is required for the majority of the functionality of the site.
			</P>
	        <P>"TDPTestCookie" - This cookie is added and removed straight away to test if a browser supports cookies.</P>		
			<P>"TDP" - This cookie is used to detect new or repeat visitors of the site and contains the following details:<BR>
			<UL>
                    <LI class="bullet">Session ID=20
                    <LI class="bullet">Appropriate branding=20
					<LI class="bullet">Language indicator=20
                    <LI class="bullet">Last visited date and time=20
                    <LI class="bullet">Last page visited </LI></UL>
					</P>
            <P>"vidi" - This cookie is used to track site usage by Cognesia - using tags.transportdirect.info. These services help us to learn more 
			about the usage of Transport Direct. The information that we collect and share is anonymous and not personally identifiable.
			It does not reveal your name, address, telephone number, or email address. For more information about Cognesia, go to <A href="http://www.cognesia.com/index.php/" target="_blank">www.cognesia.com <IMG alt="(opens in new window)" 
			 src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></A>.
			</P>
			<P>Google Analytics Cookies - Also used to help us to learn more about the usage of Transport 
			Direct. The information that we collect and share is anonymous and not personally identifiable.
			It does not reveal your name, address, telephone number, or email address. For more information about Google Analytics go to 
			<A href="http://www.google.co.uk/policies/privacy/" target="_blank">www.google.co.uk/policies/privacy 
			<IMG alt="(opens in new window)" src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif">
			</A>.
			<BR>
			<UL>
                    <LI class="bullet">"__utma" - Keeps track of how many times a browser has visited TDP
                    <LI class="bullet">"__utmb" - Works out how long a browser has stayed on TDP for a session
                    <LI class="bullet">"__utmc" - Works out how long a browser has stayed on TDP for a session
					<LI class="bullet">"__utmv" - Any browser defined reporting data in Google Analytics
					<LI class="bullet">"__utmz" - Tracks where a browser came from to the TDP	site				
					</LI>
					</UL></P>
			<BR>				  
			<P><H2>Opting out</H2></P><P>
			Most internet browsers enable you to delete cookies or receive a warning that cookies are being installed. If you do not
			want information to be collected through these cookies there is a simple procedure by which most browsers allow you to block or deny
			the cookies.  Please refer to your browser instructions or help pages to learn more about these functions. Or visit one 
				  of the sites below, which have detailed information on how to manage, control or delete cookies.
				  <UL>
					<li class="bullet"><A href="http://www.aboutcookies.org/" target="_blank">About Cookies 
					<IMG alt="(opens in new window)" src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></a></li> 
					<li class="bullet"><a href="http://www.allaboutcookies.org/">All about Cookies <IMG alt="(opens in new window)" 
					src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></a></li> 
 
			</ul>
			</P>
			<BR>
              <P><H2>DoubleClick</H2></P><P>Transport Direct uses pixels, or transparent GIF
              files, to help manage online advertising. These GIF files are
              provided by DoubleClick and enable DoubleClick to recognize a
              unique cookie on your web browser, which in turn helps us to
              learn which of our advertisements bring most users to our
              website. The cookie was placed by us, or by another advertiser
              who works with DoubleClick. With both cookies and Spotlight
              technology, the information that we collect and share is
              anonymous and not personally identifiable. It does not reveal
              your name, address, telephone number, or email address. For
              more information about DoubleClick, including information
              about how to opt out of these technologies, go to <A href="http://www.google.co.uk/policies/privacy/ads/" target="_blank">www.google.co.uk/policies/privacy/ads/ <IMG alt="(opens in new window)" src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></A>.</P>
			</TD>
			</TR>
		</TBODY>
	</TABLE>'
	WHERE Posting = 'SpecialNoticeBoard' and Placeholder = 'SpecialNoticeBoardHtmlPlaceHolder' and Culture = 'en-GB'
END
ELSE
BEGIN
	INSERT INTO dbo.SpecialContent
	(
		Posting,
		Placeholder,
		Culture,
		[Text]
	)
	VALUES
	(
		'SpecialNoticeBoard',
		'SpecialNoticeBoardHtmlPlaceHolder',
		'en-GB',
		'<TABLE id="Table2" cellspacing="0" cellpadding="5" width="100%" border="0">
        <TBODY>
        <TR>
          <TD>
            <P><H1>Transport Direct Cookie Policy</H1></P>
			<P>You should be aware that information about your browser may be automatically collected through the use of
			"cookies".  Cookies are small text files a website can use to recognise a browser and allow Transport Direct to 
			compile accurate data in order to improve the website experience for our users. 
			</P>
            <P>&nbsp;</P>
            <P><H2>Cookies used on this site</H2></P><P>
			
			"ASP.NET_SessionId" - This cookie is used to track your Session ID and is required for the majority of the functionality of the site.
			</P>
	        <P>"TDPTestCookie" - This cookie is added and removed straight away to test if a browser supports cookies.</P>		
			<P>"TDP" - This cookie is used to detect new or repeat visitors of the site and contains the following details:<BR>
			<UL>
                    <LI class="bullet">Session ID=20
                    <LI class="bullet">Appropriate branding=20
					<LI class="bullet">Language indicator=20
                    <LI class="bullet">Last visited date and time=20
                    <LI class="bullet">Last page visited </LI></UL>
					</P>
            <P>"vidi" - This cookie is used to track site usage by Cognesia - using tags.transportdirect.info. These services help us to learn more 
			about the usage of Transport Direct. The information that we collect and share is anonymous and not personally identifiable.
			It does not reveal your name, address, telephone number, or email address. For more information about Cognesia, go to <A href="http://www.cognesia.com/index.php/" target="_blank">www.cognesia.com <IMG alt="(opens in new window)" 
			 src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></A>.
			</P>
			<P>Google Analytics Cookies - Also used to help us to learn more about the usage of Transport 
			Direct. The information that we collect and share is anonymous and not personally identifiable.
			It does not reveal your name, address, telephone number, or email address. For more information about Google Analytics go to 
			<A href="http://www.google.co.uk/policies/privacy/" target="_blank">www.google.co.uk/policies/privacy 
			<IMG alt="(opens in new window)"src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif">
			</A>.
			<BR>
			<UL>
                    <LI class="bullet">"__utma" - Keeps track of how many times a browser has visited TDP
                    <LI class="bullet">"__utmb" - Works out how long a browser has stayed on TDP for a session
                    <LI class="bullet">"__utmc" - Works out how long a browser has stayed on TDP for a session
					<LI class="bullet">"__utmv" - Any browser defined reporting data in Google Analytics
					<LI class="bullet">"__utmz" - Tracks where a browser came from to the TDP	site				
					</LI>
					</UL></P>
			<BR>				  
			<P><H2>Opting out</H2></P><P>
			Most internet browsers enable you to delete cookies or receive a warning that cookies are being installed. If you do not
			want information to be collected through these cookies there is a simple procedure by which most browsers allow you to block or deny
			the cookies.  Please refer to your browser instructions or help pages to learn more about these functions. Or visit one 
				  of the sites below, which have detailed information on how to manage, control or delete cookies.
				  <UL>
					<li class="bullet"><A href="http://www.aboutcookies.org/" target="_blank">About Cookies 
					<IMG alt="(opens in new window)" src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></a></li> 
					<li class="bullet"><a href="http://www.allaboutcookies.org/">All about Cookies <IMG alt="(opens in new window)" 
					src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></a></li> 
 
			</ul>
			</P>
			<BR>
              <P><H2>DoubleClick</H2></P><P>Transport Direct uses pixels, or transparent GIF
              files, to help manage online advertising. These GIF files are
              provided by DoubleClick and enable DoubleClick to recognize a
              unique cookie on your web browser, which in turn helps us to
              learn which of our advertisements bring most users to our
              website. The cookie was placed by us, or by another advertiser
              who works with DoubleClick. With both cookies and Spotlight
              technology, the information that we collect and share is
              anonymous and not personally identifiable. It does not reveal
              your name, address, telephone number, or email address. For
              more information about DoubleClick, including information
              about how to opt out of these technologies, go to <A href="http://www.google.co.uk/policies/privacy/ads/" target="_blank">www.google.co.uk/policies/privacy/ads/ <IMG alt="(opens in new window)" src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif"></A>.</P>
			</TD>
			</TR>
		</TBODY>
	</TABLE>'
	)
END

GO


----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.9  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 005 and VersionNumber = @@value)
BEGIN
 UPDATE dbo.MDSChangeCatalogue
 SET
  ChangeDate = getdate(),
  Summary = 'Special Notice Board Update' 
 WHERE ScriptNumber = 005 AND VersionNumber = @@value
END
ELSE
BEGIN
 INSERT INTO dbo.MDSChangeCatalogue
 (
  ScriptNumber,
  VersionNumber,
  Summary
 )
 VALUES
 (
  005,
  @@value, 
  'Special Notice Board Update'
 )
END
