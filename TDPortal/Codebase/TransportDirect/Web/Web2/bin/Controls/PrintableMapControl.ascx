<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping"
    Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="uc1" TagName="MapLocationIconsDisplayControl" Src="../Controls/MapLocationIconsDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryResultTableControl" Src="../Controls/SummaryResultTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindSummaryResultControl" Src="FindSummaryResultControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PrintableMapControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.PrintableMapControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="MapKeyControl" Src="MapKeyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarJourneyDetailsTableControl" Src="CarJourneyDetailsTableControl.ascx" %>
<table summary="Printable Map" cellpadding="0" cellspacing="0">
    <tr align="left">
        <td valign="top">
            <asp:Panel ID="panelMapViewType" runat="server" CssClass="mpMapPrintableViewType" Visible="false" EnableViewState="false">
                <asp:Label ID="labelMapViewType" runat="server" CssClass="txteightb" EnableViewState="false"></asp:Label>
            </asp:Panel>
            <div class="mpMapPrintContainer">
                <asp:Image CssClass="mpMapPrintableImage" ID="imageMap" runat="server" EnableViewState="false"></asp:Image>
            </div>
        </td>
    </tr>
    <tr id="keyRow" runat="server">
        <td valign="top">
            <asp:Panel ID="panelKeysContainer" runat="server" CssClass="mpMapPrintKeysContainerJourney">
                <div class="mpMapPrintIconKeysContainer">
                    <asp:Panel ID="panelMapKeys" runat="server" CssClass="mpMapPrintPanelMapKeys">
                        <uc1:MapKeyControl ID="mapKeys" runat="server"></uc1:MapKeyControl>
                    </asp:Panel>
                    <asp:Panel ID="panelIconsDisplay" runat="server" CssClass="mpMapPrintPanelIconsDisplay">
                        <uc1:MapLocationIconsDisplayControl ID="iconsDisplay" runat="server"></uc1:MapLocationIconsDisplayControl>
                    </asp:Panel>
                </div>
                <asp:Panel ID="panelMapOverview" runat="server" Visible="false">
                    <asp:Label ID="labelOverview" runat="server" EnableViewState="false"></asp:Label>
                    <asp:Image ID="imageOverview" runat="server" EnableViewState="false"></asp:Image>
                </asp:Panel>
            </asp:Panel>
        </td>
    </tr>
    <tr id="scaleRow" runat="server">
        <td align="right">
            <span class="txtseven">
                <asp:Label ID="labelMapScaleTitle" runat="server" EnableViewState="false"></asp:Label>&nbsp;
                <asp:Label ID="labelMapScale" runat="server" EnableViewState="false"></asp:Label></span>
        </td>
    </tr>
</table>
<asp:Panel ID="panelDirections" runat="server" Visible="False">
    <div id="boxtypetwelve">
        <div id="fmview">
            <uc1:CarJourneyDetailsTableControl ID="carJourneyDetails" runat="server"></uc1:CarJourneyDetailsTableControl>
        </div>
    </div>
</asp:Panel>
