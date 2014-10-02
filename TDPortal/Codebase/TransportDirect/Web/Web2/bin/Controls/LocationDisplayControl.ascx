<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LocationDisplayControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.LocationDisplayControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellspacing="0" width="100%">
	<tr>
		<td>
			<asp:panel id="panelLocationType" runat="server">
				<asp:label id="labelLocationType" runat="server" cssclass="txtsevenb"></asp:label>
				<br />
			</asp:panel>
			<asp:label id="labelDescription" runat="server" cssclass="txtsevenb"></asp:label>
			<br />
			<asp:panel ID="locationDescriptionPanel" runat="server">
			&nbsp;&nbsp;<asp:label id="labelLocationDescription" runat="server" cssclass="txtseven"></asp:label>
			<br />
            </asp:panel>			
			<asp:label visible="false" id="labelReturnDescription" runat="server" cssclass="txtsevenb"></asp:label>
		</td>
		<td id="NewLocationButtonCell" align="right" valign="bottom">
			<cc1:tdbutton id="commandNewLocation" runat="server"></cc1:tdbutton>
		</td>
	</tr>
</table>
