<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyEmissionRelatedLinksControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyEmissionRelatedLinksControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Import namespace="System.Data" %>
<div id="ExpandableMenu">
	<UL style="padding-left: 0px; margin-left: 0px; LIST-STYLE-TYPE: none" id="ulistRelatedLink" runat="server">
		<asp:repeater id="rptRelatedLinksInternal" runat="server" enableviewstate="False">
		<HeaderTemplate>
		</HeaderTemplate>
			<itemtemplate>
				<li style="PADDING-BOTTOM: 10px">
					<a href="<%# GetLinkUrl((DataRow) Container.DataItem) %>"><%# GetLinkText((DataRow) Container.DataItem) %></a>					
				</li>
			</itemtemplate>
		</asp:repeater>
		<asp:repeater id="rptRelatedLinksExternal" runat="server" enableviewstate="False">
		<HeaderTemplate>
		</HeaderTemplate>
			<itemtemplate>
				<li style="PADDING-BOTTOM: 10px">
					<a href="<%# GetLinkUrl((DataRow) Container.DataItem) %>" target="_blank"><%# GetLinkText((DataRow) Container.DataItem) %></a>					
				</li>
			</itemtemplate>
		</asp:repeater>
	</UL>
</div>
