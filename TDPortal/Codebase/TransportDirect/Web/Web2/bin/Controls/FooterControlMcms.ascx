<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="true" Codebehind="FooterControlMcms.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FooterControlMcms" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div id="footer">
	<table id="nsiefixFooter" cellspacing="0" cellpadding="0">
		<tr>
			<td width="20%" valign="bottom" align="left">
				<div id="verSpace">
					&nbsp; 
				</div>
				<div id="ver">
					<asp:label id="labelVersion" runat="server" enableviewstate="false"></asp:label>
				</div>
			</td>
			<td>
				<div id="fnav">
					<!--<a class="BlueLink" id="HomeLinkButton" runat="server">Home</a> |-->
					<a class="BlueLink" id="HelpLinkButton" runat="server">FAQ</a> | 
					<a class="BlueLink" id="AboutLinkButton" runat="server">About Transport Direct</a> | 
					<a class="BlueLink" id="ContactUsLinkButton" runat="server">Contact us</a> | 
					<a class="BlueLink" id="SiteMapLinkButton" runat="server">Sitemap</a> | 
					<asp:placeholder id="LanguagePlaceHolder" runat="server"></asp:placeholder> | 
					<a class="BlueLink" id="RelatedSitesLinkButton" runat="server">Related sites</a>
					<br>
					<a class="BlueLink" id="TermsConditions" runat="server">Terms and Conditions</a> | 
					<a class="BlueLink" id="PrivacyPolicy" runat="server">Privacy Policy</a> | 
					<a class="BlueLink" id="DataProviders" runat="server">Data Providers</a> | 
					<a class="BlueLink" id="Accessibility" runat="server">Accessibility</a>
				</div>
			</td>
		</tr>
	</table>
	<asp:table id="CjpLoggingTable" runat="server" width="100%">
		<asp:tablerow runat="server" id="Tablerow1">
			<asp:tablecell runat="server" horizontalalign="center" id="Tablecell1">
				<br>
				<cc1:tdbutton id="buttonLogViewer" runat="server"></cc1:tdbutton>
				&nbsp;
				<cc1:tdbutton id="buttonVersionViewer" runat="server"></cc1:tdbutton>
			</asp:tablecell>
		</asp:tablerow>
	</asp:table>

</div>