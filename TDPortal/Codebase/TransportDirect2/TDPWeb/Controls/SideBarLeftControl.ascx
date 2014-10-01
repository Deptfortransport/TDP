<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideBarLeftControl.ascx.cs" Inherits="TDP.UserPortal.TDPWeb.Controls.SideBarLeftControl" %>

<% // Side Navigation panel %>
<asp:Panel ID="pnlNav" runat="server" EnableViewState="false">

	<div class="menuLeft">
		<ul class="menuLeftNav ">
			<li><a href="~/Pages/JourneyPlannerInput.aspx" title="Plan your journey">Plan your journey</a></li>
            <li><a href="~/Pages/TravelNews.aspx" title="Travel news">Travel news</a></li>
		</ul>
	</div>

</asp:Panel>