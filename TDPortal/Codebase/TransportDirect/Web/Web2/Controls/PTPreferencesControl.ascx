<%@ Register TagPrefix="uc1" TagName="FindViaLocationControl" Src="FindViaLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PTWalkingSpeedOptionsControl" Src="PTWalkingSpeedOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PTJourneyChangesOptionsControl" Src="PTJourneyChangesOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPreferencesOptionsControl" Src="FindPreferencesOptionsControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PTPreferencesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.PtPreferencesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="FindPageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="FindPreferencesOptionsControl" Src="FindPreferencesOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocationControlVia" Src="LocationControlVia.ascx" %>

<asp:panel id="ptPreferencesPanel" visible="False" runat="server">
		<uc1:FindPreferencesOptionsControl id="preferencesOptionsControl" runat="server"></uc1:FindPreferencesOptionsControl>
		<uc1:PTJourneyChangesOptionsControl id="journeyChangesOptionsControl" runat="server"></uc1:PTJourneyChangesOptionsControl>
		<uc1:PTWalkingSpeedOptionsControl id="walkingSpeedOptionsControl" runat="server"></uc1:PTWalkingSpeedOptionsControl>
		<uc1:FindViaLocationControl id="viaLocationControl" runat="server"></uc1:FindViaLocationControl>
		<div class="locationControlViaPT">
		    <uc1:LocationControlVia id="locationControlVia" runat="server" ></uc1:LocationControlVia>
		</div>
	</asp:panel>
