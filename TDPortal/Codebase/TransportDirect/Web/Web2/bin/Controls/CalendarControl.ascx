<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CalendarControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CalendarControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="calouterposition" align="center">
<div id="calouter">
	<asp:table id="calendarTable" runat="server">
		<asp:tablerow>
			<asp:tablecell scope="none" columnspan="6" horizontalalign="Center">
				<asp:label runat="server" cssclass="CalLabel" id="calendarTitle"></asp:label>
			</asp:tablecell>
			<asp:tablecell columnspan="1">
				<asp:label id="cancelText" runat="server" cssclass="screenreader"></asp:label>
				<asp:button runat="server" id="cancel" cssclass="CalButton" text="X"></asp:button>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow>
			<asp:tablecell columnspan="1">
				<asp:label id="backText" runat="server" cssclass="screenreader"></asp:label>
				<asp:button runat="server" id="backButton" cssclass="CalButton" text="&lt;"></asp:button>
			</asp:tablecell>
			<asp:tablecell columnspan="5">
				<asp:label runat="server" cssclass="CalLabel" id="monthLabel"></asp:label>
				<asp:label runat="server" cssclass="CalLabel" id="space">&nbsp</asp:label>
				<asp:label runat="server" cssclass="CalLabel" id="yearLabel"></asp:label>
			</asp:tablecell>
			<asp:tablecell columnspan="1">
				<asp:label id="fwdText" runat="server" cssclass="screenreader"></asp:label>
				<asp:button runat="server" id="fwdButton" cssclass="CalButton" text="&gt;"></asp:button>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow>
			<asp:tablecell id="hdrMon" abbr="<%# getDayofWeek(1)%>">
				<asp:label id="calendarMonday" cssclass="CalLabel" runat="server"></asp:label>
			</asp:tablecell>
			<asp:tablecell id="hdrTue" abbr="<%# getDayofWeek(2)%>">
				<asp:label id="calendarTuesday" cssclass="CalLabel" runat="server"></asp:label>
			</asp:tablecell>
			<asp:tablecell id="hdrWed" abbr="<%# getDayofWeek(3)%>">
				<asp:label id="calendarWednesday" cssclass="CalLabel" runat="server"></asp:label>
			</asp:tablecell>
			<asp:tablecell id="hdrThu" abbr="<%# getDayofWeek(4)%>">
				<asp:label id="calendarThursday" cssclass="CalLabel" runat="server"></asp:label>
			</asp:tablecell>
			<asp:tablecell id="hdrFri" abbr="<%# getDayofWeek(5)%>">
				<asp:label id="calendarFriday" cssclass="CalLabel" runat="server"></asp:label>
			</asp:tablecell>
			<asp:tablecell id="hdrSat" abbr="<%# getDayofWeek(6)%>">
				<asp:label id="calendarSaturday" cssclass="CalLabel" runat="server"></asp:label>
			</asp:tablecell>
			<asp:tablecell id="hdrSun" abbr="<%# getDayofWeek(7)%>">
				<asp:label id="calendarSunday" cssclass="CalLabel" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow>
			<asp:tablecell headers="calendar_hdrMon">
				<asp:button runat="server" id="day1" cssclass="CalButton"></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrTue">
				<asp:button runat="server" id="day2" cssclass="CalButton"></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrWed">
				<asp:button runat="server" id="day3" cssclass="CalButton"></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrThu">
				<asp:button runat="server" id="day4" cssclass="CalButton"></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrFri">
				<asp:button runat="server" id="day5" cssclass="CalButton"></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrSat">
				<asp:button runat="server" id="day6" cssclass="CalButton"></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrSun">
				<asp:button runat="server" id="day7" cssclass="CalButton"></asp:button>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow>
			<asp:tablecell headers="calendar_hdrMon">
				<asp:button runat="server" id="day8" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrTue">
				<asp:button runat="server" id="day9" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrWed">
				<asp:button runat="server" id="day10" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrThu">
				<asp:button runat="server" id="day11" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrFri">
				<asp:button runat="server" id="day12" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrSat">
				<asp:button runat="server" id="day13" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrSun">
				<asp:button runat="server" id="day14" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow>
			<asp:tablecell headers="calendar_hdrMon">
				<asp:button runat="server" id="day15" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrTue">
				<asp:button runat="server" id="day16" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrWed">
				<asp:button runat="server" id="day17" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrThu">
				<asp:button runat="server" id="day18" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrFri">
				<asp:button runat="server" id="day19" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrSat">
				<asp:button runat="server" id="day20" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrSun">
				<asp:button runat="server" id="day21" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow>
			<asp:tablecell headers="calendar_hdrMon">
				<asp:button runat="server" id="day22" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrTue">
				<asp:button runat="server" id="day23" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrWed">
				<asp:button runat="server" id="day24" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrThu">
				<asp:button runat="server" id="day25" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrFri">
				<asp:button runat="server" id="day26" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrSat">
				<asp:button runat="server" id="day27" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrSun">
				<asp:button runat="server" id="day28" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow>
			<asp:tablecell headers="calendar_hdrMon">
				<asp:button runat="server" id="day29" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrTue">
				<asp:button runat="server" id="day30" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrWed">
				<asp:button runat="server" id="day31" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrThu">
				<asp:button runat="server" id="day32" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrFri">
				<asp:button runat="server" id="day33" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrSat">
				<asp:button runat="server" id="day34" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrSun">
				<asp:button runat="server" id="day35" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow>
			<asp:tablecell headers="calendar_hdrMon">
				<asp:button runat="server" id="day36" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell headers="calendar_hdrTue">
				<asp:button runat="server" id="day37" cssclass="CalButton" text=""></asp:button>
			</asp:tablecell>
			<asp:tablecell></asp:tablecell>
			<asp:tablecell></asp:tablecell>
			<asp:tablecell></asp:tablecell>
			<asp:tablecell></asp:tablecell>
			<asp:tablecell></asp:tablecell>
		</asp:tablerow>
		<asp:tablerow id="trBankHoliday0">
			<asp:tablecell columnspan="7">
				<asp:label id="lblBankHoliday0" cssclass="txtsevenb" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow id="trBankHoliday1">
			<asp:tablecell columnspan="7">
				<asp:button runat="server" id="btnBankHoliday1" cssclass="CalButton1" text=" "></asp:button>&nbsp;
				<asp:label id="lblBankHoliday1" cssclass="txtseven" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow id="trBankHoliday2">
			<asp:tablecell columnspan="7">
				<asp:button runat="server" id="btnBankHoliday2" cssclass="CalButton2" text=" "></asp:button>&nbsp;
				<asp:label id="lblBankHoliday2" cssclass="txtseven" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow id="trBankHoliday3">
			<asp:tablecell columnspan="7">
				<asp:button runat="server" id="btnBankHoliday3" cssclass="CalButton3" text=" "></asp:button>&nbsp;
				<asp:label id="lblBankHoliday3" cssclass="txtseven" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
	</asp:table>
	<asp:label id="date" runat="server" visible="False"></asp:label>
</div>
</div>
