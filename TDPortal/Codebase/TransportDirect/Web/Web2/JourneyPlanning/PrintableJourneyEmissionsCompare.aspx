<%@ Page Language="c#" Codebehind="PrintableJourneyEmissionsCompare.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableJourneyEmissionsCompare"
    ValidateRequest="false" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsCompareControl" Src="../Controls/JourneyEmissionsCompareControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css, jpstdprint.css, EmissionsPrint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableJourneyEmissionsCompare" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div class="boxtypesixteen">
                <h1>
                    <asp:Label ID="labelTitle" runat="server" EnableViewState="false"></asp:Label></h1>
            </div>
            <div class="spacer1">
                &nbsp;</div>
            <uc1:JourneyEmissionsCompareControl ID="journeyEmissionsCompareControl" runat="server">
            </uc1:JourneyEmissionsCompareControl>
            <br />
            <asp:Panel runat="server" ID="EmissionsInformationHtmlPlaceholderControl"></asp:Panel>
            <div class="boxtypeeightstd">
                <p class="txtseven">
                    <asp:Label ID="labelDateTimeTitle" runat="server" EnableViewState="false"></asp:Label>&nbsp;
                    <asp:Label ID="labelDateTime" runat="server" EnableViewState="false"></asp:Label></p>
                <p class="txtseven">
                    <asp:Label ID="labelUsernameTitle" runat="server" EnableViewState="false"></asp:Label>
                    <asp:Label ID="labelUsername" runat="server" EnableViewState="false"></asp:Label></p>
            </div>
        </form>
    </div>
</body>
</html>
