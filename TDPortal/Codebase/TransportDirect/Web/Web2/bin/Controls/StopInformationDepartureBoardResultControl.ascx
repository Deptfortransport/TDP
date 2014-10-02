<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StopInformationDepartureBoardResultControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.StopInformationDepartureBoardResultControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>

<asp:GridView ID="DepBoardServiceGrid" runat="server" AutoGenerateColumns="false"
    OnRowDataBound="DepBoardServiceGrid_RowDataBound" CssClass="DepBoardServiceGrid" >
    <Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label ID="labelServiceNumberHeading" CssClass="txtsevenb" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="labelServiceNumber" CssClass="txtseven" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                 <asp:Label ID="labelServiceDetailHeading" CssClass="txtsevenb" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="labelServiceDetail" CssClass="txtseven" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                 <asp:Label ID="ServiceDetailLinkHeading" CssClass="txtsevenb" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
				<asp:HyperLink class="txtseven" id="ServiceDetailLink" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                 <asp:Label ID="labelRealTimeInfoHeading" CssClass="txtsevenb" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="labelRealTimeInfo" CssClass="txtseven" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>

</asp:GridView>
    