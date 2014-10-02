<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintableEBCJourneyMap.aspx.cs" Inherits="TransportDirect.UserPortal.Web.JourneyPlanning.PrintableEBCJourneyMap" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping"
    Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="EBCCalculationDetailsTableControl" Src="../Controls/EBCCalculationDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableMapControl" Src="../Controls/PrintableMapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarSummaryControl" Src="../Controls/CarSummaryControl.ascx" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc2:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,PrintableEBCJourneyMap.aspx.css">
    </cc2:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableJourneyMap" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div class="boxtypeeightstd">
                <uc1:JourneysSearchedForControl ID="journeysSearchedForControl" runat="server"></uc1:JourneysSearchedForControl>
            </div>
            
            <asp:Panel ID="panelMapOutward" runat="server" Visible="False">
                <div class="boxtypeeightstd">
                    <uc1:CarSummaryControl id="carSummaryControl" runat="server"></uc1:CarSummaryControl>
                </div>
                <uc1:PrintableMapControl ID="mapOutward" runat="server"></uc1:PrintableMapControl>
                <div class="EBCTableContent">
                    <uc1:EBCCalculationDetailsTableControl id="ebcCalculationDetailsTableControl" runat="server"></uc1:EBCCalculationDetailsTableControl>                
                </div>
            </asp:Panel>
            <div class="boxtypeeightstd">
                <p>
                    <asp:Label ID="labelDateTimeTitle" runat="server"></asp:Label><asp:Label ID="labelDateTime"
                        runat="server"></asp:Label></p>
                <p>
                    <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label><asp:Label ID="labelUsername"
                        runat="server"></asp:Label></p>
                <p>
                    <asp:Label ID="labelReferenceTitle" runat="server"></asp:Label><asp:Label ID="labelReference"
                        runat="server"></asp:Label></p>
                <p>
                </p>
            </div>
        </form>
    </div>
</body>
</html>

