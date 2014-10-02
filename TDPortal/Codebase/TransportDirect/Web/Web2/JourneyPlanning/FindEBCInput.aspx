<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindEBCInput.aspx.cs" Inherits="TransportDirect.UserPortal.Web.Templates.FindEBCInput" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="FindToFromLocationsControl" Src="../Controls/FindToFromLocationsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindLocationControl" Src="../Controls/FindLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="../Controls/FindPageOptionsControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <cc1:HeadElementControl id="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,nifty.css,ExpandableMenu.css,FindEBCInput.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#FindEBC" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="FindEBCInput" runat="server">
            <a href="#InputForm">
                <cc1:TDImage ID="imageInputFormSkipLink" runat="server" CssClass="skiptolinks" EnableViewState="false"></cc1:TDImage>
            </a>
            <uc1:HeaderControl id="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl id="expandableMenuControl" runat="server" enableviewstate="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl id="clientLink" runat="server" enableviewstate="False"></uc1:ClientLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl id="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" cssclass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="630" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel id="panelBackTop" cssclass="panelBackTop" runat="server" enableviewstate="false">
                                                        <cc1:TDButton id="commandBack" runat="server" enableviewstate="false"></cc1:TDButton>
                                                    </asp:Panel>
                                                </td>                                      
                                                <td align="right">
                                                    <cc1:HelpButtonControl id="Helpbuttoncontrol1" runat="server" enableviewstate="False">
                                                    </cc1:HelpButtonControl>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a name="FindEBC"></a>
                                                        <cc1:TDImage id="imageFindEBC1" runat="server" enableviewstate="false"></cc1:TDImage>
                                                        <cc1:TDImage id="imageFindEBC2" runat="server" enableviewstate="false"></cc1:TDImage>
                                                        <cc1:TDImage id="imageFindEBC3" runat="server" enableviewstate="false"></cc1:TDImage>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <h1>
                                                            <asp:Label id="labelFindPageTitle" runat="server" enableviewstate="false"></asp:Label>
                                                        </h1>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel id="panelErrorDisplayControl" runat="server" visible="False" enableviewstate="false">
                                            <div class="boxtypeerrormsgfour">
                                                <uc1:ErrorDisplayControl id="errorDisplayControl" runat="server" visible="False"></uc1:ErrorDisplayControl>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel id="panelSubHeading" runat="server" enableviewstate="false">
                                            <div class="boxtypeeightalt">
                                                <asp:Label id="labelFromToTitle" runat="server" cssclass="txtseven" enableviewstate="false"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel id="panelErrorMessage" runat="server" visible="False" enableviewstate="false">
                                            <div class="boxtypeeightalt">
                                                <asp:Label id="labelErrorMessages" runat="server" cssclass="txtseven" enableviewstate="false"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <!-- Journey Planning Controls -->
                                        <a id="InputForm"></a>
                                        <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <div class="boxtypetwo">
                                                        <asp:Panel ID="panelInformation" runat="server" CssClass="boxtypeinformation" EnableViewState="false">
                                                            <asp:Label ID="labelInformation" runat="server" CssClass="txtseven" EnableViewState="false"></asp:Label>
                                                        </asp:Panel>
                                                        <uc1:FindToFromLocationsControl id="locationsControl" runat="server"></uc1:FindToFromLocationsControl>
                                                        <uc1:FindLocationControl id="viaFindLocationControl" runat="server"></uc1:FindLocationControl>
                                                        <uc1:FindPageOptionsControl id="pageOptionsControltop" runat="server"></uc1:FindPageOptionsControl>    
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   <asp:Panel id="TDPageInformationHtmlPlaceHolderDefinition" runat="server" cssclass="SoftContentPanel" ScrollBars="None" enableviewstate="false"></asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <!-- White Space Column -->
                                    <td class="WhiteSpaceBetweenColumns">
                                    </td>
                                    <!-- Information Column -->
                                    <td class="HomepageMainLayoutColumn3" valign="top">
                                        <uc1:PoweredBy id="PoweredByControl" runat="server" enableviewstate="False"></uc1:PoweredBy>
                                        <asp:Panel id="TDInformationHtmlPlaceholderDefinition" runat="server" enableviewstate="false"></asp:Panel>
                                        <asp:Panel ID="TDFindEBCPromoHtmlPlaceholderDefinition" runat="server" enableviewstate="false"></asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>            
            <uc1:FooterControl ID="FooterControl1" runat="server" EnableViewState="False"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
