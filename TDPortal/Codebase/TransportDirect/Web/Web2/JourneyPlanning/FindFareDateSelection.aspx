<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareStepsControl" Src="../Controls/FindFareStepsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareSingleTravelDatesControl" Src="../Controls/FindFareSingleTravelDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareReturnTravelDatesControl" Src="../Controls/FindFareReturnTravelDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>

<%@ Page Language="c#" Codebehind="FindFareDateSelection.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.FindFareDateSelection" EnableViewState="True" %>

<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="nifty.css,expandablemenu.css,homepage.css,setup.css,jpstd.css,FindAFare.css,FindFareDateSelection.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="FindFareDateSelection" method="post" runat="server">
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <% /* Start: Region to copy to include LH menu when white labelling */ %>
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
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <!-- Journey Planning Controls -->
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <% /* End: Region to copy to include LH menu when white labelling */ %>
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <uc1:JourneyChangeSearchControl ID="theJourneyChangeSearchControl" HelpLabel="helpLabel"
                                                            runat="server"></uc1:JourneyChangeSearchControl>
                                                    </div>
                                                    <uc1:JourneysSearchedForControl ID="theJourneysSearchedForControl1" runat="server"></uc1:JourneysSearchedForControl>
                                                    <br />
                                                    <uc1:FindFareStepsControl ID="findFareStepsControl" runat="server"></uc1:FindFareStepsControl>
                                                    <cc1:HelpLabelControl ID="helpLabel" runat="server" CssMainTemplate="helpboxoutput"
                                                        Visible="False"></cc1:HelpLabelControl>
                                                    <div class="boxtypeeightstd">
                                                        <uc1:JourneyPlannerOutputTitleControl ID="JourneyPlannerOutputTitleControl1" runat="server">
                                                        </uc1:JourneyPlannerOutputTitleControl>
                                                    </div>
                                                    <asp:Panel ID="instructionPanel" runat="server">
                                                        <div class="boxtypeeightstd">
                                                            <asp:Label ID="instructionLabel" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label></div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="errorMessagePanel" runat="server" Visible="False">
                                                        <div class="boxtypeerrormsg">
                                                            <asp:Label ID="errorMessageLabel" runat="server" EnableViewState="false"></asp:Label></div>
                                                    </asp:Panel>
                                                    <br />
                                                    <uc1:FindFareSingleTravelDatesControl ID="findFareSingleTravelDatesControl" runat="server"
                                                        EnableViewState="False" Visible="False"></uc1:FindFareSingleTravelDatesControl>
                                                    <uc1:FindFareReturnTravelDatesControl ID="findFareReturnTravelDatesControl" runat="server"
                                                        EnableViewState="False" Visible="False"></uc1:FindFareReturnTravelDatesControl>
                                                    <div class="boxtypeeightstd">
                                                        <table lang="en" id="pagef" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td align="left">
                                                                    <br />
                                                                    <asp:Label ID="noteLabel" runat="server" EnableViewState="false"></asp:Label>&nbsp;</td>
                                                                <td align="right">
                                                                    <cc1:TDButton ID="buttonDateSelectionSubmit" runat="server"></cc1:TDButton></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <br />
                                                    
                                                        <uc1:AmendSaveSendControl ID="amendSaveSendControl" runat="server"></uc1:AmendSaveSendControl>
                                                        &nbsp;&nbsp;&nbsp;
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
            
        </form>
    </div>
</body>
</html>
