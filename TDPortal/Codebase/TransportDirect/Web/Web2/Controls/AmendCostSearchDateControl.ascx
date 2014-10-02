<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendCostSearchDateControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendCostSearchDateControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="DateSelectControl" Src="DateSelectControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<p class="txtseven">
	<asp:label id="instructionLabel" runat="server" EnableViewState="false"></asp:label>
</p>
<table border="0" runat="server" id="tableAmendCostSearchDateControl">
	<tr>
		<td>
			<div class="txtseven"><asp:label id="labelTitleOutward" runat="server"></asp:label></div>
		</td>
		<td>
			<uc1:dateselectcontrol id="selectControlOutward" runat="server"></uc1:dateselectcontrol>
		</td>
		<td valign="bottom" rowspan="2">
			<cc1:tdbutton id="buttonOK" runat="server"></cc1:tdbutton>
		</td>
	</tr>
	<tr id="dateControlInward" runat="server">
		<td>
			<div class="txtseven"><asp:label id="labelTitleInward" runat="server"></asp:label></div>
		</td>
		<td>
			<uc1:dateselectcontrol id="selectControlInward" runat="server"></uc1:dateselectcontrol>
		</td>
	</tr>
</table>
