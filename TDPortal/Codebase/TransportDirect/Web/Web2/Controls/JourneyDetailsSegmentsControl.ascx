<%@ Import namespace="TransportDirect.UserPortal.JourneyControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyDetailsSegmentsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyDetailsSegmentsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="LegInstructionsControl" Src="LegInstructionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccessibleInstructionControl" Src="AccessibleInstructionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NetworkMapLinksControl" Src="NetworkMapLinksControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="VehicleFeaturesControl" Src="VehicleFeaturesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccessibleFeaturesControl" Src="AccessibleFeaturesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WalkitLinkControl" Src="WalkitLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CJPUserInfoControl" Src="CJPUserInfoControl.ascx" %>

<table border="0" cellpadding="0" cellspacing="0" width="100%">
	<tbody>
		<asp:repeater id="journeySegmentsRepeater" runat="server">
			<headertemplate>
				<tr>
					<td>&nbsp;</td>
					<td><img src="<%# GetSpacerImageUrl() %>" alt="" width="95px" height="1px" /></td>
					<td class="txteightbgrey" align="right" nowrap="nowrap"><span style="visibility: hidden"><%# GetLongestInstruction () %></span></td>
					<td>&nbsp;</td>
					<td class="txtnineb" align="center">
						<asp:panel ID="Panel1" runat="server" style="<%# StartTextVisible() %>">
							<%# StartText%></asp:panel></td>
					<td colspan="3" width="100%">&nbsp;</td>
					<td><!-- departure boards/map button column -->&nbsp;</td>
				</tr>
			</headertemplate>
			<itemtemplate>
				<asp:tablerow id="interchangeTableRow1" runat="server">
					<asp:tablecell style="font-size: 1px">&nbsp;</asp:tablecell>
					<asp:tablecell class="txteightb" align="center" rowspan="6">&nbsp;</asp:tablecell>
					<asp:tablecell class="txteightbgrey" align="right" nowrap="nowrap" rowspan="3">
						<%# GetPreviousInstruction ( Container.ItemIndex, true ) %>
					</asp:tablecell>
					<asp:tablecell class="departline" rowspan="3" align="right">
						<span class="txtnineb" style="padding-left:5px">
							<%# GetPreviousArrivalDateTime( Container.ItemIndex, true ) %>
						</span>
					</asp:tablecell>
					<asp:tablecell class="bgline" rowspan="2">
						<span class="txtnineb">&nbsp;</span></asp:tablecell>
					<asp:tablecell class="txtnineb" align="left" rowspan="6" valign="middle" style="padding:5px">
						<asp:label id="alightLocationLabelControl" runat="server"></asp:label>
						<uc1:hyperlinkpostbackcontrol id="alightLocationInfoLinkControl" runat="server"></uc1:hyperlinkpostbackcontrol>
					</asp:tablecell>
					<asp:tablecell align="center" valign="middle" rowspan="6">&nbsp;
					</asp:tablecell>
					<asp:tablecell rowspan="6" colspan="2">&nbsp;</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow id="interchangeTableRow2" runat="server">
					<asp:tablecell style="font-size: 1px">&nbsp;</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow id="interchangeTableRow3" runat="server">
					<asp:tablecell style="font-size: 1px">&nbsp;</asp:tablecell>
					<asp:tableCell class="bgline" rowspan="2" valign="<%# GetNodeImageVAlign( Container.ItemIndex ) %>" align="center">
						<img src="<%# GetNodeImage( Container.ItemIndex ) %>" alt=" <%# GetNodeImageAlternateText( Container.ItemIndex ) %>" title=" <%# GetNodeImageAlternateText( Container.ItemIndex ) %>" />
					</asp:tableCell>
				</asp:tablerow>
				<asp:tablerow id="interchangeTableRow4" runat="server">
					<asp:tablecell style="font-size: 1px">&nbsp;</asp:tablecell>
					<asp:tablecell rowspan="3" colspan="2">&nbsp;</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow id="interchangeTableRow5" runat="server">
					<asp:tablecell style="font-size: 1px">&nbsp;</asp:tablecell>
					<asp:tablecell class="bgline" rowspan="2">&nbsp;</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow id="interchangeTableRow6" runat="server">
					<asp:tablecell style="font-size: 1px">&nbsp;</asp:tablecell>
				</asp:tablerow>
				<tr>
					<td style="font-size: 1px">&nbsp;</td>
					<td class="txteightb" align="center" rowspan="6">&nbsp;</td>
					<td class="txteightbgrey" rowspan="3" align="right" nowrap="nowrap"><%# GetPreviousInstruction ( Container.ItemIndex, false ) %></td>
					<td class="departline" rowspan="3" align="right">
					    <span class="txtnineb" style="padding-left:5px">
							<%# GetPreviousArrivalDateTime( Container.ItemIndex, false ) %>
						</span>
					</td>
					<td rowspan="2" class="<%# GetBackgroundLineClass (Container.ItemIndex) %>"><span class="txtnineb">&nbsp;</span></td>
					<td align="left" rowspan="6" colspan="3" valign="middle" style="padding:5px">
					    <div class="inlinemiddle floatleftonly">
						    <span class="txtnineb">
						        <asp:label id="locationLabelControl" runat="server"></asp:label>
						        <asp:HyperLink id="locationInfoLink" runat="server" Target="_blank" visible="false"/>
        						
						        <uc1:hyperlinkpostbackcontrol id="locationInfoLinkControl" runat="server"></uc1:hyperlinkpostbackcontrol>
						    </span>
						</div>
						<uc1:AccessibleFeaturesControl id="locationAccessibleFeaturesControl" runat="server"></uc1:AccessibleFeaturesControl>	
					    <br />	
						<uc1:CJPUserInfoControl id="cjpUserLocationNaptanInfo" runat="server" InfoType="NaPTAN" />
						<uc1:CJPUserInfoControl ID="cjpUserLocationCoordinateInfo" runat="server" InfoType="Coordinate" />				
						<asp:label id="startCarParkLabel" runat="server" enableviewstate="false" cssclass="txtnine"></asp:label>
					</td>
                    <td align="right" valign="bottom" rowspan="6" class="tddepartureboard">
                        <div class="centertextonly divdepartureboard">
                            <asp:HyperLink id="hyperlinkDepartureBoard" runat="server" Text="<%# DepartureBoardButtonText %>" />
                            <br />
                            <asp:HyperLink ID="hyperlinkDepartureBoardLink" runat="server" Text="<%# DepartureBoardLinkText %>"  />
                         </div>
                         <div class="centertextonly divdepartureboard">
                            <asp:HyperLink id="hyperlinkArrivalBoard" runat="server" Text="<%# ArrivalBoardButtonText %>" visible="false"/>
                            <br />
                            <asp:HyperLink ID="hyperlinkArrivalBoardLink" runat="server" Text="<%# ArrivalBoardLinkText %>" Visible="false"  />
                        </div>
                    </td>
			    </tr>
				<tr>
					<td style="font-size: 1px">&nbsp;</td>
				</tr>
				<tr>
					<td style="font-size: 1px">&nbsp;</td>
					<td class="bgline" rowspan="2" valign="<%# GetNodeImageVAlign( Container.ItemIndex ) %>" align="center">
						<img src="<%# GetNodeImage( Container.ItemIndex ) %>" alt=" <%# GetNodeImageAlternateText( Container.ItemIndex ) %>" title=" <%# GetNodeImageAlternateText( Container.ItemIndex ) %>" />
					</td>
				</tr>
				<tr>
					<td style="font-size: 1px">&nbsp;</td>
					<td class="txteightbgrey" rowspan="3" style="padding-left:6px" align="right"><%# GetCurrentInstruction ( Container.ItemIndex ) %></td>
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
					<td align="center" style="padding-bottom: 10px; padding-top: 10px">
						<table>
							<tr>
								<td align="center">
									<asp:Image id="imageMode" runat="server" imageurl="<%# GetModeImageUrl((JourneyLeg)Container.DataItem)%>" visible="<%# !HasServiceDetails((JourneyLeg)Container.DataItem)%>" alternatetext=" " title="<%# GetModeImageAlternateText( (JourneyLeg)Container.DataItem ) %>"/>
									<asp:imagebutton id="imageButtonMode" runat="server" imageurl="<%# GetModeImageUrl((JourneyLeg)Container.DataItem)%>" visible="<%# HasServiceDetails((JourneyLeg)Container.DataItem)%>" alternatetext="<%# GetModeLinkText( (JourneyLeg)Container.DataItem ) %>" title="<%# GetModeLinkText( (JourneyLeg)Container.DataItem ) %>" commandname="<%# GetCommandName((JourneyLeg)Container.DataItem) %>" commandargument="<%# GetCommandArgument((JourneyLeg)Container.DataItem) %>"/>
								</td>
							</tr>
							<tr>
								<td align="center">
									<uc1:hyperlinkpostbackcontrol id="modeLinkControl" runat="server"></uc1:hyperlinkpostbackcontrol>
									<%# FormatModeDetails( (JourneyLeg)Container.DataItem, Container.ItemIndex ) %>
									<uc1:CJPUserInfoControl id="cjpUserInfoWalkLength" runat="server" InfoType="WalkLength" newLineBefore="true" />
								</td>
							</tr>
						</table>
					</td>
					<td colspan="2" class="txtnineb" align="center" valign="middle">
					    <%# GetRoutingGuideStatus( (JourneyLeg)Container.DataItem ) %>
					   <uc1:CJPUserInfoControl id="cjpUserInfoJourneyLegSource" runat="server" InfoType="DataSource" newLineBefore="true" />
					</td>
					<td class="bgline">&nbsp;</td>
					<td valign="middle" class="txtnine" colspan="3" style="padding:5px">
						<table>
							<tr>
								<td>
									<uc1:leginstructionscontrol id="legInstructionsControl" runat="server"></uc1:leginstructionscontrol>
									<uc1:AccessibleInstructionControl id="accessibleInstructionControl" runat="server"></uc1:AccessibleInstructionControl>
									<asp:Panel ID="pnlFeatureIcons" runat="server" EnableViewState="false">
									    <uc1:vehiclefeaturescontrol id="vehicleFeaturesControl" runat="server"></uc1:vehiclefeaturescontrol>
									    <uc1:AccessibleFeaturesControl id="legAccessibleFeaturesControl" runat="server"></uc1:AccessibleFeaturesControl>
									    <br />
									    <br />
									</asp:Panel>
									<uc1:networkmaplinkscontrol id="networkMapLink" runat="server"></uc1:networkmaplinkscontrol>
									<uc1:WalkitLinkControl ID="walkitLink" runat="server" />
									<%# GetDisplayNotes( (JourneyLeg)Container.DataItem ) %>
									<uc1:CJPUserInfoControl id="cjpUserInfoDisplayNotes" runat="server" InfoType="JourneyDisplayNotes" newLineBefore="true" />
									<uc1:CJPUserInfoControl id="cjpUserInfoLegDebugInfo" runat="server" InfoType="LegDebugInfo" newLineBefore="true" />
								</td>
							</tr>
						</table>
					</td>
					<td align="right" class="tddepartureboard">
					    <div class="centertextonly divdepartureboard">
                        <cc1:TDImageButton ID="imageMapButton" runat="server" Width="42" Height="36"></cc1:TDImageButton>
                        <asp:hyperlink runat="server" id="walkitImageLink" Target="_blank" Visible ="false">
                            <asp:Image id="walkitImage" runat="server" />
                        </asp:hyperlink>
                        <br />
                        <uc1:hyperlinkpostbackcontrol id="hyperlinkPostbackControlMapButton" runat="server" ></uc1:hyperlinkpostbackcontrol>
                        <asp:hyperlink runat="server" id="walkitDirectionsLink" Target="_blank" Visible ="false" />
						</div>
					</td>
				</tr>
			</itemtemplate>
			<footertemplate>
				<tr>
					<td style="font-size: 1px">&nbsp;</td>
					<td class="txteightb" align="center" rowspan="6">&nbsp;</td>
					<td class="txteightbgrey" rowspan="3" align="right" nowrap="nowrap"><%# FooterEndInstruction %></td>
					<td class="departline" rowspan="3" align="right">
						<span class="txtnineb" style="padding-left:5px">
							<%# FooterEndDateTime%>
						</span>
					</td>
					<td class="bgline" rowspan="2"><span class="txtnineb">&nbsp;</span></td>
					<td align="left" colspan="3" valign="bottom" rowspan="6" style="padding:5px">
					    <uc1:CJPUserInfoControl id="cjpUserLocationNaptanInfo" runat="server" InfoType="NaPTAN" />
						<uc1:CJPUserInfoControl ID="cjpUserLocationCoordinateInfo" runat="server" InfoType="Coordinate" NewLineAfter="true" />	
						<div class="inlinemiddle floatleftonly">
						    <span class="txtnineb">
						        <asp:label id="endLocationLabelControl" runat="server"></asp:label>
						        <asp:HyperLink id="endLocationInfoLink" runat="server" Target="_blank" visible="false"/>
						        <uc1:hyperlinkpostbackcontrol id="endLocationInfoLinkControl" runat="server"></uc1:hyperlinkpostbackcontrol>
						    </span>
						</div>
						<uc1:AccessibleFeaturesControl id="locationAccessibleFeaturesControl" runat="server"></uc1:AccessibleFeaturesControl>
						<br />
						<asp:label id="endCarParkLabel" runat="server" enableviewstate="false" cssclass="txtnine"></asp:label>
					</td>
                    <td align="right" valign="bottom" rowspan="6" class="tddepartureboard">
                        <div class="centertextonly divdepartureboard">
                            <asp:HyperLink ID="hyperlinkArrivalBoard" runat="server" Text="<%# ArrivalBoardButtonText %>"  />
                            <br />
                            <asp:HyperLink ID="hyperlinkArrivalBoardLink" runat="server" Text="<%# ArrivalBoardLinkText %>"  />
                        </div>
                    </td>
				</tr>
				<tr>
					<td style="font-size: 1px">&nbsp;</td>
				</tr>
				<tr>
					<td style="font-size: 1px">&nbsp;</td>
					<td class="bgline" valign="bottom" align="center" rowspan="2">
						<img style="vertical-align: bottom" src="<%# EndNodeImage %>" alt="<%# EndNodeImageAlternateText %>" title="<%# EndNodeImageAlternateText %>"></img>
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
					<td colspan="4" style="font-size: 1px">&nbsp;</td>
					<td class="txtnineb" align="center">
						<%# EndText %>
					</td>
					<td align="left" colspan="4">&nbsp;</td>
				</tr>
			</footertemplate>
		</asp:repeater>
	</tbody>
</table>
