<%@ Control Language="c#" AutoEventWireup="True" Codebehind="D2DPtWalkingSpeedOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.D2DPtWalkingSpeedOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<div class="boxtypetwo boxOptions">
    <div class="optionHeadingRow txtseven" aria-controls="ptPreferencesControl_walkingSpeedOptionsControl_optionContentRow" role="button">
        <div class="boxArrowToggle">
        </div>
        <div class="boxOptionHeading">
            <asp:Label ID="labelJsQuestion" runat="server" />
        </div>
        <div class="boxOptionSelected">
            <asp:Label ID="labelOptionsSelected" runat="server" CssClass="labelJsRed hide" />
            <noscript>
                <cc1:tdbutton id="btnShow" runat="server" OnClick="btnShow_Click"></cc1:tdbutton>
            </noscript>
        </div>
    </div>
    <div id="optionContentRow" runat="server" class="optionContentRow hide" aria-expanded="false" aria-labelledby="ptPreferencesControl_walkingSpeedOptionsControl_labelJsQuestion" role="region">
        <asp:Panel ID="panelChanges" runat="server">
            <div>
                <div class="txtseven">
                    <table lang="en" cellspacing="0">
                        <tr>
                            <td width="11px"></td>
                            <td colspan="2">
                                <strong>
                                    <asp:Label ID="labelWalking" runat="server"></asp:Label>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td align="left" colspan="2">
                                <asp:Label ID="labelSRWalkingSpeed" runat="server" CssClass="screenreader"></asp:Label>
                                <asp:Label ID="displayWalkingSpeed" runat="server" CssClass="txttenb"></asp:Label>
                                <asp:Label ID="labelWalkingSpeedTitle" associatedcontrolid="listWalkingSpeed" runat="server"></asp:Label>
                                <asp:DropDownList ID="listWalkingSpeed" runat="server" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td align="left">
                                <asp:Label ID="labelSRWalkingTime" runat="server" CssClass="screenreader"></asp:Label>
                                <asp:Label ID="displayWalkingTime" runat="server" CssClass="txttenb"></asp:Label>
                                <asp:Label ID="labelWalkingTimeTitle" associatedcontrolid="listWalkingTime" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="listWalkingTime" runat="server" >
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
            </div>
        </asp:Panel>
    </div>
</div>