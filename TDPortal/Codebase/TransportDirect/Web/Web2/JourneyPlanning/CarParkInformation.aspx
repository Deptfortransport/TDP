<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarParkInformationControl" Src="../Controls/CarParkInformationControl.ascx" %>

<%@ Page Language="c#" Codebehind="CarParkInformation.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.CarParkInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,nifty.css,expandablemenu.css,CarParkInformation.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="CarParkInformation" method="post" runat="server">
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
                            <table lang="en" cellspacing="0" cellpadding="0">
                                <tr>
                                    <%--<td valign="top">
                                        <!-- left hand menu -->
                                        <div id="butl">
                                            <a href="#Summary">
                                                <cc1:TDImage ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"
                                                    EnableViewState="false"></cc1:TDImage></a>
                                        </div>
                                        <div>
                                            <asp:HyperLink ID="carParkHyperlink" runat="server" EnableViewState="false"></asp:HyperLink></div>
                                        <div>
                                            <asp:HyperLink ID="parkAndRideHyperlink" runat="server" EnableViewState="false"></asp:HyperLink></div>
                                    </td>--%>
                                    <td valign="top">
                                        <!-- main content -->
                                        
                                        
                                        <div id="primcontentlocationinfo">
                                             <div class="boxtypeeightstd">
                                                    <cc1:TDButton ID="buttonBack" runat="server" EnableViewState="false"></cc1:TDButton></div>
                                            <div id="boxtypeeightstd">
                                               
                                                <h1>
                                                    <asp:Label ID="labelCarParkInformationTitle" runat="server" EnableViewState="false"></asp:Label>&nbsp;<asp:Label
                                                        ID="labelLocationInformationTitle" runat="server" EnableViewState="false"></asp:Label></h1>
                                            </div>
                                            <div>
                                                
                                                <div id="contentareawl">
                                                    <asp:Label ID="labelErrorMessage" runat="server" Visible="False" CssClass="txtsevenb"
                                                        EnableViewState="false"></asp:Label>
                                                    <asp:Panel ID="panelCarParkDetails" runat="server">
                                                        <div id="hdtypethree">
                                                            <h2>
                                                                <asp:Label ID="labelCarParkName" runat="server" EnableViewState="false"></asp:Label></h2>
                                                        </div>
                                                        <uc1:CarParkInformationControl ID="carParkInformationControl" runat="server"></uc1:CarParkInformationControl>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            
                                        </div>
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
