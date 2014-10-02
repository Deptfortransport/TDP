<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ServiceNotesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ServiceNotesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Register TagPrefix="uc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Import namespace="TransportDirect.UserPortal.Web.Adapters" %>
<asp:Label id="headingLabel" CssClass="sdNoteHeader" runat="server" EnableViewState="False">headingLabel</asp:Label>
<table>
	<asp:repeater id="vehicleFeaturesRepeater" runat="server">
		<headertemplate>
		</headertemplate>
		<itemtemplate>
			<tr>
				<td>
					<asp:image id="VehicleFeatureImage" CssClass="sdNoteDetail" runat="server" tooltip="<%# GetToolTip((VehicleFeatureIcon)Container.DataItem) %>" alternateText=" " imageurl="<%# GetImageURL((VehicleFeatureIcon)Container.DataItem) %>">
					</asp:image>
				</td>
				<td>
					<asp:label id="VehicleFeatureText" CssClass="sdNoteDetail" runat="server" tooltip="<%# GetToolTip((VehicleFeatureIcon)Container.DataItem) %>" Text="<%# GetAltText((VehicleFeatureIcon)Container.DataItem) %>" >
					</asp:label>
				</td>
			</tr>
		</itemtemplate>
		<footertemplate>
		</footertemplate>
	</asp:repeater>
</table>
<asp:Label id="notesLabel" CssClass="sdNoteDetail" runat="server" EnableViewState="False" Visible="false" ></asp:Label>
