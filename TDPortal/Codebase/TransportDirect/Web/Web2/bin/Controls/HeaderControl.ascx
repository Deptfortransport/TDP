<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="PoweredByControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="HeaderControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.HeaderControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableviewstate="True"%>
<div id="headerarea">
	<a name="logoTop"></a>
	<div class="headerandnavigationarea">
	<div id="smalllogo">
		<cc1:TDButton id="defaultActionButton" runat="server" enableviewstate="False" CssClass="TDNavButtonDefaultAction" CssClassMouseOver="TDNavButtonDefaultActionMouseOver" />
		<cc1:TDButton id="headerHomepageLink" runat="server" enableviewstate="False" CssClass="TDNavButtonHomeLogo" CssClassMouseOver="TDNavButtonHomeLogoMouseOver" />
		<asp:HyperLink ID="headerLink" runat="server" EnableViewState="false" CssClass="TDNavButtonHomeLogoLink" Visible="false">
		    <span id="headerLinkImage" class="TDNavButtonHomeLogo"></span>
		</asp:HyperLink>
	</div>
	<div id="smallstrap">
		<cc1:tdimage id="TDSmallBannerImage" runat="server" width="840" height="47"></cc1:tdimage>
	</div>
	<asp:Panel ID="panelNavigation" runat="server">
	<div id="navarea">
        <asp:literal id="TabsStartLiteral" runat="server" enableviewstate="False"></asp:literal>
		    <table id="navigationTabs" runat="server" cellpadding="0" cellspacing="0">
		        <tr id="defaultTab" runat="server">
		            <td>
		                <div class="TDNavButtonSeperator"></div>
                            <cc1:TDButton id="homeImageButton" CssClass="TDNavButton" CssClassMouseOver="TDNavButtonMouseOver" runat="server" enableviewstate="False" />
		                <div class="TDNavButtonSeperator"></div>
		                    <cc1:TDButton id="planAJourneyImageButton" CssClass="TDNavButton" CssClassMouseOver="TDNavButtonMouseOver" runat="server" enableviewstate="False" />
		                <div class="TDNavButtonSeperator"></div>
		                    <cc1:TDButton id="findAImageButton" CssClass="TDNavButton" CssClassMouseOver="TDNavButtonMouseOver" runat="server" enableviewstate="False" />
		                <div class="TDNavButtonSeperator"></div>
		                    <cc1:TDButton id="travelInfoImageButton" CssClass="TDNavButton" CssClassMouseOver="TDNavButtonMouseOver" runat="server" enableviewstate="False" />
		                <div class="TDNavButtonSeperator"></div>
		                    <cc1:TDButton id="tipsAndToolsImageButton" CssClass="TDNavButton" CssClassMouseOver="TDNavButtonMouseOver" runat="server" enableviewstate="False"/>
                        <div class="TDNavButtonSeperator"></div>
		                    <cc1:TDButton id="loginAndRegisterImageButton" CssClass="TDNavButton" CssClassMouseOver="TDNavButtonMouseOver" runat="server" EnableViewState="false" />
		                <div class="TDNavButtonSeperator"></div>&nbsp;
                    </td>
                </tr>
                <tr id="ieTab" runat="server" visible="false">
                        <td>
                            <div class="TDNavButtonSeperator">
                            </div>
                            <font size="2">
                            <asp:LinkButton ID="homeImageLink" CssClass="TDNavButtonLink" runat="server" EnableViewState="False">
                                <span class="TDNavButtonLinkDiv">
                                    <asp:Label ID="homeImageLinkText" runat="server" EnableViewState="false"/>
                                </span>
                            </asp:LinkButton>
                            </font>
                            <div class="TDNavButtonSeperator">
                            </div>
                            <font size="2">
                            <asp:LinkButton ID="planAJourneyImageLink" CssClass="TDNavButtonLink" runat="server"
                                EnableViewState="False">
                                <span class="TDNavButtonLinkDiv">
                                    <asp:Label ID="planAJourneyImageLinkText" runat="server" EnableViewState="false"/></span>
                            </asp:LinkButton>
                            </font>
                            <div class="TDNavButtonSeperator">
                            </div>
                            <font size="2">
                            <asp:LinkButton ID="findAImageLink" CssClass="TDNavButtonLink" runat="server" EnableViewState="False">
                                <span class="TDNavButtonLinkDiv">
                                    <asp:Label ID="findAImageLinkText" runat="server" EnableViewState="false"/></span>
                            </asp:LinkButton>
                            </font>
                            <div class="TDNavButtonSeperator">
                            </div>
                            <font size="2">
                            <asp:LinkButton ID="travelInfoImageLink" CssClass="TDNavButtonLink" runat="server"
                                EnableViewState="False">
                                <span class="TDNavButtonLinkDiv">
                                    <asp:Label ID="travelInfoImageLinkText" runat="server" EnableViewState="false"/></span>
                            </asp:LinkButton>
                            </font>
                            <div class="TDNavButtonSeperator">
                            </div>
                            <font size="2">
                            <asp:LinkButton ID="tipsAndToolsImageLink" CssClass="TDNavButtonLink" runat="server"
                                EnableViewState="False">
                                <span class="TDNavButtonLinkDiv">
                                    <asp:Label ID="tipsAndToolsImageLinkText" runat="server" /></span>
                            </asp:LinkButton>
                            </font>
                            <div class="TDNavButtonSeperator">
                            </div>
                            <font size="2">
                            <asp:LinkButton ID="loginAndRegisterImageLink" CssClass="TDNavButtonLink" runat="server"
                                EnableViewState="false">
                                <span class="TDNavButtonLinkDiv">
                                    <asp:Label ID="loginAndRegisterImageLinkText" runat="server" EnableViewState="false"/></span>
                            </asp:LinkButton>
                            </font>
                            <div class="TDNavButtonSeperator">
                            </div>
                            &nbsp;
                        </td>
                    </tr>
            </table>
        <asp:literal id="TabsEndLiteral" runat="server" text="</div>"></asp:literal>
        <div class="divnavpoweredby floatrightonly">
            <uc1:PoweredBy ID="PoweredByControl" runat="server" />
        </div>
        <div class="clearboth"></div>
        <noscript>
		    <asp:placeholder id="javascriptUnknownPlaceholder" runat="server" enableviewstate="False">
			    <div class="floatrightonly">
				    <br />
                    <asp:hyperlink id="noJavaScriptHyperlink" runat="server"></asp:hyperlink>
                </div>
            </asp:placeholder>
        </noscript>
	</div>
	</asp:Panel>
	</div>
</div>
