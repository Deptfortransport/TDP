<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintableMapTileControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.PrintableMapTileControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping" Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindSummaryResultControl" Src="../Controls/FindSummaryResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CycleJourneyDetailsTableControl" Src="CycleJourneyDetailsTableControl.ascx" %>

<div>
    <asp:Repeater ID="repeaterMapTile" runat="server" EnableViewState="false">
        <HeaderTemplate>
            <uc1:PrintableHeaderControl id="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server" Text="<%# GetlabelPrinterFriendlyText() %>" ></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server" Text="<%# GetlabelInstructionsText() %>" ></asp:Label></p>
            </div>
            
            <div class="boxtypeeightstd">
                <uc1:JourneysSearchedForControl id="JourneysSearchedForControl1" runat="server"></uc1:JourneysSearchedForControl>
            </div>
            
            <div class="boxtypeeleven">
                <div class="jpsumout">
                    <uc1:ResultsTableTitleControl id="resultsTableTitleControl" runat="server"></uc1:ResultsTableTitleControl>
                </div>
                <uc1:FindSummaryResultControl id="findSummaryResultTableControl" runat="server" Visible="true"></uc1:FindSummaryResultControl>
            </div>
            
            <div id="divCycleMapHeaderControl" class="boxtypeeleven" runat="server">
                <div class="padding5Only">
                    <asp:Label id="labelMapPageHeaderTitle" runat="server" Text="<%# GetlabelMapPageHeaderTitle(Container.ItemIndex) %>" CssClass="txtsevenb"></asp:Label>
                </div>
            </div>
            
            <div class="boxtypeonem" id="divCycleImageMap" runat="server">
                <asp:Image id="imageMap" runat="server" CssClass="printableMapTileImage"  AlternateText="<%# GetImageMapAltText() %>" ></asp:Image>
            </div>
            
            <div class="printableMapTileScale" id="divCycleImageMapScale" runat="server">
                <asp:Label id="labelMapScaleTitle" runat="server" CssClass="txtseven"></asp:Label>&nbsp;
                <asp:Label id="labelMapScale" runat="server" CssClass="txtseven"></asp:Label>
            </div>
            
            <div class="boxtypethirteen" id="divCycleJourneyDetailsTable" runat="server">
                <uc1:CycleJourneyDetailsTableControl ID="cycleJourneyDetailsTableControl" runat="server"></uc1:CycleJourneyDetailsTableControl>
            </div>
            
            <asp:Literal id="literalNewPage" runat="server" visible="false" Text='<div class="NewPage"></div>'></asp:Literal>
        </HeaderTemplate>
        
        <ItemTemplate>
        
            <uc1:PrintableHeaderControl id="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
                
            <div class="boxtypeeightstd">
                <uc1:JourneysSearchedForControl id="JourneysSearchedForControl1" runat="server"></uc1:JourneysSearchedForControl>
            </div>
            
            <div class="boxtypeeleven">
                <div class="jpsumout">
                    <uc1:ResultsTableTitleControl id="resultsTableTitleControl" runat="server"></uc1:ResultsTableTitleControl>
                </div>
                <uc1:FindSummaryResultControl id="findSummaryResultTableControl" runat="server" Visible="true"></uc1:FindSummaryResultControl>
            </div>
                
            <div class="boxtypeeleven">
                <div class="padding5Only">
                    <asp:Label id="labelMapPageItemTitle" runat="server" Text="<%# GetlabelMapPageItemTitle(Container.ItemIndex) %>" CssClass="txtsevenb"></asp:Label>
                </div>
            </div>
            
            <div class="boxtypeonem" id="divCycleImageMap" runat="server">
                <asp:Image id="imageMap" runat="server" CssClass="printableMapTileImage" AlternateText="<%# GetImageMapAltText() %>"></asp:Image>
            </div>
            
            <div class="printableMapTileScale" id="divCycleImageMapScale" runat="server">
                <asp:Label id="labelMapScaleTitle" runat="server" CssClass="txtseven"></asp:Label>&nbsp;
                <asp:Label id="labelMapScale" runat="server" CssClass="txtseven"></asp:Label>
            </div>
            
            <div class="boxtypethirteen" id="divCycleJourneyDetailsTable" runat="server">
                <uc1:CycleJourneyDetailsTableControl ID="cycleJourneyDetailsTableControl" runat="server"></uc1:CycleJourneyDetailsTableControl>
            </div>
            
            <asp:Literal id="literalNewPage" runat="server" visible="false" Text='<div class="NewPage"></div>'></asp:Literal>
        </ItemTemplate>
        
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
</div>