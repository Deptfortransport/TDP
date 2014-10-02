<%@ Register TagPrefix="uc1" TagName="ShowCostTypeControl" Src="ShowCostTypeControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CostsPageTitleControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CostsPageTitleControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div><cc1:helplabelcontrol id="costingHelpLabel" runat="server" cssmaintemplate="helpboxoutput" visible="False"></cc1:helplabelcontrol>
</div>
<div class="boxtypeninenew">
	<table class="cct">
		<tr>
			<td class="cc1">
				<h3><asp:label id="labelCostPageTitle" enableviewstate="false" runat="server"></asp:label></h3>
				&nbsp;&nbsp;<asp:label id="labelLocations" enableviewstate="false" runat="server" cssclass="LocationsLabelSmall"></asp:label>
			</td>
			<td class="cc2"><uc1:showcosttypecontrol id="costTypeControl" runat="server"></uc1:showcosttypecontrol></td>
			<td class="cc3"><cc1:tdbutton id="buttonOK" runat="server"></cc1:tdbutton></td>
			<td class="cc4">
			</td>
		</tr>
	</table>
</div>
