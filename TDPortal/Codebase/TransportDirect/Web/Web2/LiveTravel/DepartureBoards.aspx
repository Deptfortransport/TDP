<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="../Controls/HyperlinkPostbackControl.ascx" %>

<%@ Page Language="c#" Codebehind="DepartureBoards.aspx.cs" ValidateRequest="false"
    AutoEventWireup="false" Inherits="TransportDirect.UserPortal.Web.Templates.DepartureBoards"
    EnableViewState="True" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/LiveTravel/DepartureBoards.aspx" />
    <meta name="description" content="Arrivals, departures and real time travel news from rail stations, airports and other transport providers.  Brought to you by Transport Direct." />
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css,DepartureBoards.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="DepartureBoards" method="post" runat="server">
            <a name="pagetop"></a>
            <!-- header -->
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl ID="clientLink" runat="server" EnableViewState="False"></uc1:ClientLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
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
                                                <td align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <cc1:TDImage id="imageDepartureBoards" runat="server" width="70" height="36">
                                                                                            </cc1:TDImage>
                                                            </td>
                                                            <td>
                                                                <h1>
                                                                    <asp:Label ID="labelDepartureBoardsTitle" runat="server"></asp:Label></h1>
                                                            </td>
                                                        </tr>
                                                    </table></td>
                                                    <% /* 
                                            <td align="right">
                                                <cc1:HelpButtonControl ID="Helpbuttoncontrol1" runat="server" EnableViewState="False">
                                                </cc1:HelpButtonControl>
                                            </td>
                                            */%>
                                            </tr>
                                        </table>
                                        <!-- Journey Planning Controls -->
                                        <a name="SkipToMain"></a>
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
                                                    <% // Start: Content to be replaced when white labelling %>
                                                    <div id="boxtypeeightstd">
                                                        <!--page title -->
                                                        <table lang="en" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td valign="top">
                                                                    <!-- main content -->
                                                                    <div id="primcontentlocationinfo">
                                                                        <div id="contentareawl">
                                                                            <asp:Panel ID="panelTrain" runat="server">
                                                                                <a name="train"></a>
                                                                                <div>
                                                                                    <!-- Train links -->
                                                                                    <table class="rsviewa" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="hdtypethree">
                                                                                                <h2>
                                                                                                    <asp:Label ID="labelTrainTitle" runat="server" EnableViewState="False"></asp:Label></h2>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <table class="rsviewa" summary="Train departure boards" cellpadding="0" cellspacing="0">
                                                                                        <asp:Repeater runat="server" ID="repeaterTrainLinks" EnableViewState="False">
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td colspan="3">
                                                                                                        &nbsp;</td>
                                                                                                </tr>
                                                                                                <tr class="rsviewcontentrow">
                                                                                                    <td class="rscolonea">
                                                                                                        <%# GetLinkTitle(Container.DataItem) %>&nbsp;
                                                                                                    </td>
                                                                                                    <td class="rscoltwoa">
                                                                                                        <%# GetLinkUrl(Container.DataItem) %>&nbsp;
                                                                                                    </td>
                                                                                                    <td class="rscolthreea">
                                                                                                        <%# GetLinkDescription(Container.DataItem) %>&nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <div class="TopOfPageRight">
                                                                                                    <asp:HyperLink ID="hyperlinkTrainTop" runat="server" EnableViewState="False">
                                                                                                        <asp:Label ID="labelHyperlinkTrainTop" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label>
                                                                                                        <asp:Image runat="server" ID="imageHyperlinkTrainTop" EnableViewState="False">
                                                                                                        </asp:Image>
                                                                                                    </asp:HyperLink>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                                <br/>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="panelLondon" runat="server">
                                                                                <a name="london"></a>
                                                                                <div>
                                                                                    <!-- London links -->
                                                                                    <table class="rsviewa" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="hdtypethree">
                                                                                                <h2>
                                                                                                    <asp:Label ID="labelLondonTitle" runat="server" EnableViewState="False"></asp:Label></h2>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <table class="rsviewa" summary="London-based departure boards" cellpadding="0" cellspacing="0">
                                                                                        <asp:Repeater runat="server" ID="repeaterLondonLinks" EnableViewState="False">
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td colspan="3">
                                                                                                        &nbsp;</td>
                                                                                                </tr>
                                                                                                <tr class="rsviewcontentrow">
                                                                                                    <td class="rscolonea">
                                                                                                        <%# GetLinkTitle(Container.DataItem) %>&nbsp;
                                                                                                    </td>
                                                                                                    <td class="rscoltwoa">
                                                                                                        <%# GetLinkUrl(Container.DataItem) %>&nbsp;
                                                                                                    </td>
                                                                                                    <td class="rscolthreea">
                                                                                                        <%# GetLinkDescription(Container.DataItem) %>&nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <div class="TopOfPageRight">
                                                                                                    <asp:HyperLink ID="hyperlinkLondonTop" runat="server" EnableViewState="False">
                                                                                                        <asp:Label ID="labelHyperlinkLondonTop" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label>
                                                                                                        <asp:Image runat="server" ID="imageHyperlinkLondonTop" EnableViewState="False">
                                                                                                        </asp:Image>
                                                                                                    </asp:HyperLink>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                                <br/>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="panelAirport" runat="server">
                                                                                <a name="airport"></a>
                                                                                <div>
                                                                                    <!-- Airport links -->
                                                                                    <table class="rsviewa" summary="Airport departure boards" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="hdtypethree">
                                                                                                <h2>
                                                                                                    <asp:Label ID="labelAirportTitle" runat="server" EnableViewState="False"></asp:Label></h2>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                &nbsp;</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="labelAirportSummary" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                &nbsp;</td>
                                                                                        </tr>
                                                                                        <tr class="rsviewcontentrow">
                                                                                            <td align="center">
                                                                                                <asp:DataList ID="datalistAirportLinks" EnableViewState="False" RepeatColumns="2"
                                                                                                    runat="server" ItemStyle-HorizontalAlign="Left" CellPadding="3" Width="525px">
                                                                                                    <ItemTemplate>
                                                                                                        <div class="txtseven">
                                                                                                            <%# GetAirportLinkText(Container.DataItem) %>&nbsp;
                                                                                                        </div>
                                                                                                    </ItemTemplate>
                                                                                                </asp:DataList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div class="TopOfPageRight">
                                                                                                    <asp:HyperLink ID="hyperlinkAirportTop" runat="server" EnableViewState="False">
                                                                                                        <asp:Label ID="labelHyperlinkAirportTop" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label>
                                                                                                        <asp:Image runat="server" ID="imageHyperlinkAirportTop" EnableViewState="False">
                                                                                                        </asp:Image>
                                                                                                    </asp:HyperLink>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                                <br/>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="panelBus" runat="server">
                                                                                <a name="bus"></a>
                                                                                <div>
                                                                                    <!-- Bus links -->
                                                                                    <table class="rsviewa" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="hdtypethree">
                                                                                                <h2>
                                                                                                    <asp:Label ID="labelBusTitle" runat="server" EnableViewState="False"></asp:Label></h2>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <table class="rsviewa" summary="Bus departure boards" cellpadding="0" cellspacing="0">
                                                                                        <asp:Repeater runat="server" ID="repeaterBusLinks" EnableViewState="False">
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td colspan="3">
                                                                                                        &nbsp;</td>
                                                                                                </tr>
                                                                                                <tr class="rsviewcontentrow">
                                                                                                    <td class="rscolonea">
                                                                                                        <%# GetLinkTitle(Container.DataItem) %>&nbsp;
                                                                                                    </td>
                                                                                                    <td class="rscoltwoa">
                                                                                                        <%# GetLinkUrl(Container.DataItem) %>&nbsp;
                                                                                                    </td>
                                                                                                    <td class="rscolthreea">
                                                                                                        <%# GetLinkDescription(Container.DataItem) %>&nbsp;
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <div class="TopOfPageRight">
                                                                                                    <asp:HyperLink ID="hyperlinkBusTop" runat="server" EnableViewState="False">
                                                                                                        <asp:Label ID="labelHyperlinkBusTop" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label>
                                                                                                        <asp:Image runat="server" ID="imageHyperlinkBusTop" EnableViewState="False">
                                                                                                        </asp:Image>
                                                                                                    </asp:HyperLink>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                                <br/>
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <% // End: Content to be replaced when white labelling %>
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
            <!-- Footer -->
            <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
