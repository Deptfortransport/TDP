<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TravelNewsIncidentPopoutControl" Src="~/Controls/TravelNewsIncidentPopoutControl.ascx" %>
<%@ Import Namespace="TransportDirect.UserPortal.JourneyControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CarJourneyDetailsTableControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.CarJourneyDetailsTableControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
    
<%@ Register TagPrefix="uc1" TagName="CJPUserInfoControl" Src="CJPUserInfoControl.ascx" %>


<div class="carJourneyDetailsBoxType1">
    <div class="carJourneyDetailsBoxType2">
        <span class="txteightb"><asp:Label ID="labelDirections" runat="server">Directions</asp:Label></span>
    </div>
    <div class="carJourneyDetailsBoxType3">
    
<asp:Table ID="RouteDirections" CellSpacing="0" Width="100%" BorderWidth="0" runat="server">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="5" CssClass="txtsevenb carJourneyDetailsTableHeader7">
			<hr class="bluerule" align="left" width="100%" size="1" noshade="noshade" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="carJourneyDetailsTableHeader1">&nbsp;</asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrTotal" CssClass="txteightb carJourneyDetailsTableHeader2">
			<%# HeaderDetail[0] %>
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrDir" CssClass="txteightb carJourneyDetailsTableHeader3">
			<%# HeaderDetail[1] %>
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrArrive" CssClass="txteightb carJourneyDetailsTableHeader4" Visible="<%# ShowDirectionTime %>">
			<%# HeaderDetail[2] %>
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrDirDistance" CssClass="txteightb carJourneyDetailsTableHeader5" Visible="<%# ShowDirectionDistance %>">
			<%# HeaderDetail[3] %>
        </asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableHeader6">&nbsp;<uc1:CJPUserInfoControl id="cjpUserJourneySpeedInfo" runat="server" InfoType="JourneySpeed" DisplayText="Speed" /></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="5" CssClass="txtsevenb carJourneyDetailsTableHeader7">
			<hr class="bluerule" align="left" width="100%" size="1" noshade="noshade" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow2" runat="server" Visible="<%# (bool) FooterDetail[8] %>">
        <asp:TableCell CssClass="carJourneyDetailsTableHeader1">&nbsp;</asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrPetrolEfficientLogoHeader" CssClass="txtseven carJourneyDetailsTableHeader2">
			<%#(string) FooterDetail[6] %>
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrPetrolEfficientTextHeader" CssClass="txtseven carJourneyDetailsTableHeader3">
            <uc1:HyperlinkPostbackControl ID="fuelEmissionLinkControlHeader" Text="<%# FooterDetail[7] %>" runat="server"></uc1:HyperlinkPostbackControl>
        </asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableHeader4" Visible="<%# ShowDirectionTime %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableHeader5" Visible="<%# ShowDirectionDistance %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableHeader6">&nbsp;</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow3" runat="server" Visible="<%# (bool) FooterDetail[11] %>">
        <asp:TableCell CssClass="carJourneyDetailsTableHeader1">&nbsp;</asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrFindNearestCarParkLogoHeader" CssClass="txtseven carJourneyDetailsTableHeader2">
			<%#(string) FooterDetail[10] %>
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrFindNearestCarParkTextHeader" CssClass="txtseven carJourneyDetailsTableHeader3">
            <uc1:HyperlinkPostbackControl ID="findNearestCarParkLinkControlHeader" Text="<%# FooterDetail[9] %>" runat="server"></uc1:HyperlinkPostbackControl>
        </asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableHeader4" Visible="<%# ShowDirectionTime %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableHeader5" Visible="<%# ShowDirectionDistance %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableHeader6">&nbsp;</asp:TableCell>
    </asp:TableRow>
