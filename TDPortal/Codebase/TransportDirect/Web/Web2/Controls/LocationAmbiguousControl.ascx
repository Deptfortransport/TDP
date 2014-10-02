<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LocationAmbiguousControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.LocationAmbiguousControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table class="alertwarning" cellspacing="3" width="100%">
	<tr>
		<td><asp:label id="labelPossibleLocations" associatedcontrolid="SelectOptionDropDown" cssclass="txtseven" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td align="left"><asp:dropdownlist id="SelectOptionDropDown" runat="server"  style="width:450px"></asp:dropdownlist></td>
	</tr>
	<tr id="gazetter" runat="server">
		<td align="left"><asp:label id="labelChooseLocation" associatedcontrolid="StationTypesDropDown" cssclass="txtseven" runat="server"></asp:label>&nbsp;
			<asp:dropdownlist id="StationTypesDropDown" runat="server"></asp:dropdownlist></td>
	</tr>
</table>
<table id="newlocationtable" class="newlocation" cellspacing="0" width="100%" runat="server">
	<tr>
		<td align="right"><cc1:tdbutton id="commandNewLocation" runat="server"></cc1:tdbutton></td>
	</tr>
</table>
