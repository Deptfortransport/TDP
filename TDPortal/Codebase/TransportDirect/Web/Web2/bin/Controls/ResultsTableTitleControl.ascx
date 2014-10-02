<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ResultsTableTitleControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="../Controls/HyperlinkPostbackControl.ascx" %>
<div>
    <asp:label id="mainLabel" runat="server" CssClass="txteightb"></asp:label>
    <asp:label id="dateLabel" runat="server" CssClass="txtseven"></asp:label>&nbsp;
    <br />
    <div id="divBankHoliday" runat="server" class="txteightb" visible="false">
        <asp:label id="bankHolidayLabel" runat="server" Visible="false"></asp:label>
        <uc1:hyperlinkpostbackcontrol runat="server" id="bankHolidayLinkControl" Visible="false"></uc1:hyperlinkpostbackcontrol>
    </div>
    <div id="divSpecialEvent" runat="server" class="txteightb" visible="false">
        <asp:label id="specialEventLabel" runat="server" Visible="false"></asp:label>
    </div>
</div> 
