<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="ModeSelectControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.ModeSelectControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"
    EnableViewState="True" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<div class="ModeSelectionDiv">
    <table cellspacing="0" cellpadding="0" class="ModeSelectionTable">
        <tr>
            <td class="ModeSelectionCell">
                <asp:Label ID="instructionLabel" runat="server" CssClass="txtsevenb" EnableViewState="False"></asp:Label>
                <asp:CheckBox ID="trainCheckBox" runat="server" CssClass="txtseven"></asp:CheckBox>
                <asp:CheckBox ID="coachCheckBox" runat="server" CssClass="txtseven"></asp:CheckBox>
                <asp:CheckBox ID="airCheckBox" runat="server" CssClass="txtseven"></asp:CheckBox>
                &nbsp;<asp:Label ID="modesLabel" runat="server" CssClass="txtsevenb" EnableViewState="False"></asp:Label>
            </td>
        </tr>
    </table>
</div>
