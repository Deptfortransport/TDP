<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PtWalkingSpeedOptionsControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.PtWalkingSpeedOptionsControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="FindPageOptionsControl.ascx" %>
<asp:Panel ID="panelPreferences" runat="server">
    <asp:Panel ID="panelChanges" runat="server">
        <div class="boxtypetwo">
            <div class="txtseven">
                <strong>
                    <asp:Label ID="labelWalking" runat="server"></asp:Label></strong>
                <table lang="en" cellspacing="0">
                    <tr>
                        <td align="left">
                            <asp:Label ID="labelSRWalkingSpeed" runat="server" CssClass="screenreader"></asp:Label>
                            <asp:Label ID="displayWalkingSpeed" runat="server" CssClass="txttenb"></asp:Label>
                            <asp:Label ID="labelWalkingSpeedTitle" associatedcontrolid="displayWalkingSpeed" runat="server"></asp:Label>
                            <asp:DropDownList ID="listWalkingSpeed" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="labelSRWalkingTime" runat="server" CssClass="screenreader"></asp:Label>
                            <asp:Label ID="displayWalkingTime" runat="server" CssClass="txttenb"></asp:Label>
                            <asp:Label ID="labelWalkingTimeTitle" associatedcontrolid="listWalkingTime" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="listWalkingTime" runat="server">
                                <asp:ListItem Value="05">05</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="30" Selected="True">30</asp:ListItem>
                                <asp:ListItem Value="25">25</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="labelWalkingTimeMinutes" runat="server"></asp:Label>
                            <asp:Label ID="displayWalkingTimeMinutes" runat="server" CssClass="txttenb"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <uc1:findpageoptionscontrol id="pageOptionsControl" runat="server"></uc1:findpageoptionscontrol>
        </div>
    </asp:Panel>
</asp:Panel>
