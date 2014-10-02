<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="D2DPageOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.D2DPageOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div class="boxNavigationButtons">
	<div id="divBack" runat="server" >
	    <cc1:tdbutton id="commandBack" runat="server"></cc1:tdbutton>&nbsp;
	</div>
	<div id="divSave" runat="server" >
	    <cc1:tdbutton id="commandSave" runat="server"></cc1:tdbutton>&nbsp;
	</div>
	<div id="divClear" runat="server" >
	    &nbsp;<cc1:tdbutton id="commandClear" runat="server"></cc1:tdbutton>
	</div>
	<div id="divNext" runat="server" >
	    &nbsp;<cc1:tdbutton id="commandNext" runat="server"></cc1:tdbutton>
	</div>
	&nbsp;
</div>
