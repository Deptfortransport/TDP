<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PrintableSimpleMapControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.PrintableSimpleMapControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="SummaryResultTableControl" Src="../Controls/SummaryResultTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapLocationIconsDisplayControl" Src="../Controls/MapLocationIconsDisplayControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping" Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="uc1" TagName="MapKeyControl" Src="../Controls/MapKeyControl.ascx" %>
<table summary="Printable SimpleMap Header">
	<tr>
		<td>
			<asp:panel id="panelLocation" runat="server">
				<b>
					<asp:label id="labelJourneys" runat="server"></asp:label></b>
			</asp:panel>
		</td>
	</tr>
	<tr>
		<td><p class="txteightb"><asp:label id="labelJourneysFor" runat="server"></asp:label></p>
		</td>
	</tr>
</table>
<table summary="Printable SimpleMap Body">
	<tr>
		<td>
			<asp:panel id="panelDateTime" runat="server">
				<p class="txteightb">
					<asp:label id="labelDateTime" runat="server"></asp:label>&nbsp;
					<asp:label id="labelDateTimeFor" runat="server"></asp:label></p>
			</asp:panel>
			<div id="boxtypeonem"><center><asp:image id="imageMap" CssClass="printableImage" runat="server"></asp:image></center>
			</div>
			<div align="right"><span class="txtseven"><asp:label id="labelMapScaleTitle" runat="server"></asp:label><asp:label id="labelMapScale" runat="server"></asp:label></span></div>
		</td>
	
		<td valign="top">
			<div id="mapbox">
				    <div id="Div1"><asp:label id="labelKey" runat="server"></asp:label></div>
				    <uc1:mapkeycontrol id="MapKeyControl1" runat="server"></uc1:mapkeycontrol>
				    <div id="mhd"><asp:label id="labelOverview" runat="server"></asp:label></div>
				    <div id="mlm"><asp:image id="imageOverview" runat="server"></asp:image></div>
				
				   
				
			</div>
		</td>
	</tr>
</table>
