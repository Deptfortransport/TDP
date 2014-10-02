<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ErrorDisplayControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ErrorDisplayControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table id="Table1" runat="server" class="ErrorDisplayTable">
	<tr>
		<td><cc1:TDImage id="imageErrorType"  runat="server" imageurl="C:\Inetpub\wwwroot\Web\images\gifs\exclamation.gif"></cc1:TDImage>&nbsp;&nbsp;
			<h3><asp:label id="labelErrorDisplayType" runat="server" CssClass="errordisplaytype"></asp:label></h3>
		</td>
	</tr>
</table>
<table class="ErrorMessageBase">
	<tr>
		<td>
			<asp:repeater id="errorsList" runat="server">
				<itemtemplate>
					<div class="txterror"><%# (string)Container.DataItem %></div>
				</itemtemplate>
			</asp:repeater>
			<p><asp:label id="labelJourneyRefNumber" runat="server" cssclass="txterror"></asp:label></p>
		</td>
	</tr>
</table>
<div></div>
