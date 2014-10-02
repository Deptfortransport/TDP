<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EBCJourneyDetails.aspx.cs" Inherits="TransportDirect.UserPortal.Web.Templates.EBCJourneyDetails" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="EBCAllDetailsControl" Src="../Controls/EBCAllDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EBCMapControl" Src="../Controls/EBCMapDetailsControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="homepage.css,nifty.css,setup.css,jpstd.css,ExpandableMenu.css,Map.css,EBCJourneyDetails.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="EBCJourneyDetails" runat="server">
        <asp:ScriptManager runat="server" ID="scriptManager1">
            <Services>
                <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
            </Services>
        </asp:ScriptManager>
        <div>
            <a href="#JourneyDetails"><cc1:TDImage ID="imageJourneySkipLink" runat="server" CssClass="skiptolinks" EnableViewState="false"></cc1:TDImage></a>
            
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
                        <a name="SkipToMain"></a>
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Details Column -->
                                    <td valign="top">
                                        
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <uc1:JourneyChangeSearchControl ID="journeyChangeSearchControl" runat="server"></uc1:JourneyChangeSearchControl>
                                                    </div>
                                                    <div class="boxtypesixteen">
                                                        <% /* This is the journey details, showing from and too... */ %>
                                                        <uc1:JourneysSearchedForControl ID="journeysSearchedControl" runat="server"></uc1:JourneysSearchedForControl>
                                                        <asp:Panel ID="errorMessagePanel" runat="server" Visible="False" EnableViewState="false">
                                                            <div class="boxtypeerrormsgtwo">
                                                                <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <a id="JourneyDetails"></a>
                                                    <asp:Panel ID="outwardPanel" runat="server">
                                                        <div class="ebcJourneyDetailsPanel">
                                                            <uc1:EBCAllDetailsControl ID="ebcAllDetailsControlOutward" runat="server" Visible="false"></uc1:EBCAllDetailsControl>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="outwardMapPanel" runat="server">
                                                        <div class="ebcJourneyDetailsPanel">
                                                            <uc1:EBCMapControl ID="ebcMapControlOutward" runat="server" Visible="false"></uc1:EBCMapControl>
                                                        </div>
                                                    </asp:Panel>
                                                    
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
    
        </div>
        </form>
    </div>
</body>
</html>
