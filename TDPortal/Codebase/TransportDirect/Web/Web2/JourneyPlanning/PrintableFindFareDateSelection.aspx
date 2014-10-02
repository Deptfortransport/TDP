<%@ Page Language="c#" Codebehind="PrintableFindFareDateSelection.aspx.cs" AutoEventWireup="True"
    Inherits="Web.Templates.PrintableFindFareDateSelection" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareSingleTravelDatesControl" Src="../Controls/FindFareSingleTravelDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareReturnTravelDatesControl" Src="../Controls/FindFareReturnTravelDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareStepsControl" Src="../Controls/FindFareStepsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintablePageInfoControl" Src="../Controls/PrintablePageInfoControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en-GB" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,FindAFarePrint.css,ticketRetailersPrint.css">
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
            <div class="boxtypeeightstd">
                <uc1:JourneysSearchedForControl ID="theJourneysSearchedForControl" runat="server"></uc1:JourneysSearchedForControl>
            </div>
            <div class="boxtypeeightstd">
                <uc1:FindFareStepsControl ID="findFareStepsControl" runat="server"></uc1:FindFareStepsControl>
            </div>
            <div class="boxtypeeightstd">
                <uc1:JourneyPlannerOutputTitleControl ID="JourneyPlannerOutputTitleControl1" runat="server">
                </uc1:JourneyPlannerOutputTitleControl>
            </div>
            <!-- INSERT RESULTSTABLETITLECONTROL -->
            
                <uc1:FindFareSingleTravelDatesControl ID="findFareSingleTravelDatesControl" runat="server"
                    EnableViewState="False" Visible="False"></uc1:FindFareSingleTravelDatesControl>
                <uc1:FindFareReturnTravelDatesControl ID="findFareReturnTravelDatesControl" runat="server"
                    EnableViewState="False" Visible="False"></uc1:FindFareReturnTravelDatesControl>
            
            <!-- Printer page information -->
            <br />
            <div id="boxtypeeightstd">
                <table lang="en" id="pagef" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="left">
                            <asp:Label ID="noteLabel" runat="server" EnableViewState="false"></asp:Label></td>
                    </tr>
                </table>
            </div>
            <br />
            <uc1:PrintablePageInfoControl ID="printablePageInfoControl" runat="server"></uc1:PrintablePageInfoControl>
            
        </form>
    </div>
</body>
</html>
