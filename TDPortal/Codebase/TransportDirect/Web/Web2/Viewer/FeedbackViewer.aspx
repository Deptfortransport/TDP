<%@ Page language="c#" Codebehind="FeedbackViewer.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.FeedbackViewer" validateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html 
lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="jpstd.css,setup.css,homepage.css,expandablemenu.css,nifty.css,FeedbackViewer.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="FeedbackViewer" method="post" runat="server">
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
			                                        <div>
				                                        <table cellpadding="2" cellspacing="2" width="100%">
					                                        <tr>
						                                        <td><h1><asp:label id="labelFeedbackViewerTitle" runat="server" enableviewstate="false"></asp:label></h1>
						                                        </td>
					                                        </tr>
					                                        <tr>
						                                        <td><asp:label id="labelInstruction" runat="server" cssclass="txtseven" enableViewState="false"></asp:label></td>
					                                        </tr>
				                                        </table>
				                                        <div class="spacer1">&nbsp;</div>
				                                        <div id="boxtypetwo">
					                                        <table cellpadding="1" cellspacing="0" width="100%">
						                                        <tr>
							                                        <td align="right"><asp:label id="labelFeedbackIdInput" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
							                                        <td align="left">&nbsp;<asp:textbox id="textFeedbackIdInput" runat="server" cssclass="txtseven" width="150px"></asp:textbox>
								                                        &nbsp;&nbsp;<cc1:tdbutton id="buttonSubmit" runat="server" enableviewstate="false"></cc1:tdbutton></td>
							                                        <td align="left"></td>
						                                        </tr>
					                                        </table>
				                                        </div>
				                                        &nbsp;<asp:label id="labelSessionWarning" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>
				                                        <div class="spacer1">&nbsp;</div>
				                                        <asp:Panel ID="panelSaved" Runat="server">
					                                        <div id="Div1" style="WIDTH: 100%;">
						                                        <table cellspacing="0" cellpadding="0" width="100%">
							                                        <tr>
								                                        <td align="center">
									                                        <asp:label id="labelSaved" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label></td>
							                                        </tr>
						                                        </table>
					                                        </div>
				                                        </asp:Panel>
				                                        <asp:Panel ID="panelFeedbackDetails" Runat="server">
					                                        <div class="spacer1">&nbsp;</div>
					                                        <div style="WIDTH: 100%; HEIGHT: 35px">
						                                        <table cellspacing="0" cellpadding="3" width="100%">
							                                        <tr>
								                                        <td align="center">
									                                        <cc1:tdbutton id="buttonJourneyRequest" runat="server"></cc1:tdbutton>&nbsp;&nbsp;
									                                        <cc1:tdbutton id="buttonJourneyResult" runat="server"></cc1:tdbutton>
									                                        &nbsp;&nbsp;
									                                        <cc1:tdbutton id="buttonItineraryManager" runat="server"></cc1:tdbutton></td>
							                                        </tr>
						                                        </table>
					                                        </div>
					                                        <div id="Div2" style="WIDTH: 100%;">
						                                        <table cellspacing="3" cellpadding="0" width="100%">
							                                        <tr>
								                                        <td align="right">
									                                        <asp:label id="labelFeedbackId" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textFeedbackId" runat="server" cssclass="txtseven" readonly="true" borderstyle="None" width="50px"></asp:textbox></td>
								                                        <td align="right">
									                                        <asp:label id="labelFeedbackStatus" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:DropDownList id="listFeedbackStatus" Runat="server" cssclass="txtseven" enableviewstate="true"></asp:DropDownList></td>
								                                        <td align="right">
									                                        <asp:label id="labelVantiveId" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textVantiveId" runat="server" cssclass="txtseven" readonly="false"></asp:textbox></td>
								                                        <td align="right">
									                                        <asp:label id="labelDelete" runat="server" cssclass="txtseven"></asp:label></td>
								                                        <td>
									                                        <asp:DropDownList id="listDelete" runat="server" cssclass="txtseven" enableviewstate="true"></asp:DropDownList></td>
								                                        <td align="right">
									                                        <cc1:tdbutton id="buttonUpdate" runat="server" enableviewstate="false"></cc1:tdbutton></td>
							                                        </tr>
						                                        </table>
					                                        </div>
					                                        <div id="Div3" style="WIDTH: 100%;">
						                                        <table cellspacing="5" cellpadding="0" width="100%" border="0">
							                                        <tr>
								                                        <td align="right">
									                                        <asp:label id="labelSessionId" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textSessionId" runat="server" cssclass="txtseven" readonly="true" borderstyle="None"
										                                        width="155" enableviewstate="false"></asp:textbox></td>
								                                        <td align="right">
									                                        <asp:label id="labelSessionCreated" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textSessionCreated" runat="server" cssclass="txtseven" readonly="true" borderstyle="None"
										                                        width="110" enableviewstate="false"></asp:textbox></td>
								                                        <td align="right">
									                                        <asp:label id="labelSessionExpires" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textSessionExpires" runat="server" cssclass="txtseven" readonly="true" borderstyle="None"
										                                        width="110" enableviewstate="false"></asp:textbox></td>
							                                        </tr>
							                                        <tr>
								                                        <td align="right">
									                                        <asp:label id="labelSubmittedTime" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textSubmittedTime" runat="server" cssclass="txtseven" readonly="true" borderstyle="None"
										                                        width="110" enableviewstate="false"></asp:textbox></td>
								                                        <td align="right">
									                                        <asp:label id="labelTimeLogged" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textTimeLogged" runat="server" cssclass="txtseven" readonly="true" borderstyle="None"
										                                        width="110" enableviewstate="false"></asp:textbox></td>
								                                        <td></td>
								                                        <td></td>
							                                        </tr>
							                                        <tr>
								                                        <td align="right">
									                                        <asp:label id="labelAcknowledgementSent" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textAcknowledgementSent" runat="server" cssclass="txtseven" readonly="true"
										                                        borderstyle="None" width="50" enableviewstate="false"></asp:textbox></td>
								                                        <td align="right">
									                                        <asp:label id="labelAcknowledgedTime" runat="server" cssclass="txtseven" enableviewstate="true"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textAcknowledgedTime" runat="server" cssclass="txtseven" readonly="true" borderstyle="None"
										                                        width="110" enableviewstate="true"></asp:textbox></td>
								                                        <td align="right">
									                                        <asp:label id="labelEmailAddress" runat="server" cssclass="txtseven" enableviewstate="true"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textEmailAddress" runat="server" cssclass="txtseven" readonly="true" borderstyle="None"
										                                        width="150" enableviewstate="true"></asp:textbox></td>
							                                        </tr>
							                                        <tr>
								                                        <td align="right">
									                                        <asp:label id="labelUserLoggedOn" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textUserLoggedOn" runat="server" cssclass="txtseven" readonly="true" borderstyle="None"
										                                        width="50" enableviewstate="false"></asp:textbox></td>
							                                        </tr>
						                                        </table>
					                                        </div>
					                                        <div id="Div4" style="WIDTH: 100%;">
						                                        <table cellspacing="3" cellpadding="0" width="100%">
							                                        <tr>
								                                        <td valign="top" align="right">
									                                        <asp:label id="labelUserFeedbackOptions" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textUserFeedbackOptions" runat="server" cssclass="txtseven" readonly="true"
										                                        borderstyle="None" textmode="MultiLine" rows="8" columns="54" enableviewstate="false"></asp:textbox></td>
								                                        <td valign="top" align="right">
									                                        <asp:label id="labelUserFeedbackDetails" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        <td>
									                                        <asp:textbox id="textUserFeedbackDetails" runat="server" cssclass="txtseven" readonly="true"
										                                        borderstyle="None" textmode="MultiLine" rows="8" columns="54" enableviewstate="false"></asp:textbox></td>
							                                        </tr>
						                                        </table>
					                                        </div>
					                                        <asp:Panel id="panelJourneyRequest" Runat="server" visible="false">
						                                        <div id="Div5" style="WIDTH: 100%;">
							                                        <table cellspacing="3" cellpadding="0" width="100%">
								                                        <tr>
									                                        <td>
										                                        <asp:label id="labelJourneyRequest" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        </tr>
								                                        <tr>
									                                        <td>
										                                        <asp:textbox id="textJourneyRequest" runat="server" cssclass="txteight" readonly="true" textmode="MultiLine"
											                                        columns="112" Rows="15" enableviewstate="false"></asp:textbox></td>
								                                        </tr>
							                                        </table>
						                                        </div>
					                                        </asp:Panel>
					                                        <asp:Panel id="panelJourneyResult" Runat="server" visible="false">
						                                        <div id="Div6" style="WIDTH: 100%;">
							                                        <table cellspacing="3" cellpadding="0" width="100%">
								                                        <tr>
									                                        <td>
										                                        <asp:label id="labelJourneyResult" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        </tr>
								                                        <tr>
									                                        <td>
										                                        <asp:textbox id="textJourneyResult" runat="server" cssclass="txteight" readonly="true" textmode="MultiLine"
											                                        columns="112" Rows="15" enableviewstate="false"></asp:textbox></td>
								                                        </tr>
							                                        </table>
						                                        </div>
					                                        </asp:Panel>
					                                        <asp:Panel id="panelItineraryManager" Runat="server" visible="false">
						                                        <div id="Div7" style="WIDTH: 100%;">
							                                        <table cellspacing="3" cellpadding="0" width="100%">
								                                        <tr>
									                                        <td>
										                                        <asp:label id="labelItineraryManager" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></td>
								                                        </tr>
								                                        <tr>
									                                        <td>
										                                        <asp:textbox id="textItineraryManager" runat="server" cssclass="txteight" readonly="true" textmode="MultiLine"
											                                        columns="112" Rows="15" enableviewstate="false"></asp:textbox></td>
								                                        </tr>
							                                        </table>
						                                        </div>
					                                        </asp:Panel>
				                                        </asp:Panel>
				                                        <asp:Panel ID="panelFeedbackError" Runat="server">
					                                        <div id="Div8" style="WIDTH: 750px;">
						                                        <table cellspacing="0" cellpadding="0" width="100%">
							                                        <tr>
								                                        <td align="center">
									                                        <asp:label id="labelFeedbackError" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label></td>
							                                        </tr>
						                                        </table>
					                                        </div>
				                                        </asp:Panel>
			                                        </div>
			                                        <br/>
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
