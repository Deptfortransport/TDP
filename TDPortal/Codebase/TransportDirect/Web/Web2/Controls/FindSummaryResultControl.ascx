<%@ Import namespace="TransportDirect.UserPortal.Web.Adapters" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindSummaryResultControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindSummaryResultControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:repeater id="summaryRepeater" runat="server" OnItemDataBound="summaryRepeater_ItemDatabound">
	<headertemplate>
		<div class="fscheaderbox">
			<table cellspacing="0" class="fscheader" summary="Journey Summary Header" lang="en">
				<tr bgcolor="#ffffff">
					<td class="fscheader10<%# GetHeaderRowCssClass() %> <%# CellCss(IndexVisible)%>"></td>
					<td class="fscheader9<%# GetHeaderRowCssClass() %> <%# CellCss(TransportVisible)%>">
						<asp:panel id="panelTransport" runat="server" visible='<%# TransportVisible %>'>
							<asp:label runat="server" id="SortTransportText" visible="<%# !ShowSortableLinks %>">
								<%# HeaderItem(9) %>
							</asp:label>
							<cc1:tdbutton id="buttonTransport" runat="server" visible="<%# ShowSortableLinks %>" tooltip="<%# HeaderAltText(9) %>"></cc1:tdbutton>
							<asp:image id="Icon9" runat="server" imageurl="<%# HeaderIconUrl(9) %>" alternateText="<%# HeaderIconAltText() %>" visible="<%# HeaderIconVisible(9) %>">
							</asp:image>
						</asp:panel>
					</td>
					<td class="fscheader1<%# GetHeaderRowCssClass() %> <%# CellCss(FromVisible)%>">
						<asp:panel id="panelFrom" runat="server" visible='<%# FromVisible%>'>
							<asp:label runat="server" id="SortOriginText" visible="<%# !ShowSortableLinks %>">
								<%# HeaderItem(1) %>
							</asp:label>
							<cc1:tdbutton id="buttonFrom" runat="server"  visible="<%# ShowSortableLinks %>" tooltip="<%# HeaderAltText(1) %>"></cc1:tdbutton>
							<asp:Image id="Icon1" runat="server" ImageUrl="<%# HeaderIconUrl(1) %>" alternateText="<%# HeaderIconAltText() %>" Visible="<%# HeaderIconVisible(1) %>">
							</asp:Image>
						</asp:panel></td>
					<td class="fscheader2<%# GetHeaderRowCssClass() %> <%# CellCss(ToVisible)%>">
						<asp:panel id="panelTo" runat="server" visible='<%# ToVisible%>'>
							<asp:label runat="server" id="SortDestinationText" visible="<%# !ShowSortableLinks %>">
								<%# HeaderItem(2) %>
							</asp:label>
							<cc1:tdbutton id="buttonTo" runat="server" visible="<%# ShowSortableLinks %>" tooltip="<%# HeaderAltText(2) %>"></cc1:tdbutton>
							<asp:Image id="Icon2" runat="server" ImageUrl="<%# HeaderIconUrl(2) %>" alternateText="<%# HeaderIconAltText() %>" Visible="<%# HeaderIconVisible(2) %>">
							</asp:Image>
						</asp:panel></td>
					<td class="fscheader3<%# GetHeaderRowCssClass() %> <%# CellCss(ChangesVisible)%>">
						<asp:panel id="panelChanges" runat="server" visible='<%# ChangesVisible%>'>
							<asp:label runat="server" id="SortInterchangesText" visible="<%# !ShowSortableLinks %>">
								<%# HeaderItem(3) %>
							</asp:label>
							<cc1:tdbutton id="buttonChanges" runat="server" visible="<%# ShowSortableLinks %>" tooltip="<%# HeaderAltText(3) %>"></cc1:tdbutton>
							<asp:Image id="Icon3" runat="server" ImageUrl="<%# HeaderIconUrl(3) %>" alternateText="<%# HeaderIconAltText() %>" Visible="<%# HeaderIconVisible(3) %>">
							</asp:Image>
						</asp:panel>
					</td>
					<td class="fscheader4<%# GetHeaderRowCssClass() %> <%# CellCss(OperatorVisible)%>">
						<asp:panel id="panelOperator" runat="server" visible='<%# OperatorVisible%>'>
							<asp:label runat="server" id="SortOperatorText" visible="<%# !ShowSortableLinks %>">
								<%# HeaderItem(4) %>
							</asp:label>
							<cc1:tdbutton id="buttonOperator" runat="server" visible="<%# ShowSortableLinks %>" tooltip="<%# HeaderAltText(4) %>"></cc1:tdbutton>
							<asp:Image id="Icon4" runat="server" ImageUrl="<%# HeaderIconUrl(4) %>" alternateText="<%# HeaderIconAltText() %>" Visible="<%# HeaderIconVisible(4) %>">
							</asp:Image>
						</asp:panel></td>
					<td class="fscheader5<%# GetHeaderRowCssClass() %>">
						<asp:label runat="server" id="SortDepartText" visible="<%# !ShowSortableLinks %>">
							<%# HeaderItem(5) %>
						</asp:label>
						<cc1:tdbutton id="buttonLeave" runat="server" visible="<%# ShowSortableLinks %>" tooltip="<%# HeaderAltText(5) %>"></cc1:tdbutton>
						<asp:Image id="Icon5" runat="server" ImageUrl="<%# HeaderIconUrl(5) %>" alternateText="<%# HeaderIconAltText() %>" Visible="<%# HeaderIconVisible(5) %>">
						</asp:Image></td>
					<td class="fscheader6<%# GetHeaderRowCssClass() %>">
						<asp:label runat="server" id="SortArriveText" visible="<%# !ShowSortableLinks %>">
							<%# HeaderItem(6) %>
						</asp:label>
						<cc1:tdbutton id="buttonArrive" runat="server" visible='<%# ShowSortableLinks %>' tooltip="<%# HeaderAltText(6) %>"></cc1:tdbutton>
						<asp:Image id="Icon6" runat="server" ImageUrl="<%# HeaderIconUrl(6) %>" alternateText="<%# HeaderIconAltText() %>" Visible="<%# HeaderIconVisible(6) %>">
						</asp:Image></td>
					<td class="fscheader7<%# GetHeaderRowCssClass() %>">
						<asp:label runat="server" id="SortDurationText" visible="<%# !ShowSortableLinks %>">
							<%# HeaderItem(7) %>
						</asp:label>
						<cc1:tdbutton id="buttonDuration" runat="server" visible="<%# ShowSortableLinks %>" tooltip="<%# HeaderAltText(7) %>"></cc1:tdbutton>
						<asp:Image id="Icon7" runat="server" ImageUrl="<%# HeaderIconUrl(7) %>" alternateText="<%# HeaderIconAltText() %>" Visible="<%# HeaderIconVisible(7) %>">
						</asp:Image></td>
					<td class="fscheader8<%# GetHeaderRowCssClass() %>"><%# HeaderItem(8) %></td>
				</tr>	
			</table>
		</div>
		<div class="fscbodybox" style="<%# GetAdditionalStyleAttribute() %>">
			<table class="fscbody" cellspacing="0" lang="en">
			<tr class="screenreader">
			    <td id="screenreaderheader10" runat="server"><asp:label runat="server" visible='<%# IndexVisible%>'></asp:label></td>
			    <td id="screenreaderheader9" runat="server"><asp:label runat="server" visible='<%# TransportVisible%>'>Transport</asp:label></td>
			    <td id="screenreaderheader1" runat="server"><asp:label runat="server" visible='<%# FromVisible%>'>From</asp:label></td>
			    <td id="screenreaderheader2" runat="server"><asp:label runat="server" visible='<%# ToVisible%>'>To</asp:label></td>
			    <td id="screenreaderheader3" runat="server"><asp:label runat="server" visible='<%# ChangesVisible%>'>Changes</asp:label></td>
			    <td id="screenreaderheader4" runat="server"><asp:label runat="server" visible='<%# OperatorVisible%>'>Operator</asp:label></td>
			    <td id="screenreaderheader5" runat="server"><asp:label runat="server">Leave</asp:label></td>
			    <td id="screenreaderheader6" runat="server"><asp:label runat="server">Arrive</asp:label></td>
			    <td id="screenreaderheader7" runat="server"><asp:label runat="server">Duration</asp:label></td>
			    <td id="screenreaderheader8" runat="server"><asp:label runat="server" visible='<%# RadioButtonsVisible %>'>Select</asp:label></td>
			</tr>
	</headertemplate>				
	<itemtemplate>
		<tr id="<%# GetItemRowId(Container.ItemIndex) %>">
			<td id="screenreaderchild10" runat="server">
				<asp:panel id="Panel4" runat="server" visible='<%# IndexVisible%>' >
					<%# ((FormattedJourneySummaryLine)Container.DataItem).DisplayNumber %>
				</asp:panel></td>
			<td id="screenreaderchild9" runat="server">
				<asp:panel id="Panel1" runat="server" visible='<%# TransportVisible%>'>
					<%# ((FormattedJourneySummaryLine)Container.DataItem).GetTransportModes() %>
				</asp:panel></td>
			<td id="screenreaderchild1" runat="server">
				<asp:panel id="Panel5" runat="server" visible='<%# FromVisible%>'>
					<%# ((FormattedJourneySummaryLine)Container.DataItem).GetOriginDescription() %>
				</asp:panel></td>
			<td id="screenreaderchild2" runat="server">
				<asp:panel id="Panel6" runat="server" visible='<%# ToVisible%>'>
					<%# ((FormattedJourneySummaryLine)Container.DataItem).GetDestinationDescription() %>
				</asp:panel></td>
			<td id="screenreaderchild3" runat="server">
				<asp:panel id="Panel2" runat="server" visible='<%# ChangesVisible%>'>
					<%# ((FormattedJourneySummaryLine)Container.DataItem).InterchangeCount %>
				</asp:panel></td>
			<td id="screenreaderchild4" runat="server">
				<asp:panel id="Panel3" runat="server" visible='<%# OperatorVisible%>'>
					<%# Server.HtmlEncode(((FormattedJourneySummaryLine)Container.DataItem).GetOperatorNames()) %>
				</asp:panel></td>
			<td id="screenreaderchild5" runat="server"><%# ((FormattedJourneySummaryLine)Container.DataItem).GetDepartureTime() %></td>
			<td id="screenreaderchild6" runat="server"><%# ((FormattedJourneySummaryLine)Container.DataItem).GetArrivalTime() %></td>
			<td id="screenreaderchild7" runat="server"><%# ((FormattedJourneySummaryLine)Container.DataItem).GetDuration() %></td>
			<td id="screenreaderchild8" runat="server">
				<asp:label runat="server" id="labelBlank" visible="<%# !RadioButtonsVisible %>">&nbsp;</asp:label>
				<asp:imagebutton id="ImageButton" Visible="<%# RadioButtonsVisible %>" CommandName="<%# ((FormattedJourneySummaryLine)Container.DataItem).JourneyIndex.ToString() %>" Runat="server" ImageUrl="<%# GetButtonImageUrl(Container.ItemIndex) %>" AlternateText="<%# AlternateText(Container.ItemIndex) %>">
				</asp:imagebutton>
			</td>
		</tr>
	</itemtemplate>
	<footertemplate>
		</table> </div>
	</footertemplate>
