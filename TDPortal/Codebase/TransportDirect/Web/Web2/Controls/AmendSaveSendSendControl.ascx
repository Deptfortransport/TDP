<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendSaveSendSendControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendSaveSendSendControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div class="txtsevenb"><strong><asp:label id="labelTypeEmailAddress" runat="server" enableviewstate="False"></asp:label></strong></div>
<table summary="AmendSaveSendSendControlTable">
    <tr>
        <td class="txtsevenb"><asp:label id="labelEnterEmail" associatedcontrolid="textEmailAddress" height="20px" runat="server" enableviewstate="False"></asp:label>
        </td>
        <td><asp:textbox id="textEmailAddress" runat="server" width="195px" maxlength="255"></asp:textbox>
        </td>
        <td><cc1:tdbutton id="buttonSend" runat="server" enableviewstate="False"></cc1:tdbutton>
        </td>
    </tr>
    <tr>
        <td class="txtnote"><asp:label id="labelDeliveryNote" runat="server" enableviewstate="False"></asp:label></td>
    </tr>
</table>
