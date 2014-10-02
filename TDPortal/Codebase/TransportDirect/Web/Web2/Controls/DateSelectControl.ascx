<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DateSelectControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.DateSelectControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:Table id="containerTable" Runat="server" CellSpacing="0" CellPadding="0" BorderWidth="0" lang="en" summary="date selection">
	<asp:TableRow>
		<asp:TableCell style="WIDTH: 80px" valign="top" align="left">
			<h4 style="DISPLAY: inline">
				<asp:label id="labelDateType" runat="server"></asp:label>&nbsp;
			</h4>
		</asp:TableCell>
		<asp:TableCell>
			<asp:label id="labelSRDate" runat="server" cssclass="screenreader"></asp:label><asp:dropdownlist id="listDays" runat="server">
				<asp:listitem value="01">01</asp:listitem>
				<asp:listitem value="02">02</asp:listitem>
				<asp:listitem value="03">03</asp:listitem>
				<asp:listitem value="04">04</asp:listitem>
				<asp:listitem value="05">05</asp:listitem>
				<asp:listitem value="06">06</asp:listitem>
				<asp:listitem value="07">07</asp:listitem>
				<asp:listitem value="08">08</asp:listitem>
				<asp:listitem value="09">09</asp:listitem>
				<asp:listitem value="10">10</asp:listitem>
				<asp:listitem value="11">11</asp:listitem>
				<asp:listitem value="12">12</asp:listitem>
				<asp:listitem value="13">13</asp:listitem>
				<asp:listitem value="14">14</asp:listitem>
				<asp:listitem value="15">15</asp:listitem>
				<asp:listitem value="16">16</asp:listitem>
				<asp:listitem value="17">17</asp:listitem>
				<asp:listitem value="18">18</asp:listitem>
				<asp:listitem value="19">19</asp:listitem>
				<asp:listitem value="20">20</asp:listitem>
				<asp:listitem value="21">21</asp:listitem>
				<asp:listitem value="22">22</asp:listitem>
				<asp:listitem value="23">23</asp:listitem>
				<asp:listitem value="24">24</asp:listitem>
				<asp:listitem value="25">25</asp:listitem>
				<asp:listitem value="26">26</asp:listitem>
				<asp:listitem value="27">27</asp:listitem>
				<asp:listitem value="28">28</asp:listitem>
				<asp:listitem value="29">29</asp:listitem>
				<asp:listitem value="30">30</asp:listitem>
				<asp:listitem value="31">31</asp:listitem>
			</asp:dropdownlist>&nbsp;
			<asp:label id="labelSRMonthYear" runat="server" cssclass="screenreader"></asp:label>
			<cc1:scriptabledropdownlist id="listMonths" runat="server" scriptname="DateSelectControl" action="return MonthSelectionChanged()"></cc1:scriptabledropdownlist> 		
			<div class="calimg">&nbsp;<cc1:TDImageButton id="commandCalendar" runat="server" imageurl=""></cc1:TDImageButton>
			</div>
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow ID="flexibilityTableRow" Runat="server" Visible="False">
		<asp:TableCell>&nbsp;</asp:TableCell>
		<asp:TableCell>
			<table cellpadding="0" cellspacing="0" style="PADDING-TOP: 5px">
				<tr>
					<td>
						<asp:label id="flexibilitySRLabel" runat="server" cssclass="screenreader" enableviewstate="False"></asp:label>
						<asp:dropdownlist id="flexibilityDropDown" runat="server" visible="true"></asp:dropdownlist>
					</td>
				</tr>
			</table>
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
		<asp:TableCell style="WIDTH: 80px" valign="top" align="left">
			<h4 style="DISPLAY: inline"></h4></asp:TableCell>
		<asp:TableCell>
			<asp:panel id="timeControlsPanel" runat="server" visible="True">
				<table cellspacing="0" cellpadding="0">
					<tr>
						<td><asp:radiobuttonlist id="radioTimeType" runat="server" cssclass="txtseven"></asp:radiobuttonlist></td>
						<td valign="middle">&nbsp;<asp:label id="labelSRHours" runat="server" cssclass="screenreader"></asp:label> 
						<cc1:scriptabledropdownlist id="listHours" runat="server" action="return HoursSelectionChanged()" scriptname="DateSelectControl">
								<asp:listitem value="00">00</asp:listitem>
								<asp:listitem value="01">01</asp:listitem>
								<asp:listitem value="02">02</asp:listitem>
								<asp:listitem value="03">03</asp:listitem>
								<asp:listitem value="04">04</asp:listitem>
								<asp:listitem value="05">05</asp:listitem>
								<asp:listitem value="06">06</asp:listitem>
								<asp:listitem value="07">07</asp:listitem>
								<asp:listitem value="08">08</asp:listitem>
								<asp:listitem value="09">09</asp:listitem>
								<asp:listitem value="10">10</asp:listitem>
								<asp:listitem value="11">11</asp:listitem>
								<asp:listitem value="12">12</asp:listitem>
								<asp:listitem value="13">13</asp:listitem>
								<asp:listitem value="14">14</asp:listitem>
								<asp:listitem value="15">15</asp:listitem>
								<asp:listitem value="16">16</asp:listitem>
								<asp:listitem value="17">17</asp:listitem>
								<asp:listitem value="18">18</asp:listitem>
								<asp:listitem value="19">19</asp:listitem>
								<asp:listitem value="20">20</asp:listitem>
								<asp:listitem value="21">21</asp:listitem>
								<asp:listitem value="22">22</asp:listitem>
								<asp:listitem value="23">23</asp:listitem>
							</cc1:scriptabledropdownlist><asp:label id="labelHrs" runat="server" cssclass="txtnote"></asp:label><asp:label id="labelSRPause" runat="server" cssclass="screenreader">.</asp:label><asp:label id="labelSRMinutes" runat="server" cssclass="screenreader"></asp:label><asp:dropdownlist id="listMinutes" runat="server">
								<asp:listitem value="00">00</asp:listitem>
								<asp:listitem value="05">05</asp:listitem>
								<asp:listitem value="10">10</asp:listitem>
								<asp:listitem value="15">15</asp:listitem>
								<asp:listitem value="20">20</asp:listitem>
								<asp:listitem value="25">25</asp:listitem>
								<asp:listitem value="30">30</asp:listitem>
								<asp:listitem value="35">35</asp:listitem>
								<asp:listitem value="40">40</asp:listitem>
								<asp:listitem value="45">45</asp:listitem>
								<asp:listitem value="50">50</asp:listitem>
								<asp:listitem value="55">55</asp:listitem>
							</asp:dropdownlist>
							<asp:label id="labelMinutes" runat="server" cssclass="txtnote"></asp:label>
						</td>
					</tr>
				</table>
			</asp:panel>
		</asp:TableCell>
	</asp:TableRow>
</asp:Table>