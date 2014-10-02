<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindFareReturnTravelDatesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindFareReturnTravelDatesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<div class="fftdtabletitlebox">
	<uc1:resultstabletitlecontrol id="resultsTableTitleControlReturn" runat="server"></uc1:resultstabletitlecontrol>
</div>
<div class="fftdheaderbox">
	<table cellspacing="0" cellpadding="0" summary="" class="fftdtable">
		
			<tr class="fftdhdrrow">
				<th id="fftdhdrreturnoutdate" class="fftdhdrreturnoutdate">
					<asp:label id="HeaderTextOutwardDate" runat="server"></asp:label>
					<cc1:tdbutton id="tdButtonOutwardDate" runat="server" visible="false"></cc1:tdbutton>
					<asp:image id='IconOutwardDate' runat='server'></asp:image>
				</th>
				<th id="fftdhdrreturnretdate" class="fftdhdrreturnretdate">
					<asp:label id="HeaderTextReturnDate" runat="server"></asp:label>
					<cc1:tdbutton id="tdButtonReturnDate" runat="server" visible="false"></cc1:tdbutton>
					<asp:image id="IconReturnDate" runat='server'></asp:image>
				</th>
				<th id="fftdhdrreturnmode" class="fftdhdrreturnmode">
					<asp:label id="HeaderTextTransportMode" runat="server"></asp:label>
					<cc1:tdbutton id="tdButtonTransport" runat="server" visible="false"></cc1:tdbutton>
					<asp:image id="IconTransportMode" runat='server'></asp:image>
				</th>
				<th id="fftdhdrreturnfarerange" class="fftdhdrreturnfarerange">
					<asp:label id="HeaderTextFareRange" runat="server"></asp:label>
				</th>
				<th id="fftdhdrreturnlowfare" class="fftdhdrreturnlowfare">
					<asp:label id="HeaderTextLowestFare" runat="server"></asp:label>
					<asp:LinkButton id="tdLinkButtonLowestFare" runat="server" CssClass="TDHyperLinkStyleButton" Visible="false"></asp:LinkButton>
					<asp:image id="IconLowestFare" runat='server'></asp:image>
				</th>
				<th id="fftdhdrreturnselect" class="fftdhdrreturnselect" runat="server">
					<asp:label id="HeaderTextSelect" runat="server"></asp:label>
				</th>
			</tr>
		
	</table>
</div>
<asp:repeater id="travelDateRepeaterReturn" enableviewstate="False" runat="server">
	<headertemplate>
		<div class="fftdbodybox" style="<%# GetScrollStyle() %>">
			<table id="<%# GetTableId()%>" cellspacing="0" cellpadding="0" summary="<%# GetTableSummary()%>" lang="en" class="fftdtable">
				<tr class="screenreader">
				<th id="header0" class="fftdbdyreturnoutdateday">
					<asp:Label id="labelSROutwardDate" runat="server"></asp:Label>
				</th>
				<th id="header1" class="fftdbdyreturnretdateday">
					<asp:Label id="labelSRReturnDate" runat="server"></asp:Label></th>
				<th id="header2" class="fftdbdyreturnmode">
					<asp:Label id="labelSRTransport" runat="server"></asp:Label></th>
				<th id="header3" class="fftdbdyreturnfarerange">
					<asp:Label id="labelSRFareRange" runat="server"></asp:Label></th>
				<th id="header4" class="fftdbdyreturnlowestfare">
					<asp:Label id="labelSRAvailable" runat="server"></asp:Label></th>
				<th id="header5" class="<%# HeadingReturnScrollClass() %>">
					<asp:Label id="labelSRSelect" runat="server"></asp:Label></th></tr>
		
				
	</headertemplate>
	<itemtemplate>
	</itemtemplate>
	<footertemplate>
		 </table> </div>
	</footertemplate>
</asp:repeater>
