<%@ Page Language="C#" Codebehind="FindCarParkResults.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.FindCarParkResults" %>

<%@ Register TagPrefix="uc1" TagName="FindCarParkResultsLocationControl" Src="../Controls/FindCarParkResultsLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCarParksResultsTableControl" Src="../Controls/FindCarParksResultsTableControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SocialBookMarkLinkControl" Src="../Controls/SocialBookMarkLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" xml:lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css,FindCarParkResults.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#ChangeJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <a href="#MainContent">
            <cc1:TDImage class="skiptolinks" ID="imageMainContentSkipLink1" runat="server"></cc1:TDImage></a>
        <form id="FindCarParkResults" method="post" runat="server">
            <a name="Header"></a>
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:expandablemenucontrol id="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu">
                        </uc1:expandablemenucontrol>
                                            
                        <div class="SocialBookmark ExpandableMenu HomePageMenu">
                            <uc1:SocialBookMarkLinkControl ID="socialBookMarkLinkControl" runat="server" EnableViewState="False"></uc1:SocialBookMarkLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                               
                                    <td valign="top">
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <a name="ChangeJourney"></a>
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <div class="boxjourneychangesearchback">
                                                            <cc1:TDButton ID="commandBack" runat="server"></cc1:TDButton>&nbsp;
                                                            <cc1:TDButton ID="commandNewSearch" runat="server"></cc1:TDButton>&nbsp;
                                                            <cc1:TDButton ID="commandAmendSearch" runat="server"></cc1:TDButton>
                                                        </div>
                                                        <div class="boxjourneychangesearchchange">
                                                           <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyPageButton" runat="server"></uc1:PrinterFriendlyPageButtonControl>&nbsp;
                                                           <cc1:helpcustomcontrol id="helpIconSelect" runat="server" helplabel="findCarParkResultsHelpLabel" ></cc1:helpcustomcontrol>
                                                        </div>
                                                    </div>
                                                    <a name="MainContent"></a>
                                                    <uc1:FindCarParkResultsLocationControl ID="resultsLocationControl" runat="server"></uc1:FindCarParkResultsLocationControl>
                                                    
                                                    <asp:Panel ID="panelErrorDisplay" runat="server">
                                                        <div class="boxtypeerrormsg">
                                                            <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                                        </div>
                                                    </asp:Panel>
                                                    
                                                    <div class="boxtypeeightstd">
                                                        <asp:Label ID="labelNote" runat="server" CssClass="txtseven"></asp:Label>
                                                    </div>
                                                    
                                                    <uc1:FindCarParksResultsTableControl ID="resultsTableControl" runat="server"></uc1:FindCarParksResultsTableControl>
                                                    
                                                    
                                                    <div class="boxtypeeightstd">
                                                        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" summary="Command buttons">
                                                            <tr>
                                                                <td align="left">
                                                                    <cc1:TDButton ID="commandShowMap" runat="server"></cc1:TDButton></td>
                                                                <td align="right">
                                                                    <cc1:TDButton ID="commandDriveFrom2" runat="server"></cc1:TDButton>&nbsp;<cc1:TDButton
                                                                        ID="commandDriveTo2" runat="server"></cc1:TDButton></td>
                                                            </tr>
                                                        </table>
                                                    </div>
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
            <uc1:FooterControl ID="FooterControl" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
