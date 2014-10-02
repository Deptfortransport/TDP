<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindPlaceDropDownControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindPlaceDropDownControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div id="divDropDown" runat="server">
    <asp:label id="labelInstructionDropDown" associatedcontrolid="listPlaces" cssclass="txtseven" runat="server"></asp:label>
    <asp:dropdownlist id="listPlaces" cssclass="FindPlace_Dropdown" runat="server"></asp:dropdownlist>
    <cc1:scriptabledropdownlist id="listPlacesScriptable" cssclass="FindPlace_Dropdown" runat="server" ></cc1:scriptabledropdownlist>
</div>
