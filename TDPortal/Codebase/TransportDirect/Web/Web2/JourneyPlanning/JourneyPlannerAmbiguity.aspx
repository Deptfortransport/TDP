<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TriStateLocationControl2" Src="../Controls/TriStateLocationControl2.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="CalendarControl" Src="../Controls/CalendarControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCarPreferencesControl" Src="../Controls/FindCarPreferencesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PTPreferencesControl" Src="../Controls/PTPreferencesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccessibleOptionsControl" Src="../Controls/AccessibleOptionsControl.ascx" %>
<%@ Page language="c#" Codebehind="JourneyPlannerAmbiguity.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.JourneyPlannerAmbiguity" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindLeaveReturnDatesControl" Src="../Controls/FindLeaveReturnDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendStopoverControl" Src="../Controls/AmendStopoverControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FavouriteLoadOptionsControl" Src="../Controls/FavouriteLoadOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmbiguousDropdownList" Src="../Controls/AmbiguousDropdownList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="../Controls/FindPageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TransportTypesControl" Src="../Controls/TransportTypesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiStateLocationControl" Src="../Controls/BiStateLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocationControl" Src="../Controls/LocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapInputControl" Src="../Controls/MapInputControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="Homepage.css, expandablemenu.css, nifty.css, setup.css,jpstd.css,CalendarSS.css,map.css,JourneyPlannerAmbiguity.aspx.css"></cc1:headelementcontrol>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#PlanningAmbiguity" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
		<form id="JourneyPlannerAmbiguity" method="post" runat="server">
		    <asp:ScriptManager runat="server" ID="scriptManager1" EnablePartialRendering="true">
                <Services>
                    <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
                </Services>
            </asp:ScriptManager>
		    <uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
		    <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl id="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="630" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelBackTop" runat="server" CssClass="panelBackTop">
                                                        <cc1:tdbutton id="commandBack" runat="server"></cc1:tdbutton>
                                                    </asp:Panel>
                                                </td>
                                                <td align="right">
                                                    <cc1:HelpButtonControl runat="server" id="helpbutton" EnableViewState="False">
                                                    </cc1:HelpButtonControl>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <h1>
                                                            <asp:label id="labelJourneyPlannerTitle" runat="server"></asp:label>
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
                                        <div class="boxtypeeightalt">
			                                <asp:label id="labelErrorMessages" runat="server" cssclass="txtseven"></asp:label>
			                            </div>
			                            <!-- Journey Planning Controls -->
			                            <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <div class="boxtypetwo">
                                                        <!-- From and To locations -->
					                                    <table cellspacing="0" width="100%">
						                                    <tr>
							                                    <td>
								                                    <table cellspacing="0" width="100%">
									                                    <tr>
										                                    <td class="findafromcolumn" align="right">
										                                        <a name="PlanningAmbiguity"></a>
										                                        <asp:label id="labelOriginTitle" runat="server" cssclass="txtsevenb"></asp:label>
										                                    </td>
										                                    <td colspan="2">									                                        
                                                                                <uc1:LocationControl ID="originLocationControl" runat="server"></uc1:LocationControl>
                                                                            </td>
									                                    </tr>
								                                    </table>
							                                    </td>
						                                    </tr>
						                                    <tr>
							                                    <td>
								                                    <table cellspacing="0" width="100%">
									                                    <tr>
										                                    <td class="findafromcolumn" align="right">
										                                        <asp:label id="labelDestinationTitle" runat="server" cssclass="txtsevenb"></asp:label>
										                                    </td>
										                                    <td colspan="2">
                                                                                <uc1:LocationControl ID="destinationLocationControl" runat="server"></uc1:LocationControl>
										                                    </td>
									                                    </tr>
								                                    </table>
							                                    </td>
						                                    </tr>
					                                    </table>
					                                    
					                                    <!-- Dates -->
					                                    <uc1:findleavereturndatescontrol id="dateControl" runat="server"></uc1:findleavereturndatescontrol>
					                                    
					                                    <!-- Submit buttons -->
					                                    <uc1:FindPageOptionsControl ID="pageOptionsControltop" runat="server"></uc1:FindPageOptionsControl>
				                                    </div>
				                                    <div class="mcMapInputBox">
                                                        <!-- Map -->
                                                        <a id="Map"></a>
                                                        <uc1:MapInputControl id="mapInputControl" runat="server" visible="false"></uc1:MapInputControl>
                                                    </div>
				                                    <asp:panel id="panelStopover" runat="server">
					                                    <div class="boxtypetwo">
						                                    <div>
							                                    <asp:label id="stopOverErrorMessageLabel" runat="server" cssclass="txtseven"></asp:label>
							                                </div>
						                                    <uc1:amendstopovercontrol id="stopoverControl" runat="server"></uc1:amendstopovercontrol>
					                                    </div>
				                                    </asp:panel>
				                                    <div class="boxtypetwo">
					                                    <table cellspacing="0" width="100%">
						                                    <tr>
							                                    <td class="findafromcolumn" align="right">
							                                        <asp:label id="labelOptions" runat="server" cssclass="txtsevenb"></asp:label>
							                                    </td>
							                                    <td colspan="2" valign="bottom">
							                                        <asp:label id="labelShow" runat="server" cssclass="txtseven"></asp:label>
							                                    </td>
						                                    </tr>
					                                    </table>
				                                    </div>
				                                    <uc1:ptpreferencescontrol id="ptPreferencesControl" runat="server"></uc1:ptpreferencescontrol>
				                                    <uc1:AccessibleOptionsControl ID="accessibleOptionsControl" runat="server"></uc1:AccessibleOptionsControl>
				                                    <uc1:findcarpreferencescontrol id="carPreferencesControl" runat="server"></uc1:findcarpreferencescontrol>
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
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
			<uc1:footercontrol id="FooterControl1" runat="server"></uc1:footercontrol>
        </form>
    </div>
</body>
</html>
