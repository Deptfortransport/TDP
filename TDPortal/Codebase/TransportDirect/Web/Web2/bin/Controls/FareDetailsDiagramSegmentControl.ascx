<%@ Register TagPrefix="uc1" TagName="LegDetailsControl" Src="LegDetailsControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FareDetailsDiagramSegmentControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FareDetailsDiagramSegmentControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import namespace="TransportDirect.UserPortal.JourneyControl" %>
<asp:repeater id="fareDetailsSegmentRepeater" runat="server" enableviewstate="False">
	<headertemplate>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
			<td colspan="6" class="<%# GetCssClass("Bottom") %>">&nbsp;</td>
		</tr>
	</headertemplate>
	<itemtemplate>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
			<td class="<%# GetEightBoldCssClass("Left") %>" align="center" rowspan="6">&nbsp;</td>
			<td class="txteightbgrey" rowspan="3" align="right" nowrap="nowrap"><%# GetPreviousInstruction ( Container.ItemIndex ) %></td>
			<td class="departline" rowspan="3" align="right">
				<span class="txtnineb" style="padding-left:5px">
					<%# GetPreviousArrivalDateTime( Container.ItemIndex ) %>
				</span>
			</td>
			<td class="<%# GetBackgroundLineClass (Container.ItemIndex) %>" rowspan="2"><span class="txtnineb">&nbsp;</span></td>
			<td class="<%# GetNineBoldCssClass("Right") %>" align="left" colspan="2" rowspan="6" width="100%" valign="middle" style="padding:5px">&nbsp;</td>
		</tr>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
		</tr>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
			<td class="bgline" rowspan="2" valign="<%# GetNodeImageVAlign( Container.ItemIndex ) %>" align="center">
				<img src="<%# GetNodeImage( Container.ItemIndex ) %>" alt=" <%# GetNodeImageAlternateText( Container.ItemIndex ) %>" />
			</td>
		</tr>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
			<td class="txteightbgrey" rowspan="3" align="right"><%# GetCurrentInstruction ( Container.ItemIndex ) %></td>
			<td class="txtnineb" rowspan="3" style="padding-left:5px" align="right">
				<%# GetDepartDateTime( Container.ItemIndex ) %>
			</td>
		</tr>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
			<td class="bgline" rowspan="2">&nbsp;</td>
		</tr>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td class="<%# GetCssClass("Left") %>">&nbsp;</td>
			<td align="center" colspan="2" style="padding-bottom: 10px; padding-top: 10px">
				<table>
					<tr>
						<td align="center">
							<img src="<%# GetModeImagePath( (PublicJourneyDetail)Container.DataItem )%>" alt="<%# GetModeImageAlternateText( (PublicJourneyDetail)Container.DataItem ) %>" />
						</td>
					</tr>
					<tr>
						<td align="center">
							<%# FormatModeDetails( (PublicJourneyDetail)Container.DataItem ) %>
						</td>
					</tr>
				</table>
			</td>
			<td class="bgline">&nbsp;</td>
			<td>
				<asp:placeholder id="faresTablePlaceholder" runat="server"></asp:placeholder>
				<asp:label id="labelBreak" runat="server">
					<br />
				</asp:label>
				<uc1:legdetailscontrol id="legDetails" runat="server"></uc1:legdetailscontrol>
			</td>
			<td class="<%# GetCssClass("Right") %>">&nbsp;</td>
		</tr>
	</itemtemplate>
	<footertemplate>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
			<td class="<%# GetEightBoldCssClass("Left") %>"  align="center" rowspan="6">&nbsp;</td>
			<td class="txteightbgrey" rowspan="3" align="right" nowrap="nowrap"><%# FooterEndInstruction %></td>
			<td class="departline" rowspan="3" align="right">
				<span class="txtnineb" style="padding-left:5px">
					<%# FooterEndDateTime%>
				</span>
			</td>
			<td class="bgline" rowspan="2"><span class="txtnineb">&nbsp;</span></td>
			<td class="<%# GetNineBoldCssClass("Right") %>"  align="left" valign="bottom" colspan="2" rowspan="6" style="padding:5px">&nbsp;</td>
		</tr>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
		</tr>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
			<td class="bgline" valign="bottom" align="center" rowspan="2">
				<img style="vertical-align: bottom" src="<%# EndNodeImage %>" alt="<%# EndNodeImageAlternateText %>"></img>
			</td>
		</tr>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
			<td class="txteightbgrey" align="right" rowspan="3"><%# FooterExitText %></td>
			<td class="txtnineb" style="padding-left:5px" rowspan="3" align="right"><%# FooterExitDateTime %></td>
		</tr>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
			<td rowspan="2">&nbsp;</td>
		</tr>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
		</tr>
		<tr>
			<td style="font-size: 1px">&nbsp;</td>
			<td colspan="6" class="<%# GetCssClass("Top") %>">&nbsp;</td>
		</tr>
	</footertemplate>
</asp:repeater>
