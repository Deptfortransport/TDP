<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FeedbackJourneyInputControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FeedbackJourneyInputControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FindLeaveReturnDatesControl" Src="../Controls/FindLeaveReturnDatesControl.ascx" %>
<div id="boxtypeten" style="WIDTH: 560px">
<table cellspacing="5" cellpadding="0" summary="FeedbackProblemTable" width="100%">
	<tr>
		<td>
			<table cellspacing="0" width="100%">
				<tr>
					<td align="right" width="60px">
						<asp:label id="searchTypeLabel" associatedcontrolid="listSearchType" runat="server" enableviewstate="False" cssclass="txtsevenb"></asp:label>
					</td>
					<td>
						&nbsp;<asp:dropdownlist id="listSearchType" runat="server"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<br/>
					</td>
				</tr>
				<tr>
					<td align="right" valign="top">
						<asp:label id="fromLabel" associatedcontrolid="fromTextBox" runat="server" enableviewstate="False" cssclass="txtsevenb"></asp:label>
					</td>
					<td valign="bottom">
						&nbsp;<asp:textbox id="fromTextBox" runat="server" width="225px" enableViewState="False"></asp:textbox>
					</td>					
				</tr>
				<tr>
					<td>
					</td>
					<td>
					    <asp:radiobuttonlist id="listLocationTypeFrom" runat="server" cssclass="txtseven" repeatdirection="Horizontal"
						    repeatcolumns="3"></asp:radiobuttonlist>
					</td>
				</tr>
				<tr>
					<td align="right" valign="top">
						<asp:label id="toLabel" associatedcontrolid="toTextbox" runat="server" enableviewstate="False" cssclass="txtsevenb"></asp:label>
					</td>
					<td valign="bottom">
						&nbsp;<asp:textbox id="toTextbox" runat="server" width="225px" enableViewState="False"></asp:textbox>
					</td>
				</tr>
				<tr>
					<td>
					</td>
					<td>
					    <asp:radiobuttonlist id="listLocationTypeTo" runat="server" cssclass="txtseven" repeatdirection="Horizontal"
						    repeatcolumns="3"></asp:radiobuttonlist>
					</td>
				</tr>
			</table>			
		</td>
	</tr>
	<tr>
		<td>
			<uc1:findleavereturndatescontrol id="dateControl" runat="server"></uc1:findleavereturndatescontrol>
		</td>
	</tr>
	<tr>
		<td>
			<table cellspacing="0" width="100%">
				<tr>
					<td align="right" width="75px">
						<asp:label id="labelModesOfTransport" runat="server" enableviewstate="false" cssclass="txtsevenb"></asp:label>
					</td>
					<td align="left" valign="bottom">
						<asp:checkbox id="checkBoxPublicTransport" runat="server" checked="true" cssclass="txtseven"></asp:checkbox>
						&nbsp;&nbsp;
						<asp:checkbox id="checkBoxCarRoute" runat="server" checked="true" cssclass="txtseven"></asp:checkbox>
						&nbsp;&nbsp;
						<asp:checkbox id="checkBoxCycle" runat="server" checked="false" cssclass="txtseven"></asp:checkbox>
					</td>
				</tr>
				<tr>
					<td>
					</td>
					<td align="left">
						<asp:checkboxlist CellSpacing=0 id="checklistModesPublicTransport"  runat="server" cssclass="txtseven" repeatcolumns="3"></asp:checkboxlist>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
</div>