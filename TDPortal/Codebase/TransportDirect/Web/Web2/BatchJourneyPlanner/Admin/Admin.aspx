<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="TransportDirect.UserPortal.Web.BatchJourneyPlanner.Admin.Admin" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../../Controls/PoweredByControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/BatchJourneyPlanner/Admin/Admin.aspx" />
    <meta name="description" content="Administer users of the batch journey planner." />
    <meta name="keywords" content="batch user administration" />
    <link rel="image_src" href="http://www.transportdirect.info/Web2/App_Themes/TransportDirect/images/gifs/softcontent/en/finda_bus.gif" />
    <cc1:HeadElementControl id="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,CalendarSS.css,HomePage.css,ExpandableMenu.css,Nifty.css,map.css,BatchJourneyPlanner.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#BatchUserAdmin" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="BatchUserAdmin" method="post" runat="server">
            <uc1:HeaderControl id="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl id="expandableMenuControl" runat="server" EnableViewState="False"
                            Categorycssclass="HomePageMenu"></uc1:ExpandableMenuControl>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl id="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" cssclass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" cellpadding="0" width="800px" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a name="BatchUserAdmin"></a>
                                                        <cc1:TDImage id="imageBatchJourneyPlanner" runat="server" Width="70" Height="36" alt=" "></cc1:TDImage>
                                                    </td>
                                                    <td>
                                                        <h1>
                                                            <asp:Label id="labelPageTitle" runat="server"></asp:Label>
                                                        </h1>
                                                    </td>
                                                </tr>
                                            </table>    
                                        </div>
                                        <asp:Panel id="panelErrorDisplayControl" runat="server" visible="False">
                                            <div class="boxtypeerrormsgfour">
                                                <uc1:ErrorDisplayControl id="errorDisplayControl" runat="server" visible="False"></uc1:ErrorDisplayControl>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel id="panelErrorMessage" runat="server" visible="False">
                                            <div class="boxtypeeightalt">
                                                <p>&nbsp;</p>
                                                <asp:Label id="labelErrorMessages" runat="server" cssclass="labelError"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <!-- user admin panel -->
                                        <asp:Panel id="panelUserAdmin" runat="server" visible="true">
                                            <div>
                                                <table width="800px">
                                                    <tr>
                                                        <td>
                                                            <p>&nbsp;</p>
                                                            <asp:Label id="labelUsers" runat="server" cssclass="labelNormal" />
                                                        </td>
                                                        <td align="right" valign="bottom">
                                                            <!-- table nav controls -->
                                                            <asp:LinkButton id="linkFirst" cssclass="labelNormal" runat="server">&lt;&lt;</asp:LinkButton>
                                                            <asp:LinkButton id="linkPrevious" cssclass="labelNormal" runat="server">Previous</asp:LinkButton>
                                                            <asp:Label id="labelNavControls" cssclass="labelNormal" runat="server" />
                                                            <asp:LinkButton id="linkNext" cssclass="labelNormal" runat="server">Next</asp:LinkButton>
                                                            <asp:LinkButton id="linkLast" cssclass="labelNormal" runat="server">&gt;&gt;</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table width="800px" class="tableBatches">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label id="labelTableUsers" cssclass="labelSubHeading" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:repeater id="usersRepeater" runat="server" OnItemDataBound="usersRepeater_ItemDatabound">
	                                                                        <headertemplate>
		                                                                        <table cellspacing="0" class="tableBatches" summary="Users table" lang="en" width="800px">
		                                                                            <col width="40%" />
		                                                                            <col width="20%" />
		                                                                            <col width="20%" />
		                                                                            <col width="10%" />
		                                                                            <col width="10%" />
			                                                                        <thead>
				                                                                        <tr bgcolor="#ffffff" valign="top">
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header1"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header2"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header3"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header4"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:Label id="header5"  runat="server" class="linkHeader" /> 
					                                                                        </td>
				                                                                        </tr>
			                                                                        </thead>
			                                                                        <tbody>
	                                                                        </headertemplate>
	                                                                        <itemtemplate>
			                                                                        <tr>
				                                                                        <td id="cell1" runat="server"></td>
				                                                                        <td id="cell2" runat="server"></td>
				                                                                        <td id="cell3" runat="server"></td>
				                                                                        <td id="cell4" runat="server"></td>
				                                                                        <td id="cell5" runat="server">
				                                                                            <asp:RadioButton id="radioSelect" groupname="radioUser" runat="server" />
				                                                                        </td>
			                                                                        </tr>
	                                                                        </itemtemplate>
	                                                                        <footertemplate>
		                                                                        </tbody></table>
	                                                                        </footertemplate>
                                                                        </asp:repeater>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <cc1:tdbutton id="buttonReloadTable" runat="server" cssclass="TDButtonDefault" onmouseover="this.className='TDButtonDefaultMouseOver'" />
                                                        </td>
                                                        <td align="right">
                                                            <cc1:tdbutton id="buttonSuspend" runat="server" cssclass="TDButtonDefault" onmouseover="this.className='TDButtonDefaultMouseOver'" />
                                                            <cc1:tdbutton id="buttonActivate" runat="server" cssclass="TDButtonDefault" onmouseover="this.className='TDButtonDefaultMouseOver'" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl id="FooterControl1" runat="server" EnableViewState="False"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
