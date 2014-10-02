<%@ Control Language="c#" AutoEventWireup="True" Codebehind="VehicleFeaturesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.VehicleFeaturesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Import namespace="TransportDirect.UserPortal.Web.Adapters" %>


<asp:repeater id="vehicleFeaturesRepeater" runat="server">
	<headertemplate>
        <div class="inlinemiddle floatleftonly featureImageDiv vehicleFeatureImageDiv">
    </headertemplate>
	<itemtemplate>
            <uc1:TDImage id="VehicleFeatureImage" runat="server" tooltip="<%# GetToolTip((VehicleFeatureIcon)Container.DataItem) %>" alternateText="<%# GetAltText((VehicleFeatureIcon)Container.DataItem) %>" imageurl="<%# GetImageURL((VehicleFeatureIcon)Container.DataItem) %>" />
	</itemtemplate>
	<footertemplate>
        </div>
	</footertemplate>
</asp:repeater>

