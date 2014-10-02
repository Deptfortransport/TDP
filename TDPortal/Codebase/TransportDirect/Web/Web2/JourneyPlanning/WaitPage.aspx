<%@ Page language="c#" Codebehind="WaitPage.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.WaitPage" EnableViewState="false" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="../Controls/FindPageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="homepage.css,setup.css,jpstd.css, WaitPage.aspx.css"></cc1:headelementcontrol>
		<asp:literal id="refresh" runat="Server"></asp:literal>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	</div>
	<div class="CenteredContent">
		<form id="WaitPage" method="post" runat="server">
			<uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
			<div class="pageContainer">
			<div id="boxtypethirteen" align="center">
				<div class="waitPageHeaderLabel">
					<asp:label id="headerLabel" runat="server"></asp:label>
				</div>
				<cc1:tdimage id="AnimatedImage" runat="server" imagealign="Middle"></cc1:tdimage>
				<p>&nbsp;</p>
				<table summary="Tip of the day message">
					<tr>
						<td></td>
						<td align="center" width="410">
							<div id="txtnineb">
								<asp:Panel ID="MessageDefinition" runat="server"></asp:Panel>
								<br /><br />
								<div id="tipofday" runat="server"> 
								    <div class="tipofdaytitle">
								        <asp:Literal runat="server" ID="litTitleTipofDay"></asp:Literal>
								    </div>
								    <div class="tipofdaycontent">
								        <asp:Literal runat="server" ID="litTipofDay"></asp:Literal>
								    </div>
								</div>
							</div>
						</td>
						<td></td>
					</tr>
				</table>
				<p>&nbsp;</p>
				<p>&nbsp;</p>
				<div class="txteight">
					<asp:label id="InformationLabel" runat="server"></asp:label>
					<a href="WaitPage.aspx"><asp:label id="SelfLink" runat="server"></asp:label></a>
				</div>
				<br />
				<uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>			
			</div>
			<br />
			<p>&nbsp;</p>
			</div>
			<uc1:footercontrol id="FooterControl1" runat="server"></uc1:footercontrol>
		</form>
		</div>
		</body>
</html>
