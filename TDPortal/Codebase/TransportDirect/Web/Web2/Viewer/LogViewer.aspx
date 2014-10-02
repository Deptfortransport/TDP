<%@ Page language="c#" Codebehind="LogViewer.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.LogViewer" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,homepage.css,expandablemenu.css,nifty.css,LogViewer.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="LogViewer" method="post" runat="server"> <!-- Transport Direct Header info -->
			<uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
			
			<table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                            <uc1:ExpandableMenuControl ID="relatedLinksControl" runat="server" EnableViewState="False"
                                                CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl ID="clientLink" runat="server" EnableViewState="False" Visible="False"></uc1:ClientLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <a name="SkipToMain"></a>
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Journey Planning Controls -->
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
                                                    <% /* Start: Content to be replaced when white labelling */ %>
                                                    <br />
			                                        <div id="boxtypeeight">
				                                        <h1><asp:label id="labelLogViewer" runat="server"></asp:label></h1>
				                                        <asp:label id="labelInstruction" runat="server" cssclass="txtseven"></asp:label><p>&nbsp;</p>
				                                        <div id="logviewerbox">
					                                        <table cellpadding="3" width="100%">
						                                        <tr>
							                                        <td colspan="3"><asp:label id="labelSessionInfo" runat="server" cssclass="txtseven"></asp:label></td>
							                                        <td colspan="3"><asp:label id="labelReferenceInfo" runat="server" cssclass="txtseven"></asp:label></td>
						                                        </tr>
						                                        <tr>
							                                        <td align="right"><asp:label id="labelSessionID" runat="server" cssclass="txtseven"></asp:label></td>
							                                        <td><asp:textbox id="textSessionID" runat="server" cssclass="txtseven" width="150px"></asp:textbox></td>
							                                        <td align="right"><asp:label id="labelStartTime" runat="server" cssclass="txtseven"></asp:label></td>
							                                        <td>&nbsp;</td>
							                                        <td align="right"><asp:label id="labelEndTime" runat="server" cssclass="txtseven"></asp:label></td>
							                                        <td>&nbsp;</td>
						                                        </tr>
						                                        <tr>
							                                        <td align="right"><asp:label id="labelMachineName" runat="server" cssclass="txtseven"></asp:label></td>
							                                        <td><asp:textbox id="textMachineName" runat="server" cssclass="txtseven" width="150px"></asp:textbox></td>
							                                        <td align="right"><asp:label id="labelCategory" runat="server" cssclass="txtseven"></asp:label></td>
							                                        <td><asp:dropdownlist id="dropCategory" runat="server"></asp:dropdownlist></td>
							                                        <td align="right"><asp:label id="labelLevel" runat="server" cssclass="txtseven"></asp:label></td>
							                                        <td><asp:dropdownlist id="dropLevel" runat="server"></asp:dropdownlist></td>
						                                        </tr>
						                                        <tr>
							                                        <td align="right"><asp:label id="labelMessage" runat="server" cssclass="txtseven"></asp:label></td>
							                                        <td colspan="5"><asp:textbox id="textMessage" runat="server" cssclass="txtseven" width="600px"></asp:textbox></td>
						                                        </tr>
						                                        <tr>
							                                        <td align="right"><asp:label runat="server" id="labelMethod" cssclass="txtseven"></asp:label></td>
							                                        <td><asp:textbox runat="server" id="textMethod" cssclass="txtseven" width="150px"></asp:textbox></td>
							                                        <td align="right"><asp:label runat="server" id="labelType" cssclass="txtseven"></asp:label></td>
							                                        <td><asp:textbox runat="server" id="textType" cssclass="txtseven" width="150px"></asp:textbox></td>
							                                        <td align="right"><asp:label runat="server" id="labelAssembly" cssclass="txtseven"></asp:label></td>
							                                        <td><asp:textbox runat="server" id="textAssembly" cssclass="txtseven" width="150px"></asp:textbox></td>
						                                        </tr>
						                                        <tr>
							                                        <td colspan="6" align="right">
								                                        <cc1:tdbutton id="submitButton" runat="server" causesvalidation="False"></cc1:tdbutton>
							                                        </td>
						                                        </tr>
					                                        </table>
				                                        </div>
				                                        <p>&nbsp;</p>
				                                        <asp:label id="labelEventsReturned" runat="server" cssclass="txtseven"></asp:label>
				                                        <asp:label id="labelNumOfEvents" runat="server" cssclass="txtseven"></asp:label>
				                                        <p>&nbsp;</p>
				                                        <asp:textbox runat="server" id="textEventsRetrieved" style="width: 815px"></asp:textbox>
			                                        </div>
                                                    <% /* End: Content to be replaced when white labelling */ %>
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
			<uc1:footercontrol id="FooterControl1" runat="server"></uc1:footercontrol>
		</form>
		</div>
	</body>
</html>
