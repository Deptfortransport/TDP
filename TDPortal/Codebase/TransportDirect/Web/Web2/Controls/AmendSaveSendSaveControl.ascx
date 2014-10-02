<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendSaveSendSaveControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendSaveSendSaveControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="txtsevenb"><strong>
		<asp:label id="labelSaveFavouriteJourneyLabel" runat="server" Height="20px" enableviewstate="False"></asp:label></strong></div>
<div class="txtsevenb"><strong><asp:label id="labelCurrentNameLabel" runat="server" enableviewstate="False"></asp:label>
		<asp:label id="labelCurrentName" runat="server" enableviewstate="False"></asp:label></strong></div>
<div class="txtsevenb"><strong><asp:label id="labelRenameInstruction" runat="server" enableviewstate="False"></asp:label></strong></div>
<div><strong class="txtsevenb"><asp:label id="labelRenameJourneyLabel" associatedcontrolid="renameTextBox" runat="server" enableviewstate="False"></asp:label></strong>&nbsp;<asp:textbox id="renameTextBox" runat="server" Width="170px" MaxLength="50"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<cc1:tdbutton id="buttonOK" runat="server" enableviewstate="False"></cc1:tdbutton>
</div>
<div class="txtnote"><asp:label id="labelSaveNote" runat="server" enableviewstate="False"></asp:label></div>
