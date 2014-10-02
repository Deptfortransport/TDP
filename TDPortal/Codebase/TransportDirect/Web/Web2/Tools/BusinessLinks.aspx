<%@ Page language="c#" Codebehind="BusinessLinks.aspx.cs" AutoEventWireup="True" validateRequest="false" Inherits="TransportDirect.UserPortal.Web.Templates.BusinessLinks" %>
<%@ Register TagPrefix="uc1" TagName="BusinessLinkTemplateSelectControl" Src="../Controls/BusinessLinkTemplateSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TriStateLocationControl2" Src="../Controls/TriStateLocationControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
        <link rel="canonical" href="http://www.transportdirect.info/Web2/Tools/BusinessLinks.aspx" />
        <meta name="description" content="Add a journey searchbox, car park finder or cycle route planner to your website. Free tools from Transport Direct." />
 		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css,BusinessLinks.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#BackButton" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="Form1" method="post" runat="server">
			<uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
			 <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
            <tr valign="top">
                <!-- Left Hand Navigaion Bar -->
                <td class="LeftHandNavigationBar" valign="top">
                    <uc1:expandablemenucontrol id="expandableMenuControl" runat="server" EnableViewState="False"
                        CategoryCssClass="HomePageMenu">
                    </uc1:expandablemenucontrol>
                    
                    <div class="HomepageBookmark">
                        <uc1:clientlinkcontrol id="clientLink" runat="server" EnableViewState="False" Visible="false">
                        </uc1:clientlinkcontrol></div>
                </td>
                <!-- Page Content -->
                <td valign="top">
                    <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                        Corners="TopLeft" CssClass="bodyArea">
                        <!-- Main content control table -->
                        <table id="maincontenttable" lang="en" cellspacing="0" width="100%" border="0">
                            <tr valign="top">
                                <!-- Journey Planning Column -->
                                <td valign="top">
                                    <!-- Top of Page Controls -->
                                    <table lang="en" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td align="left">
                                            <table>
                                                        <tr>
                                                            <td>
                                                                <a name="BackButton"></a>
                                                                <div style="width:2px; float:left;">&nbsp;</div><cc1:tdbutton id="buttonBack" runat="server"></cc1:tdbutton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <cc1:TDImage id="imageBusinessLinks" runat="server"></cc1:TDImage>
                                                            </td>
                                                            <td>                               
                                                                <h1><asp:label id="labelBusinessLinksHeader" runat="server"></asp:label></h1>
                                                            </td>
                                                        </tr>
                                            </table>
                                            </td>
                                            <% /* 
                                            <td align="right">
                                                <cc1:HelpButtonControl ID="Helpbuttoncontrol1" runat="server" EnableViewState="False">
                                                </cc1:HelpButtonControl>
                                            </td>
                                            */ %>
                                        </tr>
                                    </table>
                                    <!-- Journey Planning Controls -->
                                    <asp:panel ID="panelSubHeading" runat="server">
                                        <div class="boxtypeeightalt">
                                            <asp:Label ID="labelFromToTitle" runat="server" CssClass="txtseven"></asp:Label></div>
                                    </asp:panel>
                                    <asp:panel ID="panelErrorMessage" runat="server" Visible="False">
                                        <div id="boxtypeeightalt">
                                            <asp:Label ID="labelErrorMessages" runat="server" CssClass="txtseven"></asp:Label></div>
                                    </asp:panel>
                                    <table lang="en" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <% /* Start: Content to be replaced when white labelling */ %>
			                                        <!-- blue nav -->
			                                        
			                                        <div class="boxtypeeightstd">
				                                        <asp:label id="labelBusinessLinksSubheading" runat="server" cssclass="txtseven"></asp:label>
				                                    </div>				                                        
			                                        <asp:panel id="panelIntroduction" runat="server">
				                                        <div class="boxtypeeightstd">
					                                        <table width="100%" cellpadding="3" cellspacing="3">
						                                        <tr>
							                                        <td align="center">
								                                        <cc1:TDImage id="imageBusinessLinksExample" runat="server" enableviewstate="False"></cc1:tdimage>
								                                    </td>
						                                        </tr>
						                                        <tr><td><asp:label id="label1" runat="server"></asp:label></td></tr>
						                                        <tr>
							                                        <td class="txtseven">
								                                        <asp:label id="labelBrochure1" runat="server" enableviewstate="False"></asp:label>
								                                        <asp:hyperlink id="hyperlinkBrochure" runat="server" cssclass="txttenb" enableviewstate="False"
									                                        target="_blank"></asp:hyperlink>
								                                        <asp:label id="labelBrochure2" runat="server" enableviewstate="False"></asp:label>
								                                        <asp:hyperlink id="hyperlinkHelpSheets" runat="server" cssclass="txttenb" enableviewstate="False"
									                                        target="_blank"></asp:hyperlink>
									                                    <asp:label id="labelBrochure3" runat="server" enableviewstate="False"></asp:label>
								                                        <asp:hyperlink id="hyperlinkTechnicalGuide1" runat="server" cssclass="txttenb" enableviewstate="False"
								                                        target="_blank"></asp:hyperlink>
								                                        <asp:label id="labelBrochure4" runat="server" enableviewstate="False"></asp:label>
								                                     </td>
						                                        </tr>
						                                        <tr>
							                                        <td>
								                                        <asp:hyperlink id="hyperlinkGetAdobe" runat="server" cssclass="txtseven" enableviewstate="False"
									                                        target="_blank">
									                                        <cc1:TDImage runat="server" id="imageAdobe"></cc1:tdimage>
								                                        </asp:hyperlink>
							                                        </td>
						                                        </tr>
						                                        <tr>
							                                        <td>
								                                        <asp:label id="labelTermsConditions" runat="server" cssclass="txtsevenb"></asp:label></td>
						                                        </tr>
						                                        <tr>
							                                        <td>
								                                        <div id="areaTermsConditions" runat="server" class="businesslinktermsdiv"></div>
							                                        </td>
						                                        </tr>
						                                        <tr>
							                                        <td>
								                                        <asp:label id="labelAgreeingNote" runat="server" cssclass="txtseven"></asp:label></td>
						                                        </tr>
						                                        <tr>
							                                        <td>
								                                        <div class="boxtypeeightstd" align="right">
									                                        <cc1:tdbutton id="buttonNext" runat="server"></cc1:tdbutton></div>
							                                        </td>
						                                        </tr>
					                                        </table>
				                                        </div>
			                                        </asp:panel>
			                                        <asp:panel id="panelLocationSelection" runat="server">
			                                        <table cellpadding="3" cellspacing="3">
			                                                <tr>
			                                                    <td align="center">
			                                                    <cc1:TDImage id="imageBusinessLinksExample1" runat="server" enableviewstate="False"></cc1:tdimage>
			                                                    </td>
			                                                </tr>
					                                        <tr>
						                                        <td  valign="top">
							                                       <h1> <asp:label id="labelStep1" runat="server"></asp:label></h1>
							                                       
							                                    </td>
					                                        </tr>
					                                        <tr>
					                                            <td>
					                                                 <asp:label id="labelChooseLocation" runat="server" cssclass="txtseven"></asp:label>
					                                            </td>
					                                        </tr>
					                                        <tr>
						                                        <td id="boxtypetwoalt">
						                                            
							                                        <uc1:tristatelocationcontrol2 id="businessLinksTriStateLocationControl" runat="server"></uc1:tristatelocationcontrol2>
							                                        <table width="100%"><tr><td align="right"><cc1:tdbutton id="buttonNextLocation" runat="server"></cc1:tdbutton></td></tr></table>
							                                        </td>
					                                        </tr>
				                                        </table>
			                                        </asp:panel>
			                                        <asp:panel id="panelTemplateSelection" runat="server">
			                                        <div style="width:11px; float:left"></div>
				                                        <table width="100%" cellpadding="3" cellspacing="3">
				                                        	<tr>
					                                            <td align="center">
					                                            <cc1:TDImage id="imageBusinessLinksExample2" runat="server" enableviewstate="False"></cc1:tdimage>
					                                            </td>
					                                        </tr>
					                                        <tr>
						                                        <td valign="top">
						                                            
						                                                <h1><asp:label id="labelTemplateStep1" runat="server"></asp:label></h1>
 						                                               
							                                        <asp:label id="labelLocationChosenIs" runat="server" cssclass="txtseven"></asp:label>
							                                    </td>
					                                        </tr>
				                                        </table>
				                                        <div class="boxtypeeightblue" align="center">
					                                        <asp:label id="labelChosenLocation" runat="server" cssclass="txtsevenb"></asp:label>
				                                        </div>
				                                        <table cellpadding="3" cellspacing="3">
					                                        <tr>
						                                        <td>
							                                        <asp:label id="labelTemplateStep2" runat="server" cssclass="txtseven"></asp:label></td>
						                                        <td>
							                                        <asp:label id="labelChooseTemplate" runat="server" cssclass="txtseven"></asp:label></td>
					                                        </tr>
				                                        </table>
				                                        <div class="boxtypeeightstd" align="center">
					                                        <uc1:businesslinktemplateselectcontrol id="BusinessLinkTemplateSelectControl1" runat="server"></uc1:businesslinktemplateselectcontrol>
				                                        </div>
				                                        <p>&nbsp;</p>
				                                        <div class="boxtypeeightstd">
					                                        <table>
						                                        <tr>
							                                        <td class="txtseven">
								                                        <asp:label id="labelStep2Note1" runat="server"></asp:label>
								                                        <asp:hyperlink id="hyperlinkTechnicalGuide2" runat="server" cssclass="txttenb" enableviewstate="False"
									                                        target="_blank"></asp:hyperlink>
								                                        <asp:label id="labelStep2Note2" runat="server"></asp:label>
								                                        <asp:hyperlink id="hyperlinkTechnicalGuide4" runat="server" cssclass="txttenb" enableviewstate="False"
									                                        target="_blank"></asp:hyperlink>
								                                        <asp:label id="labelStep2Note3" runat="server"></asp:label></td>
						                                        </tr>
					                                        </table>
				                                        </div>
				                                        <div class="boxtypeeightstd">
					                                        <table width="100%">
						                                        <tr>
							                                        <td align="right">
								                                        <cc1:tdbutton id="buttonNextTemplate" runat="server"></cc1:tdbutton></td>
						                                        </tr>
					                                        </table>
				                                        </div>
			                                        </asp:panel>
			                                        <asp:panel id="panelTemplateDownload" runat="server">
			                                        <div style="width:11px; float:left"></div>
				                                        <table cellpadding="3" cellspacing="3">
				                                        	<tr>
					                                            <td align="center"><cc1:TDImage id="imageBusinessLinksExample3" runat="server" enableviewstate="False"></cc1:tdimage></td>
					                                        </tr>
					                                        <tr>
						                                        <td>
							                                        <table>
								                                        <tr>
									                                        <td class="txtseven">
										                                        <asp:label id="labelCodeSnippet" runat="server"></asp:label>
										                                        <asp:hyperlink id="hyperlinkTechnicalGuide3" runat="server" cssclass="txttenb" enableviewstate="False"
											                                        target="_blank"></asp:hyperlink></td>
								                                        </tr>
								                                        <tr>
									                                        <td>
										                                        <asp:textbox cssclass="templatecodebox" id="boxTemplateCode" runat="server" enableviewstate="False"
											                                        readonly="True" textmode="MultiLine" rows="20"></asp:textbox></td>
								                                        </tr>
							                                        </table>
						                                        </td>
					                                        </tr>
					                                        <tr>
						                                        <td>
							                                        <table width="100%">
								                                        <tr>
									                                        <td align="right">
										                                        <cc1:tdbutton id="buttonCodeFinish" runat="server"></cc1:tdbutton></td>
								                                        </tr>
							                                        </table>
						                                        </td>
					                                        </tr>
				                                        </table>
			                                        </asp:panel>
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
