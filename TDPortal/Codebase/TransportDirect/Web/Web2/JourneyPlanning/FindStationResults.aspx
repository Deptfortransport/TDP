<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>

<%@ Page Language="c#" Codebehind="FindStationResults.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.FindStationResults" %>

<%@ Register TagPrefix="uc1" TagName="FindStationResultsLocationControl" Src="../Controls/FindStationResultsLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindStationResultsTable" Src="../Controls/FindStationResultsTable.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css,FindStationResults.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#ChangeJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
    <form id="FindStationResults" method="post" runat="server">
        <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
        <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
            <tr valign="top">
                <!-- Left Hand Navigaion Bar -->
                <td class="LeftHandNavigationBar" valign="top">
                    <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                        CategoryCssClass="HomePageMenu">
                    </uc1:ExpandableMenuControl>
                </td>
                <!-- Page Content -->
                <td valign="top">
                    <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                        Corners="TopLeft" CssClass="bodyArea">
                        <!-- Main content control table -->
                        <table lang="en" cellspacing="0" width="100%" border="0">
                            <tr valign="top">
                          
                                <td valign="top">
                                    <!-- Top of Page Controls -->
                                   
                                    <table lang="en" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <a name="ChangeJourney"></a>
                                                <div id="boxjourneychangesearchcontrol">
                                                    <div class="boxjourneychangesearchback">
                                                        <cc1:TDButton ID="commandBack" runat="server"></cc1:TDButton>
                                                        <cc1:TDButton ID="commandNewSearch" runat="server" Style="vertical-align: top"></cc1:TDButton>
                                                        <cc1:TDButton ID="commandAmendSearch" runat="server" Style="vertical-align: top"></cc1:TDButton>
                                                    </div>
                                                    <div class="boxjourneychangesearchchange">
                                                        
                                                        <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyPageButton" runat="server">
                                                        </uc1:PrinterFriendlyPageButtonControl>
                                                        <cc1:helpcustomcontrol id="helpIconSelect" runat="server" helplabel="FindStationResultsHelpLabel" ></cc1:helpcustomcontrol>
                                                    </div>
                                                </div>
                                                <uc1:FindStationResultsLocationControl ID="stationResultsLocationControl" runat="server">
                                                </uc1:FindStationResultsLocationControl>
                                                <div class="boxtypeerrormsg" id="errormsg" runat="server">
                                                    <asp:Label ID="labelMessage" runat="server" Visible="False"></asp:Label></div>
                                                <div class="boxtypeeightstd">
                                                    <table cellspacing="0" cellpadding="0" width="100%" summary="Command buttons">
                                                        <tr>
                                                            <td>
                                                                
                                                                <asp:Label ID="labelResultsTableTitle" runat="server" CssClass="txtnineb"></asp:Label>
                                                            </td>
                                                           
                                                        </tr>
                                                    </table>
                                                    <asp:Label ID="labelNote" runat="server" CssClass="txtseven"></asp:Label></div>
                                                <uc1:FindStationResultsTable ID="FindStationResultsTable1" runat="server"></uc1:FindStationResultsTable>
                                                <div>
                                                </div>
                                                <div class="boxtypeeightstd">
                                                    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" summary="Command buttons">
                                                        <tr>
                                                            <td align="left">
                                                                <cc1:TDButton ID="commandShowMap" runat="server"></cc1:TDButton></td>
                                                            <td align="right">
                                                                <cc1:TDButton ID="commandNext2" runat="server"></cc1:TDButton>
                                                                <cc1:TDButton ID="commandTravelFrom2" runat="server"></cc1:TDButton>
                                                                <cc1:TDButton ID="commandTravelTo2" runat="server"></cc1:TDButton>
                                                            </td>
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
        <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
        
        
    </form>
    </div>
</body>
</html>
