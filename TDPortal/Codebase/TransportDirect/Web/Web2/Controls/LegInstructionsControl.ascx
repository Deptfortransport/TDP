<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LegInstructionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.LegInstructionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="OperatorLinkControl" Src="OperatorLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BiStateLocationControl" Src="BiStateLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ModalPopupMessage" Src="ModalPopupMessage.ascx"  %>
<%@ Register TagPrefix="uc1" TagName="CJPUserInfoControl" Src="CJPUserInfoControl.ascx" %>
<asp:Repeater id="instructionRepeater" runat="server">
	<ItemTemplate>
		<asp:Label id="takeLabel" runat="server"></asp:Label>
		<asp:HyperLink ID="planJourneyLink" runat="server" Visible = "false"></asp:HyperLink>
		<uc1:hyperlinkpostbackcontrol id="planJourneyLinkControl" runat="server" Visible="false"></uc1:hyperlinkpostbackcontrol>
		<uc1:ModalPopupMessage id="modalPopup" runat="server" visible="false" />
		<uc1:OperatorLinkControl id="operatorLinkControl" runat="server"></uc1:OperatorLinkControl>
		<asp:Label id="itemInstructionLabel" runat="server"></asp:Label>
		<asp:Repeater ID="AccessibilityLinkRepeater" runat="server">
		    <HeaderTemplate>
		        <br />
		    </HeaderTemplate>
            <ItemTemplate>
                <br />
                <asp:HyperLink ID="accessibilityLink" runat="server" Visible="false"></asp:HyperLink>
                <asp:Label id="accessibilityLabel" runat="server" Visible="false"></asp:Label>
            </ItemTemplate>
        </asp:Repeater>
        <uc1:CJPUserInfoControl id="cjpUserInfoService" runat="server" InfoType="ServiceDetail" newLineBefore="true" />        
        <br />
	</ItemTemplate>
</asp:Repeater>
<asp:Label id="noServicesInstructionLabel" runat="server"></asp:Label><br />
<asp:label id="noServiceOpenTimeLabel" runat="server" enableviewstate="false"></asp:label>
<% // <!-- Control used to populate a location object before transfering to door to door input --> %>
<uc1:BiStateLocationControl id="locationSelect" runat="server" Visible="False"></uc1:BiStateLocationControl>

