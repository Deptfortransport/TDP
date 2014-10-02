<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WalkitLinkControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.WalkitLinkControl" %>
<div id="walkitLinkContainer" class="walkitLinkContainer" runat="server">
    <asp:hyperlink runat="server" id="walkitLink" Target="_blank"></asp:hyperlink>
    <asp:Label runat="server" ID="walkitLinkLabel" Visible ="false" EnableViewState="false" />
</div>