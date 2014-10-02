<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ShowCostTypeControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ShowCostTypeControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table>
	<tr>
		<td>
			<asp:Label id="labelShow" associatedcontrolid="dropFuelCosts" runat="server" class="txtseven">labelShow</asp:Label>
		</td>
		<td>
			<asp:DropDownList ID="dropFuelCosts" EnableViewState="True" Runat="server"></asp:DropDownList>
		</td>
	</tr>
</table>
