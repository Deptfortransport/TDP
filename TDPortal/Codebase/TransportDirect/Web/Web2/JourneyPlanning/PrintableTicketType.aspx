<%@ Page Language="C#" AutoEventWireup="true" Codebehind="PrintableTicketType.aspx.cs" Inherits="TransportDirect.UserPortal.Web.Templates.PrintableTicketType" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register Src="../Controls/TicketTypeControl.ascx" TagName="TicketTypeControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
     <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css, jpstdprint.css,jpstd.css,homepage.css,nifty.css,expandablemenu.css,PrintableTicketType.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableJourneySummary" runat="server">
        
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div id="boxtypeeightstd">
                
                <h1>
                
                    <asp:Label ID="labelPageTitle" CssClass="onscreen" runat="server">Test</asp:Label>
               
                   </h1>
               
            <div>
                <div>

                        <uc1:TicketTypeControl id="TicketTypeControl1" runat="server">
                        </uc1:TicketTypeControl>
                </div>
            </div>
            </div>
        </form>
    </div>
</body>
</html>
