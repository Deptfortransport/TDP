<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>

<%@ Page Language="c#" Codebehind="ErrorPageCookies.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.ErrorPageCookies" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" Stylesheets="setup.css, jpstd.css, homepage.css,ErrorPageCookies.aspx.css,SorryPage.aspx.css"
        runat="server"></cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="ErrorPagex" method="post" runat="server">
            <uc1:headercontrol id="headerControl" runat="server">
            </uc1:headercontrol>
            <div id="boxtypeeight">
                <a name="SkipToMain"></a>
                <h1 style="margin-left:10px">
                    Sorry, an error has occurred.</h1>
                <asp:Panel ID="panelMessage" runat="server">
                    <!-- don't show link if Javascript disabled -->

                    <script type="text/javascript">                                
					<!-- 
						document.write("<p>Successful journey planning by Transport Direct requires that cookies can be written by your browser. Please check your browser security settings and ensure that cookies are allowed for Transport Direct pages.</p>");

						document.write("<p>&nbsp;</p><p>If browser security settings are not preventing Transport Direct from functioning correctly then the error may be due to a technical problem. Please <a id=\"link\" href=\"<%=GetHRefValue()%>\" onclick=\"CloseChildWindow('en-GB');\">select this link to go to the homepage</a>.</p>");

						document.write("<p>&nbsp;</p><p>If you continue to experience difficulties, you may have found an error or omission. Please let us know by using the <a id=\"link\" href=\"<%=GetHRefValueForFeedback()%>\" >Contact us</a> facility, so that we can correct it for future users.</p>");

						document.write("<p>&nbsp;</p><p>Please note that accessing Transport Direct in more than one window within the same browser session is likely to cause a technical error. We are working on this issue. Meanwhile in order to view Transport Direct in more than one window please open a new browser session rather than opening a new window (or tab) within the same browser.</p>");
					//-->
                    </script>

                    <noscript>
                        <p>
                            <span id="closeBrowser">Successful journey planning by Transport Direct requires that
                                cookies can be written by your browser. Please check your browser security settings
                                and ensure that cookies are allowed for Transport Direct pages.</span></p>
                        <p>
                            <span>If browser security settings are not preventing Transport Direct from functioning
                                correctly then the error may be due to a technical problem. Please close the browser
                                window by clicking on the 'x' in the corner.</span></p>
                        <p>
                            If you continue to experience difficulties, you may have found an error or omission.
                            Please let us know by using the 'Contact us' facility, so that we can correct it
                            for future users.</p>
                        <p>
                            &nbsp;</p>
                        <p>
                            Please note that accessing Transport Direct in more than one window within the same
                            browser session is likely to cause a technical error. We are working on this issue.
                            Meanwhile in order to view Transport Direct in more than one window please open
                            a new browser session rather than opening a new window (or tab) within the same
                            browser.</p>
                        <p>
                            &nbsp;</p>
                    </noscript>
                </asp:Panel>
                <asp:Panel ID="panelUserSurvey" runat="server">
                    <p>
                        <span id="closeBrowser">This may be due to a technical problem. Please close the browser
                            window by clicking on the 'x' in the corner.</span></p>
                </asp:Panel>
            </div>
        </form>
    </div>
</body>
</html>
