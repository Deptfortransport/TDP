	<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapControl" Src="MapControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindViaLocationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindViaLocationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="TriStateLocationControl2" Src="TriStateLocationControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="FindPageOptionsControl.ascx" %>
<div class="boxtypetwo"><asp:label id="labelJourneyOptions" runat="server" CssClass="txtsevenb"></asp:label>
	<table cellspacing="0" width="100%">
		<tbody>
			<tr>
				<td><asp:label id="travelViaLabel" runat="server" cssclass="txtseven"></asp:label></td>
				<td align="right"></td>
			</tr>
			<tr>
				<td colspan="2"><asp:label id="instructionLabel" runat="server" cssclass="txtseven"></asp:label></td>
			</tr>
			<tr>
				<td colspan="2"><uc1:tristatelocationcontrol2 id="tristateLocationControl" runat="server"></uc1:tristatelocationcontrol2></td>
			</tr>
		</tbody>
	</table>
	<uc1:findpageoptionscontrol id="pageOptionsControl" runat="server"></uc1:findpageoptionscontrol>
</div>