</asp:Table>
<table cellspacing="0px" width="100%" summary="RouteDirections1" lang="en" border="0">
    <tbody>
        <asp:Repeater ID="detailsRepeater" runat="server">
            <ItemTemplate>
                <tr>
                    <th scope="row" class="txtseven carJourneyDetailsTableCell1">
                        <asp:Panel id="panelDirectionNumber" runat="server" Visible="<%# !IsRowMapLinkVisible %>">
                            <%# (string)((object[])Container.DataItem)[0] %>
                        </asp:Panel>
                        <asp:Panel id="panelDirectionLink" runat="server" Visible="<%# IsRowMapLinkVisible %>">
                            <a href=" " onclick="<%# GetShowOnMapScript((string)((object[])Container.DataItem)[0]) %>">
                            <%# (string)((object[])Container.DataItem)[0] %></a>
                        </asp:Panel>
                    </th>
                    <td headers="<%# (string)hdrTotal.ClientID %>" class="txtseven carJourneyDetailsTableCell2">
                        <%# (string)((object[])Container.DataItem)[1] %>
                    </td>
                    <td headers="<%# (string)hdrDir.ClientID %>" class="txtseven carJourneyDetailsTableCell3">
                        <asp:Label ID="directionsLabel" runat="server" EnableViewState="false" Text="<%# SetLabelContent((string)((object[])Container.DataItem)[2]) %>">
                        </asp:Label>
                        <uc1:HyperlinkPostbackControl ID="directionsHyperlink" runat="server" Visible="<%# IsHyperlinkVisible((string)((object[])Container.DataItem)[2])%>"
                            Text="<%# GetDescription((string)((object[])Container.DataItem)[2]) %>" CommandArgument="<%# GetCarParkRef((string)((object[])Container.DataItem)[2])%>"
                            CommandName="<%# GetCarParkRef((string)((object[])Container.DataItem)[2])%>"
                            PrinterFriendly="<%# IsPrintable((string)((object[])Container.DataItem)[2])%>"></uc1:HyperlinkPostbackControl>
                        <br />
                        <asp:Label ID="openingTimesLabel" runat="server" EnableViewState="false" Visible="<%# CarParkLabelNoteVisible %>"
                            Text="<%# OpenTimeText%>">
                        </asp:Label>
                        
                        <uc1:CJPUserInfoControl id="CJPUserInfoControl1" runat="server" InfoType="LinkTOIDs" DisplayText="Toid:" NewLineBefore="false" NewLineAfter="true" />
                        <uc1:CJPUserInfoControl id="cjpUserLinkTOIDInfo" runat="server" InfoType="LinkTOIDs" DisplayText="<%# ((object[])Container.DataItem).Length > 7 ? (string)((object[])Container.DataItem)[8] : string.Empty %>" NewLineBefore="false" NewLineAfter="true" />
                         
                    </td>
                    <td headers="<%# (string)hdrDirDistance.ClientID %>" class="txtseven carJourneyDetailsTableCellTravelNews" Visible="<%# ShowTravelNewsIncidents %>" id="routeMatchingTravelNewsIncidents" runat="server">
                       <uc1:TravelNewsIncidentPopoutControl id="travelNewsIncidentPopoutControl" runat="server" TravelIncidents="<%# ((object[])Container.DataItem).Length > 9 ? (string[])((object[])Container.DataItem)[9] : new string[0]%>" PrinterFriendly="<%# IsPrintable(string.Empty)%>" />
                       <cc1:TDImage runat="server" ID="highTrafficSymbol" CssClass="highTrafficSymbol" ImageUrl="<%# HighTrafficSymbolUrl %>" Visible="<%# ((object[])Container.DataItem).Length > 10 ? (bool)((object[])Container.DataItem)[10] : false%>" 
                        ToolTip="<%# HighTrafficSymbolToolTip %>" AlternateText="<%# HighTrafficSymbolToolTip %>" onclick="<%# GetShowPopupScript() %>"/>
                    </td>
                    <td headers="<%# (string)hdrArrive.ClientID %>" class="txtseven carJourneyDetailsTableCell4" Visible="<%# ShowDirectionTime %>" id="routeDirections1CellTime" runat="server">
                        <%# (string)((object[])Container.DataItem)[3] %>
                        
                    </td>
                    <td headers="<%# (string)hdrDirDistance.ClientID %>" class="txtseven carJourneyDetailsTableCell5" Visible="<%# ShowDirectionDistance %>" id="routeDirections1CellDistance" runat="server">
                        <%# (string)((object[])Container.DataItem)[6] %>
                       
                    </td>
                    <td class="txtseven carJourneyDetailsTableCell6">
                        &nbsp;
                        <cc1:TDButton ID="buttonMap" runat="server" Text="<%# MapButtonText %>" Visible="<%# MapShow( (bool)((object[])Container.DataItem)[5] )%>"
                            CommandArgument="<%# Container.ItemIndex %>"></cc1:TDButton>
                         
                        <uc1:CJPUserInfoControl id="cjpUserJourneySpeedInfo" runat="server" InfoType="JourneySpeed" DisplayText="<%# ((object[])Container.DataItem).Length > 7 ? (string)((object[])Container.DataItem)[7] : string.Empty %>" ShowAsToolTip="false" />
                         
                    </td>
                </tr>
                <% if (TableBreakRequired(5))
                   { %>
                <tr>
                    <td colspan="5" height="15px">&nbsp;</td>
                </tr>
                <% } %>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
