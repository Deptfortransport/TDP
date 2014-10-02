<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TriStateLocationControl2" Src="../Controls/TriStateLocationControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SimpleMapControl" Src="../Controls/SimpleMapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CalendarControl" Src="../Controls/CalendarControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapZoomControl" Src="../Controls/MapZoomControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapDisabledControl" Src="../Controls/MapDisabledControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>

<%@ Page Language="c#" Codebehind="TrafficMaps.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.TrafficMap" %>

<%@ Register TagPrefix="uc1" TagName="TrafficDateTimeDropDownControl" Src="../Controls/TrafficDateTimeDropDownControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapKeyControl" Src="../Controls/MapKeyControl.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="MapLocationSelectControl" Src="../Controls/MapLocationIconsSelectControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping"
    Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/Maps/TrafficMaps.aspx" />
    <meta name="description" content="View projected traffic levels on Britain's roads with Transport Direct. Just enter a postcode or location." />
    <cc2:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,CalendarSS.css,expandablemenu.css,nifty.css,TrafficMaps.aspx.css">
    </cc2:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="TrafficMap" method="post" runat="server">
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc2:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Input and Results table-->
                                        <table lang="en" cellspacing="0" width="100%" border="0" cellpadding="0">
                                            <a name="SkipToMain"></a>
                                        
                                            <!-- Input -->
                                            <tr id="SearchTrafficMapRow" runat="server">
                                                <td valign="top">
                                                    <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td valign="top">
                                                                <!-- Top of Page Controls -->
                                                                <table lang="en" cellspacing="0" width="630" border="0">
                                                                    <tr>
                                                                       <td>
                                                                            <asp:Panel ID="panelBackTop" runat="server" CssClass="panelBackTop">
                                                                                <cc2:TDButton ID="commandBack" runat="server" EnableViewState="false" Visible="false"></cc2:TDButton>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td align="right">
                                                                            <cc2:HelpCustomControl ID="HelpControlLocationSelectTraffic" runat="server" 
                                                                                HelpLabel="helpLabelLocationSelect"></cc2:HelpCustomControl>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div class="toptitlediv">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <cc2:TDImage ID="imageTrafficMaps" runat="server" Width="70" Height="36"></cc2:TDImage>
                                                                            </td>
                                                                            <td>
                                                                                <h1>
                                                                                    <asp:Label ID="labelTrafficMapTitle" runat="server"></asp:Label>
                                                                                </h1>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div>
                                                                    <cc2:HelpLabelControl ID="helpLabelLocationSelect" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl>
                                                                </div>
                                                                <asp:Panel ID="panelErrorDisplayControl" runat="server" Visible="False">
                                                                    <div class="boxtypeerrormsgfour">
                                                                        <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                                                    </div>
                                                                </asp:Panel>
                                                                <div class="boxtypeeightalt">
                                                                    <asp:Label ID="labelTrafficMapDescription" runat="server" CssClass="txtseven"></asp:Label>
                                                                </div>
                                                                <div class="boxtypeeightalt">
                                                                    <asp:Label ID="labelTrafficMapHelp" runat="server" CssClass="txtseven"></asp:Label>
                                                                </div>
                                                                <!-- Journey Planning Controls -->
                                                                <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <div class="boxtypetwo">
                                                                                <asp:Panel ID="panelLocationSelect" runat="server">
                                                                                    <uc1:TriStateLocationControl2 ID="TriStateLocationControl1" runat="server"></uc1:TriStateLocationControl2>
                                                                                    <div class="boxtypesevenalt">
                                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                                            <tr>
                                                                                                <td align="left">
                                                                                                    <cc2:TDButton ID="previousLocationButton" runat="server"></cc2:TDButton></td>
                                                                                                 <td align="right">
                                                                                                    <cc2:TDButton ID="resolveLocationButton" runat="server"></cc2:TDButton></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="2">
                                                                            <asp:Panel ID="TDPageInformationHtmlPlaceHolderDefinition" runat="server" CssClass="SoftContentPanel" ScrollBars="None"></asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <!-- White Space Column -->
                                                            <td class="WhiteSpaceBetweenColumns">
                                                            </td>
                                                            <!-- Information Column -->
                                                            <td class="HomepageMainLayoutColumn3" valign="top">
                                                                <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
                                                                <asp:Panel ID="TDInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                            <!-- Result -->
                                            <tr id="TrafficMapResults" runat="server" visible="false">
                                                <td>
                                                    <div class="boxtypeeightstd">
                                                        <table lang="en" id="headerbox" border="0">
                                                            <tr>
                                                                <td id="mlabel">
                                                                    <cc2:TDButton ID="SelectNewLocationButton" runat="server" />
                                                                </td>
                                                                <td align="right">
                                                                    <div class="boxprinterfriendly">
                                                                        <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyPageButton" runat="server">
                                                                        </uc1:PrinterFriendlyPageButtonControl>
                                                                    
                                                                        <cc2:HelpCustomControl ID="HelpControlMapToolsSecond" runat="server" 
                                                                            HelpLabel="helpLabelToolsSecond" ></cc2:HelpCustomControl>    
                                                                    </div>
                                                                    
                                                                   
                                                                </td>
                                                               
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="boxtypeeightstd">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td valign="top">
                                                                    <h1>
                                                                        <asp:Label ID="labelMap" runat="server" CssClass="txttenb"></asp:Label>&nbsp;</h1>
                                                                </td>
                                                                <td valign="top">
                                                                    <h1>
                                                                        <asp:Label ID="labelSelectedLocation" runat="server"></asp:Label>
                                                                    </h1>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <strong>
                                                            <asp:Label ID="labelMapDescription" runat="server" CssClass="txteightb"></asp:Label>
                                                        </strong>
                                                    </div>
                                                   <cc2:HelpLabelControl ID="helpLabelToolsSecond"
                                                            runat="server" Visible="False" CssMainTemplate="helpboxoutput">
                                                         </cc2:HelpLabelControl>
                                                   <cc2:HelpLabelControl
                                                                ID="helpLabelMapZoom" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl><cc2:HelpLabelControl
                                                                    ID="helpLabelLocationSelectAmbig" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl><cc2:HelpLabelControl
                                                                        ID="helpLabelTrafficMapKey" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl>
                                                    <div class="boxtypeeightstd">
                                                         
                                                        <asp:Label ID="labelTime" runat="server" CssClass="txtsevenb" Visible="False"></asp:Label></div>
                                                    <table id="mapcontrol" cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="toolboxContainer" style="position:relative;">
                                                                <asp:Panel ID="panelMapTools" CssClass="panelMapBoxTools" runat="server">
                                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr valign="top">
                                                                                <td colspan="2">
                                                                                    <asp:Label ID="labelTrafficDateTimeDropDown" runat="server" CssClass="txtsevenb"></asp:Label>
                                                                                    <uc1:TrafficDateTimeDropDownControl ID="trafficDateTimeDropDownControl" runat="server">
                                                                                    </uc1:TrafficDateTimeDropDownControl>
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                                                                                                    <tr>
                                                                            <td valign="top" align="left">
                                                                                <uc1:CalendarControl ID="CalendarControl1" runat="server" Visible="False"></uc1:CalendarControl>
                                                                            </td>
                                                                        </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="labelInstructions" runat="server" CssClass="txtsevenb"></asp:Label>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <cc2:TDButton ID="ShowOnMapButton" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                
                                                                
                                                                <div id="panelMapKeybox">
                                                                    <table cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td valign="top" align="left">
                                                                                <asp:Panel ID="panelMapKey" runat="server">
                                                                                    <div id="mapbox">
                                                                                        <div id="mhe">
                                                                                            <table width="100%">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="labelKey" runat="server"></asp:Label></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                        <uc1:MapKeyControl ID="MapKeyControl1" runat="server"></uc1:MapKeyControl>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <uc1:SimpleMapControl ID="SimpleMapControl1" runat="server"></uc1:SimpleMapControl>
                                                                <uc1:MapDisabledControl ID="MapDisabledControl1" runat="server"></uc1:MapDisabledControl>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </cc2:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="FooterControl1" runat="server" EnableViewState="False"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
