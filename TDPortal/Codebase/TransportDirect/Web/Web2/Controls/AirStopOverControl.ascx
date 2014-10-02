<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AirStopOverControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AirStopOverControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:label id="labelFlyViaTitle" cssclass="txtsevenb" runat="server"></asp:label><br>
<asp:label id="labelFlyViaAmbiguity" runat="server" cssclass="txtseven"></asp:label>
<asp:panel id="panelVia" runat="server">
    <table title="Stopover details" cellspacing="0" cellpadding="2">
        <tr>
            <td align="right" style="text-align: right" width="145px">
                <asp:label id="labelFlyVia" runat="server" cssclass="txtseven"></asp:label>&nbsp;
            </td>
            <td>
                <asp:label id="labelSRdropFlyVia" runat="server" cssclass="screenreader"></asp:label>
                <asp:dropdownlist id="dropFlyVia" runat="server"></asp:dropdownlist>
                <asp:label id="dropFlyViaFixed" runat="server" cssclass="txtsevenb"></asp:label>&nbsp;
            </td>
        </tr>
    </table>
</asp:panel>
<asp:label id="labelStopoverOutwardTitle" cssclass="txtsevenb" runat="server"></asp:label>
<asp:panel id="panelStopoverOutward" runat="server">
    <table title="Stopover details" cellspacing="0" cellpadding="1">
        <tr>
            <td align="right" style="text-align: right" width="145px">
                <asp:label id="labelStopoverOutward" runat="server" cssclass="txtseven"></asp:label>&nbsp;
            </td>
            <td>
                <asp:label id="labelSRdropStopoverOutward" runat="server" cssclass="screenreader"></asp:label>
                <asp:dropdownlist id="dropStopoverOutward" runat="server"></asp:dropdownlist>
                <asp:label id="dropStopoverOutwardFixed" runat="server" cssclass="txtsevenb"></asp:label>&nbsp;
            </td>
        </tr>
    </table>
</asp:panel>
<asp:label id="labelStopoverReturnTitle" cssclass="txtsevenb" runat="server"></asp:label>
<asp:panel id="panelStopoverReturn" runat="server">
    <table title="Stopover details" cellspacing="0" cellpadding="1">
        <tr>
            <td align="right" style="text-align: right" width="145px">
                <asp:label id="labelStopoverReturn" runat="server" cssclass="txtseven"></asp:label>&nbsp;
            </td>
            <td>
                <asp:label id="labelSRdropStopoverReturn" runat="server" cssclass="screenreader"></asp:label>
                <asp:dropdownlist id="dropStopoverReturn" runat="server"></asp:dropdownlist>
                <asp:label id="dropStopoverReturnFixed" runat="server" cssclass="txtsevenb"></asp:label>&nbsp;
            </td>
        </tr>
    </table>
</asp:panel>
