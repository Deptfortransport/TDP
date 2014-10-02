<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapJourneyDisplayDetailsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapJourneyDisplayDetailsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div class="mjddMapJourneyLegDropDown">
    <asp:label id="labelSRSelect" associatedcontrolid="dropDownListJourneySegment" runat="server" cssclass="screenreader"></asp:label>
    <asp:dropdownlist id="dropDownListJourneySegment" runat="server"></asp:dropdownlist>
    <cc1:tdbutton id="buttonShow" runat="server"></cc1:tdbutton>
</div>
