<%@ Register TagPrefix="uc1" TagName="TicketMatrixControl" Src="../Controls/TicketMatrixControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>

<%@ Page Language="c#" Codebehind="PrintableTicketRetailers.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableTicketRetailers" %>

<%@ Register TagPrefix="uc1" TagName="RetailerMatrixControl" Src="../Controls/RetailerMatrixControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyFareHeadingControl" Src="../Controls/JourneyFareHeadingControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareStepsControl" Src="../Controls/FindFareStepsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,ticketRetailersPrint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="TicketRetailers" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            
            <uc1:JourneyFareHeadingControl ID="journeyFareHeadingControl" runat="server"></uc1:JourneyFareHeadingControl>
            
            <asp:Panel ID="panelFindFareSteps" runat="server" visible="false">
                <div class="boxtypeeightstd">
                    <uc1:FindFareStepsControl ID="findFareStepsControl" runat="server"></uc1:FindFareStepsControl>
                </div>
            </asp:Panel>
            <div class="boxtypeeightstd">
               
            <uc1:JourneyPlannerOutputTitleControl ID="journeyPlannerOutputTitle" runat="server">
            </uc1:JourneyPlannerOutputTitleControl>
                
                <p>
                    &nbsp;</p>
            </div>
            <asp:Panel ID="panelOutward" runat="server">
                <uc1:TicketMatrixControl ID="outwardTickets" runat="server"></uc1:TicketMatrixControl>
                <asp:Panel ID="outwardRetailerPanel" runat="server">
                    <uc1:RetailerMatrixControl ID="outwardRetailers" runat="server"></uc1:RetailerMatrixControl>
                </asp:Panel>
                <br />
            </asp:Panel>
            <asp:Panel ID="panelInward" runat="server">
                <uc1:TicketMatrixControl ID="inwardTickets" runat="server"></uc1:TicketMatrixControl>
                <asp:Panel ID="inwardRetailerPanel" runat="server">
                    <uc1:RetailerMatrixControl ID="inwardRetailers" runat="server"></uc1:RetailerMatrixControl>
                </asp:Panel>
                <br />
            </asp:Panel>
            <br />
        </form>
    </div>
</body>
</html>
