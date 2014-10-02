<%@ Register TagPrefix="uc1" TagName="TravelDetailsControl" Src="../Controls/TravelDetailsControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindPreferencesOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindPreferencesOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table lang="en" cellspacing="0" width="590" border="0">
	<tr>
		<td width="75%"><cc1:helplabelcontrol id="preferencesHelpLabel" visible="False" cssmaintemplate="helpboxoutput" runat="server"></cc1:helplabelcontrol></td>
		<td valign="top" align="right" width="25%"><cc1:tdbutton id="commandSubmit" runat="server"></cc1:tdbutton></td>
	</tr>
</table>
<div class="boxtypesixalt">
	<table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td class="jpthdl" align="left"><asp:label id="preferencesHeaderLabel" runat="server"></asp:label></td>
			<td align="left"><span class="jpt"><uc1:traveldetailscontrol id="loginSaveOption" runat="server"></uc1:traveldetailscontrol></span></td>
			<td align="right"></td>									
		</tr>					
	</table>
</div>

