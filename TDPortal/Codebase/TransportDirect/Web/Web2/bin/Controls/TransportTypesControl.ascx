<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TransportTypesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TransportTypesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="true" %>
	<table width="100%">
		<tr>
			<td width="43%">
				<asp:label id="labelSRJourneyType" runat="server" cssclass="screenreader"></asp:label>
				<asp:label id="labelType" runat="server"></asp:label>
			</td>
		</tr>
		<tr>
			<td>
				<asp:panel id="transportTypesPanel" runat="server">
					<asp:label id="labelTickAllTypes"  runat="server"></asp:label>
					<asp:checkboxlist id="checklistModesPublicTransport"  runat="server" repeatcolumns="3"></asp:checkboxlist>
					<br />
					<asp:label id="labelPublicModesNote"  runat="server"></asp:label>
					<br />
				</asp:panel>
			</td>
		</tr>
	</table>
