<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JourneyPageControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.JourneyPageControl" %>
<%@ Register src="~/Controls/JourneyHeadingControl.ascx" tagname="JourneyHeadingControl" tagprefix="uc1" %>


<div id="journeyNavContainer" runat="server" class="journeyNavContainer">
    <asp:Repeater ID="rptJourneyNav" runat="server" OnItemDataBound="JourneyNav_DataBound">
        <HeaderTemplate>
            <table class="journeyNavTable">
                <tr>
        </HeaderTemplate>
        <ItemTemplate>
            <td>
                <div id="journeyNavDiv" runat="server" enableviewstate="true" class="journeyNavDiv">
                    <asp:Button ID="btnJourneyNav" runat="server" OnClick="btnJourneyNav_Click" CssClass="btnJourneyNav" EnableViewState="true" />
                </div>
            </td>
        </ItemTemplate>
        <FooterTemplate>
                </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>