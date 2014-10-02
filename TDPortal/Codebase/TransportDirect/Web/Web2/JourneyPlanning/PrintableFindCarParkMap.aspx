<%@ Register TagPrefix="uc1" TagName="FindCarParkResultsLocationControl" Src="../Controls/FindCarParkResultsLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCarParksResultsTableControl" Src="../Controls/FindCarParksResultsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableMapControl" Src="../Controls/PrintableMapControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<%@ Page Language="c#" Codebehind="PrintableFindCarParkMap.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableFindCarParkMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,PrintableFindCarParkMap.aspx.css,MapPrint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableFindCarParkMap" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            
                <uc1:FindCarParkResultsLocationControl ID="carParkResultsLocationControl" runat="server">
                </uc1:FindCarParkResultsLocationControl>
            
                <uc1:FindCarParksResultsTableControl ID="carParkResultsTableControl" runat="server">
                </uc1:FindCarParksResultsTableControl>
                
            <div class="boxtypeeightstd">
                <uc1:PrintableMapControl ID="mapOutward" runat="server"></uc1:PrintableMapControl>
            </div>
            <div class="boxtypeeightstd">
                <p>
                    <asp:Label ID="labelDateTimeTitle" runat="server"></asp:Label><asp:Label ID="labelDateTime" runat="server"></asp:Label></p>
                <p>
                    <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label><asp:Label ID="labelUsername" runat="server"></asp:Label></p>
            </div>
        </form>
    </div>
</body>
</html>
