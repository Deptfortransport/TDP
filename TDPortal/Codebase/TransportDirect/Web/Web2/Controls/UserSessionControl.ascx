<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="UserSessionControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.UserSessionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:panel id="LoggedOffPanel" runat="server">
<uc1:hyperlinkpostbackcontrol id="loginLinkButton" runat="server"></uc1:hyperlinkpostbackcontrol>&nbsp;| 
<uc1:hyperlinkpostbackcontrol id="registerLinkButton" runat="server"></uc1:hyperlinkpostbackcontrol>
<asp:literal id="registerOptionalLiteral" runat="server"></asp:literal>
</asp:panel>
<asp:panel id="LoggedOnPanel" runat="server">
<uc1:hyperlinkpostbackcontrol id="logoutLinkButton" runat="server"></uc1:hyperlinkpostbackcontrol>
</asp:panel>
