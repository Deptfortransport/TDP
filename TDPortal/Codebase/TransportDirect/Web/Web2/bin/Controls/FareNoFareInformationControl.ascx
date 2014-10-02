<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FareNoFareInformationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FareNoFareInformationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<asp:panel id="faresHeadingPanel" runat="server">
		<table id="fareDetails" class="fdtctable" cellspacing="0" cellpadding="5" >
			<tr class="fdtrowheader">
				<td class="fdtcellheader">
					<strong><asp:Label id="labelMode" runat="server" EnableViewState="False"></asp:Label></strong>&nbsp;
					<asp:Label id="labelRoute" runat="server" EnableViewState="False"></asp:Label><br />
					</td>
				<td align="right" class="fdtcellcheaper"><strong>
						<asp:Label id="labelView" runat="server" EnableViewState="False"></asp:Label>
						<uc1:hyperlinkpostbackcontrol id="otherFaresLinkControl" runat="server" enableviewstate="false"></uc1:hyperlinkpostbackcontrol></strong>&nbsp;</td>
			</tr>
			<tr class="fdtrowheader">
				<td class="fdtcellheaderb" colspan="2">
					<asp:Label id="labelFareInformation" runat="server" EnableViewState="False"></asp:Label><br />
					&nbsp;</td>
			</tr>
		</table>
</asp:panel>
