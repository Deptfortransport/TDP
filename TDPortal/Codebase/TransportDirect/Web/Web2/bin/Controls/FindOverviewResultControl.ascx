<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindOverviewResultControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindOverviewResultControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Import namespace="TransportDirect.UserPortal.JourneyControl" %>
<asp:repeater id="overviewRepeater" runat="server" OnItemDataBound="overviewRepeater_DataBound">
	<headertemplate>
		<div class="focheaderbox">
			<table cellspacing="0" class="focheader" summary="Journey Overview Header" lang="en">
				<tr>
				    <td class="focheader1<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="NumberofJourneysText">
								<%# HeaderItem(1) %>
					    </asp:label>
					</td>
					<td class="focheader2<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="Label1">
								<%# HeaderItem(2) %>
					    </asp:label>
					</td>
					<td class="focheader2<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="Label2">
								<%# HeaderItem(3) %>
					    </asp:label>
					</td>
					<td class="focheader1<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="Label3">
								<%# HeaderItem(4) %>
					    </asp:label>
					</td>
					<td class="focheader3<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="Label4">
								<%# HeaderItem(5) %>
					    </asp:label>
					</td>
					<td class="focheader4<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="Label5">
								<%# HeaderItem(6) %>
					    </asp:label>
					</td>
					<td class="focheader5<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="Label6">
								<%# GetSelectTitle() %>
					    </asp:label>
					</td>
				</tr>	
	</headertemplate>				
	<itemtemplate>
        <tr class="<%# GetItemRowId(Container.ItemIndex) %>">
			<td class="focbody1<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <img title="<%# GetModeImageTitle((JourneyOverviewLine)Container.DataItem) %>"  
                alt="<%# GetModeImageAltText((JourneyOverviewLine)Container.DataItem) %>" 
                src="<%# GetModeImage((JourneyOverviewLine)Container.DataItem) %>" />
			</td>
			<td class="focbody2<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <%# GetNumberOfJourneys((JourneyOverviewLine)Container.DataItem) %>
			</td>
			<td class="focbody2<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <%# GetFastestDuration((JourneyOverviewLine)Container.DataItem) %>
			</td>
			<td class="focbody1<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <%# GetEmissions((JourneyOverviewLine)Container.DataItem) %>
			</td>
			<td class="focbody3<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <%# GetEarliestDeparture((JourneyOverviewLine)Container.DataItem) %>
			</td>
			<td class="focbody4<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <%# GetLatestDeparture((JourneyOverviewLine)Container.DataItem) %>
			</td>
			<td class="focbody5<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <cc1:tdbutton id="buttonMore" runat="server"></cc1:tdbutton>
            </td>
		</tr>
	</itemtemplate>
	<footertemplate>
		</table> </div>
	</footertemplate>
</asp:repeater>
<asp:repeater id="overviewRepeaterPrintable" runat="server">
	<headertemplate>
		<div class="focheaderbox">
			<table cellspacing="0" class="focheader" summary="Journey Overview Header" lang="en">
				<tr>
				    <td class="focheader1<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="NumberofJourneysText">
								<%# HeaderItem(1) %>
					    </asp:label>
					</td>
					<td class="focheader2<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="Label1">
								<%# HeaderItem(2) %>
					    </asp:label>
					</td>
					<td class="focheader2<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="Label2">
								<%# HeaderItem(3) %>
					    </asp:label>
					</td>
					<td class="focheader1<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="Label3">
								<%# HeaderItem(4) %>
					    </asp:label>
					</td>
					<td class="focheader3<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="Label4">
								<%# HeaderItem(5) %>
					    </asp:label>
					</td>
					<td class="focheader4<%# GetHeaderRowCssClass() %>">
                        <asp:label runat="server" id="Label5">
								<%# HeaderItem(6) %>
					    </asp:label>
					</td>
				</tr>	
	</headertemplate>				
	<itemtemplate>
        <tr class="<%# GetItemRowId(Container.ItemIndex) %>">
			<td class="focbody1<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <img title="<%# GetModeImageTitle((JourneyOverviewLine)Container.DataItem) %>"  
                alt="<%# GetModeImageAltText((JourneyOverviewLine)Container.DataItem) %>" 
                src="<%# GetModeImage((JourneyOverviewLine)Container.DataItem) %>" />
			</td>
			<td class="focbody2<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <%# GetNumberOfJourneys((JourneyOverviewLine)Container.DataItem) %>
			</td>
			<td class="focbody2<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <%# GetFastestDuration((JourneyOverviewLine)Container.DataItem) %>
			</td>
			<td class="focbody1<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <%# GetEmissions((JourneyOverviewLine)Container.DataItem) %>
			</td>
			<td class="focbody3<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <%# GetEarliestDeparture((JourneyOverviewLine)Container.DataItem) %>
			</td>
			<td class="focbody4<%# GetBodyRowCssClass(Container.ItemIndex) %>">
                <%# GetLatestDeparture((JourneyOverviewLine)Container.DataItem) %>
			</td>
		</tr>
	</itemtemplate>
	<footertemplate>
		</table> </div>
	</footertemplate>
</asp:repeater>