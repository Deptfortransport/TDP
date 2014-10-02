<%@ Register TagPrefix="uc1" TagName="RegisterControl" Src="RegisterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LogoutControl" Src="LogoutControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LoginRegisterLogoutControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.LoginRegisterLogoutControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="LoginControl" Src="LoginControl.ascx" %>
<uc1:logincontrol id="loginControl" runat="server"></uc1:logincontrol>
<uc1:registercontrol id="registerControl" runat="server"></uc1:registercontrol>
<uc1:logoutcontrol id="logoutControl" runat="server"></uc1:logoutcontrol>
