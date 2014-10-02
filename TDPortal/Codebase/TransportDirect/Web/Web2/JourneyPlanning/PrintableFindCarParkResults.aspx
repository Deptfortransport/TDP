<%@ Register TagPrefix="uc1" TagName="FindCarParkResultsLocationControl" Src="../Controls/FindCarParkResultsLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCarParksResultsTableControl" Src="../Controls/FindCarParksResultsTableControl.ascx" %>

<%@ Page Language="c#" Codebehind="PrintableFindCarParkResults.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableFindCarParkResults" %>

<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" xml:lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableFindCarParkResults" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <p>
                &nbsp;</p>
            
                <uc1:FindCarParkResultsLocationControl ID="carParkResultsLocationControl" runat="server">
                </uc1:FindCarParkResultsLocationControl>
            
            
                <uc1:FindCarParksResultsTableControl ID="carParkResultsTableControl" runat="server">
                </uc1:FindCarParksResultsTableControl>
            
            <p>
                &nbsp;</p>
            <p>
                <asp:Label ID="labelDateTimeTitle" runat="server" EnableViewState="false"></asp:Label>
                <asp:Label ID="labelDateTime" runat="server" EnableViewState="false"></asp:Label></p>
            <p>
                <asp:Label ID="labelUsernameTitle" runat="server" EnableViewState="false"></asp:Label>
                <asp:Label ID="labelUsername" runat="server" EnableViewState="false"></asp:Label></p>
        </form>
    </div>
</body>
</html>
