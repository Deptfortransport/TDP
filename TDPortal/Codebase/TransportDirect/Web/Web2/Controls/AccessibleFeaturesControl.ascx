<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccessibleFeaturesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AccessibleFeaturesControl" %>

<asp:Repeater ID="rptrAccessibleFeatures" runat="server" EnableViewState="false" OnItemDataBound="rptrAccessibleFeatures_ItemDataBound">
    <HeaderTemplate>
        <div class="inlinemiddle floatleftonly featureImageDiv accessibleFeatureImageDiv">
    </HeaderTemplate>
    <ItemTemplate>
            <asp:Image ID="imgAccessibleFeature" runat="server" EnableViewState = "false" />
    </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>
