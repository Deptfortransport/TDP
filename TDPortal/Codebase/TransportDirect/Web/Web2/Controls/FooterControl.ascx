<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="TrackingControl" Src="TrackingControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="true" CodeBehind="FooterControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.FooterControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"
    EnableViewState="False" %>
<div id="footer">
    <table id="nsiefixFooter" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td class="footerVersion">
                <div id="verSpace">
                </div>
                <div id="ver">
                    <asp:Label ID="labelVersion" runat="server" EnableViewState="false"></asp:Label>
                </div>
            </td>
            <td class="footerNavigation">
                <div id="fnav">
                    <asp:PlaceHolder ID="helpLink" runat="server">
                        <a class="BlueLink" id="HelpLinkButton" runat="server">FAQ</a> |
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="aboutLink" runat="server">
                        <a class="BlueLink" id="AboutLinkButton" runat="server">About Transport Direct</a> | 
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="contactUsLink" runat="server">
                        <a class="BlueLink" id="ContactUsLinkButton" runat="server">Contact us</a> |
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="siteMapLink" runat="server">
                        <a class="BlueLink" id="SiteMapLinkButton" runat="server">Sitemap</a> |
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="languageSwitchLink" runat="server">
                        <cc1:TDButton ID="lnkLanguageSwitch" runat="server" OnClick="lnkLanguageSwitch_Click"
                            Text="[Language]" /> |
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="relatedSitesLink" runat="server">
                        <a class="BlueLink" id="RelatedSitesLinkButton" runat="server">Related sites</a> |
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="mobileSiteLink" runat="server">
                        <a class="BlueLink" id="MobileSite" runat="server">Related sites</a> |
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="termsConditionsLink" runat="server">
                        <a class="BlueLink" id="TermsConditions" runat="server">Terms and Conditions</a> | 
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="privacyPolicyLink" runat="server">
                        <a class="BlueLink" id="PrivacyPolicy" runat="server">Privacy Policy</a> | 
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="dataProvidersLink" runat="server">
                        <a class="BlueLink" id="DataProviders" runat="server">Data Providers</a> | 
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="accessibilityLink" runat="server">
                        <a class="BlueLink" id="Accessibility" runat="server">Accessibility</a>
                    </asp:PlaceHolder>
                </div>
            </td>
        </tr>
    </table>
    <asp:Table ID="CjpLoggingTable" runat="server" Width="100%">
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" HorizontalAlign="center">
                <br />
                <cc1:TDButton ID="buttonLogViewer" runat="server"></cc1:TDButton>
                &nbsp;
                <cc1:TDButton ID="buttonVersionViewer" runat="server"></cc1:TDButton>
                &nbsp;
                <cc1:TDButton ID="buttonFeedbackViewer" runat="server"></cc1:TDButton>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:HiddenField ID="hdnUserLevel" runat="server" EnableViewState="false" />
    <uc1:TrackingControl ID="TrackingControl" runat="server"></uc1:TrackingControl>
</div>
