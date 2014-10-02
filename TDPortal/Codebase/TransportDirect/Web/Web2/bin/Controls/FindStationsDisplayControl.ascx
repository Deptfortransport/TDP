<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindStationsDisplayControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindStationsDisplayControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table cellspacing="0" width="100%">
	<tr>
		<td id="stationsdisplaycolumn" align="right"><asp:label id="directionLabel" runat="server" cssclass="txtsevenb"></asp:label></td>
		<td valign="bottom"><asp:label id="resolvedLocationLabel" runat="server" cssclass="txtsevenb"></asp:label></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td><asp:datalist id="stationsDataList" runat="server" repeatdirection="Vertical" repeatcolumns="2">
				<itemstyle cssclass="txtseven"></itemstyle>
				<itemtemplate>
					<%# (string)Container.DataItem %>
					&nbsp;&nbsp;
				</itemtemplate>
			</asp:datalist></td>
	</tr>
	<tr>
		<td valign="bottom" align="right" colspan="2">
			<cc1:tdbutton id="newLocationButton" runat="server"></cc1:tdbutton>
		</td>
	</tr>
</table>
