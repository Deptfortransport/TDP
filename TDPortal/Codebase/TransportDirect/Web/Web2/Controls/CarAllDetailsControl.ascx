<%@ Register TagPrefix="uc1" TagName="CarJourneyDetailsTableControl" Src="CarJourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarSummaryControl" Src="CarSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarJourneyTypeControl" Src="CarJourneyTypeControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarJourneyOptionsControl" Src="CarJourneyOptionsControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CarAllDetailsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CarAllDetailsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div class="fmview">
    <div class="avoidClosedRoadMessage" id="avoidClosedRoadsMessageContainer" runat="server" visible="false">
        <div class="head">
            <cc1:TDImage id="imageErrorType"  runat="server"></cc1:TDImage>&nbsp;&nbsp;
			<h3><asp:label id="labelErrorDisplayType" runat="server" CssClass="errordisplaytype"></asp:label></h3>
        </div>
        <div class="body">
            <div class="floatleft">
                <asp:Label ID="lblAvoidClosedRoadsMessage" runat="server" />
            </div>
            <div class="floatright">
                <cc1:TDButton ID="replanAvoidClosedRoads" runat="server" />
            </div>
            <div class="clearboth"></div>
        </div>
    </div>
	<div class="boxtypetenfifteen">
		<uc1:CarSummaryControl id="carSummaryControl" runat="server"></uc1:CarSummaryControl>	
	</div>
	<asp:Panel id="panelJourneyTypeControl" Runat="server">
		<div class="boxtypetwentynine">
			<uc1:CarJourneyTypeControl id="carJourneyTypeControl" runat="server"></uc1:CarJourneyTypeControl>	
		</div>
	</asp:Panel>
	<uc1:CarJourneyDetailsTableControl id="carJourneyDetailsTableControl" runat="server"></uc1:CarJourneyDetailsTableControl>
	<asp:Panel id="panelJourneyOptionsControl" Runat="server">
		<div class="boxtypetwentynine">
			<uc1:CarJourneyOptionsControl id="carJourneyOptionsControl" runat="server"></uc1:CarJourneyOptionsControl>	
		</div>
	</asp:Panel>
</div>
