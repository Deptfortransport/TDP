<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyTicketCostsControl" Src="../Controls/JourneyTicketCostsControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>

<%@ Page Language="c#" Codebehind="PrintableRefineTickets.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableRefineTickets" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,RefineTicketsPrint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableRefineTickets" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div id="boxtypelargeeight">
                <asp:Label ID="labelTitle" CssClass="ExtendedLabels" runat="server"></asp:Label>
                <br/>
            </div>
            <div class="RefineTicketsOuterControl">
                <div class="RefineTicketsOuterControlHeading">
                    <asp:Label ID="labelOutward" runat="server"></asp:Label>
                    <asp:Label ID="labelOutwardLocations" CssClass="LocationsTitleLabel" runat="server"></asp:Label>
                </div>
                <uc1:JourneyTicketCostsControl ID="outwardJourneyTicketCostsControl" runat="server">
                </uc1:JourneyTicketCostsControl>
            </div>
            <asp:Panel ID="returnTicketsPanel" runat="server" CssClass="RefineTicketsOuterControl">
                <div class="RefineTicketsOuterControlHeading">
                    <asp:Label ID="labelReturn" runat="server"></asp:Label>
                    <asp:Label ID="labelReturnLocations" runat="server" CssClass="LocationsTitleLabel"></asp:Label></div>
                <uc1:JourneyTicketCostsControl ID="returnJourneyTicketCostsControl" runat="server"></uc1:JourneyTicketCostsControl>
            </asp:Panel>
            <div class="boxtypeeightstd">
                <p class="txtseven">
                    <asp:Label ID="labelDateTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelDate" runat="server"></asp:Label></p>
                <p class="txtseven">
                    <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelUsername" runat="server"></asp:Label></p>
            </div>
        </form>
    </div>
</body>
</html>
