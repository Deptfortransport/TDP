<%@ Control Language="c#" AutoEventWireup="True" Codebehind="D2DPTPreferencesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.D2DPTPreferencesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="D2DPTWalkingSpeedOptionsControl" Src="D2DPTWalkingSpeedOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="D2DPTJourneyChangesOptionsControl" Src="D2DPTJourneyChangesOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="FindPageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="D2DLocationControlVia" Src="D2DLocationControlVia.ascx" %>

<asp:panel id="ptPreferencesPanel" visible="False" runat="server">
	<uc1:D2DPTJourneyChangesOptionsControl id="journeyChangesOptionsControl" runat="server"></uc1:D2DPTJourneyChangesOptionsControl>
	<uc1:D2DPTWalkingSpeedOptionsControl id="walkingSpeedOptionsControl" runat="server"></uc1:D2DPTWalkingSpeedOptionsControl>
	<div class="locationControlViaPT">
	    <uc1:D2DLocationControlVia id="locationControlVia" runat="server" ></uc1:D2DLocationControlVia>
	</div>
</asp:panel>
