<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ClientLinkControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ClientLinkControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div id="linkContainer" runat="server" style="DISPLAY: none">
    <div id="bookmarkStar">
	    <cc1:tdimage id="linkStar" runat="server" align="middle" class="ClientLinkImageStyle"></cc1:tdimage>
    </div>
    <div id="bookmarkText">
	    <cc1:scriptablehyperlink id="clientLink" runat="server" scriptname="ClientLinks" enableviewstate="false"></cc1:scriptablehyperlink>
    </div>
</div>