</asp:repeater>
<asp:repeater id="summaryRepeaterPrintable" runat="server" visible="False">
	<headertemplate>
		<table cellspacing="0" class="fscbody" summary="Journey Summary Details" lang="en">
			<tr bgcolor="#ffffff">
				<td class="fscheader10<%# GetHeaderRowCssClass() %>"></td>
				<td class="fscheader9<%# GetHeaderRowCssClass() %>">
					<asp:panel id="Panel7" runat="server" visible='<%# TransportVisible%>'>
						<%# HeaderItem(9)%>
						<asp:Image id="Image9" runat="server" ImageUrl="<%# HeaderIconUrl(9) %>" Visible="<%# HeaderIconVisible(9) %>">
						</asp:Image>
					</asp:panel>
				</td>
				<td scope="col" class="fscheader1<%# GetHeaderRowCssClass() %>">
					<asp:panel id="Panel8" runat="server" visible='<%# FromVisible%>'>
						<%# HeaderItem(1) %>
						<asp:Image id="Image1" runat="server" ImageUrl="<%# HeaderIconUrl(1) %>" Visible="<%# HeaderIconVisible(1) %>" AlternateText="Sort Icon">
						</asp:Image>
					</asp:panel>
				</td>
				<td class="fscheader2<%# GetHeaderRowCssClass() %>">
					<asp:panel id="Panel9" runat="server" visible='<%# ToVisible%>'>
						<%# HeaderItem(2) %>
						<asp:Image id="Image2" runat="server" ImageUrl="<%# HeaderIconUrl(2) %>" Visible="<%# HeaderIconVisible(2) %>" AlternateText="Sort Icon">
						</asp:Image>
					</asp:panel>
				</td>
				<td class="fscheader3<%# GetHeaderRowCssClass() %>">
					<asp:panel id="Panel10" runat="server" visible='<%# ChangesVisible%>'>
						<%# HeaderItem(3) %>
						<asp:Image id="Image3" runat="server" ImageUrl="<%# HeaderIconUrl(3) %>" Visible="<%# HeaderIconVisible(3) %>" AlternateText="Sort Icon">
						</asp:Image>
					</asp:panel>
				</td>
				<td class="fscheader4<%# GetHeaderRowCssClass() %>">
					<asp:panel id="Panel11" runat="server" visible='<%# OperatorVisible%>'>
						<%# HeaderItem(4) %>
						<asp:Image id="Image4" runat="server" ImageUrl="<%# HeaderIconUrl(4) %>" Visible="<%# HeaderIconVisible(4) %>" AlternateText="Sort Icon">
						</asp:Image>
					</asp:panel>
				</td>
				<td class="fscheader5<%# GetHeaderRowCssClass() %>"><%# HeaderItem(5) %>
					<asp:Image id="Image5" runat="server" ImageUrl="<%# HeaderIconUrl(5) %>" Visible="<%# HeaderIconVisible(5) %>" AlternateText="Sort Icon">
					</asp:Image></td>
				<td class="fscheader6<%# GetHeaderRowCssClass() %>"><%# HeaderItem(6) %>
					<asp:Image id="Image6" runat="server" ImageUrl="<%# HeaderIconUrl(6) %>" Visible="<%# HeaderIconVisible(6) %>" AlternateText="Sort Icon">
					</asp:Image></td>
				<td class="fscheader7<%# GetHeaderRowCssClass() %>"><%# HeaderItem(7) %>
					<asp:Image id="Image7" runat="server" ImageUrl="<%# HeaderIconUrl(7) %>" Visible="<%# HeaderIconVisible(7) %>" AlternateText="Sort Icon">
					</asp:Image></td>
			</tr>
	</headertemplate>
	<itemtemplate>
		<tr>
			<td class="fscbody10<%# GetBodyRowCssClass(Container.ItemIndex) %>">
				<asp:panel id="Panel12" runat="server" visible='<%# IndexVisible%>'>
					<%# ((FormattedJourneySummaryLine)Container.DataItem).DisplayNumber %>
				</asp:panel></td>
			<td class="fscbody9<%# GetBodyRowCssClass(Container.ItemIndex) %>">
				<asp:panel id="Panel13" runat="server" visible='<%# TransportVisible%>'>
					<%# ((FormattedJourneySummaryLine)Container.DataItem).GetTransportModes() %>
				</asp:panel></td>
			<td class="fscbody1<%# GetBodyRowCssClass(Container.ItemIndex) %>">
				<asp:panel id="Panel14" runat="server" visible='<%# FromVisible%>'>
					<%# ((FormattedJourneySummaryLine)Container.DataItem).GetOriginDescription() %>
				</asp:panel></td>
			<td class="fscbody2<%# GetBodyRowCssClass(Container.ItemIndex) %>">
				<asp:panel id="Panel15" runat="server" visible='<%# ToVisible%>'>
					<%# ((FormattedJourneySummaryLine)Container.DataItem).GetDestinationDescription() %>
				</asp:panel></td>
			<td class="fscbody3<%# GetBodyRowCssClass(Container.ItemIndex) %>">
				<asp:panel id="Panel16" runat="server" visible='<%# ChangesVisible%>'>
					<%# ((FormattedJourneySummaryLine)Container.DataItem).InterchangeCount %>
				</asp:panel></td>
			<td class="fscbody4<%# GetBodyRowCssClass(Container.ItemIndex) %>">
				<asp:panel id="Panel17" runat="server" visible='<%# OperatorVisible%>'>
					<%# Server.HtmlEncode(((FormattedJourneySummaryLine)Container.DataItem).GetOperatorNames()) %>
				</asp:panel></td>
			<td class="fscbody5<%# GetBodyRowCssClass(Container.ItemIndex) %>"><%# ((FormattedJourneySummaryLine)Container.DataItem).GetDepartureTime() %></td>
			<td class="fscbody6<%# GetBodyRowCssClass(Container.ItemIndex) %>"><%# ((FormattedJourneySummaryLine)Container.DataItem).GetArrivalTime() %></td>
			<td class="fscbody7<%# GetBodyRowCssClass(Container.ItemIndex) %>"><%# ((FormattedJourneySummaryLine)Container.DataItem).GetDuration() %></td>
		</tr>
	</itemtemplate>
	<footertemplate>
		</table>
	</footertemplate>
</asp:repeater><asp:panel id="fscPanelShow" runat="server" cssclass="fscpanelshow">
	<table lang="en" cellspacing="0" cellpadding="5" width="100%">
		<tr>
			<td style="TEXT-ALIGN: right" valign="middle" align="right">
				<asp:label id="labelShow" associatedcontrolid="dropShow" runat="server" cssclass="txtsevenb"></asp:label>
				<asp:dropdownlist id="dropShow" runat="server"></asp:dropdownlist>
				<cc1:tdbutton id="commandShow" runat="server"></cc1:tdbutton>
				<cc1:TDButton ID="buttonToggleShowTenShowAll" runat="server" CausesValidation="False"></cc1:TDButton>
			</td>
		</tr>
	</table>
</asp:panel>
