<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindCarParksResultsTableControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindCarParksResultsTableControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Import namespace="System.Data" %>
<asp:repeater id="repeaterResultTable" runat="server">
	<headertemplate>
		<div id="boxtypestationresults">
			<table id="carParkResultsTable" lang="en" summary="car parks found results table" cellpadding="0"
				cellspacing="0" width="100%" border="0">
				<tr id="underlineCarPark">
					<th id="fscheadercp1<%# OptionHeaderStyle%>" align="left" class="underlineCarPark">
						<asp:ImageButton ID="commandSortByOption" Runat="server" ImageUrl='<%# OptionSortSymbolUrl %>' AlternateText='<%# SortByOptionAlternateText %>' Visible='<%#SortVisible%>' OnClick="CommandSortByOption_Click">
						</asp:ImageButton>&nbsp;<%# OptionTitle %>
					</th>
					<th id="fscheadercp2<%# CarParkNameHeaderStyle%>" style="width: <%# GetNameHeaderWidth%>px;" align="left" class="underlineCarPark">
						<asp:ImageButton ID="commandSortByCarParkName" Runat="server" ImageUrl='<%# CarParkNameSortSymbolUrl %>' AlternateText='<%# SortByCarParkNameAlternateText %>' Visible='<%#SortVisible%>' OnClick="CommandSortByCarParkName_Click">
						</asp:ImageButton>&nbsp;<%# CarParkTitle %>
					</th>
					<% if (GetShowingNumberOfSpaces) { %>										
					<th id="fscheadercp3<%# NumberOfSpacesHeaderStyle%>" align="left" class="underlineCarPark">
						<asp:ImageButton ID="CommandSortByNumberOfSpaces" Runat="server" ImageUrl='<%# NumberOfSpacesSortSymbolUrl %>' AlternateText='<%# SortByNumberOfSpacesAlternateText %>' Visible='<%#SortVisible%>' OnClick="CommandSortByNumberOfSpaces_Click">
						</asp:ImageButton>&nbsp;<%# NumberOfSpacesTitle %>
					</th>
					<% } %>
					<% if (GetShowingIsSecure) { %>															
					<th id="fscheadercp4<%# PMSPAHeaderStyle%>" align="left" class="underlineCarPark">
						<asp:ImageButton ID="CommandSortBySecure" Runat="server" ImageUrl='<%# PMSPASortSymbolUrl %>' AlternateText='<%# SortByPMSPAAlternateText %>' Visible='<%#SortVisible%>' OnClick="CommandSortBySecure_Click">
						</asp:ImageButton>&nbsp;<%# PMSPATitle %>
					</th>
					<% } %>
					<% if (GetShowingDisabledSpaces) { %>																			
					<th id="fscheadercp5<%# HasDisabledSpacesHeaderStyle%>" align="left" class="underlineCarPark">
						<asp:ImageButton ID="CommandSortByDisabled" Runat="server" ImageUrl='<%# HasDisabledSpacesSymbolUrl %>' AlternateText='<%# SortByHasDisabledSpacesAlternateText %>' Visible='<%#SortVisible%>' OnClick="CommandSortByDisabled_Click">
						</asp:ImageButton>&nbsp;<%# HasDisabledSpacesTitle %>
					<% } %>					
					</th>
					<th id="fscheadercp6<%# DistanceHeaderStyle%>" align="left" class="underlineCarPark">
						<asp:ImageButton ID="commandSortByDistance" Runat="server" ImageUrl='<%# DistanceSortSymbolUrl %>' AlternateText='<%# SortByDistanceAlternateText %>' Visible='<%#SortVisible%>' OnClick="CommandSortByDistance_Click">
						</asp:ImageButton>&nbsp;<%# DistanceTitle %>
					</th>
					<th id="fscheadercp7" align="left" class="underlineCarPark" style="<%# IsSelectVisible %>">
						<div id="panelSelectHeader" style="<%# IsSelectVisible %>" ><asp:label Visible='<%#SortVisible%>' Runat="server"><%# SelectTitle %></asp:label> </div>
					</th>
				</tr>
				<tr>
					<td colspan="<%# GetColumnCount %>">
						<div id="carParkResultsItems" class="<%# NumberOfRowsToDisplayStyle%>">
							<table id="carParkResultsItemRowsTable" lang="en" summary="car parks found results table" cellpadding="0" cellspacing="0" width="<%# GetTableWidth %>" border="0">
	</headertemplate>
	<itemtemplate>
				<tr id="<%# GetItemRowId(Container.ItemIndex) %>">				
				    <% if (!IsRowSelectLinkVisible) { %>
					<td class="txtseven fscbodycp1<%# GetTextCssClass(Container.ItemIndex)%>" align="left">&nbsp;&nbsp;<%# GetRowIndex((DataRow) Container.DataItem) %></td>
					<% } %>
					<% if (IsRowSelectLinkVisible) { %>
					<td class="txtseven fscbodycp1<%# GetTextCssClass(Container.ItemIndex)%>" align="left">&nbsp;&nbsp;<a href=" " onclick="<%# GetShowOnMapScript((DataRow) Container.DataItem) %>"><%# GetRowIndex((DataRow) Container.DataItem) %></a></td>
					<% } %>
					<td class="txtseven fscbodycp2<%# GetTextCssClass(Container.ItemIndex)%>" style="padding:5px; width: <%# GetNameHeaderWidth%>px;" align="left" >
						<uc1:hyperlinkpostbackcontrol ID="carParkInfoLinkControl" Runat="server" CommandName='<%# GetCarParkRef((DataRow) Container.DataItem) %>' CommandArgument='<%# GetCarParkRef((DataRow) Container.DataItem) %>' Text='<%# GetCarParkName((DataRow) Container.DataItem) %>' ToolTipText='<%# GetCarParkName((DataRow) Container.DataItem) %>' >
						</uc1:hyperlinkpostbackcontrol>
					</td>
					<% if (GetShowingNumberOfSpaces) { %>
					<td  class="txtseven fscbodycp3<%# GetTextCssClass(Container.ItemIndex)%>" align="center"><%# GetNumberOfSpaces((DataRow) Container.DataItem)%>&nbsp;</td>
					<% } %>
					<% if (GetShowingIsSecure) { %>					
					<td class="txtseven fscbodycp4<%# GetTextCssClass(Container.ItemIndex)%>" align="center"><asp:image id="fscSecureImage" imageURL="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/tick.gif" AlternateText = " " Visible="<%# GetIsSecureVisible((DataRow) Container.DataItem)%>" runat="server"></asp:image><%# GetIsSecure((DataRow) Container.DataItem)%>&nbsp;</td>
					<% } %>
					<% if (GetShowingDisabledSpaces) { %>
					<td class="txtseven fscbodycp5<%# GetTextCssClass(Container.ItemIndex)%>" align="center"><asp:image id="fscDisabledImage" imageURL="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/tick.gif" AlternateText = " " Visible="<%# GetHasDisabledSpacesVisible((DataRow) Container.DataItem)%>" runat="server"></asp:image><%# GetHasDisabledSpaces((DataRow) Container.DataItem)%>&nbsp;</td>
					<% } %>
					<td class="txtseven fscbodycp6<%# GetTextCssClass(Container.ItemIndex)%>" align="left">&nbsp;&nbsp;<%# GetDistance((DataRow) Container.DataItem)%></td>					
					<td class="txtseven fscbodycp7<%# GetTextCssClass(Container.ItemIndex)%>" style="<%# IsSelectVisible %>">
						<div  style="<%# IsSelectVisible %>" >
							<asp:imagebutton id="radioButtonImage" runat="server" CommandName="<%# GetCarParkRef((DataRow) Container.DataItem) %>">
							</asp:imagebutton>
						</div>
					</td>
				</tr>
	</itemtemplate>
	<footertemplate>
							</table>
						</div>
					</td>
				</tr> 
			</table> </div> 
	</footertemplate>
</asp:repeater>
