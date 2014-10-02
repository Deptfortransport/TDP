<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Page language="c#" Codebehind="StaticPrinterFriendly.aspx.cs" validateRequest = "false" AutoEventWireup="true" Inherits="TransportDirect.UserPortal.Web.StaticPrinterFriendly" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>"  xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css, nifty.css,MapIncidents.css, FooterPages.css"></cc1:headelementcontrol>
		<meta name="ROBOTS" content="NOODP" />
	</head>
	<body>
    <div class="CenteredContent">	
		<form id="Form1" method="post" runat="server">
			<uc1:printableheadercontrol id="printableHeaderControl" runat="server"></uc1:printableheadercontrol>
			<!-- print area -->
			<div id="Div1">
				<p class="txtsevenb"><asp:label id="labelPrinterFriendly" cssclass="onscreen" runat="server"></asp:label></p>
				<p class="txtsevenb"><asp:label id="labelInstructions" cssclass="onscreen" runat="server"></asp:label></p>
				<p>&nbsp;</p>
				<asp:Panel id="Body_Text" runat="server"></asp:Panel>
			</div>
        </form>			
    </div>
	</body>
</html>


