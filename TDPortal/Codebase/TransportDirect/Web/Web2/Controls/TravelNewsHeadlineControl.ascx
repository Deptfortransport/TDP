<%@ Import namespace="TransportDirect.UserPortal.TravelNews" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TravelNewsHeadlineControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TravelNewsHeadlineControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:panel id="panelNoHeadlines" runat="server" enableviewstate="false">
	<asp:label id="labelNoHeadlines" runat="server" cssclass="txtnineb" enableviewstate="false"></asp:label>
</asp:panel>
<div id="incidentDetails" class="incidentDetails" style="display:none;" runat="server">
</div>
<div id="panelHeadlines" runat="server">
    <asp:label id="labelHeadlineTextScreenReader" runat="server" cssclass="screenreader"></asp:label>
	<asp:repeater id="rptTravelNews" runat="server" enableviewstate="false">
		<itemtemplate>
			<div class="txtninen">
				<%# GetHeadlineText(Container.DataItem) %>
			</div>
		</itemtemplate>
		<AlternatingItemTemplate>
			<div class="txtninenalt">
				<%# GetHeadlineText(Container.DataItem) %>
			</div>
		</AlternatingItemTemplate>
	</asp:repeater>
</div>
