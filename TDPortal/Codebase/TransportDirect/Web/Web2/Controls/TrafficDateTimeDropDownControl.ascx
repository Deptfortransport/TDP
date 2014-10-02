<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TrafficDateTimeDropDownControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TrafficDateTimeDropDownControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tr>
        <td>
            <table cellspacing="1" cellpadding="0" border="0">
                <tr>
                    <td><asp:label id="labelDate" cssclass="txtseven" runat="server"></asp:label></td>
                    <td><asp:label id="labelSRDate" associatedcontrolid="DropDownListDay" runat="server" cssclass="screenreader"></asp:label>
                        <asp:dropdownlist id="DropDownListDay" runat="server"></asp:dropdownlist></td>
                    <td><asp:label id="labelSRMonthYear" associatedcontrolid="DropDownListMonthYear" runat="server" cssclass="screenreader"></asp:label>
                        <asp:dropdownlist id="DropDownListMonthYear" runat="server"></asp:dropdownlist></td>
                    <td><asp:imagebutton id="commandCalendar" runat="server" visible="false"></asp:imagebutton></td>
               
                    <td><asp:label id="labelTime" cssclass="txtseven" runat="server"></asp:label></td>
                    <td><asp:label id="labelSRHoursPre" associatedcontrolid="DropDownListHours" runat="server" cssclass="screenreader"></asp:label>
                        <asp:dropdownlist id="DropDownListHours" runat="server"></asp:dropdownlist>
                        <asp:label id="labelSRHoursPost" runat="server" cssclass="screenreader"></asp:label></td>
                    <td><asp:label id="labelSRMinutesPre" associatedcontrolid="DropDownListMinutes" runat="server" cssclass="screenreader"></asp:label>
                        <asp:dropdownlist id="DropDownListMinutes" runat="server"></asp:dropdownlist>
                        <asp:label id="labelSRMinutesPost" runat="server" cssclass="screenreader"></asp:label></td>
                    <td align="left"><asp:label id="label24HourClock" cssclass="txtseven" runat="server"></asp:label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="ShowOnMapRow" runat="server" visible="false">
        <td>
            <table width="100%">
                <tr>
                    <td align="right"><cc1:tdbutton id="commandShowOnMap" runat="server" visible="false"></cc1:tdbutton></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
