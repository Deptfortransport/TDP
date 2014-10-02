<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FareDetailsTableControl" Src="FareDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FareDetailsDiagramControl" Src="FareDetailsDiagramControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ZonalFareDetailsTableSegmentControl" Src="ZonalFareDetailsTableSegmentControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyFaresControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.JourneyFaresControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div class="boxtypetwelvetickets">
    <cc1:HelpLabelControl ID="helpJourneyFaresLabelControl" CssMainTemplate="helpboxinfaretable"
        Visible="False" runat="server"></cc1:HelpLabelControl>
    <table class="fdetail" width="100%">
        <tr>
            <td valign="top">
                <asp:Panel ID="matchingReturnFaresPanel" runat="server">
                    <div id="dmtitle">
                        <asp:Label ID="labelSeeOutwardFares" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label></div>
                </asp:Panel>
                <asp:Panel ID="faresPanel" runat="server">
                    <asp:Panel ID="faresHeadingPanel" runat="server">
                        <div class="dmtitletickets">
                            <table width="100%">
                                <tr class="txtseven">
                                    <td align="right" colspan="3">
                                        <a href="#discounts">
                                            <asp:Label ID="hyperlinkAmendFareDetails" runat="server" EnableViewState="false"></asp:Label>
                                            <asp:Image ID="hyperlinkImageAmendFareDetails" runat="server"></asp:Image></a></td>
                                </tr>
                                <tr>
                                    <td class="txteightb" colspan="2">
                                        <h2>
                                            <asp:Label ID="labelJourneyDirection" runat="server" EnableViewState="false"></asp:Label></h2>
                                        &nbsp;&nbsp;
                                        <asp:Label ID="labelLocations" runat="server" EnableViewState="false" CssClass="LocationsLabel"></asp:Label></td>
                                    <td align="right">
                                        <!--<cc1:helpcustomcontrol id="helpJourneyFaresControl" runat="server" imagealign="Right" scrolltohelp="True"
											helplabel="helpJourneyFaresLabelControl"></cc1:helpcustomcontrol>-->
                                        <!--<cc1:TDButton ID="buttonRetailers" runat="server"></cc1:TDButton>&nbsp;-->
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="labelNoSelectionError" runat="server" EnableViewState="false" CssClass="errormsg txtlineheight15"></asp:Label>
                                        <div id="divNotes" runat="server" enableviewstate="false">
                                            <div class="floatleft">
                                                <asp:Label ID="labelNotePrefix" runat="server" EnableViewState="false" CssClass="txtnoteb txtlineheight15">&nbsp;</asp:Label>
                                            </div>
                                            <div class="floatleft">
                                                <asp:Label ID="labelAdditionalNote" runat="server" EnableViewState="false" CssClass="txtnoteb txtlineheight15">&nbsp;</asp:Label>
                                                <asp:Label ID="labelSingleTicketsNote" runat="server" EnableViewState="false" CssClass="txtnoteb txtlineheight15">&nbsp;</asp:Label>
                                                <asp:Label ID="labelMultipleTravelcardsNote" runat="server" EnableViewState="false" CssClass="txtnoteb txtlineheight15">&nbsp;</asp:Label>
                                            </div>
                                            <div class="clearboth"></div>
                                        </div>
                                        <asp:Label ID="labelFaresExplanation" runat="server" EnableViewState="false" CssClass="txtseven txtlineheight15">&nbsp;</asp:Label>
                                        <asp:Label ID="labelNoFareInformation" runat="server" EnableViewState="false" CssClass="txtseven txtlineheight15"></asp:Label>
                                        <asp:Label ID="labelFindCheaperPrefix" runat="server" EnableViewState="false" CssClass="txtseven txtlineheight15"></asp:Label>
                                            <span class="txtseven txtlineheight15">
                                                <uc1:HyperlinkPostbackControl ID="findCheaperLinkControl" runat="server" EnableViewState="false">
                                                </uc1:HyperlinkPostbackControl>
                                            </span>
                                        <asp:Label ID="labelFindCheaperSuffix" runat="server" EnableViewState="false" CssClass="txtseven txtlineheight15"></asp:Label>
                                        <asp:Label ID="labelBreakOfJourney" runat="server" EnableViewState="false" CssClass="txtseven txtlineheight15"></asp:Label>
                                        <asp:Label ID="labelNoThroughFare" runat="server" CssClass="txtseven txtlineheight15"></asp:Label></td>
                                    <td align="right">
                                        <cc1:TDButton ID="buttonChangeView" runat="server"></cc1:TDButton>
                                        <p>
                                            <strong>
                                                <asp:Label ID="labelView" runat="server" EnableViewState="False"></asp:Label>
                                                <uc1:HyperlinkPostbackControl ID="viewOtherFareLinkControl" runat="server" EnableViewState="false">
                                                </uc1:HyperlinkPostbackControl>
                                            </strong>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="faresDisplayPanel" runat="server">
                        <div class="dmview">
                            <asp:Label ID="labelFaresIncludedAbove" runat="server" CssClass="faresIncludedAbove"></asp:Label>
                            <uc1:FareDetailsDiagramControl ID="fareDetailsDiagram" runat="server"></uc1:FareDetailsDiagramControl>
                            <uc1:FareDetailsTableControl ID="fareDetailsTable" runat="server"></uc1:FareDetailsTableControl>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>
