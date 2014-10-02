<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FavouriteLoggedInControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FavouriteLoggedInControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div id="boxtypeone">
	<asp:label id="favouritesLabel" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;
	<asp:dropdownlist id="favouritesDropList" runat="server"></asp:dropdownlist>&nbsp;
	<cc1:tdbutton id="buttonOK" runat="server"></cc1:tdbutton>
</div>
