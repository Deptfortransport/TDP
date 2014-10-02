<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PrintableHeaderControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.PrintableHeaderControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
    
<div class="TopWrapper">
	<div class="PrintDirlogo">
		<cc1:TDImage id="transportDirectLogoImg" runat="server"/>
	</div>
	<div class="PrintStrap">
		<cc1:TDImage id="connectingPeopleImg" runat="server"/>
	</div>
</div>