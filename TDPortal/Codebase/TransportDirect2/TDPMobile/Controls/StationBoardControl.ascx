<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StationBoardControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.StationBoardControl" %>

<div id="stationBoardUnavailableDiv" runat="server" class="stationBoardUnavailable">
    <asp:Label ID="stationBoardUnavailableLbl" runat="server" CssClass="error" />
</div>

<div id="stationBoardDiv" runat="server" class="stationBoard">
    <div id="results" class="stationBoardResult jsshow">
        
        <div id="directionContainer" class="directionContainer">
            <table class="directionTable">
                <tr>
                    <td>
                        <div id="directionDepartureDiv" runat="server" class="direction directionDeparture">
                            <asp:Button ID="btnDepartures" runat="server" OnClick="btnDepartures_Click" CssClass="btnDirection btnDepartures" EnableViewState="true" />
                        </div>
                    </td>
                    <td>
                        <div id="directionArrivalDiv" runat="server" class="direction directionArrival">
                            <asp:Button ID="btnArrivals" runat="server" OnClick="btnArrivals_Click" CssClass="btnDirection btnArrivals" EnableViewState="true" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
                    
        <asp:Repeater ID="stationBoardRptr" runat="server" OnItemDataBound="stationBoardRptr_DataBound">
            <HeaderTemplate>
                <ul id="stationBoardList" class="stationBoardList">
                <li>
                    <div class="stationBoardItem stationBoardHead">
                        <div class="serviceTime">
                            <asp:Label ID="serviceTimeScheduledLbl" runat="server" />
                        </div>
                        <div class="serviceLocation">
                            <asp:Label ID="serviceStationLbl" runat="server" />
                        </div>
                        <div class="servicePlatform">
                            <asp:Label ID="servicePlatformLbl" runat="server" />
                        </div>
                    </div>
                </li>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:HyperLink ID="showDetailsLink" runat="server">
                        <div class="stationBoardItem">
                            <asp:HiddenField ID="serviceId" runat="server" />
                            <div class="serviceTime">
                                <asp:Label ID="serviceTimeScheduledLbl" runat="server" CssClass="timeScheduled" />
                                <asp:Label ID="serviceTimeActualLbl" runat="server" CssClass="timeActual"/>
                            </div>
                            <div class="serviceLocation">
                                <asp:Label ID="serviceStationLbl" runat="server" CssClass="station" />
                                <asp:Label ID="serviceReportLbl" runat="server" CssClass="report" />
                            </div>
                            <div class="servicePlatform">
                                <asp:Label ID="servicePlatformLbl" runat="server" CssClass="platform" />
                            </div>
                        </div>
                    </asp:HyperLink>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>

        <div class="stationBoardLastUpdated">
            <asp:Label ID="stationBoardLastUpdatedLbl" runat="server"/>
            <asp:HyperLink ID="stationBoardUpdateLink" runat="server" CssClass="stationBoardUpdate"></asp:HyperLink>
        </div>

    </div>
</div>