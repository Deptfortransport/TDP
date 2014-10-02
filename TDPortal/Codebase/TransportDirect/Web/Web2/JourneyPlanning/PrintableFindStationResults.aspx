<%@ Register TagPrefix="uc1" TagName="FindStationResultsTable" Src="../Controls/FindStationResultsTable.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindStationResultsLocationControl" Src="../Controls/FindStationResultsLocationControl.ascx" %>

<%@ Page Language="c#" Codebehind="PrintableFindStationResults.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableFindStationResults" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,PrintableFindStationResults.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableFindStationResults" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <p>
                &nbsp;</p>
            
                <uc1:FindStationResultsLocationControl ID="locationControl" runat="server"></uc1:FindStationResultsLocationControl>
            
                <uc1:FindStationResultsTable ID="stationResultsTable" runat="server"></uc1:FindStationResultsTable>
            
            <p>
                &nbsp;</p>
            <p>
                <asp:Label ID="labelDateTimeTitle" runat="server"></asp:Label>
                <asp:Label ID="labelDateTime" runat="server"></asp:Label></p>
            <p>
                <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label>
                <asp:Label ID="labelUsername" runat="server"></asp:Label></p>
        </form>
    </div>
</body>
</html>
