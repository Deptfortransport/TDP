<%@ Import namespace="TransportDirect.UserPortal.Web.Controls" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneysSearchedForControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneysSearchedForControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<a name="MainContent"></a>
<div id="outerDiv" runat="server" >
	<div class="boxtype11">
		<div class="txtelevenb"><asp:literal id="literalTitle" runat="server"></asp:literal></div>
		<asp:table runat="server" cellspacing="0" id="tableOutward" cssclass="jsearchedfor">
			<asp:tablerow runat="server" id="rowOutwardLocation">
				<asp:tablecell runat="server" id="cellTitle" cssclass="jdstitlecell"></asp:tablecell>
				<asp:tablecell runat="server" id="cellDepartLocation" width="40%" cssclass="jdstoprow"></asp:tablecell>
				<asp:tablecell runat="server" id="cellTo" horizontalalign="center" width="20%" cssclass="jdstoprowb"></asp:tablecell>
				<asp:tablecell runat="server" id="cellArriveLocation" horizontalalign="left" width="40%" cssclass="jdstoprow"></asp:tablecell>
			</asp:tablerow>
			<asp:tablerow runat="server" id="rowOutwardTime">
				<asp:tablecell runat="server" id="cellBlank"></asp:tablecell>
				<asp:tablecell runat="server" id="cellDepartTime" columnspan="2" width="50%" horizontalalign="left" cssclass="jds"></asp:tablecell>
				<asp:tablecell runat="server" id="cellArriveTime" width="50%" horizontalalign="left" cssclass="jds"></asp:tablecell>
			</asp:tablerow>
			<asp:tablerow runat="server" id="rowReturnLocation">
				<asp:tablecell runat="server" id="cellReturnTitle" cssclass="jdstitlecell"></asp:tablecell>
				<asp:tablecell runat="server" id="cellReturnDepartLocation" width="40%" cssclass="jdstoprow"></asp:tablecell>
				<asp:tablecell runat="server" id="cellReturnTo" horizontalalign="center" width="20%" cssclass="jdstoprowb"></asp:tablecell>
				<asp:tablecell runat="server" id="cellReturnArriveLocation" horizontalalign="left" width="40%" cssclass="jdstoprow"></asp:tablecell>
			</asp:tablerow>
			<asp:tablerow runat="server" id="rowReturnTime">
				<asp:tablecell runat="server" id="cellReturnBlank" cssclass=""></asp:tablecell>
				<asp:tablecell runat="server" id="cellReturnDepartTime" columnspan="2" width="50%" horizontalalign="left" cssclass="jds"></asp:tablecell>
				<asp:tablecell runat="server" id="cellReturnArriveTime" width="50%" horizontalalign="left" cssclass="jds"></asp:tablecell>
			</asp:tablerow>
		</asp:table>
	</div>
</div>
