<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PeopleTravellingControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.PeopleTravellingControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="peopletravellingcontainer">
	<table cellpadding="1" cellspacing="0">
		<tr>
			<td>
				<asp:label id="labelPeopleTravelling" runat="server" cssclass="txtsevenb" enableviewstate="False">labelPeopleTravelling</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
			</td>
			<td id="dataCell" runat="server">
				<asp:label id="labelAdult" enableviewstate="False" cssclass="txtseven" runat="server">labelAdult</asp:label>
				<asp:label id="labelAdultSR" enableviewstate="False" cssclass="screenreader" runat="server">labelAdult</asp:label>&nbsp;
				<asp:label id="labelAdultRO" enableviewstate="False" cssclass="txtseven" runat="server"></asp:label>
				<cc1:scriptabledropdownlist id="dropAdult" runat="server"></cc1:scriptabledropdownlist>&nbsp;&nbsp;&nbsp;
				<asp:label id="labelChild" enableviewstate="False" cssclass="txtseven" runat="server">labelChild</asp:label>
				<asp:label id="labelChildSR" enableviewstate="False" cssclass="screenreader" runat="server">labelChild</asp:label>&nbsp;
				<asp:label id="labelChildRO" enableviewstate="False" cssclass="txtseven" runat="server"></asp:label>
				<cc1:scriptabledropdownlist id="dropChild" runat="server"></cc1:scriptabledropdownlist>&nbsp;&nbsp;
			</td>
			<td>
				<asp:label id="labelInstructions" runat="server" cssclass="txtseven" enableviewstate="False"></asp:label>
			</td>
		</tr>
	</table>
</div>
