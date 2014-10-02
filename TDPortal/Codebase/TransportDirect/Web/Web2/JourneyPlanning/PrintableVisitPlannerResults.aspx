<%@ Page Language="c#" Codebehind="PrintableVisitPlannerResults.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableVisitPlannerResults" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="VisitPlannerRequestDetailsControl" Src="../Controls/VisitPlannerRequestDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RouteSelectionControl" Src="../Controls/RouteSelectionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsControl" Src="../Controls/JourneyDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsTableControl" Src="../Controls/JourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableMapControl" Src="../Controls/PrintableMapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,visitplannerprint.css,MapPrint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="Form1" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <asp:Table runat="server" ID="tableLayout">
                <asp:TableRow>
                    <asp:TableCell>
                        <uc1:VisitPlannerRequestDetailsControl ID="visitPlannerRequestDetailsControl" runat="server">
                        </uc1:VisitPlannerRequestDetailsControl>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowJourney1" runat="server">
                    <asp:TableCell>
                        <span class="txteightb">
                            <asp:Label runat="server" ID="labelJourneyDetails1"></asp:Label></span>
                        <br />
                        <uc1:RouteSelectionControl runat="server" ID="routeSelectionJourney1"></uc1:RouteSelectionControl>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowJourney2" runat="server">
                    <asp:TableCell>
                        <span class="txteightb">
                            <asp:Label runat="server" ID="labelJourneyDetails2"></asp:Label></span>
                        <br />
                        <uc1:RouteSelectionControl runat="server" ID="routeSelectionJourney2"></uc1:RouteSelectionControl>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowJourney3" runat="server">
                    <asp:TableCell>
                        <span class="txteightb">
                            <asp:Label runat="server" ID="labelJourneyDetails3"></asp:Label></span>
                        <br />
                        <uc1:RouteSelectionControl runat="server" ID="routeSelectionJourney3"></uc1:RouteSelectionControl>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowDetails">
                    <asp:TableCell>
                        <div class="boxtypeprint">
                            <div class="boxtypewhiteprint">
                                <span class="txteightb">
                                    <asp:Label ID="labelDetails" runat="server"></asp:Label>&nbsp; </span>
                            </div>
                            <uc1:JourneyDetailsTableControl ID="journeyDetailsTableControl" runat="server"></uc1:JourneyDetailsTableControl>
                            <uc1:JourneyDetailsControl ID="journeyDetailsControl" runat="server"></uc1:JourneyDetailsControl>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowMap">
                    <asp:TableCell>
                        <div class="boxtypewhiteprint">
                            <span class="txteightb">
                                <asp:Label ID="labelMap" runat="server"></asp:Label>&nbsp; </span>
                        </div>
                        <div class="boxtypeeightstd">
                            <uc1:PrintableMapControl ID="mapControl" runat="server"></uc1:PrintableMapControl>
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <div class="boxtypeeightstd">
                <p class="txtseven">
                    <asp:Label ID="labelDateTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelDate" runat="server"></asp:Label>
                </p>
                <p class="txtseven">
                    <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelUsername" runat="server"></asp:Label>
                </p>
            </div>
        </form>
    </div>
</body>
</html>
