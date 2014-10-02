<%@ Page Language="c#" Codebehind="PrintableJourneyEmissions.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableJourneyEmissions" %>

<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsControl" Src="../Controls/JourneyEmissionsControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,ExtendAdjustReplanPrintable.css,PrintableJourneyEmissions.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableJourneyEmissions" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div class="boxtypesixteen">
                <h1>
                    <asp:Label ID="labelTitle" runat="server" CssClass="ExtendedLabels" EnableViewState="false"></asp:Label></h1>
            </div>
            <br/>
            <div class="spacer1">
                &nbsp;</div>
            <asp:Panel ID="summaryPanel" CssClass="boxtypelargetwo" runat="server">
                <div>
                    <div class="boxtypetwentyfive">
                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControl" runat="server" />
                    </div>
                    <div class="boxtypetwentysix">
                        <uc1:ResultsSummaryControl ID="resultsSummaryControl" runat="server"></uc1:ResultsSummaryControl>
                    </div>
                    <div class="spacer2">
                        &nbsp;</div>
                </div>
            </asp:Panel>
            <uc1:JourneyEmissionsControl ID="journeyEmissionsControl" runat="server"></uc1:JourneyEmissionsControl>
            <div id="boxtypetwelvegreen">
                <div class="dmtitletwo">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="labelSaveFuelTitle" runat="server" CssClass="txtsevenb" EnableViewState="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br/>
                            </td>
                        </tr>
                        <tr>
                            <td class="txtseven">
                                <asp:Literal ID="labelSaveFuelText" runat="server"  EnableViewState="false"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
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
