<%@ Register TagPrefix="uc1" TagName="PeopleTravellingControl" Src="PeopleTravellingControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TicketMatrixControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TicketMatrixControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="matrixborder">
	<div class="matrixheading">
		<h2><asp:label id="labelJourneyType" runat="server" enableviewstate="False"></asp:label></h2>&nbsp;
		<asp:label id="labelFor" cssclass="txtseven" runat="server" enableviewstate="False"></asp:label>
		<asp:label id="labelDate" cssclass="txtseven" runat="server" enableviewstate="False"></asp:label></div>
	<uc1:peopletravellingcontrol id="peopleTravellingControl" runat="server"></uc1:peopletravellingcontrol>
	<asp:repeater id="ticketsRepeater" runat="server" visible="true">
		<headertemplate>
			<table class="matrixtable" cellpadding="2px" cellspacing="0" summary="<%# MatrixTableSummary %>">
		</headertemplate>
		<itemtemplate>
			<tr>
				<td class="ticketcell" align="left">
					<table cellpadding="2" cellspacing="0" class="ticketcelltable">
						<tr>
							<td rowspan="2" align="left" valign="top">
								<asp:image id="imageTicket" runat="server" enableviewstate="False"></asp:image></td>
							<td align="left" valign="top">
								<asp:label id="labelMode" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label><span class="txtsevenb">:
								</span>
								<asp:label id="labelTicketType" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label></td>
						</tr>
						<tr>
							<td align="left" valign="top">
								<asp:label id="labelJourneyDescription" cssclass="txtseven" runat="server" enableviewstate="False"></asp:label></td>
						</tr>
						<tr>
							<td colspan="2" align="right">
								<asp:repeater id="faresRepeater" runat="server">
									<headertemplate>
										<table cellpadding="0" cellspacing="0">
									</headertemplate>
									<itemtemplate>
										<tr>
											<td align="right">&nbsp;
												<asp:label id="labelFareDescription" cssclass="txtseven" runat="server" enableviewstate="False">
													<%# ((string[])Container.DataItem)[0] %>
												</asp:label></td>
											<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:label id="labelFare" cssclass="txtseven" runat="server" enableviewstate="False">
													<%# ((string[])Container.DataItem)[1] %>
												</asp:label></td>
										</tr>
									</itemtemplate>
									<footertemplate>
										</table>
									</footertemplate>
								</asp:repeater>	
							</td>
						</tr>
					</table>
				</td>
				<asp:placeholder id="placeHolderColumns" runat="server"></asp:placeholder>
			</tr>
		</itemtemplate>
		<footertemplate>
			<tr>
				<td>&nbsp;</td>
				<asp:placeholder id="placeHolderColumns" runat="server"></asp:placeholder>
			</tr>
			</table>
		</footertemplate>
	</asp:repeater></div>
