<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LocationSelectControl2.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.LocationSelectControl2" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table class="JPTable JPTableClass" cellspacing="0" cellpadding="0">
	<tr>
		<td style="PADDING-BOTTOM: 5px"><asp:panel id="panelNoMatch" width="100%" runat="server" visible="False" CssClass="panelnomatch">
				<asp:label id="labelNoMatchNote1" runat="server" cssclass="txtseven"></asp:label>
				<div class="clearboth" />
				<asp:label id="labelNoMatchNote2" runat="server" cssclass="txtseven"></asp:label>
			</asp:panel><asp:panel id="panelNewLocation" runat="server" visible="False">
				<asp:label id="labelNewLocationNote" runat="server" cssclass="txtsevenb"></asp:label>
			</asp:panel></td>
	</tr>
	<tr>
		<td>
			<table class="LocationSelectControlButtonCell" lang="en" cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td width="2"><asp:label id="labelSRLocation" associatedcontrolid="textLocation" runat="server" cssclass="screenreader"></asp:label></td>
					<td id="cellLocationText" runat="server">
					    <asp:textbox id="textLocation" runat="server" columns="48"></asp:textbox>
					    <asp:HiddenField ID="hdnLocationId"  runat="server" Value="" />
					</td>
					<td class="FindOnMapCell"><span class="floatleft"><asp:checkbox id="checkUnsureSpelling" runat="server" ></asp:checkbox></span>
						<span class="floatright">
					<cc1:tdbutton id="commandMap" runat="server" style="display:none;"></cc1:tdbutton></span></td>							
				</tr>
			</table>
			<asp:label id="labelSRSelect" associatedcontrolid="listLocationType" runat="server" cssclass="screenreader"></asp:label>
			<fieldset id="locationTypes" runat="server">
			    <asp:radiobuttonlist id="listLocationType" runat="server" cssclass="txtseven radioLocationType" repeatdirection="Horizontal"
							    repeatcolumns="3"></asp:radiobuttonlist>
			</fieldset>
		</td>
	</tr>
</table>
