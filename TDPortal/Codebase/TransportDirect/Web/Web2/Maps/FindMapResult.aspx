<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindMapResult.aspx.cs" Inherits="TransportDirect.UserPortal.Web.Templates.FindMapResult" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="MapFindControl" Src="../Controls/MapFindControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <cc1:HeadElementControl id="headElementControl" runat="server" Stylesheets="homepage.css,nifty.css,setup.css,jpstd.css,ExpandableMenu.css,FindMapResult.aspx.css,Map.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#ChangeJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="FindMapResult" runat="server">
        <asp:ScriptManager runat="server" ID="scriptManager1">
            <Services>
                <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
            </Services>
        </asp:ScriptManager>
        <div>
            
            <a href="#Map"><cc1:TDImage ID="imageMapSkipLink" runat="server" CssClass="skiptolinks" EnableViewState="false"></cc1:TDImage></a>
            
            <uc1:HeaderControl id="headerControl" runat="server"></uc1:HeaderControl>
            
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Details Column -->
                                    <td valign="top">
                                        
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <a name="ChangeJourney"></a>
                                                    <!-- Navigation controls -->
                                                    <div class="boxjourneychangesearchcontrol">
                                                        <div class ="boxjourneychangesearchchangetwo">
                                                            <cc1:TDButton id="commandBack" runat="server" enableviewstate="false" Visible="false"></cc1:TDButton>
                                                            <cc1:tdbutton id="buttonNewSearch" runat="server"></cc1:tdbutton>
                                                        </div>
                                                        <div class="boxjourneychangesearchchange">
                                                            <uc1:printerfriendlypagebuttoncontrol id="printerFriendlyPageButton" runat="server"></uc1:printerfriendlypagebuttoncontrol>
                                                            <cc1:helpbuttoncontrol id="pageHelpButton" runat="server" Visible="true" EnableViewState="false" ></cc1:helpbuttoncontrol>
                                                        </div>
                                                    </div>
                                                    <asp:Panel id="panelErrorDisplayControl" runat="server" visible="False" enableviewstate="false">
                                                        <div class="boxtypeerrormsgfour">
                                                            <uc1:ErrorDisplayControl id="errorDisplayControl" runat="server" visible="False"></uc1:ErrorDisplayControl>
                                                        </div>
                                                    </asp:Panel>
                                                    <!-- Title -->
                                                    <div class="boxtypeeightstd">
                                                        <h1>
                                                            <asp:Label ID="labelMap" runat="server"></asp:Label>
                                                            <asp:Label ID="labelSelectedLocation" runat="server"></asp:Label>
                                                        </h1>
                                                    </div>
                                                    <!-- Map -->
                                                    <a id="Map"></a>
                                                    <uc1:MapFindControl id="mapFindControl" runat="server"></uc1:MapFindControl>
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
            
            <uc1:FooterControl id="FooterControl1" runat="server"></uc1:FooterControl>
        
        </div>
        </form>
    </div>
</body>
</html>
