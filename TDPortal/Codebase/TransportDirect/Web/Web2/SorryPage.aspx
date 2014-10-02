<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>

<%@ Page Language="c#" Codebehind="SorryPage.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.SorryPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" Stylesheets="setup.css, jpstd.css, homepage.css, SorryPage.aspx.css"
        runat="server"></cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	</div>
    <div class="CenteredContent">
        <form id="SorryPage" method="post" runat="server">
            <uc1:headercontrol id="headerControl" runat="server">
            </uc1:headercontrol>
            <br />
            <div id="boxtypeeight">
                <asp:Panel ID="panelMessage" runat="server">
                    <!-- don't show link if Javascript disabled -->

                    <script type="text/javascript">                                
					<!-- 
						document.write("<p>Sorry, the demand for local transport information is high at the moment and we are unable to provide the information you require.  Please try again later or go to the <a id=\"link\" class=\"errorLink\" href=\"<%=GetHRefValue()%>\" onclick=\"CloseChildWindow('en-GB');\">Transport Direct Homepage</a> to plan another journey.</p>");
					-->
                    </script>

                    <noscript>
                        <p>
                            <span id="closeBrowser">Sorry, the demand for local transport information is high at
                                the moment and we are unable to provide the information you require. Please close
                                the browser window by clicking on the 'x' in the corner.</span></p>
                        <p>
                            &nbsp;</p>
                    </noscript>
                </asp:Panel>
            </div>
        </form>
    </div>
</body>
</html>
