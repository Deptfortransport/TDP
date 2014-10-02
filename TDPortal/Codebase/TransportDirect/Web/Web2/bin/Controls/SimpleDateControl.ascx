<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SimpleDateControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.SimpleDateControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:panel id="datePanel" runat="server" style="PADDING-RIGHT: 1px; PADDING-LEFT: 1px; PADDING-BOTTOM: 3px; PADDING-TOP: 1px; TEXT-ALIGN: right">
	<asp:label id="labelDate" runat="server" cssclass="txtsevenb" enableviewstate="False"></asp:label>
	<asp:label id="labelSRDate" associatedcontrolid="listDays" runat="server" cssclass="screenreader" enableviewstate="False"></asp:label>
	<asp:dropdownlist id="listDays" runat="server">
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
	</asp:dropdownlist>
	<asp:label id="labelRODay" runat="server" enableviewstate="False"></asp:label>
	<asp:label id="labelSRMonthYear" associatedcontrolid="listMonths" runat="server" cssclass="screenreader" enableviewstate="False"></asp:label>
	<asp:dropdownlist id="listMonths" runat="server"></asp:dropdownlist>
	<asp:label id="labelROMonth" runat="server" enableviewstate="False"></asp:label>
</asp:panel>
