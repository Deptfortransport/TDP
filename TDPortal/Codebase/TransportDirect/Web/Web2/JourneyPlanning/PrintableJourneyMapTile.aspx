<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintableJourneyMapTile.aspx.cs" Inherits="TransportDirect.UserPortal.Web.Templates.PrintableJourneyMapTile" %>

<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrintableMapTileControl" Src="../Controls/PrintableMapTileControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
<head>
    <cc2:HeadElementControl id="headElementControl" runat="server" stylesheets="setup.css,jpstdprint.css,PrintableJourneyMapTile.aspx.css">
    </cc2:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableJourneyMapTile" runat="server">
            <div>
                <asp:Panel id="panelMapOutward" runat="server" visible="false">
                    <uc1:PrintableMapTileControl id="mapTileOutward" runat="server"></uc1:PrintableMapTileControl>
                </asp:Panel>
                
                <asp:Literal id="literalNewPage" runat="server" visible="false" Text='<div class="NewPage"></div>'></asp:Literal>
                
                <asp:Panel id="panelMapReturn" runat="server" visible="false">
                    <uc1:PrintableMapTileControl id="mapTileReturn" runat="server"></uc1:PrintableMapTileControl>
                </asp:Panel>
                
                <div class="boxtypeeightstd">
                    <p>
                        <asp:Label id="labelDateTimeTitle" runat="server" enableviewstate="false"></asp:Label>
                        <asp:Label id="labelDateTime" runat="server" enableviewstate="false"></asp:Label></p>
                    <p>
                        <asp:Label id="labelUsernameTitle" runat="server" enableviewstate="false" visible="false"></asp:Label>
                        <asp:Label id="labelUsername" runat="server" enableviewstate="false" visible="false"></asp:Label></p>
                    <p>
                        <asp:Label id="labelReferenceTitle" runat="server" enableviewstate="false" visible="false"></asp:Label>
                        <asp:Label id="labelReference" runat="server" enableviewstate="false" visible="false"></asp:Label></p>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
