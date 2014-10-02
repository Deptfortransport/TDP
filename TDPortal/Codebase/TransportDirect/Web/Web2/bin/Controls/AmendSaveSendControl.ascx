<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendSaveSendControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendSaveSendControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="AmendViewControl" Src="AmendViewControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendCostSearchDateControl" Src="AmendCostSearchDateControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendCostSearchDayControl" Src="AmendCostSearchDayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendFaresControl" Src="AmendFaresControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendStopoverControl" Src="AmendStopoverControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendMessageControl" Src="AmendSaveSendMessageControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendAmendControl" Src="AmendSaveSendAmendControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendSaveControl" Src="AmendSaveSendSaveControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendSaveFullControl" Src="AmendSaveSendSaveFullControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendLoginControl" Src="AmendSaveSendLoginControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendSendControl" Src="AmendSaveSendSendControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendCarDetailsControl" Src="AmendCarDetailsControl.ascx" %>
<cc1:helplabelcontrol id="amendHelpLabelControl" visible="False" cssmaintemplate="helpboxoutput" runat="server"></cc1:helplabelcontrol>
<div id="boxtypethirteen">
	<asp:imagebutton id="imageButtonTabAmendCostSearchDate" 
		accesskey="" tabindex="-32768" runat="server" borderwidth="0" imagealign="bottom"></asp:imagebutton><asp:imagebutton id="imageButtonTabFareDetail" 
		accesskey="" tabindex="-32768" runat="server" borderwidth="0" imagealign="bottom"></asp:imagebutton><asp:imagebutton id="imageButtonTabAmendDateTime" 
		accesskey="" tabindex="-32768" runat="server" borderwidth="0" imagealign="bottom"></asp:imagebutton><asp:imagebutton id="imageButtonTabAmendStopover" 
		accesskey="" tabindex="-32768" runat="server" borderwidth="0" imagealign="bottom"></asp:imagebutton><asp:imagebutton id="imageButtonTabSaveFavourite" 
		accesskey="" tabindex="-32768" runat="server" borderwidth="0" imagealign="bottom"></asp:imagebutton><asp:imagebutton ID="imageButtonTabAmendCarDetails" 
		accesskey="" tabindex="-32768" runat="server" borderwidth="0" imagealign="bottom"></asp:imagebutton><asp:imagebutton id="imageButtonTabSendEmail" 
		accesskey="" tabindex="-32768" runat="server" borderwidth="0" imagealign="bottom"></asp:imagebutton><asp:imagebutton id="imageButtonTabAmendDay" 
		accesskey="" tabindex="-32768" runat="server" borderwidth="0" imagealign="bottom"></asp:imagebutton><asp:imagebutton id="imageButtonTabAmendView" 
		accesskey="" tabindex="-32768" runat="server" imagealign="bottom" borderwidth="0"></asp:imagebutton></div>
<div class="boxtypetwelveamendcontrol">
	<table lang="en" cellspacing="0" cellpadding="0" width="100%" summary="Amend or send to a Friend">
		<!--
		<tr>
			<td>
				<div id="helpimg">&nbsp;<cc1:helpcustomcontrol id="amendHelpCustomControl" runat="server" helplabel="amendHelpLabelControl" scrolltohelp="False"></cc1:helpcustomcontrol></div>
			</td>
		</tr>
		-->
		<tr>
			<td><asp:panel id="thePanel" runat="server">
					<uc1:amendcostsearchdatecontrol id="theAmendCostSearchDateControl" runat="server"></uc1:amendcostsearchdatecontrol>
					<uc1:amendfarescontrol id="theAmendFaresControl" runat="server"></uc1:amendfarescontrol>
					<uc1:amendstopovercontrol id="theAmendStopoverControl" runat="server"></uc1:amendstopovercontrol>
					<uc1:amendCarDetailsControl id="theAmendCarDetailsControl" runat="server"></uc1:amendCarDetailsControl>
					<uc1:amendsavesendamendcontrol id="theAmendSaveSendAmendControl" runat="server" visible="False"></uc1:amendsavesendamendcontrol>
					<uc1:amendsavesendsavecontrol id="theAmendSaveSendSaveControl" runat="server" visible="False"></uc1:amendsavesendsavecontrol>
					<uc1:amendsavesendsavefullcontrol id="theAmendSaveSendSaveFullControl" runat="server" visible="False"></uc1:amendsavesendsavefullcontrol>
					<uc1:amendsavesendsendcontrol id="theAmendSaveSendSendControl" runat="server" visible="False"></uc1:amendsavesendsendcontrol>
					<uc1:amendsavesendlogincontrol id="theAmendSaveSendLoginControl" runat="server" visible="False"></uc1:amendsavesendlogincontrol>
					<uc1:amendsavesendmessagecontrol id="theAmendSaveSendMessageControl" runat="server" visible="False"></uc1:amendsavesendmessagecontrol>
					<uc1:amendcostsearchdaycontrol id="theAmendCostSearchDayControl" runat="server"></uc1:amendcostsearchdaycontrol>
					<uc1:amendviewcontrol id="theAmendViewControl" runat="server"></uc1:amendviewcontrol>
				</asp:panel></td>
		</tr>
	</table>
</div>
