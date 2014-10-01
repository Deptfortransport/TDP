<%@ Page Title="" Language="C#" MasterPageFile="~/TDPMobile.Master" AutoEventWireup="true" CodeBehind="StopInformation.aspx.cs" Inherits="TDP.UserPortal.TDPMobile.StopInformation" %>
<%@ Register src="~/Controls/StationBoardControl.ascx" tagname="StationBoardControl" tagprefix="uc1" %>
<%@ Register src="~/Controls/WaitControl.ascx" tagname="WaitControl" tagprefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" runat="server">
    <asp:Panel ID="stopInfoContainer" runat="server" CssClass="stopInfo">
        
        <asp:UpdatePanel ID="stopInfoUpdate" runat="server">
            <ContentTemplate>
        
            <div id="locationTitleDiv" runat="server" enableviewstate="false" class="locationTitleDiv">
                <asp:Label ID="locationTitle" runat="server" EnableViewState="false"></asp:Label>
            </div>

            <uc1:StationBoardControl ID="stationBoardControl" runat="server"></uc1:StationBoardControl>

            </ContentTemplate>
        </asp:UpdatePanel>
        
    </asp:Panel>
</asp:Content>

