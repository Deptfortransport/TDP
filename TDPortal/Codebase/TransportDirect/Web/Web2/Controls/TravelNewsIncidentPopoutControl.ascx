<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TravelNewsIncidentPopoutControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TravelNewsIncidentPopoutControl" %>
<asp:Repeater ID="RoadTravelNews" runat="server">
    <ItemTemplate>
        <asp:Image CssClass="roadNewsItem" ID="roadNewsItem" runat="server" />
    </ItemTemplate>
</asp:Repeater>