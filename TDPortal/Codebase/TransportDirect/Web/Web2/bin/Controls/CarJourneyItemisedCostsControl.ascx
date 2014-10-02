<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="SummaryC02EmissionsControl" Src="SummaryC02EmissionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarParkCostsControl" Src="CarParkCostsControl.ascx" %>
<%@ Register TagPrefix	="uc1" TagName="CostsPageTitleControl" Src="CostsPageTitleControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CarJourneyItemisedCostsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CarJourneyItemisedCostsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="OtherCostsControl" Src="OtherCostsControl.ascx" %>
<uc1:costspagetitlecontrol id="CostPageTitle" runat="server"></uc1:costspagetitlecontrol>
<div class="boxtypetwelve">
	<div class="dmtitle">
		<table class="txtseven carCostsTable cct" cellpadding="0" cellspacing="0">
			<tr>
				<td class="cc5" valign="top"><asp:label id="labelFuelCost" runat="server" enableviewstate="False"></asp:label></td>
				<td valign="top" align="right"><cc1:tdimage id="imageFuelCost" runat="server"></cc1:tdimage></td>
				<td class="ccPounds" valign="top"><asp:label id="labelFuelCharge" runat="server" enableviewstate="False"></asp:label></td>
				<td class="ccPence"><!-- Blank column no pence for fuel charge--></td>
				<td class="cc8" valign="top"><asp:label id="labelFuelInstruction" runat="server" enableviewstate="False"></asp:label></td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="cc5"><asp:label id="labelRunningCost" runat="server" visible="False" enableviewstate="False"></asp:label></td>
				<td align="right"><cc1:tdimage id="imageRunningCost" runat="server" ></cc1:tdimage></td>
				<td class="ccPounds"><asp:label id="labelRunningCharge" runat="server" visible="False" enableviewstate="False"></asp:label></td>
				<td class="ccPence"><!-- Blank column no pence for running costs --></td>
				<td class="cc8"><asp:label id="labelRunningInstruction" runat="server" visible="False" enableviewstate="False"></asp:label></td>
			</tr>
			<uc1:othercostscontrol id="OtherCosts" runat="server"></uc1:othercostscontrol>
			<uc1:carparkcostscontrol id="CarParkCosts" runat="server"></uc1:carparkcostscontrol>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td class="cc5"><asp:label id="labelTotalCost" runat="server" enableviewstate="False"></asp:label></td>
				<td>&nbsp;</td>
				<td class="ccPounds"><asp:label id="labelTotalValuePounds" runat="server" enableviewstate="False"></asp:label></td>
				<td class="ccPence"><asp:label id="labelTotalValuePence" runat="server" enableviewstate="False"></asp:label></td>
				<td class="cc8"><asp:label id="labelParkingCost" runat="server" enableviewstate="False"></asp:label></td>
			</tr>
			<tr>
				<td colspan="5"><asp:label id="labelReturnInstruction" runat="server" enableviewstate="False"></asp:label></td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<uc1:SummaryC02EmissionsControl id="summaryCO2EmissionsControl" runat="server"></uc1:SummaryC02EmissionsControl>
		</table>
	</div>
</div>