<asp:Table ID="RouteDirections2" CellSpacing="0" Width="100%" BorderWidth="0" runat="server">
    <asp:TableRow runat="server" Visible="<%# (bool) FooterDetail[5] %>">
        <asp:TableCell CssClass="carJourneyDetailsTableFooter1">&nbsp;</asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrCongestionSymbol" CssClass="txtseven carJourneyDetailsTableFooter2">
			<%#(string) FooterDetail[2] %>
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrCongestionText" CssClass="txtseven carJourneyDetailsTableFooter3">
			<%# (string)FooterDetail[3] %>
        </asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter4" Visible="<%# ShowDirectionTime %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter5" Visible="<%# ShowDirectionDistance %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter6">&nbsp;</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server" Visible="<%# (bool) FooterDetail[4] %>">
        <asp:TableCell CssClass="carJourneyDetailsTableFooter1">&nbsp;</asp:TableCell>
        <asp:TableCell HorizontalAlign="Left" ID="hdrThinkLogo" CssClass="txtseven carJourneyDetailsTableFooter2">
			<%#(string) FooterDetail[0] %>
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrThinkText" CssClass="txtseven carJourneyDetailsTableFooter3">
			<%# (string) FooterDetail[1] %>
        </asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter4" Visible="<%# ShowDirectionTime %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter5" Visible="<%# ShowDirectionDistance %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter6">&nbsp;</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server" Visible="<%# (bool) FooterDetail[12] %>">
        <asp:TableCell CssClass="carJourneyDetailsTableFooter1">&nbsp;</asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrCarSharingLogo" CssClass="txtseven carJourneyDetailsTableFooter2">
			<%#(string) FooterDetail[13] %>
        </asp:TableCell>
        <asp:TableCell Visible="<%# NonPrintable %>" HorizontalAlign="left" ID="hdrCarSharingLink" CssClass="txtseven carJourneyDetailsTableFooter3">
            <asp:hyperlink runat="server" id="carSharingLink" NavigateUrl="<%# FooterDetail[15] %>"><%# FooterDetail[14] %></asp:hyperlink>
        </asp:TableCell>
        <asp:TableCell Visible="<%# !NonPrintable %>" HorizontalAlign="left" ID="hdrCarSharingText" CssClass="txtseven carJourneyDetailsTableFooter3">
            <%# FooterDetail[14] %>
        </asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter4" Visible="<%# ShowDirectionTime %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter5" Visible="<%# ShowDirectionDistance %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter6">&nbsp;</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server" Visible="<%# (bool) FooterDetail[8] %>">
        <asp:TableCell CssClass="carJourneyDetailsTableFooter1">&nbsp;</asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrPetrolEfficientLogo" CssClass="txtseven carJourneyDetailsTableFooter2">
			<%#(string) FooterDetail[6] %>
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrPetrolEfficientText" CssClass="txtseven carJourneyDetailsTableFooter3">
            <uc1:HyperlinkPostbackControl ID="fuelEmissionLinkControlFooter" Text="<%# FooterDetail[7] %>" runat="server"></uc1:HyperlinkPostbackControl>
        </asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter4" Visible="<%# ShowDirectionTime %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter5" Visible="<%# ShowDirectionDistance %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter6">&nbsp;</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow1" runat="server" Visible="<%# (bool) FooterDetail[11] %>">
        <asp:TableCell CssClass="carJourneyDetailsTableFooter1">&nbsp;</asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrFindNearestCarParkLogo" CssClass="txtseven carJourneyDetailsTableFooter2">
			<%#(string) FooterDetail[10] %>
        </asp:TableCell>
        <asp:TableCell HorizontalAlign="left" ID="hdrFindNearestCarParkText" CssClass="txtseven carJourneyDetailsTableFooter3">
            <uc1:HyperlinkPostbackControl ID="findNearestCarParkLinkControlFooter" Text="<%# FooterDetail[9] %>" runat="server"></uc1:HyperlinkPostbackControl>
        </asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter4" Visible="<%# ShowDirectionTime %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter5" Visible="<%# ShowDirectionDistance %>">&nbsp;</asp:TableCell>
        <asp:TableCell CssClass="carJourneyDetailsTableFooter6">&nbsp;</asp:TableCell>
    </asp:TableRow>
</asp:Table>

    </div>
</div>