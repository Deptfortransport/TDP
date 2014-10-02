<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyAccessibilityLinksControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyAccessibilityLinksControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false"%>
<%@ Import namespace="System.Data" %>
<div class="boxtypeeightstd">
	<asp:label runat="server" cssclass="txtseven" id="labelInfoText" EnableViewState="False"></asp:label>
</div>
<br/>
<div class="boxtypeeightstd">
	<asp:datalist id="transportModeList" repeatdirection="Horizontal" repeatcolumns="7" runat="server" EnableViewState="False">
		<itemstyle cssclass="journeyAccessibilityLinksControl"></itemstyle>
		<itemtemplate>
			<table>
				<tr>
					<td class="txtseven">
						<div align="center">
							<a href="<%# GetLinkUrl((DataRow) Container.DataItem) %>" target="_blank" title="<%# GetLinkTitle((DataRow) Container.DataItem) %>" >
							<img src="<%# GetLinkImage((DataRow) Container.DataItem) %>" alt="<%# GetLinkTitle((DataRow) Container.DataItem) %>" />
							<br/><%# GetLinkText((DataRow) Container.DataItem) %>
							</a>
						</div>
					</td>
				</tr>
			</table>
		</itemtemplate>
	</asp:datalist>
</div>