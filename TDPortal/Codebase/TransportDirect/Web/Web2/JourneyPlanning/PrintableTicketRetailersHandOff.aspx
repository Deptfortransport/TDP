<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="RetailerHandoffHeadingControl" Src="../Controls/RetailerHandoffHeadingControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RetailerHandoffDetailControl" Src="../Controls/RetailerHandoffDetailControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>

<%@ Page Language="c#" Codebehind="PrintableTicketRetailersHandOff.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableTicketRetailersHandOff" %>

<%@ Register TagPrefix="uc1" TagName="RetailerInformationControl" Src="../Controls/RetailerInformationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,ticketRetailersPrint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="TicketRetailersHandOff" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div id="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div id="boxtypeeight">
                <h1>
                    <asp:Label ID="labelPageName" runat="server"></asp:Label></h1>
            </div>
            <uc1:RetailerHandoffHeadingControl ID="handoffHeading" runat="server"></uc1:RetailerHandoffHeadingControl>
            <uc1:RetailerHandoffDetailControl ID="handoffDetail" runat="server"></uc1:RetailerHandoffDetailControl>
            <asp:Panel ID="panelDiscountCardDisclaimer" runat="server">
                <div id="tdisclaimer">
                    <asp:Label ID="labelDiscountDisclaimer" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label></div>
            </asp:Panel>
            <br />
            <asp:Panel ID="panelHandoff" runat="server">
                <div id="tdisclaimer">
                    <p>
                        <asp:Label ID="labelList3Heading" runat="server" EnableViewState="False" Font-Bold="True"></asp:Label></p>
                    <ul>
                        <li>
                            <asp:Label ID="labelList3Item1" runat="server" EnableViewState="False"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="labelList3Item2" runat="server" EnableViewState="False"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="labelList3Item3" runat="server" EnableViewState="False"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="labelList3Item4" runat="server" EnableViewState="False"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="labelList3Item5" runat="server" EnableViewState="False"></asp:Label>
                        </li>   
                        <li>
                            <asp:Label ID="labelList3Item6" runat="server" EnableViewState="False"></asp:Label>
                        </li>
                    </ul>
                </div>
            </asp:Panel>
            <asp:Panel ID="panelOfflineInformation" runat="server">
                <uc1:RetailerInformationControl ID="offlineRetailerInformation" runat="server"></uc1:RetailerInformationControl>
                <br />
            </asp:Panel>
        </form>
    </div>
</body>
</html>
