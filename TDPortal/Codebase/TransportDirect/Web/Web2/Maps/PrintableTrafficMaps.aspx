<%@ Page Language="c#" Codebehind="PrintableTrafficMaps.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableTrafficMap" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrintableSimpleMapControl" Src="../Controls/PrintableSimpleMapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,PrintableTrafficMaps.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableTrafficMap" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div id="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <table cellspacing="0" width="100%">
                <tr>
                    <td colspan="2">
                        <uc1:PrintableSimpleMapControl ID="PrintableSimpleMapControl" runat="server"></uc1:PrintableSimpleMapControl>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            <asp:Label ID="labelDateTimeTitle" runat="server" Font-Size="Smaller"></asp:Label>
                            &nbsp;<asp:Label
                                ID="labelDateTime" runat="server"></asp:Label></p>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <p>
                            <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label><asp:Label ID="labelUsername"
                                runat="server"></asp:Label></p>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
