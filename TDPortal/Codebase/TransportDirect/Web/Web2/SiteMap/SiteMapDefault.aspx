<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Page language="c#" Codebehind="SiteMapDefault.aspx.cs" validateRequest="false" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.SiteMapDefault" EnableViewState="True" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML 
lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,homepage.css,SiteMapDefault.aspx.css"></cc1:headelementcontrol>
		<asp:literal id="baseliteral" runat="server"></asp:literal>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="SiteMapDefault" method="post" runat="server">
			<uc1:headercontrol id="HeaderControl1" runat="server"></uc1:headercontrol>
			<div id="boxtypeeightsm">
				<div>
					<h1><asp:label id="lblSiteMapTitle" runat="server"></asp:label></h1>
				</div>
				<div id="boxtypeeightsmap">
                    <a name="SkipToMain"></a>
					<table width="100%" >
						<tr>
							<td width="20%"><div id="smh">
							<asp:Panel ID="QuickPlannersTitle" runat="server"></asp:Panel>
										</div>
							</td>
							<td width="20%"><div id="smh">
							<asp:Panel ID="JourneyPlannerTitle" runat="server"></asp:Panel>
										</div>
							</td>
							<td width="20%"><div id="smh">
							<asp:Panel ID="MapsTitle" runat="server"></asp:Panel>			
							</div>
							</td>
							<td width="20%"><div id="smh">
							<asp:Panel ID="LiveTravelTitle" runat="server"></asp:Panel>
							</div>
							</td>
							<td width="20%"><div id="smh">
							<asp:Panel ID="TDOnTheMoveTitle" runat="server"></asp:Panel>		
							</div>
							</td>
						</tr>
						<tr>
							<td valign="top"><div id="smc">
							<asp:Panel ID="QuickPlannersBody" runat="server"></asp:Panel>		
							</div>
							</td>
							<td valign="top"><div id="smc">
							<asp:Panel ID="JourneyPlannerBody" runat="server"></asp:Panel>			
							</div>
							</td>
							<td valign="top"><div id="smc">
							<asp:Panel ID="MapsBody" runat="server"></asp:Panel>
							</div>
							</td>
							<td valign="top"><div id="smc">
							<asp:Panel ID="LiveTravelBody" runat="server"></asp:Panel>
							</div>
							</td>
							<td valign="top"><div id="smc">
							<asp:Panel ID="TDOnTheMoveBody" runat="server"></asp:Panel>
							</div>
							</td>
						</tr>
					</table>
				</div>
			</div>
			<uc1:footercontrol id="FooterControl1" runat="server"></uc1:footercontrol>
		</form>
		</div>
	</body>
</HTML>
