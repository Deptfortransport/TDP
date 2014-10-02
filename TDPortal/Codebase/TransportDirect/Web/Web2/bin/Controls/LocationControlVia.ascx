<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationControlVia.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.LocationControlVia" %>
<%@ Register TagPrefix="uc1" TagName="LocationControl" Src="LocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="FindPageOptionsControl.ascx" %>

<div class="boxtypetwo">
    <asp:label id="labelJourneyOptions" runat="server" CssClass="txtsevenb"></asp:label>
	<table cellspacing="0" width="100%">
		<tbody>
			<tr>
				<td><asp:label id="travelViaLabel" runat="server" cssclass="txtseven"></asp:label></td>
				<td></td>
			</tr>
			<tr>
				<td colspan="2">
				    <asp:label id="instructionLabel" runat="server" cssclass="txtseven"></asp:label>
				</td>
			</tr>
			<tr>
			    <td colspan="2">
			        <uc1:LocationControl ID="locationControl" runat="server" />
			    </td>
			</tr>
		</tbody>
	</table>
	<uc1:findpageoptionscontrol id="pageOptionsControl" runat="server"></uc1:findpageoptionscontrol>
</div>