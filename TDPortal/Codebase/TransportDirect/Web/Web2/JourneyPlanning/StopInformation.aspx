<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StopInformation.aspx.cs" Inherits="TransportDirect.UserPortal.Web.JourneyPlanning.StopInformation" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css, homepage.css, nifty.css, expandablemenu.css, map.css, StopInformation.aspx.css">
    </cc1:HeadElementControl>
</head>
<body dir="ltr">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#BackButton" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
     <div class="CenteredContent">
        <a href="#Summary">
            <img id="summaryPlannersSkipLink" runat="server" class="skiptolinks"></a>
        <form id="StopInformation" method="post" runat="server">
            <uc1:HeaderControl ID="headerControl" runat="server">
            </uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:expandablemenucontrol ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu">
                        </uc1:expandablemenucontrol>
                        <uc1:expandablemenucontrol ID="relatedLinksControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu">
                        </uc1:expandablemenucontrol>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            
                            <table lang="en" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td valign="top" colspan="2">
                                        <a name="BackButton"></a>
                                        <div id="boxtypeeightstd1">
                                            <div class="boxtypeeightstd">
                                                <cc1:TDButton ID="buttonBack" runat="server" EnableViewState="false">
                                                </cc1:TDButton>
                                            </div>
                                        </div>
                                        <div id="boxtypeeightstd">
                                            <h1>
                                                <asp:Label ID="labelStationInformationTitle" runat="server"></asp:Label><asp:Label
                                                    ID="labelLocationInformationTitle" runat="server"></asp:Label></h1>
                                        </div>
                                        <div id="primcontentlocationinfo">
                                            <div id="contentareaw1">
                                               
                                                <div id="hdtypethree">
                                                    <h2>
                                                        <asp:Label ID="labelStationName" runat="server"></asp:Label></h2>
                                                </div>
                                                 <asp:Label ID="labelStationCode" runat="server" CssClass="txtseven"></asp:Label>
                                                 <asp:Label ID="labelErrorMessage" runat="server" Visible="False" CssClass="txtsevenb"></asp:Label>
                                                             
                                            </div>
                                            <a name="Summary"></a>
                                            <table class="stopInformationContainerTable">
                                                 <tr>
                                                    <td valign="top">
                                                        <asp:Panel ID="StopInformationControlContainer1" CssClass="stopInformationContainer" runat="server" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Panel ID="StopInformationControlContainer2" CssClass="stopInformationContainer" runat="server" />
                                                    </td>
                                                </tr>
                                             </table>
                                        </div>
                                        
                                     </td>
                                     
                                     
                                </tr>
                               
                                
                            </table>
                           
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="FooterControl1" runat="server">
            </uc1:FooterControl>
        </form>
    </div>
</body>
</html>
