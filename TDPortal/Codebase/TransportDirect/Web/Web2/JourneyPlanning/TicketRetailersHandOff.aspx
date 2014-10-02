<%@ Register TagPrefix="uc1" TagName="RetailerHandoffHeadingControl" Src="../Controls/RetailerHandoffHeadingControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RetailerHandoffDetailControl" Src="../Controls/RetailerHandoffDetailControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>

<%@ Page ValidateRequest="false" Language="c#" Codebehind="TicketRetailersHandOff.aspx.cs"
    AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.TicketRetailersHandOff" %>

<%@ Register TagPrefix="uc1" TagName="RetailerInformationControl" Src="../Controls/RetailerInformationControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,ticketRetailers.css,homepage.css,expandablemenu.css,nifty.css,TicketRetailersHandOff.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="TicketRetailersHandOff" method="post" runat="server">
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl ID="clientLink" runat="server" EnableViewState="False" Visible="False">
                            </uc1:ClientLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <a name="SkipToMain"></a>
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="595" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelBackTop" CssClass="panelBackTop" runat="server">
                                                    </asp:Panel>
                                                </td>
                                                <td align="left">
                                                    <h1>
                                                        <asp:Label ID="labelFindPageTitle" runat="server"></asp:Label></h1>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- Journey Planning Controls -->
                                        <asp:Panel ID="panelSubHeading" runat="server">
                                            <div class="boxtypeeightalt">
                                                <asp:Label ID="labelFromToTitle" runat="server" CssClass="txtseven"></asp:Label></div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelErrorMessage" runat="server" Visible="False">
                                            <div id="boxtypeeightalt">
                                                <asp:Label ID="labelErrorMessages" runat="server" CssClass="txtseven"></asp:Label></div>
                                        </asp:Panel>
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <% /* Start: Content to be replaced when white labelling */ %>
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <div class="boxjourneychangesearchback">
                                                            <cc1:TDButton ID="back1" runat="server" EnableViewState="False"></cc1:TDButton>
                                                        </div>
                                                        <div class="boxjourneychangesearchchange">
                                                            <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyButton" runat="server"></uc1:PrinterFriendlyPageButtonControl>
                                                        </div>
                                                    </div>
                                                    <div id="boxtypeeightstd">
                                                        <h1>
                                                            <asp:Label ID="labelPageName" runat="server" EnableViewState="False"></asp:Label></h1>
                                                        <asp:Label ID="labelClickContinue" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label>
                                                    </div>
                                                    <uc1:RetailerHandoffHeadingControl ID="handoffHeading" runat="server"></uc1:RetailerHandoffHeadingControl>
                                                    <uc1:RetailerHandoffDetailControl ID="handoffDetail" runat="server"></uc1:RetailerHandoffDetailControl>
                                                    <asp:Panel ID="panelDiscountCardDisclaimer" runat="server">
                                                        <div id="tdisclaimer">
                                                            <asp:Label ID="labelDiscountDisclaimer" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label></div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="panelHandoff" runat="server">
                                                        <div id="Div2">
                                                            <table width="833px">
                                                                <tr>
                                                                    <td align="right" valign="top">
                                                                        <font size="2">
                                                                            <cc1:tdbutton id="nextButton" runat="server" enableclientscript="true"  causesvalidation="False" style="display:none; vertical-align:top;"></cc1:tdbutton>
                                                                            </font>
                                                                            <noscript style="vertical-align:top;"><font size="2"><asp:hyperlink id="hyperlinkNext" runat="server" target="_blank"><asp:label id="Label1" cssclass="TDButtonHyperlink" runat="server"></asp:label></asp:hyperlink></font></noscript>
                                                                        
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" rowspan="2">
                                                                        <p>
                                                                            <asp:Label ID="labelList1Heading" runat="server" Font-Bold="True" EnableViewState="False"></asp:Label></p>
                                                                        <ul class="listerdisc">
                                                                            <li>
                                                                                <asp:Label ID="labelList1Item1" runat="server" EnableViewState="False"></asp:Label>
                                                                            </li>
                                                                            <li>
                                                                                <asp:Label ID="labelList1Item2" runat="server" EnableViewState="False"></asp:Label>
                                                                            </li>
                                                                            <li>
                                                                                <asp:Label ID="labelList1Item3" runat="server" EnableViewState="False"></asp:Label>
                                                                            </li>
                                                                        </ul>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="Div3">
                                                            <p>
                                                                <asp:Label ID="labelList2Heading" runat="server" EnableViewState="False" Font-Bold="True"></asp:Label></p>
                                                            <ul class="listerdisc">
                                                                <li>
                                                                    <asp:Label ID="labelList2Item1" runat="server" EnableViewState="False"></asp:Label>
                                                                </li>
                                                                <li>
                                                                    <asp:Label ID="labelList2Item2" runat="server" EnableViewState="False"></asp:Label>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <br />
                                                        <div id="Div4">
                                                            <p>
                                                                <asp:Label ID="labelList3Heading" runat="server" EnableViewState="False" Font-Bold="True"></asp:Label></p>
                                                            <ul class="listerdisc">
                                                                <li>
                                                                    <asp:Label ID="labelList3Item1" runat="server" EnableViewState="False"></asp:Label>
                                                                </li>
                                                                <li>
                                                                    <asp:Label ID="labelList3Item2" runat="server" EnableViewState="False"></asp:Label>
                                                                </li>
                                                                <li>
                                                                    <asp:Label ID="labelList3Item3" runat="server" EnableViewState="False"></asp:Label>
                                                                </li>
                                                                <li>
                                                                    <asp:Label ID="labelList3Item4" runat="server" EnableViewState="False"></asp:Label>
                                                                </li>
                                                                <li>
                                                                    <asp:Label ID="labelList3Item5" runat="server" EnableViewState="False"></asp:Label>
                                                                </li>
                                                                <li>
                                                                    <asp:Label ID="labelList3Item6" runat="server" EnableViewState="False"></asp:Label>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="panelOfflineInformation" runat="server">
                                                        <div id="Div5">
                                                        </div>
                                                        <br />
                                                        <div id="Div6">
                                                            <asp:Label ID="labelOfflineInstructions" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label></div>
                                                        <uc1:RetailerInformationControl ID="offlineRetailerInformation" runat="server"></uc1:RetailerInformationControl>
                                                        <br />
                                                    </asp:Panel>
                                                    <% /* End: Content to be replaced when white labelling */ %>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
