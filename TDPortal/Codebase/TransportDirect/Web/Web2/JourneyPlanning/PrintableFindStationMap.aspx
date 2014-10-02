<%@ Register TagPrefix="uc1" TagName="PrintableMapControl" Src="../Controls/PrintableMapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindStationResultsTable" Src="../Controls/FindStationResultsTable.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindStationResultsLocationControl" Src="../Controls/FindStationResultsLocationControl.ascx" %>

<%@ Page Language="c#" Codebehind="PrintableFindStationMap.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableFindStationMap" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapLocationIconsDisplayControl" Src="../Controls/MapLocationIconsDisplayControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,PrintableFindStationMap.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableFindStationMap" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            
                <uc1:FindStationResultsLocationControl ID="locationControl" runat="server"></uc1:FindStationResultsLocationControl>
            
                <uc1:FindStationResultsTable ID="stationResultsTable" runat="server"></uc1:FindStationResultsTable>
            
            <table summary="Printable SimpleMap Body" cellpadding="0" cellspacing="0" id="mapTable" runat="server">
                <tr>
                    <td>
                        <div id="boxtypeonem">
                            <center>
                                <asp:Image ID="imageMap" CssClass="printableImage" runat="server"></asp:Image></center>
                        </div>
                        <div align="right">
                            <span class="txtseven">
                                <asp:Label ID="labelMapScaleTitle" runat="server"></asp:Label><asp:Label ID="labelMapScale"
                                    runat="server"></asp:Label></span></div>
                    </td>
                    <td valign="top">
                        <div id="mapbox">
                            <asp:Panel ID="panelIcons" runat="server">
                                <uc1:MapLocationIconsDisplayControl ID="iconsDisplay" runat="server"></uc1:MapLocationIconsDisplayControl>
                            </asp:Panel>
                            <div id="mhd">
                                <asp:Label ID="labelOverview" runat="server"></asp:Label></div>
                            <div id="mlm">
                                <asp:Image ID="imageOverview" runat="server"></asp:Image></div>
                        </div>
                    </td>
                </tr>
            </table>
            <p>
                <asp:Label ID="labelDateTimeTitle" runat="server"></asp:Label><asp:Label ID="labelDateTime"
                    runat="server"></asp:Label></p>
            <p>
                <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label><asp:Label ID="labelUsername"
                    runat="server"></asp:Label></p>
        </form>
    </div>
</body>
</html>
