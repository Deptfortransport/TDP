<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintableFindMapResult.aspx.cs" Inherits="TransportDirect.UserPortal.Web.Templates.PrintableFindMapResult" %>

<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableMapControl" Src="../Controls/PrintableMapControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
<head>
    <cc2:HeadElementControl id="headElementControl" runat="server" stylesheets="setup.css,jpstdprint.css,MapPrint.css">
    </cc2:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableFindMapResult" runat="server">
        <div>
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            
            <div class="boxtypeeightstd">
                <h1>
                   <asp:Label ID="labelMap" runat="server"></asp:Label>
                   <asp:Label ID="labelSelectedLocation" runat="server"></asp:Label>
                </h1>
            </div>
            
            <div class="boxtypeeightstd">
                <uc1:PrintableMapControl ID="map" runat="server"></uc1:PrintableMapControl>
            </div>
            
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
