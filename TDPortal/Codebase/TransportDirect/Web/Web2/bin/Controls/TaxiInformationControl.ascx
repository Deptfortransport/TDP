<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TaxiInformationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TaxiInformationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>

<asp:Label id="comments" runat="server" cssclass="txtseven" enableviewstate="False">[Comments]</asp:Label>

<asp:Repeater id="operators" runat="server" enableviewstate="False">
	<headertemplate>
		<table lang="en" summary="Operators" style="MARGIN-TOP: 10px">
	</headertemplate>
	<itemtemplate>
		<tr>
			<td width="80" height="20">
				<span class="txtseven">
					<asp:Label text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' runat="server" id="OperatorName" enableviewstate="False"/>
				</span>
			</td>
			<td>
				<span class="txtseven">
					<asp:Label text='<%# DataBinder.Eval(Container.DataItem, "PhoneNumber")%>' runat="server" id="OperatorNumber" enableviewstate="False"/>
				</span>
			</td>
			<td>
				<span class="txtseven">
					<asp:Image id="isAccessible" runat="server" enableviewstate="False"></asp:Image>
				</span>
			</td>
		</tr>
	</itemtemplate>
	<footertemplate>
		</table>
	</footertemplate>
</asp:Repeater>
<asp:Repeater id="alternativeStops" runat="server" visible="False" enableviewstate="False">
	<headertemplate>
		<table lang="en" summary="Alternative Stops">
			<tr>
				<td colspan="3" height="40"><span class="txtseven"><b>Alternative Stops:</b></span></td>
			</tr>
	</headertemplate>
	<itemtemplate>
		<tr>
			<td>
				<asp:PlaceHolder runat="server" id="NewAlternativeStop" enableviewstate="False"></asp:PlaceHolder>
			</td>
		</tr>
	</itemtemplate>
	<footertemplate>
		</table>
	</footertemplate>
</asp:Repeater>

<table lang="en" summary="Accessibility Information" style="MARGIN-TOP: 10px">
	<tr>
		<td width="35" valign="top">
			<asp:Image id="accessibleIcon" runat="server" visible="False" enableviewstate="False"></asp:Image>
		</td>
		<td>
			<asp:Label id="accessibleText" runat="server" cssclass="txtseven" visible="False" enableviewstate="False">[Accessible Text]</asp:Label>
		</td>
	</tr>
</table>