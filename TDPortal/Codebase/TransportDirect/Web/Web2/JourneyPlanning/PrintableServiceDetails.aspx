<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ServiceOperationControl" Src="../Controls/ServiceOperationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ServiceNotesControl" Src="../Controls/ServiceNotesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ServiceHeaderControl" Src="../Controls/ServiceHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>

<%@ Page Language="c#" Codebehind="PrintableServiceDetails.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableServiceDetails" %>

<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CallingPointsControl" Src="../Controls/CallingPointsControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css">
    </cc1:HeadElementControl>
</head>
<body dir="ltr">
    <div class="CenteredContent">
        <form id="PrintableServiceDetails" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div class="boxtypeeightstd">
                <h1>
                    <asp:Label ID="labelServiceDetailsTitle" runat="server"></asp:Label></h1>
                <div>
                    &nbsp;</div>
            </div>
            <div id="boxtypetwelve">
                <div id="dmtitle">
                    <uc1:ServiceHeaderControl ID="serviceHeaderControl" runat="server"></uc1:ServiceHeaderControl>
                    <br/>
                </div>
                <div id="dmview">
                    <br/>
                    <uc1:CallingPointsControl ID="callingPointsControlBefore" runat="server"></uc1:CallingPointsControl>
                    <br/>
                    <uc1:CallingPointsControl ID="callingPointsControlLeg" runat="server"></uc1:CallingPointsControl>
                    <br/>
                    <uc1:CallingPointsControl ID="callingPointsControlAfter" runat="server"></uc1:CallingPointsControl>
                    <br/>
                    <uc1:ServiceNotesControl ID="serviceNotesControl" runat="server"></uc1:ServiceNotesControl>
                    <br/>
                    <uc1:ServiceOperationControl ID="serviceOperationControl" runat="server"></uc1:ServiceOperationControl>
                    <br/>
                </div>
            </div>
            <div class="boxtypeeightstd">
                <p class="txtseven">
                    <asp:Label ID="labelDateTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelDate" runat="server"></asp:Label></p>
                <p class="txtseven">
                    <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelUsername" runat="server"></asp:Label></p>
                <p class="txtseven">
                    <asp:Label ID="labelReferenceNumberTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelJourneyReferenceNumber" runat="server"></asp:Label></p>
            </div>
        </form>
    </div>
</body>
</html>
