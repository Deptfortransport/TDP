<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyReplanSegmentControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyReplanSegmentControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false"%>
<%@ Import namespace="TransportDirect.UserPortal.JourneyControl" %>
<table border="0" class="jdetail2" cellpadding="0" cellspacing="0">
	<tbody>
		<asp:repeater id="journeySegmentsRepeater" runat="server">
			<headertemplate>
				<tr>
					<td>&nbsp;</td>
					<td><img src="<%# SpacerImageUrl %>" alt="" width="95px" height="1px" /></td>
					<td class="txteightbgrey" align="right" nowrap="nowrap"><span style="visibility: hidden"><%# GetLongestInstruction %></span></td>
					<td>&nbsp;</td>
					<td class="txteightb" align="center">
						<asp:panel runat="server" style="<%# StartTextVisible() %>" ID="Panel1">
							<%# StartText%>
						</asp:panel></td>
					<td colspan="2" width="100%">&nbsp;</td>
				</tr>
			</headertemplate>
			<itemtemplate>
				<div id="interchangeDiv" runat="server">
					<tr id="interchangeTableRow1">
						<td class="spacer2">&nbsp;</td>
						<td class="txteightb" align="center" rowspan="6">&nbsp;</td>
						<td class="txteightbgrey" align="right" nowrap rowspan="3">
							<%# GetPreviousInstruction ( Container.ItemIndex, true ) %>
						</td>
						<td class="departline" rowspan="3" align="right">
							<span class="txtnineb" style="padding-left:5px">
								<%# GetPreviousArrivalDateTime( Container.ItemIndex, true ) %>
							</span>
						</td>
						<td class="bgline" rowspan="2">
							</td>
						
						<td id="<%# GetHighlightCellId( Container.ItemIndex, "Bottom", true ) %>" class="<%# GetHighlightCellClass( Container.ItemIndex, "Bottom", true ) %>" rowspan="3" align="right">
							<span class="txtnineb" style="padding-left:5px">
								&nbsp;
							</span>
						</td>
						
						<td class="txtnineb" align="left" rowspan="6" valign="middle" style="padding:5px">
							<asp:label id="alightLocationLabelControl" runat="server"></asp:label>
							<asp:label id="alightLocationInfoLinkLabel" runat="server"></asp:label>
						</td>
					</tr>
					<tr id="interchangeTableRow2">
						<td class="spacer2">&nbsp;</td>
					</tr>
					<tr id="interchangeTableRow3">
						<td class="spacer2">&nbsp;</td>
						<td class="bgline" rowspan="2" valign="<%# GetNodeImageVAlign( Container.ItemIndex ) %>" align="center">
							<img src="<%# GetNodeImage( Container.ItemIndex ) %>" alt=" <%# GetNodeImageAlternateText( Container.ItemIndex ) %>" title=" <%# GetNodeImageAlternateText( Container.ItemIndex ) %>" />
						</td>
					</tr>
					<tr id="interchangeTableRow4">
						<td class="spacer2">&nbsp;</td>
						<td rowspan="3" colspan="2">&nbsp;</td>

						<td id="<%# GetHighlightCellId( Container.ItemIndex, "Top", true ) %>" class="<%# GetHighlightCellClass( Container.ItemIndex, "Top", true ) %>" rowspan="3" style="padding-left:5px" align="right">
							&nbsp;
						</td>

						<td rowspan="3">&nbsp;</td>
					</tr>
					<tr id="interchangeTableRow5">
						<td class="spacer2">&nbsp;</td>
						<td class="bgline" rowspan="2">&nbsp;</td>
					</tr>
					<tr id="interchangeTableRow6">
						<td class="spacer2">&nbsp;</td>
					</tr>
				</div>

				<tr>
					<td class="spacer2">&nbsp;</td>
					<td class="txteightb" align="center" rowspan="6">&nbsp;</td>
					<td class="txteightbgrey" rowspan="3" align="right" nowrap="nowrap"><%# GetPreviousInstruction ( Container.ItemIndex, false ) %></td>
					<td class="departline" rowspan="3" align="right">
						<span class="txtnineb" style="padding-left:5px">
							<%# GetPreviousArrivalDateTime( Container.ItemIndex, false ) %>
						</span>
					</td>
					<td rowspan="2" class="<%# GetBackgroundLineClass (Container.ItemIndex) %>"><span class="txtnineb">&nbsp;</span></td>
					<td id="<%# GetHighlightCellId( Container.ItemIndex, "Bottom", false ) %>" class="<%# GetHighlightCellClass( Container.ItemIndex, "Bottom", false ) %>" rowspan="3" align="right">
						<span class="txtnineb" style="padding-left:5px">
							&nbsp;
						</span>
					</td>
					<td class="txtnineb" align="left" rowspan="6" valign="middle" style="padding:5px">
						<asp:label id="locationLabelControl" runat="server"></asp:label>
						<asp:label id="locationInfoLinkLabel" runat="server"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="spacer2">&nbsp;</td>
				</tr>
				<tr>
					<td class="spacer2">&nbsp;</td>
					<td class="bgline" rowspan="2" valign="<%# GetNodeImageVAlign( Container.ItemIndex ) %>" align="center">
						<img src="<%# GetNodeImage( Container.ItemIndex ) %>" alt=" <%# GetNodeImageAlternateText( Container.ItemIndex ) %>" title=" <%# GetNodeImageAlternateText( Container.ItemIndex ) %>" />
					</td>
				</tr>
				<tr>
					<td class="spacer2">&nbsp;</td>
					<td class="txteightbgrey" rowspan="3" align="right"><%# GetCurrentInstruction ( Container.ItemIndex ) %></td>
					<td class="txtnineb" rowspan="3" style="padding-left:5px" align="right">
						<%# GetDepartDateTime( Container.ItemIndex ) %>
					</td>
					<td id="<%# GetHighlightCellId( Container.ItemIndex, "Top", false ) %>" class="<%# GetHighlightCellClass( Container.ItemIndex, "Top", false ) %>" rowspan="3" style="padding-left:5px" align="right">
						&nbsp;
					</td>
				</tr>
				<tr>
					<td class="spacer2">&nbsp;</td>
					<td class="bgline" rowspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td class="spacer2">&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td align="center" style="padding-bottom: 10px; padding-top: 10px">
						<table>
							<tr>
								<td align="center">
									<cc1:tdimage id="imageMode" runat="server" imageurl="<%# GetModeImageUrl((PublicJourneyDetail)Container.DataItem)%>" alternatetext=" " />
								</td>
							</tr>
							<tr>
								<td align="center">
									<asp:label id="modeLinkLabel" runat="server"></asp:label>
									<%# FormatModeDetails( (PublicJourneyDetail)Container.DataItem ) %>
								</td>
							</tr>
						</table>
					</td>
					<td colspan="2" class="txtnineb" align="center" valign="middle"><%# GetFrequencyText( (PublicJourneyDetail)Container.DataItem )%></td>
					<td class="bgline">&nbsp;</td>
					<td id="<%# GetHighlightCellId( Container.ItemIndex, "Middle", false ) %>" class="<%# GetHighlightCellClass( Container.ItemIndex, "Middle", false ) %>" valign="middle" align="center">
						<cc1:scriptablecheckbox id="checkJourneyElement" EnableClientScript="true" ScriptName="JourneyReplanElementSelection" Action="HandleReplanSegmentChecks()" Value="<%# GetItemIndex( Container.ItemIndex ) %>" Checked="<%# GetCheckedStatus( Container.ItemIndex ) %>" runat="server"></cc1:scriptablecheckbox>
					</td>
					<td valign="middle" colspan="2" style="padding:5px">
						<asp:label id="labelTickBoxInputMessage" associatedcontrolid="checkJourneyElement" runat="server" class="txtnine"></asp:label>
					</td>
				</tr>
			</itemtemplate>
			<footertemplate>
				<tr>
					<td class="spacer2">&nbsp;</td>
					<td class="txteightb" align="center" rowspan="6">&nbsp;</td>
					<td class="txteightbgrey" rowspan="3" align="right" nowrap="nowrap"><%# FooterEndInstruction %></td>
					<td class="departline" rowspan="3" align="right">
						<span class="txtnineb" style="padding-left:5px">
							<%# FooterEndDateTime%>
						</span>
					</td>
					<td class="bgline" rowspan="2"><span class="txtnineb">&nbsp;</span></td>
					<td id="<%# GetHighlightCellId( Container.ItemIndex, "Bottom", false ) %>" class="<%# GetHighlightCellClass( Container.ItemIndex, "Bottom", false ) %>" rowspan="3" align="right">&nbsp;
					</td>
					<td class="txtnineb" align="left" valign="middle" rowspan="6" style="padding:5px">
						<asp:label id="endLocationLabelControl" runat="server"></asp:label>
						<asp:label id="endLocationInfoLinkLabel" runat="server"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="spacer2">&nbsp;</td>
				</tr>
				<tr>
					<td class="spacer2">&nbsp;</td>
					<td class="bgline" valign="bottom" align="center" rowspan="2">
						<img style="vertical-align: bottom" src="<%# EndNodeImage %>" alt="<%# EndNodeImageAlternateText %>" title="<%# EndNodeImageAlternateText %>" />
					</td>
				</tr>
				<tr>
					<td class="spacer2">&nbsp;</td>
					<td class="txteightbgrey" align="right" rowspan="3"><%# FooterExitText %></td>
					<td class="txtnineb" style="padding-left:5px" rowspan="3" align="right"><%# FooterExitDateTime %></td>
					<td class="arriveline" align="right" rowspan="3">&nbsp;</td>
				</tr>
				<tr>
					<td class="spacer2">&nbsp;</td>
					<td rowspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td class="spacer2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" class="spacer2">&nbsp;</td>
					<td class="txteightb" align="center">
						<%# EndText %>
					</td>
					<td align="left" colspan="4">&nbsp;</td>
				</tr>
			</footertemplate>
		</asp:repeater>
	</tbody>
</table>

