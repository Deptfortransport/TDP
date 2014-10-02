<%@ Register TagPrefix="uc1" TagName="ShowNewsControl" Src="../Controls/ShowNewsControl.ascx" %>

<%@ Page Language="c#" Codebehind="PrintableTravelNews.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableTravelNews" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="TravelNewsDetailsControl" Src="../Controls/TravelNewsDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TravelNewsMapKeyControl" Src="../Controls/TravelNewsMapKeyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableTravelNews" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div id="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <!-- End of Transport Direct Header info -->
            <table id="travelview" cellspacing="0">
                <tr>
                    <td>
                        <h1>
                            <asp:Label ID="lblLiveTravelNews" runat="server"></asp:Label></h1>
                    </td>
                </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDateTime" runat="server" CssClass="txtseven"></asp:Label></td>
                    </tr>
                <tr>
                    <td>
                        <p>
                            &nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h1>
                            <asp:Label ID="lblSpecificNews" runat="server"></asp:Label></h1>
                    </td>
                </tr>
            </table>
            <table class="travelNewsSummaryBox" cellspacing="2">
                <tr>
                    <td>
                        <asp:Label ID="labelNoTravelNews" runat="server" CssClass="txtseven"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lblTransportDrop" runat="server" EnableViewState="False"></asp:Label></h2>
                        <span>&nbsp;</span><asp:Label ID="lblTransportDropValue" runat="server" CssClass="txtseven"
                            EnableViewState="False"></asp:Label></td>
                    <td>
                        <h2>
                            <asp:Label ID="lblRegionDrop" runat="server" EnableViewState="False"></asp:Label></h2>
                        <span>&nbsp;</span><asp:Label ID="lblRegionDropValue" runat="server" CssClass="txtseven"
                            EnableViewState="False"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lblDelaysDrop" runat="server"></asp:Label></h2>
                        <span>&nbsp;</span><asp:Label ID="lblDelaysDropValue" runat="server" CssClass="txtseven"></asp:Label></td>
                    <td>
                        <h2>
                            <asp:Label ID="lblType" runat="server"></asp:Label></h2>
                        <span>&nbsp;</span><asp:Label ID="lblTypeDropValue" runat="server" CssClass="txtseven"></asp:Label>
                    </td>
                </tr>
            </table>
           
            <table class="travelview" cellspacing="0">
                <tr>
                    <td colspan="2">
                        <uc1:TravelNewsDetailsControl ID="TravelNewsDetails" runat="server" EnableViewState="False">
                        </uc1:TravelNewsDetailsControl>
                        <asp:Panel runat="server" ID="PanelMapControls">
                            <div class="travelNewsMapBox">
                                <asp:Image runat="server" CssClass="printableTravelNewsImage" ID="ImageMap"></asp:Image></div>
                            <br/>
                            <div class="travelNewsMapBox">
                                <uc1:TravelNewsMapKeyControl ID="MapKeyControl" runat="server"></uc1:TravelNewsMapKeyControl>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <div id="Div1">
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
