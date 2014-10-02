<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl2" Src="../Controls/ResultsSummaryControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsCompareControl" Src="../Controls/JourneyEmissionsCompareControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>

<%@ Page Language="c#" Codebehind="PrintableJourneyEmissionsCompareJourney.aspx.cs"
    AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.PrintableJourneyEmissionsCompareJourney"
    ValidateRequest="false" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css, jpstdprint.css, emissionsprint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableJourneyEmissionsCompareJourney" method="post" runat="server">
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
            <asp:Panel ID="panelSummary" runat="server" CssClass="boxtypelargetwo">
                <div class="boxtypetwentyfive">
                    <uc1:ResultsTableTitleControl ID="resultsTableTitleControl" runat="server"></uc1:ResultsTableTitleControl>
                </div>
                <div class="boxtypetwentysix">
                    <uc1:ResultsSummaryControl2 ID="resultsSummaryControl" runat="server"></uc1:ResultsSummaryControl2>
                </div>
                <div class="spacer2">
                    &nbsp;</div>
            </asp:Panel>
            <uc1:JourneyEmissionsCompareControl ID="journeyEmissionsCompareControl" runat="server">
            </uc1:JourneyEmissionsCompareControl>
            <br/>
            <asp:Panel runat="server" id="EmissionsInformationHtmlPlaceholderControl"></asp:Panel>
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
