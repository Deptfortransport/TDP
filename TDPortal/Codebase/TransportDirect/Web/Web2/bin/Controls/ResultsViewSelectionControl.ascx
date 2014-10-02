<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ResultsViewSelectionControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ResultsViewSelectionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div class="txtseven">
	<asp:label id="selectionLabel" runat="server" enableviewstate="false"></asp:label>
	<asp:dropdownlist id="viewSelectionDropDown" runat="server" enableviewstate="true"></asp:dropdownlist>
	<cc1:tdbutton id="okButton" runat="server" enableviewstate="false"></cc1:tdbutton>
</div>
