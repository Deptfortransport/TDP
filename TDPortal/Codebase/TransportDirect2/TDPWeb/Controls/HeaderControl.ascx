<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderControl.ascx.cs" Inherits="TDP.UserPortal.TDPWeb.Controls.HeaderControl" %>
<%@ Register TagPrefix="uc1" TagName="SkipToLinkControl" Src="SkipToLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LanguageLinkControl" Src="LanguageLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StyleLinkControl" Src="StyleLinkControl.ascx" %>

<a id="top"></a>
<asp:Panel ID="pnlHeaderMastheadContainer" runat="server" EnableViewState="false">
    <div class="header">
	    <div class="logoWrap">
			<div class="logo">
				<a href="~/Pages/JourneyPlannerInput.aspx" title="Transport Direct" rel="external" accesskey="1" tabindex="1"></a>
			</div>
		</div>
		<div class="topMenuBar">
            <h2 id="accessibiliyMenuSR" runat="server" class="screenReaderOnly" enableviewstate="false"></h2>
            <ul>
                <li id="liSkipToContent" runat="server" class="menuItem screenReaderOnly" enableviewstate="false">
                    <uc1:SkipToLinkControl id="skipToLinkControl" runat="server"></uc1:SkipToLinkControl> |
                </li>
                <li id="liCookies" runat="server" class="menuItem" enableviewstate="false">
                    <asp:HyperLink ID="lnkCookies" runat="server" EnableViewState="false"></asp:HyperLink> |
                </li>
				<li id="liAccessibility" runat="server" class="menuItem" enableviewstate="false">
                    <asp:HyperLink ID="lnkAccessibility" runat="server" EnableViewState="false"></asp:HyperLink> |
                </li>
				<li id="liStyleLinks" class="liStyleLinks">
                    <asp:Label ID="lblStyleLinks" runat="server" EnableViewState="false" CssClass="screenReaderOnly"></asp:Label>
                    <ul id="liStyleLinksFontSize">
                        <li class="menuItem fontNormal">[<asp:HyperLink ID="fontNormalLnk" runat="server" CssClass="fontNormalLink"></asp:HyperLink>]</li>
						<li class="menuItem fontMedium">[<asp:HyperLink ID="fontLargerLnk" runat="server" CssClass="fontLargerLink"></asp:HyperLink>]</li>
						<li class="menuItem fontLarge">[<asp:HyperLink ID="fontLargestLnk" runat="server" CssClass="fontLargestLink"></asp:HyperLink>]</li>
					</ul>
					<ul id="liStyleLinksStyleSwitch">                                    
                        <li class="menuItem styleNormal"><asp:HyperLink ID="styleNormalLink" runat="server" CssClass="styleSwitch"></asp:HyperLink></li>
						<li class="menuItem styleDyslexia"><asp:HyperLink ID="styleDyslexiaLink" runat="server" CssClass="styleSwitch"></asp:HyperLink></li>
						<li class="menuItem styleVisibility"><asp:HyperLink ID="styleVisibilityLink" runat="server" CssClass="styleSwitch"></asp:HyperLink></li>
					</ul>
				</li>
                <li id="liLanguage" runat="server" class="menuItem" enableviewstate="false">
				    | <uc1:LanguageLinkControl id="languageLinkControl" runat="server"></uc1:LanguageLinkControl>
                </li>
			</ul>
		</div>
	</div>
    <div class="clear"> </div>
</asp:Panel>

<% // Header panel for primary header content %>
<asp:Panel ID="pnlHeaderPrimaryContainer" runat="server" EnableViewState="false"></asp:Panel>

<% // Header panel for secondary header content %>
<asp:Panel ID="pnlHeaderSecondaryContainer" runat="server" EnableViewState="false"></asp:Panel>