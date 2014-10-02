<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ServiceOperationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ServiceOperationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Register TagPrefix="uc1" TagName="OperatorLinkControl" Src="OperatorLinkControl.ascx" %>
<asp:Label id="headingLabel" CssClass="sdNoteHeader" runat="server" EnableViewState="False">headingLabel</asp:Label>
<br/>
<span class="sdNoteDetail">
	<asp:Label id="operatorText" runat="server" EnableViewState="False">operatorText</asp:Label>
	<uc1:OperatorLinkControl id="operatorLinkControl" runat="server" EnableViewState="False"></uc1:OperatorLinkControl>
</span>
<br/>
<asp:Label id="serviceValidityText" CssClass="sdNoteDetail" runat="server" EnableViewState="False">serviceValidityText</asp:Label>
<br/>
