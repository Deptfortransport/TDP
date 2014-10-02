<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindFareSingleTravelDatesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindFareSingleTravelDatesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<div class="fftdtabletitlebox"><uc1:resultstabletitlecontrol id="resultsTableTitleControlOutward" runat="server"></uc1:resultstabletitlecontrol></div>
<div class="fftdheaderbox">
	<table class="fftdtable" cellspacing="0" cellpadding="0" summary="">
		
			<tr class="fftdhdrrow">
				<th class="fftdhdrsingleoutdate" id="fftdhdrsingleoutdate">
					<asp:label id="HeaderTextSingleDate" runat="server"></asp:label><cc1:tdbutton id="tdButtonSingleDate" runat="server" visible="false"></cc1:tdbutton><asp:image id="IconSingleDate" runat="server"></asp:image></th>
				<th class="fftdhdrsinglemode" id="fftdhdrsinglemode">
					<asp:label id="HeaderTextTransportMode" runat="server"></asp:label><cc1:tdbutton id="tdButtonTransport" runat="server" visible="false"></cc1:tdbutton><asp:image id="IconTransportMode" runat="server"></asp:image></th>
				<th class="fftdhdrsinglefarerange" id="fftdhdrsinglefarerange">
					<asp:label id="HeaderTextFareRange" runat="server"></asp:label></th>
				<th class="fftdhdrsinglelowfare" id="fftdhdrsinglelowfare">
					<asp:label id="HeaderTextLowestFare" runat="server"></asp:label><asp:LinkButton id="tdLinkButtonLowestFare" runat="server" CssClass="TDHyperLinkStyleButton" Visible="False"></asp:LinkButton><asp:image id="IconLowestFare" runat="server"></asp:image></th>
				<th class="fftdhdrsingleselect" id="fftdhdrsingleselect" runat="server">
					<asp:label id="HeaderTextSelect" runat="server"></asp:label></th></tr>
		
	</table>
</div>
<asp:repeater id="travelDateRepeaterSingle" enableviewstate="False" runat="server">
	<headertemplate>
		<div class="fftdbodybox" style="<%# GetScrollStyle() %>">
			<table id="<%# GetTableId()%>" cellspacing="0" cellpadding="0" summary="<%# GetTableSummary()%>" lang="en" class="fftdtable">
				
				<tr class="screenreader">
				<th id="header0" class="fftdbdysingleoutdateday">
					<asp:Label id="labelSROutwardDate" runat="server"></asp:Label><asp:Label id="labelSRReturnDate" runat="server" Visible=False></asp:Label>
				</th>
				<th id="header1" class="fftdbdysinglemode">
					<asp:Label id="labelSRTransport" runat="server"></asp:Label></th>
				<th id="header2" class="fftdbdysinglefarerange">
					<asp:Label id="labelSRFareRange" runat="server"></asp:Label></th>
				<th id="header3" class="fftdbdysinglelowestfare">
					<asp:Label id="labelSRAvailable" runat="server"></asp:Label></th>
				<th id="header4" class="<%# HeadingSingleScrollClass() %>">
					<asp:Label id="labelSRSelect" runat="server"></asp:Label></th></tr>
				
				
	</headertemplate>
	<itemtemplate>
	</itemtemplate>
	<footertemplate>
		</table> </div>
	</footertemplate>
</asp:repeater>
