<%@ Page language="c#" Codebehind="HelpFullJPrinter.aspx.cs" validateRequest="false" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.HelpFullJPrinter" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="Controls/FooterControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstdprint.css, jpstd.css, HelpFullJPrinter.aspx.css"></cc1:headelementcontrol>
		<asp:literal id="basehelpliteral" runat="server"></asp:literal>
	</head>
	<body>
		<form id="HelpFullJP" method="post" runat="server">
			<!-- page titlebar version 1 no print -->
			<uc1:printableheadercontrol id="printableHeaderControl" runat="server"></uc1:printableheadercontrol>
			<div id="boxtypeeightstd">
				<p class="txtsevenb"><asp:label id="labelPrinterFriendly" cssclass="onscreen" runat="server"></asp:label></p>
				<p class="txtsevenb"><asp:label id="labelInstructions" cssclass="onscreen" runat="server"></asp:label></p>
			</div>
			<div id="boxtypeeightstdb">
                <div class="titleLabelPad">
				    <h1>Help</h1>
				</div>
			</div>
			<div id="boxtypenine">
			    <asp:Panel id="HelpBodyText" runat="server"></asp:Panel></div>
			</form>
	</body>
</html>
