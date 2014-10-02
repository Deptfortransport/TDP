<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapRegionSelectControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapRegionSelectControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div id="boxtypetwoalt" style="WIDTH: 216px; PADDING-TOP: 0px;">
	<table cellspacing="0" cellpadding="1px" border="0" width="100%">
		<tr>
			<td colspan="2">
				<asp:label id="headingRegion" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr>
			<td colspan="2" runat="server" id="regionsCell" valign="middle">
				<asp:dropdownlist id="regionsList" runat="server" style="WIDTH: 160px;" enableviewstate="True"></asp:dropdownlist>
				<cc1:tdbutton id="regionsListOK" runat="server" enableviewstate="False"></cc1:tdbutton>
			</td>
		</tr>
		<tr>
			<td align="left"><asp:label id="headingMap" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label></td>
			<td align="right"><asp:label id="headingClickToSelect" cssclass="txtseven" runat="server" enableviewstate="False"></asp:label></td>
		</tr>
		<tr>
			<td colspan="2" bgcolor="white" align="center">
				<cc1:imagemapcontrol id="imageMap1" runat="server"></cc1:imagemapcontrol>
			</td>
		</tr>
		<%--<tr>
			<td colspan="2" align="left">
			    <cc1:tdbutton id="backButton" runat="server"></cc1:tdbutton>
			</td>
		</tr>--%>
	</table>
	 
</div>
