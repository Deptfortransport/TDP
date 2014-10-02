<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FareDetailsDiagramControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FareDetailsDiagramControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="FareDetailsDiagramSegmentControl" Src="FareDetailsDiagramSegmentControl.ascx" %>
<asp:repeater id="fareDetailsDiagramRepeater" runat="server" enableviewstate="false">
	<headertemplate>
		<table border="0" cellspacing="0" cellpadding="0" width="100%">
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td class="txteightbgrey" align="right" nowrap="nowrap"><span style="visibility: hidden"><%# GetLongestInstruction %></span></td>
				<td>&nbsp;</td>
				<td class="txteightb" align="center" valign="baseline">
					<%# StartText %></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
	</headertemplate>
	<itemtemplate>
		<uc1:faredetailsdiagramsegmentcontrol id="detailSegment" runat="server"></uc1:faredetailsdiagramsegmentcontrol>
	</itemtemplate>
	<footertemplate>
		<tr>
			<td colspan="4" style="font-size: 1px">&nbsp;</td>
			<td class="txteightb" align="center" valign="top">
				<%# EndText %>
			</td>
			<td align="left" colspan="2">&nbsp;</td>
		</tr>
		</table>
	</footertemplate>
</asp:repeater>
