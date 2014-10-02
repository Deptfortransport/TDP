<%@ Page Language="c#" Codebehind="PrintableParkAndRide.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableParkAndRide" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ParkAndRideTableControl" Src="../Controls/ParkAndRideTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PritableParkAndRide" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div id="boxtypeeightstd">
                <p class="txtsevenb">
                    &nbsp;<asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server" EnableViewState="False"></asp:Label></p>
                <p class="txtsevenb">
                    &nbsp;<asp:Label ID="labelInstructions" CssClass="onscreen" runat="server" EnableViewState="False"></asp:Label></p>
            </div>
            <table id="travelview" cellspacing="0">
                <tr>
                    <td>
                        <div>
                            <h1>
                                &nbsp;<asp:Label ID="labelParkAndRideHeading" runat="server" EnableViewState="False">Park and ride schemes</asp:Label></h1>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>
                            &nbsp;</p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="boxtypeten" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <h2>
                                        <asp:Label ID="labelRegionDrop" runat="server" EnableViewState="False">Region:</asp:Label></h2>
                                    <span>&nbsp;</span><asp:Label ID="labelRegionDropValue" runat="server" CssClass="txtseven"
                                        EnableViewState="False"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <uc1:ParkAndRideTableControl ID="parkAndRideTable" runat="server"></uc1:ParkAndRideTableControl>
        </form>
    </div>
</body>
</html>
